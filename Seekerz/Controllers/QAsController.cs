using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seekerz.Data;
using Seekerz.Models;

namespace Seekerz.Controllers
{
    public class QAsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QAsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QAs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.QA.Include(q => q.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: QAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qA = await _context.QA
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.QAId == id);
            if (qA == null)
            {
                return NotFound();
            }

            return View(qA);
        }

        // GET: QAs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: QAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QAId,Question,Answer,Notes,UserId")] QA qA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", qA.UserId);
            return View(qA);
        }

        // GET: QAs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qA = await _context.QA.FindAsync(id);
            if (qA == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", qA.UserId);
            return View(qA);
        }

        // POST: QAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QAId,Question,Answer,Notes,UserId")] QA qA)
        {
            if (id != qA.QAId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QAExists(qA.QAId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", qA.UserId);
            return View(qA);
        }

        // GET: QAs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qA = await _context.QA
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.QAId == id);
            if (qA == null)
            {
                return NotFound();
            }

            return View(qA);
        }

        // POST: QAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qA = await _context.QA.FindAsync(id);
            _context.QA.Remove(qA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QAExists(int id)
        {
            return _context.QA.Any(e => e.QAId == id);
        }
    }
}
