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
    public class MenuModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuModels
        public async Task<IActionResult> Index()
        {

          var MenuListDisplay =    from mm in _context.MenuModel
            join mhm in _context.MenuHelperModel on mm.MenuHelperModelId equals mhm.Id
            join ro in _context.Roles.ToList() on mm.RollId equals ro.Id

            select new MenuModel() {

                 Id = mm.Id , MenuHelperModelId = mhm.Id , MenuHelperModelIdText = mhm.Con_Act_Name , RollId = ro.Id , RollIdText = ro.Name
            } ;
            return View(MenuListDisplay);
        }

        // GET: MenuModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.MenuModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
        }

        // GET: MenuModels/Create
        public IActionResult Create()
        {

            ViewData["MenuHelper"] = _context.MenuHelperModel.ToList();
            ViewData["RollList"] = _context.Roles.ToList();
            return View();
        }

        // POST: MenuModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuHelperModelId,RollId")] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuModel);
        }

        // GET: MenuModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.MenuModel.FindAsync(id);
            if (menuModel == null)
            {
                return NotFound();
            }
            return View(menuModel);
        }

        // POST: MenuModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuHelperModelId,RollId")] MenuModel menuModel)
        {
            if (id != menuModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuModelExists(menuModel.Id))
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
            return View(menuModel);
        }

        // GET: MenuModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModel = await _context.MenuModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
        }

        // POST: MenuModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuModel = await _context.MenuModel.FindAsync(id);
            _context.MenuModel.Remove(menuModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuModelExists(int id)
        {
            return _context.MenuModel.Any(e => e.Id == id);
        }
    }
}
