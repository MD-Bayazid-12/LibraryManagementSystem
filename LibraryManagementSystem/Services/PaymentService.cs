using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessPaymentAsync(int studentId, decimal amount)
        {
            var payment = await CreatePaymentAsync(studentId, amount);

            // Simulate payment processing
            await Task.Delay(1000);

            // Update payment status
            await UpdatePaymentStatusAsync(payment.PaymentId, PaymentStatus.Completed);

            // Update student's access
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                student.LastPaymentDate = DateTime.Now;
                student.PaymentDueDate = DateTime.Now.AddMonths(1);
                student.IsActive = true;
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> CheckAccessAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return false;

            return student.HasAccess;
        }

        public async Task<Payment> CreatePaymentAsync(int studentId, decimal amount)
        {
            var payment = new Payment
            {
                StudentId = studentId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                PaymentForMonth = DateTime.Now,
                Status = PaymentStatus.Pending,
                PaymentMethod = "Online"
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task UpdatePaymentStatusAsync(int paymentId, PaymentStatus status)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                payment.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}