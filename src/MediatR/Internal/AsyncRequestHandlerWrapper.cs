using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Internal
{
    internal abstract class AsyncRequestHandlerWrapper<TResponse>
    {
        public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, CancellationToken cancellationToken);
    }

    internal class AsyncRequestHandlerWrapper<TRequest, TResponse> : AsyncRequestHandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;

        public AsyncRequestHandlerWrapper(IAsyncRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public override Task<TResponse> HandleAsync(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            return _inner.HandleAsync((TRequest)request, cancellationToken);
        }
    }
}