using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymentService _paymentService;

        public BookingController(ApplicationDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Student)
                .Include(b => b.Book)
                .ToListAsync();
            return View(bookings);
        }

        // GET: Booking/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Students = await _context.Students.Where(s => s.IsActive).ToListAsync();
            ViewBag.Books = await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,BookId,BookingStartDate,BookingEndDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Check if student has access
                var hasAccess = await _paymentService.CheckAccessAsync(booking.StudentId);
                if (!hasAccess)
                {
                    ModelState.AddModelError("", "Student does not have active access. Please make a payment first.");
                    ViewBag.Students = await _context.Students.Where(s => s.IsActive).ToListAsync();
                    ViewBag.Books = await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();
                    return View(booking);
                }

                // Check if book is available
                var book = await _context.Books.FindAsync(booking.BookId);
                if (book == null || book.AvailableCopies <= 0)
                {
                    ModelState.AddModelError("", "Book is not available for booking.");
                    ViewBag.Students = await _context.Students.Where(s => s.IsActive).ToListAsync();
                    ViewBag.Books = await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();
                    return View(booking);
                }

                // Check if student already has active bookings
                var activeBookings = await _context.Bookings
                    .Where(b => b.StudentId == booking.StudentId && !b.IsReturned && b.Status == BookingStatus.Active)
                    .CountAsync();

                if (activeBookings >= 3)
                {
                    ModelState.AddModelError("", "Student already has maximum active bookings (3).");
                    ViewBag.Students = await _context.Students.Where(s => s.IsActive).ToListAsync();
                    ViewBag.Books = await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();
                    return View(booking);
                }

                booking.BookingDate = DateTime.Now;
                booking.Status = BookingStatus.Active;
                booking.IsReturned = false;

                // Reduce available copies
                book.AvailableCopies--;

                _context.Add(booking);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Booking created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = await _context.Students.Where(s => s.IsActive).ToListAsync();
            ViewBag.Books = await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();
            return View(booking);
        }

        // POST: Booking/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.ReturnDate = DateTime.Now;
            booking.IsReturned = true;
            booking.Status = BookingStatus.Returned;

            // Increase available copies
            if (booking.Book != null)
            {
                booking.Book.AvailableCopies++;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Book returned successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Student)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking != null)
            {
                if (!booking.IsReturned)
                {
                    // Increase available copies if not returned
                    if (booking.Book != null)
                    {
                        booking.Book.AvailableCopies++;
                    }
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Booking deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}