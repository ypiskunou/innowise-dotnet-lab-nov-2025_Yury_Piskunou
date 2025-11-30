using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application;
using ProductService.Application.Features.Categories.CreateCategory;
using ProductService.Application.Features.Categories.GetAllCategories;
using ProductService.Application.Features.Categories.GetCategoryById;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Presentation.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController: ControllerBase
{
    private readonly ISender _sender;
    public CategoriesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _sender.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }

    [HttpGet("{id:guid}", Name = "GetCategoryById")]
    public async Task<IActionResult?> GetCategoryById(Guid id)
    {
        var category = await _sender.Send(new GetCategoryByIdQuery(id));
        return Ok(category);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto categoryDto)
    {
        var command = new CreateCategoryCommand(categoryDto);
        var createdCategory = await _sender.Send(command);
        
        return CreatedAtRoute("GetCategoryById", new { id = createdCategory.Id }, createdCategory);
    }

    
}