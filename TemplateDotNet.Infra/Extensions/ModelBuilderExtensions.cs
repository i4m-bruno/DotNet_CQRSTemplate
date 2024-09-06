using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TemplateDotNet.Infra.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyConfigurationsOfType(this ModelBuilder modelBuilder, Type type)
        {
            var applyConfigurationMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration", BindingFlags.Instance | BindingFlags.Public);

            var typeChildrens = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .AsParallel()
                                        .Where(t => t.GetInterfaces().Any(t => t == type))
                                        .ToList();

            Parallel.ForEach(typeChildrens, typeChildren =>
            {
                var typeConfiguration = typeChildren.GetInterfaces()
                                                    .AsParallel()
                                                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                                                    .First()
                                                    .GenericTypeArguments.First();

                var applyConfigurationGenericMethod = applyConfigurationMethod?.MakeGenericMethod(typeConfiguration);
                applyConfigurationGenericMethod?.Invoke(modelBuilder, [Activator.CreateInstance(typeChildren)]);
            });
        }
    }
}
