using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreProject.Models;

namespace AspNetCoreProject.Controllers
{
    public class Student_ResultController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Student_ResultController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student_Result
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student_Result.ToListAsync());
        }

        // GET: Student_Result/Details/5
        [ServiceFilter(typeof(CommonActionFilter))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_Result = await _context.Student_Result
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student_Result == null)
            {
                return NotFound();
            }

            return View(student_Result);
        }

        // GET: Student_Result/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student_Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,stu_Rre")] Student_Result student_Result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student_Result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student_Result);
        }

        // GET: Student_Result/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_Result = await _context.Student_Result.FindAsync(id);
            if (student_Result == null)
            {
                return NotFound();
            }
            return View(student_Result);
        }

        // POST: Student_Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,stu_Rre")] Student_Result student_Result)
        {
            if (id != student_Result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student_Result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Student_ResultExists(student_Result.Id))
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
            return View(student_Result);
        }

        // GET: Student_Result/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_Result = await _context.Student_Result
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student_Result == null)
            {
                return NotFound();
            }

            return View(student_Result);
        }

        // POST: Student_Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student_Result = await _context.Student_Result.FindAsync(id);
            _context.Student_Result.Remove(student_Result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Student_ResultExists(int id)
        {
            return _context.Student_Result.Any(e => e.Id == id);
        }
    }
}
