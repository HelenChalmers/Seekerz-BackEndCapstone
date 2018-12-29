using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seekerz.Data;
using Seekerz.Models;

namespace Seekerz.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        //method gets user
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext ctx,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Jobs
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

       


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
