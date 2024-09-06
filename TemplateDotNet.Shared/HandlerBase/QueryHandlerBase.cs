using TemplateDotNet.Shared.HandlerBase.Interfaces;

namespace TemplateDotNet.Shared.HandlerBase
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandlerBase<TQuery, TResult>
    {
        public async Task<TResult> HandleAsync(TQuery query)
        {
            try
            {
                return await HandleCoreAsync(query);
            }
            catch (Exception)
            {
                //TODO log de erros
                throw;
            }
        }

        protected abstract Task<TResult> HandleCoreAsync(TQuery query);
    }
}
