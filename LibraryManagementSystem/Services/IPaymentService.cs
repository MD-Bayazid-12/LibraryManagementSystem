using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(int studentId, decimal amount);
        Task<bool> CheckAccessAsync(int studentId);
        Task<Payment> CreatePaymentAsync(int studentId, decimal amount);
        Task UpdatePaymentStatusAsync(int paymentId, PaymentStatus status);
    }
}