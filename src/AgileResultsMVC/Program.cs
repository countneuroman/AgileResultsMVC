using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Data;
using AgileResultsMVC.Models;
using AgileResultsMVC.Services;
using Microsoft.AspNetCore.Identity;

namespace AgileResultsMVC;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AgileResultsMvcDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AgileResultsMVCContext")));

        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AgileResultsMvcDbContext>();
            
        builder.Services.AddScoped<ITasksRepository, TasksRepository>();
        builder.Services.AddScoped<ITaskService, TaskService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.Run();
    }
}