using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace havenix.Models
{
    public class Showtime
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public DateTime StartTime { get; set; }

        public string Room { get; set; }
    }
}