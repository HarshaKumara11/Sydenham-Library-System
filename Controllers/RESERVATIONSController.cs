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
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "STATUS");
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
            var ReservedItem = await _context.Products.FirstOrDefaultAsync(p => p.ID == rESERVATIONS.Prodid);
            if (rESERVATIONS != null)
            {
                _context.RESERVATIONS.Remove(rESERVATIONS);
                ReservedItem.AVAILABILITY = 1;
                _context.Update(ReservedItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateIssue(int id)
        {
            var reservation = _context.RESERVATIONS
                .Include(r => r.PRODUCTS)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            // Create a new Issue entity based on the reservation details
            DateTime TodayDate = DateTime.Now;

            // Adjust DueDate to be 14 days from today with time set to 11:49 PM
            DateTime dueDate = TodayDate.AddDays(14).Date.Add(new TimeSpan(23, 49, 0));

            var issue = new ITEMISSUE
            {
                Prodid = reservation.Prodid,
                Issuedto = reservation.Reservedby,
                Phone = reservation.Reservedbyphone,
                Createddate = TodayDate,
                Duedate = dueDate,
                Status = 3
                //ReservedBy = reservation.Reservedby,
                //ReservedByPhone = reservation.Reservedbyphone,
                //ProductTitle = reservation.PRODUCTS.TITLE,
                //// Set other issue properties as needed
            };

            // Add the new issue to the database
            _context.ITEMISSUES.Add(issue);
            _context.SaveChanges();

            var ReservedItem = _context.Products.FirstOrDefault(p => p.ID == reservation.Prodid);
            ReservedItem.AVAILABILITY = 3; 
            _context.Update(ReservedItem);
            _context.SaveChanges();

            // Remove the reservation from the database
            _context.RESERVATIONS.Remove(reservation);
            _context.SaveChanges();

            // Redirect to the newly created issue's details page
            return RedirectToAction("Details", "ITEMISSUES", new { id = issue.ID });
        }

        private bool RESERVATIONSExists(int id)
        {
            return _context.RESERVATIONS.Any(e => e.Id == id);
        }
    }
}
