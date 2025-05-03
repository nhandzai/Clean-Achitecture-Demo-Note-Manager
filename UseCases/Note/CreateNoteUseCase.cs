using NoteApp.UseCases.Interfaces;

namespace NoteApp.UseCases.Note
{
    public class CreateNoteUseCase
    {
        private readonly INoteRepository _repository;

        public CreateNoteUseCase(INoteRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(string title, string content)
        {
            var newNote = new Entities.Note(title, content);
            await _repository.AddAsync(newNote);
        }
    }
}