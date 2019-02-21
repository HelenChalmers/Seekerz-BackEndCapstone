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

            //get current user
            var user = await GetCurrentUserAsync();

            //get all the companies from database
            List<Company> AllCompanies = await _context.Company.Where(c => c.UserId == user.Id).ToListAsync();

            //creating a select list of companies
            List<SelectListItem> usersCompanies = new List<SelectListItem>();

            //Create a view model
            JobCreateViewModel viewModel = new JobCreateViewModel();

            //building up the select list 
            foreach (Company c in AllCompanies)
            {
                SelectListItem sli = new SelectListItem()
                {
                    //provide text to sli
                    Text = c.Name,
                    //give value to sli
                    Value = c.CompanyId.ToString()
                };
                usersCompanies.Add(sli);
            }
            //Builds a select item "select company" and giving a value of 0
            usersCompanies.Insert(0, new SelectListItem
            {
                Text = "Select an existing Company",
                Value = ""
            });
            //SelectListItem defaultSli = new SelectListItem
            //{
            // Text = "Select Company",
            //Value = "0"
            //};

            //Sets it at position 0
            // usersCompanies.Insert(0, defaultSli);

            //JobCreateViewModel viewmodel = new JobCreateViewModel
            //{
            //  UsersCompanies = usersCompanies
            //};
            viewModel.UsersCompanies = usersCompanies;


            return View(viewModel);
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(JobCreateViewModel viewModel)
        {
            

            //Remove User, UserId and IsActive
            ModelState.Remove("Job.User");
            ModelState.Remove("Job.UserId");

            ModelState.Remove("Job.Company");
            

            //Check if model state is valid
            if (ModelState.IsValid)
            {

                //Get current user
                ApplicationUser user = await GetCurrentUserAsync();

                //Add user to Model
                viewModel.Job.User = user;
                viewModel.Job.UserId = user.Id;

                //Set IsActive
                viewModel.Job.IsActive = true;

                _context.Add(viewModel.Job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
           
            return View(viewModel);
        }

        // POST: Job/CreateCompany
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateCompany(JobCreateViewModel newJob)
        {
            var user = await GetCurrentUserAsync();

            // If the user is already in the ModelState
            // Remove user from model state
            ModelState.Remove("Job.User");
            ModelState.Remove("Job.UserId");

            // If model state is valid
            if (ModelState.IsValid)
            {

                

                //Add user to model
                newJob.Job.User = user;

                //Add userId to Model
                newJob.Job.UserId = user.Id;
                //If a user enters a new company name
                if (newJob.UserCompany.Name != null)
                {
                    //Add that company to the database
                    _context.Add(newJob.UserCompany);

                    await _context.SaveChangesAsync();
                }
                // Redirect to details view with id of product made using new object
                return RedirectToAction(nameof(Create));
            }
            return View(newJob);
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
        //This makes the user to be logged in.  
        [Authorize]
           //a Search Method that takes a string parameter "search"
        public async Task<IActionResult> SearchResults(string search)
        {
            //this gets the user
            var user = await GetCurrentUserAsync();
            //grabs all of the jobs of the current user based on the UserId of the position
            //uses a .contain for the search -so whatever position contains what is put in the search is pulled up
            //uses a .contain for the company - so the company that was added searches for the company that was typed in by the user.
            //returns a list of positions based on either the company or positions that the user is searching for. 
            var searchBar = _context.Job
             .Where(y => y.UserId == user.Id)
             .Where(x => x.Position.Contains(search) ||
             search == null ||
             x.Company.Name.Contains(search)).ToList();
            return View(searchBar);
        }
    }
}
