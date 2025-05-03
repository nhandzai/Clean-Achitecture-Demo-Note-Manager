using Microsoft.Extensions.DependencyInjection;
using NoteApp.UseCases.Interfaces;
using NoteApp.InterfaceAdapters.Repositories;
using System.Reflection;

namespace NoteApp.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register all use cases dynamically
            var useCaseTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains("NoteApp.UseCases") && t.Name.EndsWith("UseCase"));
            foreach (var useCaseType in useCaseTypes)
            {
                services.AddTransient(useCaseType);
            }

            // Register repositories
            services.AddSingleton<JsonNoteRepository>();
            services.AddSingleton<XmlNoteRepository>();

            services.AddSingleton<INoteRepository>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var type = config["RepositoryType"]?.ToLowerInvariant();
                return type switch
                {
                    "xml" => provider.GetRequiredService<XmlNoteRepository>(),
                    _ => provider.GetRequiredService<JsonNoteRepository>()
                };
            });

            services.AddSingleton<RepositoryFactory>();
            return services;
        }
    }
}