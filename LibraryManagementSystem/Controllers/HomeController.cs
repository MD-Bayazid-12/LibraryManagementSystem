using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalStudents = await _context.Students.CountAsync();
            var totalBooks = await _context.Books.CountAsync();
            var activeBookings = await _context.Bookings
                .Where(b => b.Status == Models.BookingStatus.Active)
                .CountAsync();
            var overdueBookings = await _context.Bookings
                .Where(b => b.BookingEndDate < DateTime.Now && b.Status == Models.BookingStatus.Active)
                .CountAsync();

            ViewBag.TotalStudents = totalStudents;
            ViewBag.TotalBooks = totalBooks;
            ViewBag.ActiveBookings = activeBookings;
            ViewBag.OverdueBookings = overdueBookings;

            return View();
        }
    }
}