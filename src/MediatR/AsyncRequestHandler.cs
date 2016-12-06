using System.Threading;
using System.Threading.Tasks;

namespace MediatR
{
    /// <summary>
    /// Helper class for asynchronous requests that return a void response
    /// </summary>
    /// <typeparam name="TMessage">The type of void request being handled</typeparam>
    public abstract class AsyncRequestHandler<TMessage> : IAsyncRequestHandler<TMessage, Unit>
        where TMessage : IRequest
    {
        async Task<Unit> IAsyncRequestHandler<TMessage, Unit>.HandleAsync(TMessage message, CancellationToken cancellationToken)
        {
            await HandleAsync(message, cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        /// <summary>
        /// Handles a void request
        /// </summary>
        /// <param name="message">The request message</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task representing the void response from the request</returns>
        protected abstract Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}
