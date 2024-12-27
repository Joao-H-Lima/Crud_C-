using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProjetoSimpliss.Data;
using System.IO;

namespace ProjetoSimpliss
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjetoSimplissContext>
    {
        public ProjetoSimplissContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjetoSimplissContext>();

            // Ajuste o caminho para o seu appsettings.json se necessário
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ProjetoSimplissContext");

            optionsBuilder.UseNpgsql(connectionString);

            return new ProjetoSimplissContext(optionsBuilder.Options);
        }
    }
}
