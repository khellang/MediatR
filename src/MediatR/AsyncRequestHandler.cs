using System.Threading;
using System.Threading.Tasks;

namespace MediatR
{
    /// <summary>
    /// Helper class for asynchronous requests that return a void response
    /// </summary>
    /// <typeparam name="TRequest">The type of void request being handled</typeparam>
    public abstract class AsyncRequestHandler<TRequest> : IAsyncRequestHandler<TRequest, Unit>
        where TRequest : IRequest
    {
        async Task<Unit> IAsyncRequestHandler<TRequest, Unit>.HandleAsync(TRequest request, CancellationToken cancellationToken)
        {
            await HandleAsync(request, cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        /// <summary>
        /// Handles a void request
        /// </summary>
        /// <param name="request">The request message</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task representing the void response from the request</returns>
        protected abstract Task HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
