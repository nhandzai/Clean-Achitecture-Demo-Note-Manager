using NoteApp.InterfaceAdapters.Repositories;
using NoteApp.UseCases.Interfaces;

namespace NoteApp.Infrastructure
{
    public class RepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INoteRepository GetRepository(string type)
        {
            return type switch
            {
                // Add other repository types 
                "json" => _serviceProvider.GetService<JsonNoteRepository>() ?? throw new InvalidOperationException("JsonNoteRepository is not registered."),                
                "xml" => _serviceProvider.GetService<XmlNoteRepository>() ?? throw new InvalidOperationException("XmlNoteRepository is not registered."),
                _ => throw new ArgumentException("Invalid repository type")
            };
        }
    }
}