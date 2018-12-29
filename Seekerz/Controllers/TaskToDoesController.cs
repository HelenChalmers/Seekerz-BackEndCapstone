using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seekerz.Data;
using Seekerz.Models;
using Seekerz.Models.JobViewModels;

namespace Seekerz.Controllers
{
    public class TaskToDoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public TaskToDoesController(ApplicationDbContext ctx,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: TaskToDoes
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            
            var tasks = _context.TaskToDo
                .OrderBy(td => td.CompleteDate)
                .Where(td => td.Jobs.UserId == user.Id && td.IsCompleted == false)
                .ToListAsync();
            
            return View(await tasks);
        }


        // GET: TaskToDoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskToDo = await _context.TaskToDo
                .Include(t => t.Jobs)
                .FirstOrDefaultAsync(m => m.TaskToDoId == id);
            if (taskToDo == null)
            {
                return NotFound();
            }

            return View(taskToDo);
        }

        // GET: TaskToDoes/Create - passing in the ID of the job so that when the task is created, the JobId is associated with the task 
        public async Task<IActionResult> Create(int id)
        {
            Job job = await _context.Job
                .FirstOrDefaultAsync(j => j.JobId == id);


            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: TaskToDoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskToDo model, int id)
        {
            


            if (ModelState.IsValid)
            {
                //this builds up the object of the tasks
                TaskToDo taskToDo = new TaskToDo()
                {
                    NewTask = model.NewTask,
                    CompleteDate = model.CompleteDate,
                    IsCompleted = false,
                    JobId = id
                };
                _context.Add(taskToDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["JobId"] = new SelectList(_context.Job, "JobId", "Position", taskToDo.JobId);
            return View(model);
        }

        // GET: TaskToDoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskToDo = await _context.TaskToDo.FindAsync(id);
            if (taskToDo == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Job, "JobId", "Position", taskToDo.JobId);
            return View(taskToDo);
        }

        // POST: TaskToDoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskToDoId,NewTask,CompleteDate,IsCompleted,JobId")] TaskToDo taskToDo)
        {
            if (id != taskToDo.TaskToDoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskToDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskToDoExists(taskToDo.TaskToDoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "TaskToDoes");
                // new { id = taskToDo.TaskToDoId }
            }
            ViewData["JobId"] = new SelectList(_context.Job, "JobId", "Position", taskToDo.JobId);
            return View(taskToDo);
        }

        // GET: TaskToDoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskToDo = await _context.TaskToDo
                .Include(t => t.Jobs)
                .FirstOrDefaultAsync(m => m.TaskToDoId == id);
            if (taskToDo == null)
            {
                return NotFound();
            }

            return View(taskToDo);
        }

        // POST: TaskToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskToDo = await _context.TaskToDo.FindAsync(id);
            _context.TaskToDo.Remove(taskToDo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskToDoExists(int id)
        {
            return _context.TaskToDo.Any(e => e.TaskToDoId == id);
        }
    }
}
