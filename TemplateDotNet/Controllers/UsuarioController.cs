using Microsoft.AspNetCore.Mvc;
using TemplateDotNet.Application.Usuarios.Commands;
using TemplateDotNet.Application.Usuarios.ViewModels;
using TemplateDotNet.Shared.HandlerBase.Interfaces;

namespace TemplateDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ICommandResult<UsuarioVm>>> CadastrarUsuario([FromServices] ICommandHandlerBase<CadastrarUsuarioCommand, UsuarioVm> handler, [FromBody]CadastrarUsuarioCommand command)
        {
            var result = await handler.HandleAsync(command);
            if(result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
