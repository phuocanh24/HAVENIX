using System.ComponentModel.DataAnnotations;

namespace havenix.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string SeatNumber { get; set; }

        public bool IsBooked { get; set; }

        public int ShowtimeId { get; set; }

        public virtual Showtime Showtime { get; set; }
    }
}