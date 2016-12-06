namespace MediatR.Internal
{
    internal abstract class RequestHandlerWrapper<TResponse>
    {
        public abstract TResponse Handle(IRequest<TResponse> request);
    }

    internal class RequestHandlerWrapper<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public RequestHandlerWrapper(IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public override TResponse Handle(IRequest<TResponse> request)
        {
            return _inner.Handle((TRequest)request);
        }
    }
}