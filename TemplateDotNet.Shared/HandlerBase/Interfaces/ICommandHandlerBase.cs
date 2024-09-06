namespace TemplateDotNet.Shared.HandlerBase.Interfaces
{
    public interface ICommandHandlerBase<TCommand, TResult> where TCommand : ICommand
    {
        Task<ICommandResult<TResult>> HandleAsync(TCommand command);
    }
}
