using FarmPoint.Application.Common.Mappings;
using FarmPoint.Domain.Entities;

namespace FarmPoint.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
