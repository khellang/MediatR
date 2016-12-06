﻿namespace MediatR.Tests
{
    using System;
    using Shouldly;
    using StructureMap;
    using Xunit;

    public class ExceptionTests
    {
        private readonly IMediator _mediator;

        public class Ping : IRequest<Pong> { }
        public class Pong {}
        public class VoidPing : IRequest { }
        public class Pinged : INotification { }

        public ExceptionTests()
        {
            var container = new Container(cfg =>
            {
                cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
                cfg.For<IMediator>().Use<Mediator>();
            });
            _mediator = container.GetInstance<IMediator>();
        }

        [Fact]
        public void Should_throw_for_send()
        {
            Should.Throw<InvalidOperationException>(() => _mediator.Send(new Ping()));
        }

        [Fact]
        public void Should_throw_for_void_send()
        {
            Should.Throw<InvalidOperationException>(() => _mediator.Send(new VoidPing()));
        }

        [Fact]
        public void Should_not_throw_for_publish()
        {
            Should.NotThrow(() => _mediator.Publish(new Pinged()));
        }

        [Fact]
        public void Should_throw_for_async_send()
        {
            Should.Throw<InvalidOperationException>(() => _mediator.SendAsync(new Ping()));
        }

        [Fact]
        public void Should_throw_for_async_void_send()
        {
            Should.Throw<InvalidOperationException>(() => _mediator.SendAsync(new VoidPing()));
        }

        [Fact]
        public void Should_not_throw_for_async_publish()
        {
            Should.NotThrow(() => _mediator.PublishAsync(new Pinged()));
        }
    }
}