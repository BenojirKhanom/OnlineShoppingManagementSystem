using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreProject.Models;
using Newtonsoft.Json;

namespace AspNetCoreProject.Controllers
{
    public class DeliveryBoysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryBoysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryBoys
       
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryBoys.ToListAsync());
        }

    
        public ActionResult Sample()
        {
            return View();
        }

        [HttpPost]
        public JsonResult insertDeliveryBoyInfo(string DeliveryBoyJson)
        {
            var js = new JsonSerializer();
            DeliveryBoy[] Delivery = (DeliveryBoy[])JsonConvert.DeserializeObject(DeliveryBoyJson, typeof(DeliveryBoy[]));
            try
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var v in Delivery)
                        {
                            v.DeliveryBoyId = 0;
                            _context.DeliveryBoys.Add(v);
                            _context.SaveChanges();
                        }


                        dbContextTransaction.Commit();
                        return Json("Data are inserted in DeliveryBoy List");

                    }
                    catch (Exception ex)
                    {
                        string k = ex.Message;
                        dbContextTransaction.Rollback();
                        return Json("There is a Problem arise");
                    }

                }
            }
            catch (Exception ex)
            {
                string k = ex.Message;
            }
            return Json("There is a Problem arise");
        }

        // GET: DeliveryBoys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryBoy = await _context.DeliveryBoys
                .FirstOrDefaultAsync(m => m.DeliveryBoyId == id);
            if (deliveryBoy == null)
            {
                return NotFound();
            }

            return View(deliveryBoy);
        }

        // GET: DeliveryBoys/Create
       
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryBoys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryBoyId,DeliveryBoyName")] DeliveryBoy deliveryBoy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryBoy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryBoy);
        }

        // GET: DeliveryBoys/Edit/5
    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryBoy = await _context.DeliveryBoys.FindAsync(id);
            if (deliveryBoy == null)
            {
                return NotFound();
            }
            return View(deliveryBoy);
        }

        // POST: DeliveryBoys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryBoyId,DeliveryBoyName")] DeliveryBoy deliveryBoy)
        {
            if (id != deliveryBoy.DeliveryBoyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryBoy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryBoyExists(deliveryBoy.DeliveryBoyId))
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
            return View(deliveryBoy);
        }

        // GET: DeliveryBoys/Delete/5
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryBoy = await _context.DeliveryBoys
                .FirstOrDefaultAsync(m => m.DeliveryBoyId == id);
            if (deliveryBoy == null)
            {
                return NotFound();
            }

            return View(deliveryBoy);
        }

        // POST: DeliveryBoys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryBoy = await _context.DeliveryBoys.FindAsync(id);
            _context.DeliveryBoys.Remove(deliveryBoy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryBoyExists(int id)
        {
            return _context.DeliveryBoys.Any(e => e.DeliveryBoyId == id);
        }
    }
}
