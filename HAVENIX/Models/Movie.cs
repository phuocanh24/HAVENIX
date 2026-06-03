using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace havenix.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public string Duration { get; set; }

        public string ReleaseDate { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

        public string Status { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}