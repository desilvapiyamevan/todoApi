using System.Collections.Concurrent;
using TodoApi.Models;

namespace TodoApi.MemoryStore;

public class MemoryTodoStore 
{
    private readonly ConcurrentDictionary<int, TodoItem> _toDoItems = new();

    public IReadOnlyList<TodoItem> GetAll() =>
        _toDoItems.Values
            .OrderBy(item => item.CreatedDate)
            .ToList();

    public TodoItem? GetById(int id) =>
        _toDoItems.TryGetValue(id, out var item) ? item : null;

    public TodoItem Add(TodoItem item)
    {
        if (_toDoItems.TryAdd(item.ToDoItemId, item))       
            return item;
        else
            throw new InvalidOperationException($"Item title : '{item.Title}' is not added ");
               
    }   

    public bool Delete(int id) => _toDoItems.TryRemove(id, out _);
}
