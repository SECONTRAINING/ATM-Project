using DataObjectLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer
{
    public class ATMDBContext : DbContext
    {
        private readonly string _ConStr;

        public ATMDBContext(DbContextOptions<ATMDBContext> options) : base(options)
        {
        }

        public ATMDBContext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _ConStr = configuration.GetConnectionString("ATMDBConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConStr);
            }
        }

        // DbSet properties for your model classes go here
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<BranchDetail> BranchDetails { get; set; }
        public DbSet<LoginCredential> LoginCredentials { get; set; }
        
    }
}
