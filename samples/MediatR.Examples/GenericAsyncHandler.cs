using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Examples
{
    public class GenericAsyncHandler : IAsyncNotificationHandler<IAsyncNotification>
    {
        private readonly TextWriter _writer;

        public GenericAsyncHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Handle(IAsyncNotification notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("Got notified also async.");
        }
    }
}
