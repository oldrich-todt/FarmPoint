using FarmPoint.Application.TodoLists.Queries.ExportTodos;

namespace FarmPoint.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
