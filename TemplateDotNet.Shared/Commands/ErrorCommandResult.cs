using TemplateDotNet.Shared.HandlerBase.Interfaces;

namespace TemplateDotNet.Shared.Commands
{
    public class ErrorCommandResult<T> : ICommandResult<T> where T : class
    {
        public bool Success { get; }

        public string Message { get; }

        public T Result { get; }

        public ErrorCommandResult(string message, T result)
        {
            Success = false;
            Message = message;
            Result = result;
        }
    }
}
