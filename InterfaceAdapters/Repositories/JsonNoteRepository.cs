using NoteApp.Entities;
using NoteApp.UseCases.Interfaces;
using System.Text.Json;

namespace NoteApp.InterfaceAdapters.Repositories
{
    public class JsonNoteRepository : INoteRepository
    {
        private readonly string _filePath;
        private readonly ILogger<JsonNoteRepository> _logger;
        private readonly IConfiguration _configuration;
        public JsonNoteRepository(ILogger<JsonNoteRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _filePath = _configuration["NotesFilePath"] ?? "notes.json";
        }

        private async Task<List<Note>> ReadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath)) return new();
                var json = await File.ReadAllTextAsync(_filePath);

                return JsonSerializer.Deserialize<List<Note>>(json) ?? new();
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading from the file: {FilePath}", _filePath);
                throw; 
            }
        }

        private async Task WriteToFile(List<Note> notes)
        {
            try
            {
                var json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while writing to the file: {FilePath}", _filePath);
                throw; 
            }
        }

        public async Task<List<Note>> GetAllAsync()
        {
            try
            {
                return await ReadFromFile();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all notes.");
                throw;
            }
        }

        public async Task<Note?> GetByIdAsync(Guid id)
        {
            try
            {
                var notes = await ReadFromFile();
                return notes.FirstOrDefault(n => n.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a note by ID: {Id}", id);
                throw;
            }
        }

        public async Task AddAsync(Note note)
        {
            try
            {
                var notes = await ReadFromFile();
                notes.Add(note);
                await WriteToFile(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new note.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var notes = await ReadFromFile();
                notes.RemoveAll(n => n.Id == id);
                await WriteToFile(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a note by ID: {Id}", id);
                throw;
            }
        }
    }
}