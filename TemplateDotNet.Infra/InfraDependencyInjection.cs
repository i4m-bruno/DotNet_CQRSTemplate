using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateDotNet.Infra.Context;
using TemplateDotNet.Infra.Interfaces;

namespace TemplateDotNet.Infra
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection InjetarInfra(this IServiceCollection services, IConfiguration configuration)
        {
            AdicionarDbContext<AppDbContext>(services, configuration);
            services.AddScoped<IAppDbContext, AppDbContext>();
            return services;
        }

        private static void AdicionarDbContext<T>(IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            var cnn = ObterConnectionString(configuration, typeof(T).Name);
            services.AddDbContext<T>(options => options.UseSqlServer(cnn));
        }

        private static string ObterConnectionString(IConfiguration configuration, string connectionName)
            => configuration.GetConnectionString(connectionName) ?? throw new NullReferenceException("cnn não configurada");
    }
}
