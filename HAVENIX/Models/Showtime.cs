using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace havenix.Models
{
    public class Showtime
    {
        public Showtime()
        {
            Seats = new HashSet<Seat>();
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }

        [Index("IX_Showtime_Movie_StartTime", 1)]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Index("IX_Showtime_Movie_StartTime", 2)]
        public DateTime StartTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Room { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}