using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sydenham_Library_System.Data;
using Sydenham_Library_System.Models;
using System.Diagnostics;

namespace Sydenham_Library_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchBooks(string searchBook)
        {
            try
            {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
                var books = _context.Products
                            .Include(b => b.AUTHORS)
                            .Include(b => b.PRODTYPE)
                            .Include(b => b.GENRE)
                            .Include(b => b.Availability)
                            .Where(b => b.TITLE.Contains(searchBook) ||
                                        b.AUTHORS.AUTHORNAME.Contains(searchBook) ||
                                        b.PRODTYPE.PRODTYPE.Contains(searchBook) ||
                                        b.GENRE.GENRENAME.Contains(searchBook))
                            .Select(b => new
                            {
                                ItemId = b.ID,
                                Title = b.TITLE,
                                Author = b.AUTHORS.AUTHORNAME,
                                ProductType = b.PRODTYPE.PRODTYPE,
                                Genre = b.GENRE.GENRENAME,
                                Availability = b.Availability.STATUS
                            })
                            .ToList();
            #pragma warning restore CS8602 // Dereference of a possibly null reference.

                return Json(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for books");
                return StatusCode(500);
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
