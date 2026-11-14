using System.Windows.Input;
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValiddationBehavior<TRequest, TRespone>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TRespone>
    where TRequest : ICommand<TRespone>
{
    public async Task<TRespone> Handle(TRequest request, RequestHandlerDelegate<TRespone> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults = 
            await Task.WhenAll(validators.Select(v=>v.ValidateAsync(context,cancellationToken)));
        var failures = validationResults
            .Where(r=>r.Errors.Count>0)
            .SelectMany(r=>r.Errors)
            .ToList();
        if(failures.Count>0)
            throw new ValidationException(failures);
        return await next();
    }
}
