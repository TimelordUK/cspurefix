using Microsoft.Extensions.ObjectPool;
using PureFix.Dictionary.Contained;
using PureFix.LogMessageParser;
using System.Reflection;

namespace SeeFixServer
{
    public class WebContainer
    {
        public WebApplication Init(string[] args)
        {
            // Add services to the container.
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IDictContainer, DictContainer>();

            var app = builder.Build();
            app.UseCors(b => b
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:3000"));

            var root = app.Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var assembly = Assembly.GetAssembly(typeof(PureFix.Types.FIX44.Logon));
            var dicts = app.Services.GetService<IDictContainer>();
            if (assembly != null && dicts != null)
            {
                dicts.Init(PathUtil.GetPath("DictionaryMeta.json"), assembly);
            }

            return app;
        }
    }
}
