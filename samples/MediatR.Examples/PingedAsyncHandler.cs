﻿using System.Threading;

namespace MediatR.Examples
{
    using System.IO;
    using System.Threading.Tasks;

    public class PingedAsyncHandler : IAsyncNotificationHandler<PingedAsync>
    {
        private readonly TextWriter _writer;

        public PingedAsyncHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public Task HandleAsync(PingedAsync notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("Got pinged async.");
        }
    }

    public class PingedAlsoAsyncHandler : IAsyncNotificationHandler<PingedAsync>
    {
        private readonly TextWriter _writer;

        public PingedAlsoAsyncHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public Task HandleAsync(PingedAsync notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("Got pinged also async.");
        }
    }
}