using Microsoft.Extensions.Configuration;
using PureFix.Dictionary.Contained;
using PureFix.LogMessageParser;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var root = app.Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
var assembly = Assembly.GetAssembly(typeof(PureFix.Types.FIX44.QuickFix.Logon));
var dicts = new DictContainer();
if (assembly != null)
{
    dicts.Init(PathUtil.GetPath("DictionaryMeta.json"), assembly);
}

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
