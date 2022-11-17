using System.Globalization;
using FarmPoint.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace FarmPoint.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
    }
}
