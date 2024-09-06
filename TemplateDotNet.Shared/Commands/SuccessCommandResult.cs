using TemplateDotNet.Shared.HandlerBase.Interfaces;

namespace TemplateDotNet.Shared.Commands
{
    public class SuccessCommandResult<T> : ICommandResult<T> where T : class
    {
        public bool Success { get; }

        public string Message { get; }

        public T Result {  get; }

        public SuccessCommandResult(string message, T result)
        {
            Success = true;
            Message = message;
            Result = result;
        }
    }
}
