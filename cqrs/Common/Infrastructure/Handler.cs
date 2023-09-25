using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cqrs.Common.Application;
using Cqrs.Common.Application.Command;
using Cqrs.Common.Application.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Common.Infrastructure;

public class Handler : IHandler
{
    private readonly IServiceProvider _serviceProvider;

    public IEnumerable<TEvent> Handle<TEvent>(ICommand<TEvent> command)
        where TEvent : IEvent
    {
        var handler = this._serviceProvider.GetService<ICommandHandler<TEvent>>();
        return handler.Handle(command);
    }

    public IAsyncEnumerable<TEvent> HandleAsync<TEvent>(ICommand<TEvent> command)
        where TEvent : IEvent
    {
        var handler = this._serviceProvider.GetService<ICommandHandler<TEvent>>();
        return handler.HandleAsync(command);
    }

    public TResult Handle<TResult>(IQuery<TResult> query)
        where TResult : IResult
    {
        var handler = this._serviceProvider.GetService<IQueryHandler<TResult>>();
        return handler.Handle(query);
    }

    public Task<TResult> HandleAsync<TResult>(IQuery<TResult> query)
        where TResult : IResult
    {
        var handler = this._serviceProvider.GetService<IQueryHandler<TResult>>();
        return handler.HandleAsync(query);
    }

    public Handler(IServiceProvider provider)
    {
        this._serviceProvider = provider;
    }
}