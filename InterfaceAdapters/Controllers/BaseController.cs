using Microsoft.AspNetCore.Mvc;
using System;

namespace NoteApp.InterfaceAdapters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TUseCase> : ControllerBase
    {
        protected readonly TUseCase _useCase;

        protected BaseController(TUseCase useCase)
        {
            _useCase = useCase;
        }
    }
}