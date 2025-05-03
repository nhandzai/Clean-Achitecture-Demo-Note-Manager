using NoteApp.UseCases.Interfaces;

namespace NoteApp.UseCases.Note
{
    public class GetAllNotesUseCase
    {
        private readonly INoteRepository _repository;

        public GetAllNotesUseCase(INoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Entities.Note>> Execute()
        {
            return await _repository.GetAllAsync();
        }
    }
}