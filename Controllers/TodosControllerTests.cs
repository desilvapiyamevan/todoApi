
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="TodosController"/>.
/// </summary>
public class TodosControllerTests
{
    private readonly Mock<ITodoService> _serviceMock;
    private readonly TodosController _controller;

    public TodosControllerTests()
    {
        _serviceMock = new Mock<ITodoService>();
        _controller = new TodosController(_serviceMock.Object);
    }


    [Fact]
    public void GetAll_ReturnsOkWithAllItems()
    {
        // Arrange
        IReadOnlyList<TodoItem> todos = new List<TodoItem>
        {
            new() { ToDoItemId = 1, Title = "My Birth Day", Description = "Party On Sunday" },
            new() { ToDoItemId = 2, Title = "School Production", Description = "Saturday at GWSC" }
        };
        _serviceMock.Setup(s => s.GetAll()).Returns(todos);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(todos, okResult.Value);
        _serviceMock.Verify(s => s.GetAll(), Times.Once);
    }

    [Fact]
    public void GetAll_WhenNoItems_ReturnsOkWithEmptyList()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetAll()).Returns(new List<TodoItem>());

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsAssignableFrom<IReadOnlyList<TodoItem>>(okResult.Value);
        Assert.Empty(value);
    }
      

    [Fact]
    public void GetById_WhenItemExists_ReturnsOkWithItem()
    {
        // Arrange
        var item = new TodoItem { ToDoItemId = 15, Title = "My Birth Day", Description = "Party On Sunday" };
        _serviceMock.Setup(s => s.GetById(15)).Returns(item);

        // Act
        var result = _controller.GetById(15);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(item, okResult.Value);
    }

    [Fact]
    public void GetById_WhenItemDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns((TodoItem?)null);

        // Act
        var result = _controller.GetById(100);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetById_PassesRequestedIdToService()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns((TodoItem?)null);

        // Act
        _controller.GetById(25);

        // Assert
        _serviceMock.Verify(s => s.GetById(25), Times.Once);
    }

    // ---------- Create ----------

    [Fact]
    public void Create_ReturnsOkWithCreatedItem()
    {
        // Arrange
        var request = new ToDoCreateRequest { Title = "Go To Gym", Description = "Go To Gym on Sunday" };
        var created = new TodoItem { ToDoItemId = 1, Title = request.Title, Description = request.Description };
        _serviceMock.Setup(s => s.Add(request)).Returns(created);

        // Act
        var result = _controller.Create(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(created, okResult.Value);
        _serviceMock.Verify(s => s.Add(request), Times.Once);
    }

    [Fact]
    public void Create_PassesRequestBodyToService()
    {
        // Arrange
        var request = new ToDoCreateRequest { Title = "Go To Gym", Description = "Go To Gym on Sunday" };
        _serviceMock.Setup(s => s.Add(It.IsAny<ToDoCreateRequest>()))
                    .Returns(new TodoItem { ToDoItemId = 1, Title = request.Title, Description = request.Description });

        // Act
        _controller.Create(request);

        // Assert
        _serviceMock.Verify(s => s.Add(It.Is<ToDoCreateRequest>(
            r => r.Title == "Go To Gym" && r.Description == "Go To Gym on Sunday")), Times.Once);
    }
}
