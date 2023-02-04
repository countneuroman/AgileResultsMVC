using System.Collections.Generic;
using System.Threading.Tasks;
using AgileResultsMVC.Data;
using AgileResultsMVC.Models;
using System.Security.Claims;

namespace AgileResultsMVC.Services;

public class TaskService : ITaskService
{
    private readonly ITasksRepository _tasksRepository;

    public TaskService(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<UserTask> GetTaskById(int? id) => await _tasksRepository.GetTaskById(id);
    public async Task RemoveTaskById(int id) => await _tasksRepository.RemoveTaskById(id);
    public async Task<List<UserTask>> GetAllUserTasks(string userId)
    {
        return await _tasksRepository.GetUserTasks(userId);
    }
}