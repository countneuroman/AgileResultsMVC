using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AgileResultsMVC.Data
{
    public class AgileResultsMVCContext : IdentityDbContext<User>
    {
        public DbSet<AllTask> AllTask { get; set; }
        public AgileResultsMVCContext(DbContextOptions<AgileResultsMVCContext> options)
            : base(options)
        {
        }
        //Настраиваем БД для связи один к одному
        //Чтобы каждому пользователю выводить его задачи (а не все)
        //ВОЗМОЖНО НЕ РАБОТАЕТ!
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<User>()
                .HasOne(u=>u.AllTask)
                .WithOne(p=>p.User)
                .HasForeignKey<AllTask>(p => p.userId);
        }
    }
}
