using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application;

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
    
    
}