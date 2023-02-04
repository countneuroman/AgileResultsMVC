using System.Collections.Generic;
using System.Threading.Tasks;
using AgileResultsMVC.Models;

namespace AgileResultsMVC.Data;

public interface ITasksRepository
{
    Task<List<UserTask>> GetUserTasks(string userId);
    Task<UserTask> GetTaskById(int? id);
    Task RemoveTaskById(int id);
}