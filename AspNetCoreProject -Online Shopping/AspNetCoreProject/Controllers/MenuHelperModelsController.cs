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
    public class MenuHelperModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuHelperModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuHelperModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuHelperModel.ToListAsync());
        }

        // GET: MenuHelperModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuHelperModel = await _context.MenuHelperModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuHelperModel == null)
            {
                return NotFound();
            }

            return View(menuHelperModel);
        }

        // GET: MenuHelperModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuHelperModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ControllerName,ActionName")] MenuHelperModel menuHelperModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuHelperModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuHelperModel);
        }

        // GET: MenuHelperModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuHelperModel = await _context.MenuHelperModel.FindAsync(id);
            if (menuHelperModel == null)
            {
                return NotFound();
            }
            return View(menuHelperModel);
        }

        // POST: MenuHelperModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ControllerName,ActionName")] MenuHelperModel menuHelperModel)
        {
            if (id != menuHelperModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuHelperModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuHelperModelExists(menuHelperModel.Id))
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
            return View(menuHelperModel);
        }

        // GET: MenuHelperModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuHelperModel = await _context.MenuHelperModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuHelperModel == null)
            {
                return NotFound();
            }

            return View(menuHelperModel);
        }

        // POST: MenuHelperModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuHelperModel = await _context.MenuHelperModel.FindAsync(id);
            _context.MenuHelperModel.Remove(menuHelperModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuHelperModelExists(int id)
        {
            return _context.MenuHelperModel.Any(e => e.Id == id);
        }
    }
}
