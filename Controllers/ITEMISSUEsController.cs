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
    public class ITEMISSUEsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ITEMISSUEsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ITEMISSUEs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ITEMISSUES.Include(i => i.Availability).Include(i => i.PRODUCTS);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ITEMISSUEs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iTEMISSUE = await _context.ITEMISSUES
                .Include(i => i.Availability)
                .Include(i => i.PRODUCTS)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (iTEMISSUE == null)
            {
                return NotFound();
            }

            return View(iTEMISSUE);
        }

        // GET: ITEMISSUEs/Create
        public IActionResult Create()
        {
            ViewData["Status"] = new SelectList(_context.AVAILABILITY, "ID", "STATUS");
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "TITLE");
            return View();
        }

        // POST: ITEMISSUEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Prodid,Issuedto,Phone,Createddate,Duedate,Returndate,Status")] ITEMISSUE iTEMISSUE)
        {
            if (ModelState.IsValid)
            {
                iTEMISSUE.Status = 3;
                iTEMISSUE.Returndate = null;
                _context.Add(iTEMISSUE);

                var RefItem = _context.Products.FirstOrDefault(p => p.ID == iTEMISSUE.Prodid);

                if (RefItem != null)
                {
                    if (RefItem.AVAILABILITY == 2)
                    {
                        // Handle item reserved case
                        ModelState.AddModelError("", "Item is reserved");
                    }
                    else if (RefItem.AVAILABILITY == 3)
                    {
                        // Handle item already issued case
                        ModelState.AddModelError("", "Item is already issued");
                    }
                    else if (RefItem.AVAILABILITY == 1)
                    {
                        // Update availability to indicate item is now issued
                        RefItem.AVAILABILITY = 3; // Assuming 3 means "issued"
                        _context.Update(RefItem);
                    }
                    else
                    {
                        // Handle unknown status
                        ModelState.AddModelError("", "Item status is unknown or not available for issuing");
                    }
                }
                else
                {
                    // Handle case where product is not found
                    ModelState.AddModelError("", "Product not found");
                }

                // If the model state is valid, save changes
                if (ModelState.IsValid)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Status"] = new SelectList(_context.AVAILABILITY, "ID", "ID", iTEMISSUE.Status);
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "TITLE", iTEMISSUE.Prodid);
            return View(iTEMISSUE);
        }

        // GET: ITEMISSUEs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iTEMISSUE = await _context.ITEMISSUES.FindAsync(id);
            if (iTEMISSUE == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(_context.AVAILABILITY, "ID", "STATUS", iTEMISSUE.Status);
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "TITLE", iTEMISSUE.Prodid);
            return View(iTEMISSUE);
        }

        // POST: ITEMISSUEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Prodid,Issuedto,Phone,Createddate,Duedate,Returndate,Status")] ITEMISSUE iTEMISSUE)
        {
            if (id != iTEMISSUE.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iTEMISSUE);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ITEMISSUEExists(iTEMISSUE.ID))
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
            ViewData["Status"] = new SelectList(_context.AVAILABILITY, "ID", "STATUS", iTEMISSUE.Status);
            ViewData["Prodid"] = new SelectList(_context.Products, "ID", "TITLE", iTEMISSUE.Prodid);
            return View(iTEMISSUE);
        }

        // GET: ITEMISSUEs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iTEMISSUE = await _context.ITEMISSUES
                .Include(i => i.Availability)
                .Include(i => i.PRODUCTS)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (iTEMISSUE == null)
            {
                return NotFound();
            }

            return View(iTEMISSUE);
        }

        // POST: ITEMISSUEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iTEMISSUE = await _context.ITEMISSUES.FindAsync(id);
            var IssueItem = await _context.Products.FirstOrDefaultAsync(i => i.ID == iTEMISSUE.Prodid);

            if (iTEMISSUE != null)
            {
                _context.ITEMISSUES.Remove(iTEMISSUE);
                IssueItem.AVAILABILITY = 1;
                _context.Update(IssueItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRetuns(int? id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var RetunItemIssue = await _context.ITEMISSUES.FirstOrDefaultAsync(m => m.ID == id);
            var ReturnItemProduct = await _context.Products.FirstOrDefaultAsync(p => p.ID == RetunItemIssue.Prodid);

            if (RetunItemIssue == null)
            {
                return NotFound();
            }

            // Toggle the status based on the current state
            if (RetunItemIssue.Status == 3)
            {

                RetunItemIssue.Returndate = DateTime.Now;
                RetunItemIssue.Status = 4;
                ReturnItemProduct.AVAILABILITY = 1;
                _context.Update(ReturnItemProduct);
            }

            _context.Update(RetunItemIssue);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = id });

        }



        private bool ITEMISSUEExists(int id)
        {
            return _context.ITEMISSUES.Any(e => e.ID == id);
        }
    }
}
