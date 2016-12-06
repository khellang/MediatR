namespace MediatR
{
    /// <summary>
    /// Helper class for requests that return a void response
    /// </summary>
    /// <typeparam name="TMessage">The type of void request being handled</typeparam>
    public abstract class RequestHandler<TMessage> : IRequestHandler<TMessage, Unit>
        where TMessage : IRequest
    {
        Unit IRequestHandler<TMessage, Unit>.Handle(TMessage message)
        {
            Handle(message);

            return Unit.Value;
        }

        /// <summary>
        /// Handles a void request
        /// </summary>
        /// <param name="message">The request message</param>
        protected abstract void Handle(TMessage message);
    }
}