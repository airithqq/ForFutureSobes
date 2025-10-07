using ForFutureSobes.Data;
using ForFutureSobes.Services;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Interfaces;
using ForFutureSobes.Mapping;
using ForFutureSobes.DTOs;
using AutoMapper;
using ForFutureSobes.Domain;
using ForFutureSobes.Repository;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FluentValidation.AspNetCore;
using ForFutureSobes.Validator;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});



var connectionString = builder.Configuration.GetConnectionString("ForFutureSobesConnectionString");
builder.Services.AddScoped< ITaskRepository, TaskRepository>();
builder.Services.AddScoped< ITaskService, ManageTaskService> ();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddHttpClient<IGeminiService, GeminiService>();
builder.Services.AddScoped<IGeminiConfig, GeminiConfig>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateThemeDtoValidator>();


builder.Services.AddSingleton(sp =>
    builder.Configuration.GetSection("Gemini").Get<GeminiSettings>());

builder.Services.AddDbContext<ForFutureSobesDbContext>(options =>
options.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)

));


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
