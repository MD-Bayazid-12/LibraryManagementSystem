using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string Phone { get; set; }

        public string Address { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastPaymentDate { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public bool HasAccess => IsActive && PaymentDueDate > DateTime.Now;

        // Navigation properties
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}