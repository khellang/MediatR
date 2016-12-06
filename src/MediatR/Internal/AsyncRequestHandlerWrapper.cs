using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Internal
{
    internal abstract class AsyncRequestHandlerWrapper<TResult>
    {
        public abstract Task<TResult> HandleAsync(IRequest<TResult> message, CancellationToken cancellationToken);
    }

    internal class AsyncRequestHandlerWrapper<TCommand, TResult> : AsyncRequestHandlerWrapper<TResult>
        where TCommand : IRequest<TResult>
    {
        private readonly IAsyncRequestHandler<TCommand, TResult> _inner;

        public AsyncRequestHandlerWrapper(IAsyncRequestHandler<TCommand, TResult> inner)
        {
            _inner = inner;
        }

        public override Task<TResult> HandleAsync(IRequest<TResult> message, CancellationToken cancellationToken)
        {
            return _inner.Handle((TCommand)message, cancellationToken);
        }
    }
}