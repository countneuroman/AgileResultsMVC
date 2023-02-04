using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileResultsMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileResultsMVC.Data;

public class TasksRepository : ITasksRepository
{
    private readonly AgileResultsMvcDbContext _context;
    
    public TasksRepository(AgileResultsMvcDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<UserTask>> GetUserTasks(string userId) =>
        await _context.AllTask.Where(tasks => tasks.UserId == userId).ToListAsync();

    public async Task<UserTask> GetTaskById(int? id) =>
        await _context.AllTask
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task RemoveTaskById(int id)
    {
        UserTask task = await GetTaskById(id);
        _context.AllTask.Remove(task);
        await _context.SaveChangesAsync();
    }
}