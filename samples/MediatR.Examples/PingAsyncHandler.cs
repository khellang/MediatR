using System.Threading;

namespace MediatR.Examples
{
    using System.Threading.Tasks;

    public class PingAsyncHandler : IAsyncRequestHandler<PingAsync, Pong>
    {
        public Task<Pong> HandleAsync(PingAsync message, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Pong { Message = message.Message + " Pong" });
        }
    }
}