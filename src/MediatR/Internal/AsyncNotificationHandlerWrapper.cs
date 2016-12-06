using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Internal
{
    internal abstract class AsyncNotificationHandlerWrapper
    {
        public abstract Task Handle(INotification message, CancellationToken cancellationToken);
    }

    internal class AsyncNotificationHandlerWrapper<TNotification> : AsyncNotificationHandlerWrapper
        where TNotification : INotification
    {
        private readonly IAsyncNotificationHandler<TNotification> _inner;

        public AsyncNotificationHandlerWrapper(IAsyncNotificationHandler<TNotification> inner)
        {
            _inner = inner;
        }

        public override Task Handle(INotification message, CancellationToken cancellationToken)
        {
            return _inner.Handle((TNotification)message, cancellationToken);
        }
    }
}