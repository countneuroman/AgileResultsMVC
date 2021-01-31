using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Models;

namespace AgileResultsMVC.Data
{
    public class AgileResultsMVCContext : DbContext
    {
        public AgileResultsMVCContext(DbContextOptions<AgileResultsMVCContext> options)
            : base(options)
        {
        }

        public DbSet<AllTask> AllTask { get; set; }
    }
}
