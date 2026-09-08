using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime BookingStartDate { get; set; }

        [Required]
        public DateTime BookingEndDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; }

        public BookingStatus Status { get; set; }

        // Navigation properties
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }

    public enum BookingStatus
    {
        Active,
        Expired,
        Returned,
        Cancelled
    }
}