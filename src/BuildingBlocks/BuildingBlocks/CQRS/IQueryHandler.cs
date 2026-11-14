using BuildingBlocks.CQRS;
using MediatR;
namespace BuildingBlocks.CQRS;

public interface IQueryHandler<in TQuery, TRespone>
    : IRequestHandler<TQuery, TRespone>
    where TQuery : IQuery<TRespone>
    where TRespone : notnull
{
    
}