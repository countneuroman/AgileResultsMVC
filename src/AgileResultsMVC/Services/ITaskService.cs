

using System.Collections.Generic;
using System.Threading.Tasks;
using AgileResultsMVC.Models;

namespace AgileResultsMVC.Services;

public interface ITaskService
{
    Task<UserTask> GetTaskById(int? id);
    Task RemoveTaskById(int id);
    Task<List<UserTask>> GetAllUserTasks(string userId);
}