using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Interfaces.Configuration;

namespace TestAutoGlass.Infra.Presistence
{
    public class PostgreDbContext : DbContext, IPostgreDbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Products> Products { get; set; }

        public PostgreDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
