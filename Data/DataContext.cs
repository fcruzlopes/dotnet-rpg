using System.IO;
using dotnet_rpg.Constants;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString;

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Rictusempra", Damage = 1 },
                new Skill { Id = 2, Name = "Incendio", Damage = 200 },
                new Skill { Id = 3, Name = "AvadaKedavra", Damage = 100000 }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(Settings.AppsettingsFile)
                    .Build();
                var connectionString = configuration.GetConnectionString(Settings.DbConnectionName);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}