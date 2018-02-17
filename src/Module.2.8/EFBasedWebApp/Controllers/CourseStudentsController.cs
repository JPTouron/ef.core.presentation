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
    public class CourseStudentsController : Controller
    {
        private readonly SchoolContext _context;

        public CourseStudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: CourseStudents
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.CourseStudents.Include(c => c.Course).Include(c => c.Student);
            return View(await schoolContext.ToListAsync());
        }

        // GET: CourseStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseStudent == null)
            {
                return NotFound();
            }

            return View(courseStudent);
        }

        // GET: CourseStudents/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: CourseStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,StudentId")] CourseStudent courseStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseStudent.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", courseStudent.StudentId);
            return View(courseStudent);
        }

        // GET: CourseStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents.SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseStudent == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseStudent.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", courseStudent.StudentId);
            return View(courseStudent);
        }

        // POST: CourseStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,StudentId")] CourseStudent courseStudent)
        {
            if (id != courseStudent.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseStudentExists(courseStudent.CourseId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseStudent.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", courseStudent.StudentId);
            return View(courseStudent);
        }

        // GET: CourseStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseStudent == null)
            {
                return NotFound();
            }

            return View(courseStudent);
        }

        // POST: CourseStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseStudent = await _context.CourseStudents.SingleOrDefaultAsync(m => m.CourseId == id);
            _context.CourseStudents.Remove(courseStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseStudentExists(int id)
        {
            return _context.CourseStudents.Any(e => e.CourseId == id);
        }
    }
}
