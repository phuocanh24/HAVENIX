namespace HAVENIX.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<havenix.Data.HavenixDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(havenix.Data.HavenixDbContext context)
        {
            // === MOVIES ===
            context.Movies.AddOrUpdate(
                m => m.Name,

                new havenix.Models.Movie
                {
                    Id = 1,
                    Name = "Mai",
                    Genre = "Tình cảm",
                    Duration = "131 phút",
                    Image = "/img/mai.jpg",
                    Description = "Phim tình cảm Việt Nam"
                },

                new havenix.Models.Movie
                {
                    Id = 2,
                    Name = "Bố Già",
                    Genre = "Gia đình",
                    Duration = "128 phút",
                    Image = "/img/bogia.jpg",
                    Description = "Câu chuyện gia đình cảm động"
                },

                new havenix.Models.Movie
                {
                    Id = 3,
                    Name = "Nhà Bà Nữ",
                    Genre = "Drama",
                    Duration = "102 phút",
                    Image = "/img/nhabanu.jpg",
                    Description = "Phim drama xã hội Việt Nam"
                },

                new havenix.Models.Movie
                {
                    Id = 4,
                    Name = "Lật Mặt 7",
                    Genre = "Hành động",
                    Duration = "130 phút",
                    Image = "/img/latmat7.jpg",
                    Description = "Phim hành động gay cấn"
                },

                new havenix.Models.Movie
                {
                    Id = 5,
                    Name = "Doraemon",
                    Genre = "Anime",
                    Duration = "109 phút",
                    Image = "/img/doraemon.jpg",
                    Description = "Phim hoạt hình tuổi thơ"
                },

                new havenix.Models.Movie
                {
                    Id = 6,
                    Name = "Thanh Gươm Diệt Quỷ",
                    Genre = "Anime",
                    Duration = "117 phút",
                    Image = "/img/thanhguom.jpg",
                    Description = "Anime hành động nổi tiếng"
                },

                new havenix.Models.Movie
                {
                    Id = 7,
                    Name = "Attack On Titan",
                    Genre = "Anime",
                    Duration = "120 phút",
                    Image = "/img/aot.jpg",
                    Description = "Cuộc chiến nhân loại và Titan"
                },

                new havenix.Models.Movie
                {
                    Id = 8,
                    Name = "Siêu Lừa Gặp Siêu Lầy",
                    Genre = "Hài",
                    Duration = "113 phút",
                    Image = "/img/sieulua.jpg",
                    Description = "Phim hài Việt Nam"
                },

                new havenix.Models.Movie
                {
                    Id = 9,
                    Name = "Thám Tử Kiên",
                    Genre = "Trinh thám",
                    Duration = "115 phút",
                    Image = "/img/thamtukien.jpg",
                    Description = "Phim điều tra phá án"
                },

                new havenix.Models.Movie
                {
                    Id = 10,
                    Name = "One Piece",
                    Genre = "Anime",
                    Duration = "120 phút",
                    Image = "/img/onepiece.jpg",
                    Description = "Hải tặc Luffy"
                }
            );

            context.Showtimes.RemoveRange(context.Showtimes.ToList());
            context.SaveChanges();

            int id = 1;

            int[][] movieHours =
            {
                new[] { 9, 12, 15, 18, 21 },
                new[] { 10, 13, 16, 19, 22 },
                new[] { 11, 14, 17, 20, 23 },
                new[] { 9, 13, 17, 21, 23 },
                new[] { 10, 12, 16, 20, 22 },
                new[] { 11, 15, 18, 21, 23 },
                new[] { 9, 14, 16, 19, 22 },
                new[] { 10, 15, 17, 20, 23 },
                new[] { 11, 13, 18, 21, 22 },
                new[] { 9, 12, 16, 19, 23 }
            };

            for (int d = 0; d < 7; d++)
            {
                DateTime day = new DateTime(2026, 5, 22).AddDays(d);

                for (int movieId = 1; movieId <= 10; movieId++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        context.Showtimes.AddOrUpdate(
                            s => s.Id,
                            new havenix.Models.Showtime
                            {
                                Id = id++,
                                MovieId = movieId,

                                StartTime = new DateTime(
                                    day.Year,
                                    day.Month,
                                    day.Day,
                                    movieHours[movieId - 1][i],
                                    0,
                                    0
                                ),

                                Room = "Phòng " + (((movieId + i) % 5) + 1)
                            }
                        );
                    }
                }
            }

            context.SaveChanges();
        }
    }
}