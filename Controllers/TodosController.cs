using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodoService _service;

    public TodosController(TodoService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerOperation(
    Summary = "Get all todos",
    Description = "Returns the complete list of todo items.")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
    Summary = "Returns a To Do item by id",
    Description = "Returns a To Do item by id.")]
    [ProducesResponseType<TodoItem>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TodoItem> GetById(int id)
    {
        var item = _service.GetById(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [SwaggerOperation(
    Summary = "Create a new todo item",
    Description = "Creates a new todo item and returns it.")]
    [ProducesResponseType<TodoItem>(StatusCodes.Status201Created)]
    public IActionResult Create(ToDoCreateRequest todo)
    {
        return Ok(_service.Add(todo));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
    Summary = "Deletes a To Do item by id",
    Description = "Deletes a To Do item by id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        return _service.Delete(id)? NoContent(): NotFound();
    }
}