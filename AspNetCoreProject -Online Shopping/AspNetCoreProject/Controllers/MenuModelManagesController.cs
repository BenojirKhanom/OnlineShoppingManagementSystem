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
    public class MenuModelManagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuModelManagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuModelManages
        public IActionResult Index()
        {
            IList<MenuModelManage> menuModelManages = new List<MenuModelManage>();


            var MenuListWithRollDisplay = from mm in _context.MenuModel
                                          join mhm in _context.MenuHelperModel on mm.MenuHelperModelId equals mhm.Id
                                          join ro in _context.Roles.ToList() on mm.RollId equals ro.Id
                                          join mmm in _context.MenuModelManage.ToList() on mm.Id equals mmm.MenuModelId
                                          select new MenuModelManage()
                                          {

                                              Id = mmm.Id,
                                              Con_Act_Roll = mhm.Con_Act_Name + "_" + ro.Name,
                                              Delete = mmm.Delete,
                                              Insert = mmm.Insert,
                                              Update = mmm.Update,
                                              Retrive = mmm.Retrive

                                          };









            return View(MenuListWithRollDisplay);
        }

        // GET: MenuModelManages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModelManage = await _context.MenuModelManage
                .Include(m => m.MenuModel)
                .FirstOrDefaultAsync(m => m.MenuModelId == id);
            if (menuModelManage == null)
            {
                return NotFound();
            }

            return View(menuModelManage);
        }

        // GET: MenuModelManages/Create
        public IActionResult Create(string RollName)
        {
            IList<MenuModelManage> menuModelManages = new List<MenuModelManage>();

            var MenuListWithRollDisplay = from mm in _context.MenuModel
                                          join mhm in _context.MenuHelperModel on mm.MenuHelperModelId equals mhm.Id
                                          join ro in _context.Roles.ToList() on mm.RollId equals ro.Id

                                          select new MenuModel()
                                          {

                                              Id = mm.Id,
                                              MenuHelperModelId = mhm.Id,
                                              MenuHelperModelIdText = mhm.Con_Act_Name,
                                              RollId = ro.Id,
                                              RollIdText = ro.Name
                                          };

            foreach (var mlwrd in MenuListWithRollDisplay)
            {
                menuModelManages.Add(new MenuModelManage()
                {
                    Con_Act_Roll = mlwrd.MenuHelperModelIdText + "_" + mlwrd.RollIdText,
                    Delete = false,
                    Insert = false,
                    Update = false,
                    MenuModelId = mlwrd.Id,
                    Retrive = false
                });
            }

            ViewData["MenuModelId"] = new SelectList(_context.MenuModel, "Id", "Id");
            return View(menuModelManages);
        }

        // POST: MenuModelManages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IList<MenuModelManage> menuModelManages)
        {
            var prevPermisonDel = from om in _context.MenuModelManage
                                  join m in menuModelManages
                               on om.MenuModelId equals m.MenuModelId
                                  select om;

            foreach (var deleteEntity in prevPermisonDel)
            {
                _context.MenuModelManage.Remove(deleteEntity);
            }

            if (ModelState.IsValid)
            {
                _context.AddRange(menuModelManages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(menuModelManages);

        }

        // GET: MenuModelManages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModelManage = await _context.MenuModelManage.FindAsync(id);
            if (menuModelManage == null)
            {
                return NotFound();
            }
            ViewData["MenuModelId"] = new SelectList(_context.MenuModel, "Id", "Id", menuModelManage.MenuModelId);
            return View(menuModelManage);
        }

        // POST: MenuModelManages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuModelId,Insert,Update,Delete")] MenuModelManage menuModelManage)
        {
            if (id != menuModelManage.MenuModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuModelManage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuModelManageExists(menuModelManage.MenuModelId))
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
            ViewData["MenuModelId"] = new SelectList(_context.MenuModel, "Id", "Id", menuModelManage.MenuModelId);
            return View(menuModelManage);
        }

        // GET: MenuModelManages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuModelManage = await _context.MenuModelManage
                .Include(m => m.MenuModel)
                .FirstOrDefaultAsync(m => m.MenuModelId == id);
            if (menuModelManage == null)
            {
                return NotFound();
            }

            return View(menuModelManage);
        }

        // POST: MenuModelManages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuModelManage = await _context.MenuModelManage.FindAsync(id);
            _context.MenuModelManage.Remove(menuModelManage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuModelManageExists(int id)
        {
            return _context.MenuModelManage.Any(e => e.MenuModelId == id);
        }
    }
}
