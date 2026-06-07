using System.ComponentModel.DataAnnotations;

namespace havenix.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }

        public int SeatId { get; set; }

        public virtual Seat Seat { get; set; }

        [Required]
        [StringLength(50)]
        public string TicketCode { get; set; }

        [Range(0, 10000000)]
        public decimal Price { get; set; }
    }
}