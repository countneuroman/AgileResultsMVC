using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AgileResultsMVC.Data
{
    public class AgileResultsMvcDbContext : IdentityDbContext<User>
    {
        public DbSet<UserTask> AllTask { get; set; }
       
        public AgileResultsMvcDbContext(DbContextOptions<AgileResultsMvcDbContext> options)
            : base(options)
        {
        }
       
        //Настраиваем БД для связи один к одному
        //Чтобы каждому пользователю выводить его задачи (а не все)
        //ВОЗМОЖНО НЕ РАБОТАЕТ!
        //Возможно поможет пересоздание БД, удаление папки миграций, команда Drop-Database, затем заного инициализировать миграцию.
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder
        //        .Entity<User>()
        //        .HasOne(u=>u.AllTask)
        //        .WithOne(p=>p.User)
        //        .HasForeignKey<AllTask>(p => p.userId);
        //}
        
    }
}
