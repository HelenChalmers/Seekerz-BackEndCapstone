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

namespace Seekerz.Controllers
{
    public class JobsController : Controller
    {
        //Create variable to represent database
        private readonly ApplicationDbContext _context;

        //Create variable to represent User Data
        private readonly UserManager<ApplicationUser> _userManager;


        //Create component to get current user from the _userManager variable
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Job.Include(j => j.Company).Include(j => j.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.Company)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,Position,PersonalNotes,ToldNss,IsActive,UserId,CompanyId")] Job job)
        {
            //Remove User, UserId and IsActive
            ModelState.Remove("Job.UserId");
            ModelState.Remove("Job.User");
            ModelState.Remove("Job.IsActive");

            //Check if model state is valid

            if (ModelState.IsValid)
            {
                //Get current user
                var user = await GetCurrentUserAsync();

                //Add user to Model
                job.User = user;
                job.UserId = user.Id;

                //Set IsActive
                job.IsActive = true;

                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Name", job.CompanyId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", job.UserId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Location", job.CompanyId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", job.UserId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,Position,PersonalNotes,ToldNss,IsActive,UserId,CompanyId")] Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobId))
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Location", job.CompanyId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", job.UserId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.Company)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.FindAsync(id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.JobId == id);
        }
    }
}
