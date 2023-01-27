using FluentValidation;
using Microsoft.Extensions.Options;
using WebUI.OptionsValidators;

namespace WebUI.Extensions;

public static class OptionsBuilderFluentValidationExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(
      this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
            provider => new FluentValidationOptions<TOptions>(
              optionsBuilder.Name, provider));
        return optionsBuilder;
    }

    public static OptionsBuilder<TOptions> AddWithFluentValidationOnStartup<TOptions, TValidator>(
        this IServiceCollection services,
        string configurationSection)
    where TOptions : class
    where TValidator : class, IValidator<TOptions>
    {
        // Add the validator
        services.AddScoped<IValidator<TOptions>, TValidator>();

        return services.AddOptions<TOptions>()
            .BindConfiguration(configurationSection)
            .ValidateFluentValidation()
            .ValidateOnStart();
    }
}
