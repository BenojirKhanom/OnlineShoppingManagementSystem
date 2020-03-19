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
    public class ShippingInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShippingInfoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShippingInfoes.Include(s => s.DeliveryBoy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShippingInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingInfo = await _context.ShippingInfoes
                .Include(s => s.DeliveryBoy)
                .FirstOrDefaultAsync(m => m.ShippingId == id);
            if (shippingInfo == null)
            {
                return NotFound();
            }

            return View(shippingInfo);
        }

        // GET: ShippingInfoes/Create
        public IActionResult Create()
        {
            ViewData["DeliveryBoyId"] = new SelectList(_context.DeliveryBoys, "DeliveryBoyId", "DeliveryBoyName");
            return View();
        }

        // POST: ShippingInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShippingId,DeliveryBoyId,ShippingCost,ShippingDate")] ShippingInfo shippingInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryBoyId"] = new SelectList(_context.DeliveryBoys, "DeliveryBoyId", "DeliveryBoyName", shippingInfo.DeliveryBoyId);
            return View(shippingInfo);
        }

        // GET: ShippingInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingInfo = await _context.ShippingInfoes.FindAsync(id);
            if (shippingInfo == null)
            {
                return NotFound();
            }
            ViewData["DeliveryBoyId"] = new SelectList(_context.DeliveryBoys, "DeliveryBoyId", "DeliveryBoyName", shippingInfo.DeliveryBoyId);
            return View(shippingInfo);
        }

        // POST: ShippingInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShippingId,DeliveryBoyId,ShippingCost,ShippingDate")] ShippingInfo shippingInfo)
        {
            if (id != shippingInfo.ShippingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingInfoExists(shippingInfo.ShippingId))
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
            ViewData["DeliveryBoyId"] = new SelectList(_context.DeliveryBoys, "DeliveryBoyId", "DeliveryBoyName", shippingInfo.DeliveryBoyId);
            return View(shippingInfo);
        }

        // GET: ShippingInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingInfo = await _context.ShippingInfoes
                .Include(s => s.DeliveryBoy)
                .FirstOrDefaultAsync(m => m.ShippingId == id);
            if (shippingInfo == null)
            {
                return NotFound();
            }

            return View(shippingInfo);
        }

        // POST: ShippingInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingInfo = await _context.ShippingInfoes.FindAsync(id);
            _context.ShippingInfoes.Remove(shippingInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingInfoExists(int id)
        {
            return _context.ShippingInfoes.Any(e => e.ShippingId == id);
        }
    }
}
