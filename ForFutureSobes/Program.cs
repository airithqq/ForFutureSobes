using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ForFutureSobes.API.Filters;
using ForFutureSobes.Infrastructure.Data;
using ForFutureSobes.Model.Domain;
using ForFutureSobes.API;


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
builder.Services.AddDbContext<ForFutureSobesDbContext>(options =>
options.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)

));

builder.Services.AddSingleton(sp =>
        builder.Configuration.GetSection("Gemini").Get<GeminiSettings>());

builder.Services.AddAPI(builder.Configuration);

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
