using Eqra.Features.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Eqra.Features.DataAccess.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
