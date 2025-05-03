using NoteApp.Entities;
using NoteApp.UseCases.Interfaces;
using System.Xml.Serialization;

namespace NoteApp.InterfaceAdapters.Repositories
{
    public class XmlNoteRepository : INoteRepository
    {
        private readonly string _filePath;
        private readonly ILogger<XmlNoteRepository> _logger;

        public XmlNoteRepository(ILogger<XmlNoteRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _filePath = configuration["NotesFilePath"] ?? "notes.xml";
        }

        #pragma warning disable CS1998 
        private async Task<List<Note>> ReadFromFile()

        {
            try
            {
                if (!File.Exists(_filePath)) return new();
                using var stream = File.OpenRead(_filePath);
                var serializer = new XmlSerializer(typeof(List<Note>));
                return (List<Note>)(serializer.Deserialize(stream) ?? new List<Note>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading XML file: {FilePath}", _filePath);
                throw;
            }
        }


        private async Task WriteToFile(List<Note> notes)

        {
            try
            {
                using var stream = File.Create(_filePath);
                var serializer = new XmlSerializer(typeof(List<Note>));
                serializer.Serialize(stream, notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing XML file: {FilePath}", _filePath);
                throw;
            }
        }

        public async Task<List<Note>> GetAllAsync() => await ReadFromFile();
        public async Task<Note?> GetByIdAsync(Guid id) => (await ReadFromFile()).FirstOrDefault(n => n.Id == id);
        public async Task AddAsync(Note note)
        {
            var notes = await ReadFromFile();
            notes.Add(note);
            await WriteToFile(notes);
        }
        public async Task DeleteAsync(Guid id)
        {
            var notes = await ReadFromFile();
            notes.RemoveAll(n => n.Id == id);
            await WriteToFile(notes);
        }
    }
}
