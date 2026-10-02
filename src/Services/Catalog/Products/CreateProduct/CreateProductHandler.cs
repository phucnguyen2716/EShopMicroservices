using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name,List<string> Category,string Description,IFormFile ImageFile,decimal Price)
: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Create Product entity from command object

        var product=new Product
        {
            Name=command.Name,
            Category=command.Category,
            Description=command.Description,
            ImageFile=command.ImageFile,
            Price=command.Price
        };
        //TODO
        //save to database
        
        //return result
        return new CreateProductResult(Guid.NewGuid());
    }
}