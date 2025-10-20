using FluentValidation.AspNetCore;
using FluentValidation;
using ForFutureSobes.Application.Configs;
using ForFutureSobes.Application.Interfaces;
using ForFutureSobes.Application.Mapping;
using ForFutureSobes.Application.Services;
using ForFutureSobes.Application.Validator;
using ForFutureSobes.Infrastructure.Interfaces;
using ForFutureSobes.Infrastructure.Repository;

namespace ForFutureSobes.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, ManageTaskService>();
            services.AddScoped<IThemeRepository, ThemeRepository>();
            services.AddScoped<IGeminiConfig, GeminiConfig>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<IGeminiRepository, GeminiRepository>();

            services.AddHttpClient<IGeminiService, GeminiService>();
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateThemeDtoValidator>();

            services.AddAutoMapper(typeof(TaskSummaryMappingProfile).Assembly);
            services.AddAutoMapper(typeof(TaskMappingProfile).Assembly);

            return services;
        }
    }

}
