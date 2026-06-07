using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace havenix.Models
{
    public class Booking
    {
        public Booking()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int ShowtimeId { get; set; }

        public virtual Showtime Showtime { get; set; }

        public DateTime BookingDate { get; set; }

        [Range(0, 10000000)]
        public decimal TotalAmount { get; set; }

        [StringLength(30)]
        public string Status { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}