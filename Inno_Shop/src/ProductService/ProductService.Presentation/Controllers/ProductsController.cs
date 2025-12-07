using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Features.Products.CreateProduct;
using ProductService.Application.Features.Products.DeleteProduct;
using ProductService.Application.Features.Products.GetAllProducts;
using ProductService.Application.Features.Products.GetMyProducts;
using ProductService.Application.Features.Products.GetProductById;
using ProductService.Application.Features.Products.GetProductPreviews;
using ProductService.Application.Features.Products.HideProductsByUserId;
using ProductService.Application.Features.Products.RestoreProductsByUserId;
using ProductService.Application.Features.Products.UpdateProduct;
using ProductService.Shared.DataTransferObjects;
using ProductService.Shared.RequestFeatures;

namespace ProductService.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController: ControllerBase
{
    private readonly ISender _sender;
    public ProductsController(ISender sender) => _sender = sender;
    
    [HttpGet]
    [HttpHead]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters productParameters)
    {
        var pagedResult = await _sender.Send(new GetAllProductsQuery(productParameters));
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.products);
    }
    
    [HttpGet("my")]
    [Authorize] 
    public async Task<IActionResult> GetMyProducts()
    {
        var products = await _sender.Send(new GetMyProductsQuery());
        return Ok(products);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _sender.Send(new GetProductByIdQuery(id));
        return Ok(product);
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
    
    [HttpPut("hide-by-user/{userId:guid}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> HideProductsByUser(Guid userId)
    {
        await _sender.Send(new HideProductsByUserIdCommand(userId));
        return NoContent();
    }
    
    [HttpPut("restore-by-user/{userId:guid}")]
    public async Task<IActionResult> RestoreProductsByUser(Guid userId)
    {
        await _sender.Send(new RestoreProductsByUserIdCommand(userId));
        return NoContent();
    }
    
    [HttpGet("previews")]
    public async Task<IActionResult> GetProductPreviews([FromQuery] string? searchTerm)
    {
        var previews = await _sender.Send(new GetProductPreviewsQuery(searchTerm));
        return Ok(previews);
    }
}