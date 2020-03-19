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
    public class OrderDtlsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDtlsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDtls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderDtls.Include(o => o.Employee).Include(o => o.Order).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderDtls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDtl = await _context.OrderDtls
                .Include(o => o.Employee)
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDtlId == id);
            if (orderDtl == null)
            {
                return NotFound();
            }

            return View(orderDtl);
        }

        // GET: OrderDtls/Create
        public IActionResult Create()
        {
            ViewData["EmpID"] = new SelectList(_context.Employees, "EmpID", "Country");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Orderdate");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: OrderDtls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDtlId,OrderId,ProductId,EmpID,Quentity,TotalCost")] OrderDtl orderDtl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDtl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpID"] = new SelectList(_context.Employees, "EmpID", "Country", orderDtl.EmpID);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Orderdate", orderDtl.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderDtl.ProductId);
            return View(orderDtl);
        }

        // GET: OrderDtls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDtl = await _context.OrderDtls.FindAsync(id);
            if (orderDtl == null)
            {
                return NotFound();
            }
            ViewData["EmpID"] = new SelectList(_context.Employees, "EmpID", "Country", orderDtl.EmpID);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Orderdate", orderDtl.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderDtl.ProductId);
            return View(orderDtl);
        }

        // POST: OrderDtls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDtlId,OrderId,ProductId,EmpID,Quentity,TotalCost")] OrderDtl orderDtl)
        {
            if (id != orderDtl.OrderDtlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDtl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDtlExists(orderDtl.OrderDtlId))
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
            ViewData["EmpID"] = new SelectList(_context.Employees, "EmpID", "Country", orderDtl.EmpID);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Orderdate", orderDtl.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderDtl.ProductId);
            return View(orderDtl);
        }

        // GET: OrderDtls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDtl = await _context.OrderDtls
                .Include(o => o.Employee)
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDtlId == id);
            if (orderDtl == null)
            {
                return NotFound();
            }

            return View(orderDtl);
        }

        // POST: OrderDtls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDtl = await _context.OrderDtls.FindAsync(id);
            _context.OrderDtls.Remove(orderDtl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDtlExists(int id)
        {
            return _context.OrderDtls.Any(e => e.OrderDtlId == id);
        }
    }
}
