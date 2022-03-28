using System.Collections.Concurrent;

namespace MicroService;

public class TodoStore {

    private ConcurrentDictionary<int, Todo> _todos = new();

    public IEnumerable<Todo> GetAll() => _todos.Select(t => t.Value);
    public Todo? Get(int id) {
        if (_todos.TryGetValue(id, out var todo)) {
            return todo;
        }

        return null;
    }

    public Todo Add(Todo todo) {
        lock (_todos) {
            if (_todos.IsEmpty) {
                todo.Id = 1;
            } else {
                todo.Id = _todos.Max(t => t.Key) + 1;
            }
            _todos.TryAdd(todo.Id, todo);
        }
        
        return todo;
    }

    public bool Update(int id, Todo todo) {
        if (_todos.TryGetValue(id, out var match)) {
            if (!string.IsNullOrEmpty(todo.Description)) {
                match.Description = todo.Description;
            }

            return true;
        }

        return false;
    }

    public bool Remove(int id) => _todos.Remove(id, out var _);
}