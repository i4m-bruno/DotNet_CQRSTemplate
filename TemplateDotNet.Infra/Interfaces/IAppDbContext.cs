using Microsoft.EntityFrameworkCore;
using TemplateDotNet.Domain.Entities;

namespace TemplateDotNet.Infra.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Usuario> Usuarios { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}