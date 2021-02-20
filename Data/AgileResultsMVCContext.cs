using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
