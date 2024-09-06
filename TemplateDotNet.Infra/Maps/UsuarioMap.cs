using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateDotNet.Domain.Entities;
using TemplateDotNet.Infra.Interfaces;

namespace TemplateDotNet.Infra.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>, IAppDbContextMap
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
        }
    }
}
