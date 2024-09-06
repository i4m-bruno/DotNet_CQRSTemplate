using TemplateDotNet.Shared.HandlerBase.Interfaces;

namespace TemplateDotNet.Shared.HandlerBase
{
    public abstract class CommandHandlerBase<TCommand, TResult> : ICommandHandlerBase<TCommand, TResult> where TCommand : ICommand
    {
        public async Task<ICommandResult<TResult>> HandleAsync(TCommand command)
        {
            try
            {
                return await HandleCoreAsync(command);
            }
            catch (Exception)
            {
                //TODO Colocar lógica para log de erros
                throw;
            }
        }

        protected abstract Task<ICommandResult<TResult>> HandleCoreAsync(TCommand command);
    }
}
