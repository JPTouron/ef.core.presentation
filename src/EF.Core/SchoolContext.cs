using EF.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace EF.Core
{
    public class SchoolContext : DbContext
    {
        protected SchoolContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = School; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}