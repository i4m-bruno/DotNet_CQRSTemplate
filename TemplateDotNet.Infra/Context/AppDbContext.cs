using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TemplateDotNet.Domain.Entities;
using TemplateDotNet.Infra.Extensions;
using TemplateDotNet.Infra.Interfaces;

namespace TemplateDotNet.Infra.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(AppDbContext)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsOfType(typeof(IAppDbContextMap)); 
        }
    }
}
