using TemplateDotNet.Application.Usuarios.ViewModels;
using TemplateDotNet.Domain.Entities;
using TemplateDotNet.Infra.Interfaces;
using TemplateDotNet.Shared.Attributes;
using TemplateDotNet.Shared.Commands;
using TemplateDotNet.Shared.Enum;
using TemplateDotNet.Shared.HandlerBase;
using TemplateDotNet.Shared.HandlerBase.Interfaces;

namespace TemplateDotNet.Application.Usuarios.Commands
{
    public class CadastrarUsuarioCommand(string nome) : ICommand
    {
        public string Nome { get; set; } = nome;
    }

    [Injection(DI.Scoped)]
    public class CadastrarUsuarioCommandHandler(IAppDbContext context) : CommandHandlerBase<CadastrarUsuarioCommand, UsuarioVm>
    {
        private readonly IAppDbContext _context = context;
        protected override async Task<ICommandResult<UsuarioVm>> HandleCoreAsync(CadastrarUsuarioCommand command)
        {
            var usuario = new Usuario(command.Nome);

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return new SuccessCommandResult<UsuarioVm>("Testado com sucesso", new UsuarioVm(usuario.Nome));
        }
    }
}
