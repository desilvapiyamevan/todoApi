using TodoApi.MemoryStore;
using TodoApi.Models;
namespace TodoApi.Services;

public class TodoService(MemoryTodoStore toDoStoreItems) : ITodoService
{
    private readonly MemoryTodoStore _todoItems = toDoStoreItems;
    private int _nextToDoItemId = 0;
    public IReadOnlyList<TodoItem> GetAll() =>
      _todoItems.GetAll()
          .ToList();

    public TodoItem? GetById(int id) =>
        _todoItems.GetById(id);

    public TodoItem Add(ToDoCreateRequest todo)
    {
        var id = Interlocked.Increment(ref _nextToDoItemId);
        var toDoItem = new TodoItem()
        {
            ToDoItemId = id,
            Title = todo.Title,
            Description = todo.Description,
        };

        _todoItems.Add(toDoItem);
        return toDoItem;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo != null)
            return _todoItems.Delete(id);
        return false;
    }
}