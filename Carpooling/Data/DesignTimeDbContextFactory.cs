using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data
{
    public class TPIContextFactory : IDesignTimeDbContextFactory<TPIContext>
    {
        public TPIContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TPIContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TPINet;Trusted_Connection=true;MultipleActiveResultSets=true");

            return new TPIContext(optionsBuilder.Options);
        }
    }
}
