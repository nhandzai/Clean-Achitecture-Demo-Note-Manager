using Microsoft.AspNetCore.Mvc;
using NoteApp.Entities;
using NoteApp.UseCases.Note;

namespace NoteApp.InterfaceAdapters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly CreateNoteUseCase _createUseCase;
        private readonly GetAllNotesUseCase _getAllUseCase;
        private readonly DeleteNoteUseCase _deleteUseCase;

        public NoteController(
            CreateNoteUseCase createUseCase,
            GetAllNotesUseCase getAllUseCase,
            DeleteNoteUseCase deleteUseCase
        )
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _getAllUseCase.Execute();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Note note)
        {
            await _createUseCase.Execute(note.Title, note.Content);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteUseCase.Execute(id);
            return Ok();
        }
    }
}