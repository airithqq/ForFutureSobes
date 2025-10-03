using ForFutureSobes.Data;
using ForFutureSobes.Services;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Interfaces;
using ForFutureSobes.Mapping;
using ForFutureSobes.Repository;
using System.Reflection;
using ForFutureSobes.Services.External;
using ForFutureSobes.Domain.Gemini;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration.GetConnectionString("ForFutureSobesConnectionString");

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, ManageTaskService> ();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<IThemeService, ThemeService>();


builder.Services.AddHttpClient<IGeminiService, GeminiService>(client =>
{
    client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
});

builder.Services.AddDbContext<ForFutureSobesDbContext>(options =>
options.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)

));


builder.Services.AddAutoMapper(typeof(TaskMappingProfile).Assembly);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
