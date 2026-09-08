using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public DateTime? PaymentForMonth { get; set; }

        public string PaymentMethod { get; set; }

        public string TransactionId { get; set; }

        public PaymentStatus Status { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}