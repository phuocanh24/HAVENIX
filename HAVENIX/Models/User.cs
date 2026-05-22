using System;
using System.ComponentModel.DataAnnotations;

namespace havenix.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public string Password { get; set; }
    }
}