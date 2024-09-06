using Microsoft.OpenApi.Models;
using System.Reflection;
using TemplateDotNet.Application;
using TemplateDotNet.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InjetarApplication(builder.Configuration);
builder.Services.InjetarInfra(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Template API",
    });
});

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
