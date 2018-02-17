using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EF.Core;
using EF.Core.Model;

namespace EFBasedWebApp.Controllers
{
    public class DegreeThesisController : Controller
    {
        private readonly SchoolContext _context;

        public DegreeThesisController(SchoolContext context)
        {
            _context = context;
        }

        // GET: DegreeThesis
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.DegreeThesis.Include(d => d.Student);
            return View(await schoolContext.ToListAsync());
        }

        // GET: DegreeThesis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeThesis = await _context.DegreeThesis
                .Include(d => d.Student)
                .SingleOrDefaultAsync(m => m.DegreeThesisId == id);
            if (degreeThesis == null)
            {
                return NotFound();
            }

            return View(degreeThesis);
        }

        // GET: DegreeThesis/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: DegreeThesis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompletionDate,DegreeThesisId,Mentor,StartDate,StudentId,ThesisSubject")] DegreeThesis degreeThesis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degreeThesis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", degreeThesis.StudentId);
            return View(degreeThesis);
        }

        // GET: DegreeThesis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeThesis = await _context.DegreeThesis.SingleOrDefaultAsync(m => m.DegreeThesisId == id);
            if (degreeThesis == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", degreeThesis.StudentId);
            return View(degreeThesis);
        }

        // POST: DegreeThesis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompletionDate,DegreeThesisId,Mentor,StartDate,StudentId,ThesisSubject")] DegreeThesis degreeThesis)
        {
            if (id != degreeThesis.DegreeThesisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degreeThesis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeThesisExists(degreeThesis.DegreeThesisId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", degreeThesis.StudentId);
            return View(degreeThesis);
        }

        // GET: DegreeThesis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeThesis = await _context.DegreeThesis
                .Include(d => d.Student)
                .SingleOrDefaultAsync(m => m.DegreeThesisId == id);
            if (degreeThesis == null)
            {
                return NotFound();
            }

            return View(degreeThesis);
        }

        // POST: DegreeThesis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var degreeThesis = await _context.DegreeThesis.SingleOrDefaultAsync(m => m.DegreeThesisId == id);
            _context.DegreeThesis.Remove(degreeThesis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeThesisExists(int id)
        {
            return _context.DegreeThesis.Any(e => e.DegreeThesisId == id);
        }
    }
}
