using TodoApi.Models;
namespace TodoApi.Services;

/// <summary>
/// Business logic for managing to-do items
/// </summary>
public interface ITodoService
{
    IReadOnlyList<TodoItem> GetAll();
    
    TodoItem? GetById(int id);
   
    TodoItem Add(ToDoCreateRequest request);
   
    /// <summary>Returns true if the item existed and was deleted.</summary>
    bool Delete(int id);
}
