using NoteApp.UseCases.Interfaces;

namespace NoteApp.UseCases.Note
{
    public class DeleteNoteUseCase
    {
        private readonly INoteRepository _repository;

        public DeleteNoteUseCase(INoteRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}