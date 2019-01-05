  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seekerz.Data;
using Seekerz.Models;
using Seekerz.Models.JobViewModels;

namespace Seekerz.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public JobsController(ApplicationDbContext ctx,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Active Jobs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //getting the user
            var user = await GetCurrentUserAsync();

            var userjobs = _context.Job
                .Where(j => j.UserId == user.Id && j.IsActive == true)
                .ToListAsync();
            //var applicationDbContext = _context.Job.Include(j => j.Company).Include(j => j.User);

            return View(await userjobs);
        }
        //Getting Archived Jobs
        [Authorize]
        public async Task<IActionResult> ArchivedIndex()
        {
            //getting the user
            var user = await GetCurrentUserAsync();

            var userarchivedjobs = _context.Job
                .Where(j => j.UserId == user.Id && j.IsActive == false)
                .ToListAsync();
            //var applicationDbContext = _context.Job.Include(j => j.Company).Include(j => j.User);

            return View(await userarchivedjobs);
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
                .Include(j => j.UserTasks)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();

            List<Company> AllCompanies = await _context.Company.Where(c => c.UserId == user.Id).ToListAsync();

            List<SelectListItem> usersCompanies = new List<SelectListItem>();

            foreach (Company c in AllCompanies)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = c.Name;
                sli.Value = c.CompanyId.ToString();
                usersCompanies.Add(sli);
            };
            //Buils a select item "select company" and giving a value of 0
            SelectListItem defaultSli = new SelectListItem
            {
                Text = "Select Company",
                Value = "0"
            };

            //Sets it at position 0
            usersCompanies.Insert(0, defaultSli);

            JobCreateViewModel viewmodel = new JobCreateViewModel
            {
                UsersCompanies = usersCompanies
            };

            //ViewData["CompanyId"] = new SelectList(_context.Company.Where(c => c.UserId == user.Id.ToString()), "CompanyId", "Name");
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View(viewmodel);
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobCreateViewModel viewModel)
        {
            //Remove User, UserId and IsActive
            ModelState.Remove("Job.User");
            ModelState.Remove("Job.UserId");
            

            //Get current user
            ApplicationUser user = await GetCurrentUserAsync();

                //Add user to Model
                viewModel.Job.User = user;
                viewModel.Job.UserId = user.Id;

                //Set IsActive
                viewModel.Job.IsActive = true;

            //Check if model state is valid
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
           
            return View(viewModel);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var job = await _context.Job.FirstOrDefaultAsync(j => j.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Name", job.CompanyId);

            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", job.UserId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Job job)
        {

            if (id != job.JobId)
            {
                return NotFound();
            }


            //Remove user and userid
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                //Get Current User
                var user = await GetCurrentUserAsync();

                //Add user to model
                job.User = user;

                //Add userId to Model
                job.UserId = user.Id;

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

            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Name", job.CompanyId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", job.UserId);
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
            //gets the job user wants to delete and the tasks associated with it
            var job = await _context.Job
                .Include(j => j.UserTasks)
                .SingleOrDefaultAsync(j => j.JobId == id);

            //if there are tasks on the job, loop over them and delete them before deleting the job
            if (job.UserTasks.Count > 0)
            {
                foreach (TaskToDo tasktodo in job.UserTasks)
                {
                    _context.Remove(tasktodo);
                }
            }

            //deletes job and redirects to the index page.
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.JobId == id);
        }

        //Search Bar Function
        [Authorize]
        public async Task<IActionResult> SearchResults(string search)
        {
            var user = await GetCurrentUserAsync();

            var searchBar = _context.Job
             .Where(y => y.UserId == user.Id)
             .Where(x => x.Position.Contains(search) ||
             search == null ||
             x.Company.Name.Contains(search)).ToList();
            return View(searchBar);
        }
    }
}
