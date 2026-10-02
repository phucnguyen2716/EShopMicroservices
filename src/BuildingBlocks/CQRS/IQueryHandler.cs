using BuildingBlocks.CQRS;
using MediatR;
namespace BuildingBlock.CQRS;

public interface IQueryHandler<in TQuery, TRespone>
    : IRequestHandler<TQuery, TRespone>
    where TQuery : IQuery<TRespone>
    where TRespone : notnull
{
    
}