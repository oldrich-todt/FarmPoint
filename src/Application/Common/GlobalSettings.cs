using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FarmPoint.Application.Common;

// this class uses configuration from Infrastructure and WebUI application - move it to appropriate place
public class GlobalSettings
{
    public static string ConfigurationKey => nameof(GlobalSettings);
    public static string LogLevelKey => $"{ConfigurationKey}:{nameof(LogLevel)}";
    public static string UseInMemoryDatabaseKey => $"{ConfigurationKey}:{nameof(UseInMemoryDatabase)}";
    public static string ConnectionStringKey => $"{ConfigurationKey}:{nameof(ConnectionString)}";

    public string LogLevel { get; set; } = "Information";
    public bool UseInMemoryDatabase { get; set; } = true;
    public string? ConnectionString { get; set; }
}

public class GlobalSettingsValidator: AbstractValidator<GlobalSettings>
{
    public GlobalSettingsValidator()
    {
        RuleFor(gs => gs.LogLevel).NotNull(); // use EventLogType enume when moved
        RuleFor(gs => gs.ConnectionString).NotEmpty().When(gs => !gs.UseInMemoryDatabase);
    }
}
