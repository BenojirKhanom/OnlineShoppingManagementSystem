using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreProject.Controllers
{
    public class CustomerReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment _env;

        public CustomerReviewsController(ApplicationDbContext context , IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: CustomerReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerReviews.Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerReview = await _context.CustomerReviews
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (customerReview == null)
            {
                return NotFound();
            }

            return View(customerReview);
        }

        // GET: CustomerReviews/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address");
            return View();
        }

        // POST: CustomerReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,CustomerId,Opinion,ImageFile")] CustomerReview customerReview ,IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                string imagePath = _env.WebRootPath + "\\Images\\" + formFile.FileName;
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                customerReview.ImageFile = "~/Images/" + formFile.FileName;
                _context.Add(customerReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", customerReview.CustomerId);
            return View(customerReview);
        }

        // GET: CustomerReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var customerReview = await _context.CustomerReviews.FindAsync(id);
            if (customerReview == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", customerReview.CustomerId);
            return View(customerReview);
        }

        // POST: CustomerReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,CustomerId,Opinion,ImageFile")] CustomerReview customerReview, IFormFile formFile)
        {
            if (id != customerReview.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile != null)
                    {

                        string imagePath = _env.WebRootPath + "\\Images\\" + formFile.FileName;
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }

                        customerReview.ImageFile = "~/Images/" + formFile.FileName;


                    }


                    _context.Update(customerReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerReviewExists(customerReview.ReviewId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", customerReview.CustomerId);
            return View(customerReview);
        }

        // GET: CustomerReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerReview = await _context.CustomerReviews
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (customerReview == null)
            {
                return NotFound();
            }

            return View(customerReview);
        }

        // POST: CustomerReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerReview = await _context.CustomerReviews.FindAsync(id);
            _context.CustomerReviews.Remove(customerReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerReviewExists(int id)
        {
            return _context.CustomerReviews.Any(e => e.ReviewId == id);
        }
    }
}
