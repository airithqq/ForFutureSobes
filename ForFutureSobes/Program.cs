using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using ForFutureSobes.API.Filters;
using ForFutureSobes.Infrastructure.Interfaces;
using ForFutureSobes.Infrastructure.Repository;
using ForFutureSobes.Infrastructure.Data;
using ForFutureSobes.Application.Mapping;
using ForFutureSobes.Application.Interfaces;
using ForFutureSobes.Application.Configs;
using ForFutureSobes.Application.Services;
using ForFutureSobes.Application.Validator;
using ForFutureSobes.Model.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddSwaggerGen(p =>
{
    p.ParameterFilter<PriorityParameterFilter>();
});

builder.Services.AddSwaggerGen(v =>
{
    v.ParameterFilter<VariantsOfResponseFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("ForFutureSobesConnectionString");
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, ManageTaskService> ();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<IGeminiConfig, GeminiConfig>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<IGeminiRepository, GeminiRepository>();

builder.Services.AddHttpClient<IGeminiService, GeminiService>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateThemeDtoValidator>();

builder.Services.AddSingleton(sp =>
    builder.Configuration.GetSection("Gemini").Get<GeminiSettings>());

builder.Services.AddDbContext<ForFutureSobesDbContext>(options =>
options.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)

));

builder.Services.AddAutoMapper(typeof(TaskSummaryMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(TaskMappingProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
