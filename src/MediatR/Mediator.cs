using System.Threading;
using MediatR.Internal;

namespace MediatR
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Default mediator implementation relying on single- and multi instance delegates for resolving handlers.
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly SingleInstanceFactory _singleInstanceFactory;
        private readonly MultiInstanceFactory _multiInstanceFactory;

        private static readonly ConcurrentDictionary<Type, Type> HandlerTypeCache;
        private static readonly ConcurrentDictionary<Type, Type> WrapperTypeCache;

        static Mediator()
        {
            HandlerTypeCache = new ConcurrentDictionary<Type, Type>();
            WrapperTypeCache = new ConcurrentDictionary<Type, Type>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="singleInstanceFactory">The single instance factory.</param>
        /// <param name="multiInstanceFactory">The multi instance factory.</param>
        public Mediator(SingleInstanceFactory singleInstanceFactory, MultiInstanceFactory multiInstanceFactory)
        {
            _singleInstanceFactory = singleInstanceFactory;
            _multiInstanceFactory = multiInstanceFactory;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var defaultHandler = GetHandler(request);

            var result = defaultHandler.Handle(request);

            return result;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var defaultHandler = GetAsyncHandler(request);

            var result = defaultHandler.HandleAsync(request, cancellationToken);

            return result;
        }

        public void Publish<TNotification>(TNotification notification)
            where TNotification : INotification
        {
            var handlerType = typeof(INotificationHandler<TNotification>);

            var handlers = GetNotificationHandlers(notification, handlerType)
                .Cast<INotificationHandler<TNotification>>()
                .ToArray();

            foreach (var handler in handlers)
            {
                handler.Handle(notification);
            }
        }

        public Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken))
            where TNotification : INotification
        {
            var handlerType = typeof(IAsyncNotificationHandler<TNotification>);

            var tasks = GetNotificationHandlers(notification, handlerType)
                .Cast<IAsyncNotificationHandler<TNotification>>()
                .Select(handler => handler.HandleAsync(notification, cancellationToken))
                .ToArray();

            return Task.WhenAll(tasks);
        }

        private RequestHandlerWrapper<TResponse> GetHandler<TResponse>(IRequest<TResponse> request)
        {
            return GetHandler<RequestHandlerWrapper<TResponse>, TResponse>(request,
                typeof(IRequestHandler<,>),
                typeof(RequestHandlerWrapper<,>));
        }

        private AsyncRequestHandlerWrapper<TResponse> GetAsyncHandler<TResponse>(IRequest<TResponse> request)
        {
            return GetHandler<AsyncRequestHandlerWrapper<TResponse>, TResponse>(request,
                typeof(IAsyncRequestHandler<,>),
                typeof(AsyncRequestHandlerWrapper<,>));
        }

        private TWrapper GetHandler<TWrapper, TResponse>(object request, Type handlerType, Type wrapperType)
        {
            var requestType = request.GetType();

            var genericHandlerType = HandlerTypeCache.GetOrAdd(requestType, handlerType, (type, root) => root.MakeGenericType(type, typeof(TResponse)));
            var genericWrapperType = WrapperTypeCache.GetOrAdd(requestType, wrapperType, (type, root) => root.MakeGenericType(type, typeof(TResponse)));

            var handler = GetHandler(request, genericHandlerType);

            return (TWrapper) Activator.CreateInstance(genericWrapperType, handler);
        }

        private object GetHandler(object request, Type handlerType)
        {
            try
            {
                return _singleInstanceFactory(handlerType);
            }
            catch (Exception e)
            {
                throw BuildException(request, e);
            }
        }

        private IEnumerable<object> GetNotificationHandlers(object notification, Type handlerType)
        {
            try
            {
                return _multiInstanceFactory(handlerType);
            }
            catch (Exception e)
            {
                throw BuildException(notification, e);
            }
        }

        private static InvalidOperationException BuildException(object message, Exception inner)
        {
            return new InvalidOperationException("Handler was not found for request of type " + message.GetType() + ".\r\nContainer or service locator not configured properly or handlers not registered with your container.", inner);
        }
    }
}
