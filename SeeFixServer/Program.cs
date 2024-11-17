using Microsoft.Extensions.Configuration;
using PureFix.Dictionary.Contained;
using PureFix.LogMessageParser;
using SeeFixServer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var container = new WebContainer();
var app = container.Init(args);

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
