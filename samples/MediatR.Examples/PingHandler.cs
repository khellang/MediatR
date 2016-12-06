namespace MediatR.Examples
{
    public class PingHandler : IRequestHandler<Ping, Pong>
    {
        public Pong Handle(Ping request)
        {
            return new Pong {Message = request.Message + " Pong"};
        }
    }
}