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
    public class PRODUCTSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PRODUCTSController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PRODUCTS
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.AUTHORS).Include(p => p.Availability).Include(p => p.GENRE).Include(p => p.PRODTYPE);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PRODUCTS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pRODUCTS = await _context.Products
                .Include(p => p.AUTHORS)
                .Include(p => p.Availability)
                .Include(p => p.GENRE)
                .Include(p => p.PRODTYPE)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pRODUCTS == null)
            {
                return NotFound();
            }

            return View(pRODUCTS);
        }

        // GET: PRODUCTS/Create
        public IActionResult Create()
        {
            ViewData["AUTHOR"] = new SelectList(_context.AUTHOR, "ID", "ID");
            ViewData["AVAILABILITY"] = new SelectList(_context.AVAILABILITY, "ID", "ID");
            ViewData["GENRES"] = new SelectList(_context.GENRES, "ID", "ID");
            ViewData["PRODTYPES"] = new SelectList(_context.PRODUCTTYPES, "ID", "ID");
            return View();
        }

        // POST: PRODUCTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PRODID,TITLE,AUTHOR,PRODTYPES,GENRES,AVAILABILITY")] PRODUCTS pRODUCTS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pRODUCTS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AUTHOR"] = new SelectList(_context.AUTHOR, "ID", "ID", pRODUCTS.AUTHOR);
            ViewData["AVAILABILITY"] = new SelectList(_context.AVAILABILITY, "ID", "ID", pRODUCTS.AVAILABILITY);
            ViewData["GENRES"] = new SelectList(_context.GENRES, "ID", "ID", pRODUCTS.GENRES);
            ViewData["PRODTYPES"] = new SelectList(_context.PRODUCTTYPES, "ID", "ID", pRODUCTS.PRODTYPES);
            return View(pRODUCTS);
        }

        // GET: PRODUCTS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pRODUCTS = await _context.Products.FindAsync(id);
            if (pRODUCTS == null)
            {
                return NotFound();
            }
            ViewData["AUTHOR"] = new SelectList(_context.AUTHOR, "ID", "ID", pRODUCTS.AUTHOR);
            ViewData["AVAILABILITY"] = new SelectList(_context.AVAILABILITY, "ID", "ID", pRODUCTS.AVAILABILITY);
            ViewData["GENRES"] = new SelectList(_context.GENRES, "ID", "ID", pRODUCTS.GENRES);
            ViewData["PRODTYPES"] = new SelectList(_context.PRODUCTTYPES, "ID", "ID", pRODUCTS.PRODTYPES);
            return View(pRODUCTS);
        }

        // POST: PRODUCTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PRODID,TITLE,AUTHOR,PRODTYPES,GENRES,AVAILABILITY")] PRODUCTS pRODUCTS)
        {
            if (id != pRODUCTS.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pRODUCTS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PRODUCTSExists(pRODUCTS.ID))
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
            ViewData["AUTHOR"] = new SelectList(_context.AUTHOR, "ID", "ID", pRODUCTS.AUTHOR);
            ViewData["AVAILABILITY"] = new SelectList(_context.AVAILABILITY, "ID", "ID", pRODUCTS.AVAILABILITY);
            ViewData["GENRES"] = new SelectList(_context.GENRES, "ID", "ID", pRODUCTS.GENRES);
            ViewData["PRODTYPES"] = new SelectList(_context.PRODUCTTYPES, "ID", "ID", pRODUCTS.PRODTYPES);
            return View(pRODUCTS);
        }

        // GET: PRODUCTS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pRODUCTS = await _context.Products
                .Include(p => p.AUTHORS)
                .Include(p => p.Availability)
                .Include(p => p.GENRE)
                .Include(p => p.PRODTYPE)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pRODUCTS == null)
            {
                return NotFound();
            }

            return View(pRODUCTS);
        }

        // POST: PRODUCTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pRODUCTS = await _context.Products.FindAsync(id);
            if (pRODUCTS != null)
            {
                _context.Products.Remove(pRODUCTS);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PRODUCTSExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
