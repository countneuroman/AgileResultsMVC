﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Data;
using AgileResultsMVC.Models;
using AgileResultsMVC.ViewModels;
using System.Security.Claims;
using AgileResultsMVC.Services;

namespace AgileResultsMVC.Controllers;

public class AllTasksController : Controller
{
    private readonly AgileResultsMvcDbContext _context;
    private readonly ITaskService _taskService;

    public AllTasksController(AgileResultsMvcDbContext context, ITaskService taskService)
    {
        _context = context;
        _taskService = taskService;
    }

    //Проверка значения в БД(реализовано атрибутом Remote в модели).
    //Также проверяет количество созданных задач(на каждый период можно создать не более 3 задач!).
    [AcceptVerbs("GET", "POST")]
    public IActionResult VertifyPeriod(UserTask allTask)
    {

        //Вычисляем сколько задач создано на каждый период у каждого пользователя.
        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userTasks = from tasks in _context.AllTask
            where tasks.UserId == userId
            select tasks;
        var countDayPeriod = userTasks.Count(s => s.Period.StartsWith("Day"));
        var countWeekPeriod = userTasks.Count(s => s.Period.StartsWith("Week"));
        var countMonthPeriod = userTasks.Count(s => s.Period.StartsWith("Month"));

        switch(allTask.Period)
        {
            case "Day":
                return countDayPeriod < 3 ? Json(true) : Json($"Нельзя создать более 3 задач на день!");
            case "Week":
                return countWeekPeriod < 3 ? Json(true) : Json($"Нельзя создать более 3 задач на неделю!");
            case "Month":
                return countMonthPeriod < 3 ? Json(true) : Json($"Нельзя создать более 3 задач на месяц!");
            default:
                return Json($"Неправильное значение периода!");
        }
    }

    // GET: AllTasks
    public async Task<IActionResult> Index()
    {
        //Если пользователь не авторизован, страница задач не откроется.
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return NotFound();
        var allTaskUser = await _taskService.GetAllUserTasks(userId);
        return View(allTaskUser);
        //TODO Заменить на встроенное сообщение об ошибке.
    }

    // GET: AllTasks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();
        
        var userTask = await _taskService.GetTaskById(id);
        if (userTask == null)
            return NotFound();
        
        return View(userTask);
    }

    // GET: AllTasks/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: AllTasks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Period,Title,Description,CreateData,CompletionDate,UserId")] UserTask allTask)
    {
        if (!ModelState.IsValid) return View(allTask);
        //Значение = текущий пользователь в системе.
        allTask.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        switch (allTask.Period)
        {
            //Определение даты создания и даты окончания задачи.
            case "Day":
                allTask.CreateData = DateTime.Now;
                allTask.CompletionDate = DateTime.Now.AddDays(1);
                break;
            case "Week":
                allTask.CreateData = DateTime.Now;
                allTask.CompletionDate = DateTime.Now.AddDays(7);
                break;
            case "Month":
                allTask.CreateData = DateTime.Now;
                allTask.CompletionDate = DateTime.Now.AddMonths(1);
                break;
        }

        _context.Add(allTask);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: AllTasks/Edit/5
    public async Task<IActionResult> Edit(int? id, TaskEditModel taskEditModel)
    {
        if (id is null or 0)
            return NotFound();
        
        //Проверяем, есть ли задача с таким ID.
        var task = await _taskService.GetTaskById(id);

        if (task == null)
            return NotFound();
            
        //Проверяем, относятся ли данные к тому пользователю, который авторзован.
        if(task.UserId!= User.FindFirstValue(ClaimTypes.NameIdentifier))
            return NotFound();
            
        //Передаем в модель представления данные, которые нужно редактировать.
        taskEditModel.Id = task.Id;
        taskEditModel.Title = task.Title;
        taskEditModel.Description = task.Description;
        return View(taskEditModel);
    }

    // POST: AllTasks/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaskEditModel taskEditModel)
    {
        var task = await _taskService.GetTaskById(id);
        if (id != task.Id)
            return NotFound();
            

        if (!ModelState.IsValid) return View(taskEditModel);
        try
        {
            task.Title = taskEditModel.Title;
            task.Description = taskEditModel.Description;
            //allTask.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Update(task);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AllTaskExists(task.Id))
                return NotFound();

            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: AllTasks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();
        
        var userTask = await _taskService.GetTaskById(id);
        if (userTask == null)
            return NotFound();
        
        return View(userTask);
    }

    // POST: AllTasks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _taskService.RemoveTaskById(id);
        return RedirectToAction(nameof(Index));
    }

    private bool AllTaskExists(int id) => _context.AllTask.Any(e => e.Id == id);
}