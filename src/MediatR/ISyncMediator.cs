namespace MediatR
{
    /// <summary>
    /// Defines a synchronous mediator to encapsulate request/response and publishing interaction patterns
    /// </summary>
    public interface ISyncMediator
    {
        /// <summary>
        /// Send a request to a single handler
        /// </summary>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <returns>Response</returns>
        TResponse Send<TResponse>(IRequest<TResponse> request);

        /// <summary>
        /// Send a notification to multiple handlers
        /// </summary>
        /// <param name="notification">Notification object</param>
        void Publish<TNotification>(TNotification notification)
            where TNotification : INotification;
    }
}