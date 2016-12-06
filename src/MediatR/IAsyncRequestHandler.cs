using System.Threading;
using System.Threading.Tasks;

namespace MediatR
{
    /// <summary>
    /// Defines an asynchronous handler for a request
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled</typeparam>
    /// <typeparam name="TResponse">The type of response from the handler</typeparam>
    public interface IAsyncRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles an asynchronous request
        /// </summary>
        /// <param name="message">The request message</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task representing the response from the request</returns>
        Task<TResponse> Handle(TRequest message, CancellationToken cancellationToken);
    }
}