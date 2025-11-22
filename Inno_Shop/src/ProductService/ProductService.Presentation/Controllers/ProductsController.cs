using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application;
using ProductService.Application.Features.Products.CreateProduct;
using ProductService.Application.Features.Products.DeleteProduct;
using ProductService.Application.Features.Products.GetAllProducts;
using ProductService.Application.Features.Products.UpdateProduct;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController: ControllerBase
{
    private readonly ISender _sender;
    public ProductsController(ISender sender) => _sender = sender;
    
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _sender.Send(new GetAllProductsQuery());
        return Ok(products);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto productDto)
    {
        var command = new CreateProductCommand(productDto);
        
        var createdProduct = await _sender.Send(command);

        return CreatedAtAction(nameof(GetAllProducts), new { id = createdProduct.Id }, createdProduct);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductForUpdateDto productDto)
    {
        var command = new UpdateProductCommand(id, productDto);
        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProductCommand(id);
        await _sender.Send(command);
        return NoContent();
    }
}