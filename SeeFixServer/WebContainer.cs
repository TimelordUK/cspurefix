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
                // Try new TypeRegistry.json first, fall back to legacy DictionaryMeta.json
                var registryPath = PathUtil.GetPath("TypeRegistry.json");
                var legacyPath = PathUtil.GetPath("DictionaryMeta.json");

                if (registryPath != null && File.Exists(registryPath))
                {
                    Console.WriteLine($"Loading type registry from: {registryPath}");
                    dicts.Init(registryPath, assembly);
                }
                else if (legacyPath != null && File.Exists(legacyPath))
                {
                    Console.WriteLine($"Loading legacy dictionary meta from: {legacyPath}");
                    dicts.Init(legacyPath, assembly);
                }
                else
                {
                    Console.WriteLine("Warning: No type registry or dictionary meta found");
                }
            }

            return app;
        }
    }
}
