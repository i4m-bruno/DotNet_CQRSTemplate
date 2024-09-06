namespace TemplateDotNet.Shared.HandlerBase.Interfaces
{
    public interface ICommandResult<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T Result { get; }
    }
}
