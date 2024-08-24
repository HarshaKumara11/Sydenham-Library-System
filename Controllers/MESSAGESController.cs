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
    public class MESSAGESController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MESSAGESController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MESSAGES
        public async Task<IActionResult> Index()
        {
            return View(await _context.MESSAGES.ToListAsync());
        }

        // GET: MESSAGES/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mESSAGES = await _context.MESSAGES
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mESSAGES == null)
            {
                return NotFound();
            }

            return View(mESSAGES);
        }

        // GET: MESSAGES/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MESSAGES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,From,Phone,Subject,Msgbody,Status,Createddate,Readdate")] MESSAGES mESSAGES)
        {
            if (ModelState.IsValid)
            {
                mESSAGES.Createddate = DateTime.Now;
                _context.Add(mESSAGES);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Your message recived. We will contact you soon if required";
                return RedirectToAction("Create");
            }
            return View(mESSAGES);
        }

        // GET: MESSAGES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mESSAGES = await _context.MESSAGES.FindAsync(id);
            if (mESSAGES == null)
            {
                return NotFound();
            }
            return View(mESSAGES);
        }

        // POST: MESSAGES/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,From,Phone,Subject,Msgbody,Status,Createddate,Readdate")] MESSAGES mESSAGES)
        {
            if (id != mESSAGES.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mESSAGES);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MESSAGESExists(mESSAGES.Id))
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
            return View(mESSAGES);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadUnread(int? id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var msg = await _context.MESSAGES.FirstOrDefaultAsync(m => m.Id == id);

            if (msg == null)
            {
                return NotFound();
            }

            // Toggle the status based on the current state
            if (msg.Status == "UNREAD")
            {
                msg.Status = "READ";
                msg.Readdate = DateTime.Now;
            }
            else if (msg.Status == "READ")
            {
                msg.Status = "UNREAD";
            }
            _context.Update(msg);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = id });

        }

        // GET: MESSAGES/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mESSAGES = await _context.MESSAGES
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mESSAGES == null)
            {
                return NotFound();
            }

            return View(mESSAGES);
        }

        // POST: MESSAGES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mESSAGES = await _context.MESSAGES.FindAsync(id);
            if (mESSAGES != null)
            {
                _context.MESSAGES.Remove(mESSAGES);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MESSAGESExists(int id)
        {
            return _context.MESSAGES.Any(e => e.Id == id);
        }
    }
}
