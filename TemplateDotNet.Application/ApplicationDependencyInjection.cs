using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TemplateDotNet.Shared.Attributes;
using TemplateDotNet.Shared.Enum;
using TemplateDotNet.Shared.HandlerBase.Interfaces;

namespace TemplateDotNet.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection InjetarApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHandlers(typeof(ICommandHandlerBase<,>));
            services.AddHandlers(typeof(IQueryHandlerBase<,>));
            return services;
        }

        private static void AddHandlers(this IServiceCollection services, Type iHandlerType)
        {
            var tipos = BuscarTipos(iHandlerType);
            InjetarHandlers(services, tipos, iHandlerType);
        }

        // Ioc de cada um dos tipos que implementam o IHandlerType
        private static void InjetarHandlers(IServiceCollection services, List<Type> tipos, Type iHandlerType)
        {
            Parallel.ForEach(tipos, tipo => {
                var lifetime = BuscarLifetime(tipo);
                var @interface = BuscarInterface(tipo, iHandlerType);
                Injetar(services, @interface, lifetime, tipo);
            });
        }

        private static void Injetar(IServiceCollection services, Type @interface, DI lifetime, Type tipo)
        {
            switch(lifetime)
            {
                case DI.Scoped:
                    services.AddScoped(@interface, tipo); break;

                case DI.Transient:
                    services.AddTransient(@interface, tipo); break;

                case DI.Singleton:
                    services.AddSingleton(@interface, tipo); break;

                default:
                    throw new Exception($"Impossível injetar o serviço {@interface.Name} , {tipo.Name}");
            }
        }

        // Busca a interface do command handler concreto, EX: ICommandHandlerBase<CadastrarUsuarioCommand, UsuarioVm>
        private static Type BuscarInterface(Type tipo, Type iHandlerType)
            => tipo.GetInterfaces()
                    .AsParallel()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == iHandlerType) ?? throw new Exception($"Interface não encontrada para o tipo {tipo.Name}");

        private static DI BuscarLifetime(Type tipo)
        {
            var attribute = tipo.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == nameof(InjectionAttribute)) ?? throw new Exception("É preciso fornecer o lifetime do command handler");
            var value = attribute.ConstructorArguments.FirstOrDefault().Value ?? throw new Exception("É preciso fornecer o lifetime do command handler");
            return (DI)value;
        }

        // Busca tipos que implementam IHandlerType
        private static List<Type> BuscarTipos(Type iHandlerType)
            => Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .AsParallel()
                        .Where(type => type.GetInterfaces().Any(type => type.IsGenericType && type.GetGenericTypeDefinition() == iHandlerType))
                        .ToList();
    }
}
