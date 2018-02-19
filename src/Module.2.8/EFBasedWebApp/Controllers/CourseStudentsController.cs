using EF.Core;
using EF.Core.Model;
using EFBasedWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EFBasedWebApp.Controllers
{
    public class CourseStudentsController : Controller
    {
        private readonly SchoolContext _context;

        public CourseStudentsController(SchoolContext context)
        {
            _context = context;
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

        // GET: CourseStudents/Delete/5
        public async Task<IActionResult> Delete(int? courseId, int? studentId)
        {
            if (courseId == null && studentId == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .SingleOrDefaultAsync(m => m.CourseId == courseId && m.StudentId == studentId);
            if (courseStudent == null)
            {
                return NotFound();
            }

            return View(courseStudent);
        }

        // POST: CourseStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int courseId, int studentId)
        {
            var courseStudent = await _context.CourseStudents.SingleOrDefaultAsync(m => m.CourseId == courseId && m.StudentId == studentId);
            _context.CourseStudents.Remove(courseStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CourseStudents/Details/5

        //CourseStudents/details/1/2
        public async Task<IActionResult> Details(int? courseId, int? studentId)
        {
            if (courseId == null && studentId == null)
            {
                return NotFound();
            }

            var courseStudent = await _context.CourseStudents
                .Include(c => c.Course)
                .Include(c => c.Student)
                .SingleOrDefaultAsync(m => m.CourseId == courseId && m.StudentId == studentId);

            if (courseStudent == null)
            {
                return NotFound();
            }

            return View(courseStudent);
        }



        // GET: CourseStudents
        public async Task<IActionResult> Index2()
        {
            //var schoolContext = _context.CourseStudents.Include(c => c.Course).Include(c => c.Student);


            var n = from s in _context.Students
                    join cs in _context.CourseStudents on s.StudentId equals cs.StudentId
                    join c in _context.Courses on cs.CourseId equals c.CourseId
                    group new { s.FirstName, s.LastName } by new { c.Title, c.Department.Name } into g
                    select g;

            var data = n.ToList();

            return View(data);
        }



        // GET: CourseStudents
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.CourseStudents.Include(c => c.Course).Include(c => c.Student);            
            return View(await schoolContext.ToListAsync());
        }

        private bool CourseStudentExists(int courseId, int studentId)
        {
            return _context.CourseStudents.Any(e => e.CourseId == courseId && e.StudentId == studentId);
        }
    }
}