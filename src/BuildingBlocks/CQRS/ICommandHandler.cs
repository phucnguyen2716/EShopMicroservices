using System.Windows.Input;
using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
    
}
public interface ICommandHandler<in TCommand, TRespone> 
    : IRequestHandler<TCommand, TRespone>
    where TCommand : ICommand<TRespone>
    where TRespone : notnull
{
    
}