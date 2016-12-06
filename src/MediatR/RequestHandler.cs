namespace MediatR
{
    /// <summary>
    /// Helper class for requests that return a void response
    /// </summary>
    /// <typeparam name="TRequest">The type of void request being handled</typeparam>
    public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest, Unit>
        where TRequest : IRequest
    {
        Unit IRequestHandler<TRequest, Unit>.Handle(TRequest request)
        {
            Handle(request);

            return Unit.Value;
        }

        /// <summary>
        /// Handles a void request
        /// </summary>
        /// <param name="request">The request message</param>
        protected abstract void Handle(TRequest request);
    }
}