using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(50)]
        public string ISBN { get; set; }

        public string Genre { get; set; }

        [Required]
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public string Description { get; set; }

        public string BookImage { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        // Navigation property
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}