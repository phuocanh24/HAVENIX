using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace havenix.Models
{
    public class Movie
    {
        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(80)]
        public string Genre { get; set; }

        // Lưu số phút, ví dụ: 131
        [Range(1, 500)]
        public int Duration { get; set; }

        // Lưu đúng kiểu ngày tháng để lọc / sắp xếp / truy vấn
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        // Chuẩn hóa trạng thái
        [Required]
        [StringLength(30)]
        [RegularExpression("Đang chiếu|Sắp chiếu|Ngừng chiếu",
            ErrorMessage = "Trạng thái phim không hợp lệ.")]
        public string Status { get; set; }

        [NotMapped]
        public string DurationText
        {
            get { return Duration + " phút"; }
        }

        [NotMapped]
        public string ReleaseDateText
        {
            get { return ReleaseDate.ToString("dd/MM/yyyy"); }
        }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}