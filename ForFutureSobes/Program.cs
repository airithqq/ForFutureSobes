using ForFutureSobes.Data;
using ForFutureSobes.Services;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ForFutureSobesConnectionString");

builder.Services.AddScoped< ITaskService, ManageTaskService> ();
builder.Services.AddDbContext<ForFutureSobesDbContext>(options =>
options.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)

));



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
