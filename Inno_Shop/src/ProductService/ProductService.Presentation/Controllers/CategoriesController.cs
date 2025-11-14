using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application;

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

    // [HttpPost]
    // [ServiceFilter(typeof(ValidationFilterAttribute))]
    // public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto book)
    // {
    //     var createdBook = await _service.BookService.AddBookAsync(book);
    //     return CreatedAtRoute("GetBookById", new { id = createdBook.Id }, createdBook);
    // }
    //
    // [HttpPut("{id:guid}")]
    // [ServiceFilter(typeof(ValidationFilterAttribute))]
    // public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookForUpdateDto book)
    // {
    //     await _service.BookService.UpdateBookAsync(id, book, true);
    //     return NoContent();
    // }
    //
    // [HttpDelete("{id:guid}")]
    // public async Task<IActionResult> DeleteBook(Guid id)
    // {
    //     await _service.BookService.DeleteBookAsync(id, false);
    //     return NoContent();
    // }
}