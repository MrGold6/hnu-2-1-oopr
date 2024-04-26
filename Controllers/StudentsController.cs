using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabW4.Data;
using LabW4.Models;

namespace LabW4.Controllers
{
    public class StudentsController : Controller
    {
        private readonly LabW4Context _context;

        public StudentsController(LabW4Context context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var labW4Context = _context.Student.Include(s => s.University).Include(s => s.Voucher);
            return View(await labW4Context.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.University)
                .Include(s => s.Voucher)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["UniversityId"] = new SelectList(_context.University, "UniversityId", "UniversityId");
            ViewData["VoucherId"] = new SelectList(_context.Voucher, "VoucherId", "VoucherId");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Name,Surname,Lastname,Age,Email,PhoneNumber,Address,Date_of_admission,IsGraduated,Atestat,NumbeOfRecordBook,Document,Group,Speciality,Course,VoucherId,UniversityId")] Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["UniversityId"] = new SelectList(_context.University, "UniversityId", "UniversityId", student.UniversityId);
            ViewData["VoucherId"] = new SelectList(_context.Voucher, "VoucherId", "VoucherId", student.VoucherId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,Name,Surname,Lastname,Age,Email,PhoneNumber,Address,Date_of_admission,IsGraduated,Atestat,NumbeOfRecordBook,Document,Group,Speciality,Course,VoucherId,UniversityId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
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

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.University)
                .Include(s => s.Voucher)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
