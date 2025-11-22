using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application;
using ProductService.Application.Features.Products.CreateProduct;
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
}