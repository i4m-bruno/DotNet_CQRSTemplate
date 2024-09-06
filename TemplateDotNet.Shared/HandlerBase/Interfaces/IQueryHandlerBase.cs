namespace TemplateDotNet.Shared.HandlerBase.Interfaces
{
    public interface IQueryHandlerBase<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
