using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sydenham_Library_System.Data;
using Sydenham_Library_System.Models;

namespace Sydenham_Library_System.Controllers
{
    public class RESERVATIONSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RESERVATIONSController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RESERVATIONS
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RESERVATIONS.Include(r => r.PRODUCTS);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RESERVATIONS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rESERVATIONS = await _context.RESERVATIONS
                .Include(r => r.PRODUCTS)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rESERVATIONS == null)
            {
                return NotFound();
            }

            return View(rESERVATIONS);
        }

        // GET: RESERVATIONS/Create
        public IActionResult Create()
        {
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "ID");
            return View();
        }

        // POST: RESERVATIONS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prodid,Reservedby,Reservedbyphone,Createddate")] RESERVATIONS rESERVATIONS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rESERVATIONS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "ID", rESERVATIONS.Prodid);
            return View(rESERVATIONS);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(int Prodid, string Reservedby, string Reservedbyphone)
        {
            try
            {
                DateTime createdDate = DateTime.Now;
                // Create a new reservation record
                RESERVATIONS newReservation = new RESERVATIONS
                {
                    Prodid = Prodid,
                    Reservedby = Reservedby,
                    Reservedbyphone = Reservedbyphone,
                    Createddate = createdDate,
                    Duedate = createdDate.AddDays(3)
                };

                _context.RESERVATIONS.Add(newReservation);

                //// Update the product availability status
                var product = _context.Products.Find(Prodid);
                if (product != null)
                {
                    product.AVAILABILITY = 2; // Assuming 2 represents "Reserved" in your Availability table
                    _context.Products.Update(product);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: RESERVATIONS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rESERVATIONS = await _context.RESERVATIONS.FindAsync(id);
            if (rESERVATIONS == null)
            {
                return NotFound();
            }
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "ID", rESERVATIONS.Prodid);
            return View(rESERVATIONS);
        }

        // POST: RESERVATIONS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prodid,Reservedby,Reservedbyphone,Createddate")] RESERVATIONS rESERVATIONS)
        {
            if (id != rESERVATIONS.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rESERVATIONS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RESERVATIONSExists(rESERVATIONS.Id))
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
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "ID", rESERVATIONS.Prodid);
            return View(rESERVATIONS);
        }

        // GET: RESERVATIONS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rESERVATIONS = await _context.RESERVATIONS
                .Include(r => r.PRODUCTS)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rESERVATIONS == null)
            {
                return NotFound();
            }

            return View(rESERVATIONS);
        }

        // POST: RESERVATIONS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rESERVATIONS = await _context.RESERVATIONS.FindAsync(id);
            if (rESERVATIONS != null)
            {
                _context.RESERVATIONS.Remove(rESERVATIONS);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RESERVATIONSExists(int id)
        {
            return _context.RESERVATIONS.Any(e => e.Id == id);
        }
    }
}
