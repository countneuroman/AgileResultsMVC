using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgileResultsMVC.Data;
using AgileResultsMVC.Models;
using System.Security.Claims;
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
        //Также проверяет количество созданных задач(на каждый период можно создать не более 3 задач!).
        [AcceptVerbs("GET", "POST")]
        public IActionResult VertifyPeriod(AllTask allTask)
        {

            //Вычисляем сколько задач создано на каждый период у каждого пользователя.
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userTasks = from tasks in _context.AllTask
                                  where tasks.UserId == userId
                                  select tasks;
            int countDayPeriod = userTasks.Count(s => s.Period.StartsWith("Day"));
            int countWeekPeriod = userTasks.Count(s => s.Period.StartsWith("Week"));
            int countMonthPeriod = userTasks.Count(s => s.Period.StartsWith("Month"));

            switch(allTask.Period)
            {
                case "Day":
                    if (countDayPeriod < 3)
                    {
                        return Json(true);
                    }
                    return Json($"Нельзя создать более 3 задач на день!");
                case "Week":
                    if (countWeekPeriod < 3)
                    {
                        return Json(true);
                    }
                    return Json($"Нельзя создать более 3 задач на неделю!");
                case "Month":
                    if (countMonthPeriod < 3)
                    {
                        return Json(true);
                    }
                    return Json($"Нельзя создать более 3 задач на месяц!");
                default:
                    return Json($"Неправильное значение периода!");
            }
        }

        // GET: AllTasks
        public async Task<IActionResult> Index()
        {
            //Если пользователь не авторизован, страница задач не откроется.
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId!=null)
            {
                var AllTaskUser = from tasks in _context.AllTask
                                  where tasks.UserId==userId
                                  select tasks;
                return View(await AllTaskUser.ToListAsync());
            }
            //TODO Заменить на встроенное сообщение об ошибке.
            return NotFound();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Period,Title,Description,CreateData,CompletionDate,UserId")] AllTask allTask)
        {
            if (ModelState.IsValid)
            {
                //Значение = текущий пользователь в системе.
                allTask.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Period,Title,Description,CreateData,CompletionDate,UserId")] AllTask allTask)
        {
            if (id != allTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    allTask.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
