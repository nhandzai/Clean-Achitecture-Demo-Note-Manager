namespace NoteApp.UseCases.Interfaces
{
    public interface INoteRepository
    {
        Task<List<Entities.Note>> GetAllAsync();
        Task<Entities.Note?> GetByIdAsync(Guid id);
        Task AddAsync(Entities.Note note);
        Task DeleteAsync(Guid id);
    }
}
