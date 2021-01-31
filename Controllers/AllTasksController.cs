using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Data;
using AgileResultsMVC.Models;

namespace AgileResultsMVC.Controllers
{
    public class AllTasksController : Controller
    {
        private readonly AgileResultsMVCContext _context;

        public AllTasksController(AgileResultsMVCContext context)
        {
            _context = context;
        }

        //Проверка значения в БД(реализовано атрибутом Remote в модели).
        //НЕ РАБОТАЕТ ПРОВЕРКА НА НОЛЬ!
        [AcceptVerbs("GET", "POST")]
        public IActionResult VertifyPeriod(AllTask allTask)
        {            
            if (allTask.Period == "Day" || allTask.Period == "Week" || allTask.Period == "Month")
            {
                return Json(true);
            }
            return Json($"Wrong period value!");            
        }

        // GET: AllTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllTask.ToListAsync());
        }

        // GET: AllTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allTask = await _context.AllTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allTask == null)
            {
                return NotFound();
            }

            return View(allTask);
        }

        // GET: AllTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Period,Title,Description,CreateData,CompletionDate")] AllTask allTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allTask);
        }

        // GET: AllTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allTask = await _context.AllTask.FindAsync(id);
            if (allTask == null)
            {
                return NotFound();
            }
            return View(allTask);
        }

        // POST: AllTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Period,Title,Description,CreateData,CompletionDate")] AllTask allTask)
        {
            if (id != allTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllTaskExists(allTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(allTask);
        }

        // GET: AllTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allTask = await _context.AllTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allTask == null)
            {
                return NotFound();
            }

            return View(allTask);
        }

        // POST: AllTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allTask = await _context.AllTask.FindAsync(id);
            _context.AllTask.Remove(allTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllTaskExists(int id)
        {
            return _context.AllTask.Any(e => e.Id == id);
        }
    }
}
