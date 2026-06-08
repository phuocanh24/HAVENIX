namespace HAVENIX.Migrations
{
    using havenix.Models;
    using System;
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
            context.Showtimes.RemoveRange(context.Showtimes.ToList());
            context.SaveChanges();

            var movies = new[]
            {
                new Movie { Name = "MAI", Genre = "Tình cảm", Duration = 131, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Tiếng Việt", Image = "/Content/Img/maipos.jpg", Description = "Phim tình cảm Việt Nam" },
                new Movie { Name = "BỐ GIÀ", Genre = "Gia đình", Duration = 128, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Tiếng Việt", Image = "/Content/Img/bogiaposter.jpg", Description = "Câu chuyện gia đình cảm động" },
                new Movie { Name = "Nhà Bà Nữ", Genre = "Drama", Duration = 102, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Tiếng Việt", Image = "/Content/Img/nhabanuposter.jpg", Description = "Phim drama xã hội Việt Nam" },
                new Movie { Name = "LẬT MẶT 7: MỘT ĐIỀU ƯỚC", Genre = "Gia đình", Duration = 130, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Tiếng Việt", Image = "/Content/Img/latmat7poster.jpg", Description = "Phim tình cảm gia đình" },
                new Movie { Name = "DORAEMON: KHÁM PHÁ ĐÁY ĐẠI DƯƠNG", Genre = "Anime", Duration = 109, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Lồng tiếng", Image = "/Content/Img/doraemonposter.jpg", Description = "Phim hoạt hình tuổi thơ" },
                new Movie { Name = "THANH GƯƠM DIỆT QUỶ: VÔ HẠN THÀNH", Genre = "Anime", Duration = 117, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Phụ đề", Image = "/Content/Img/tgdqposter.jpg", Description = "Anime hành động nổi tiếng" },
                new Movie { Name = "ATTACK ON TITAN 4", Genre = "Anime", Duration = 120, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Phụ đề", Image = "/Content/Img/attachposter.jpg", Description = "Cuộc chiến nhân loại và Titan" },
                new Movie { Name = "SIÊU LỪA GẶP SIÊU LẦY", Genre = "Hài", Duration = 113, ReleaseDate = new DateTime(2026, 5, 22), Status = "Đang chiếu", Language = "Tiếng Việt", Image = "/Content/Img/sieuluaposter.jpg", Description = "Phim hài Việt Nam" },
                new Movie { Name = "THÁM TỬ KIÊN: KỲ ÁN KHÔNG ĐẦU", Genre = "Trinh thám", Duration = 115, ReleaseDate = new DateTime(2026, 5, 22), Status = "Sắp chiếu", Language = "Tiếng Việt", Image = "/Content/Img/thamtukienposter.jpg", Description = "Phim điều tra phá án" },
                new Movie { Name = "LẬT MẶT 8: VÒNG TAY NẮNG", Genre = "Gia đình", Duration = 120, ReleaseDate = new DateTime(2026, 5, 22), Status = "Sắp chiếu", Language = "Tiếng Việt", Image = "/Content/Img/latmat8poster.jpg", Description = "Phim tình cảm gia đình" },
                new Movie { Name = "QUỶ ĂN TẠNG 2", Genre = "Kinh dị", Duration = 120, ReleaseDate = new DateTime(2026, 5, 22), Status = "Sắp chiếu", Language = "Phụ đề", Image = "/Content/Img/quyposter.jpg", Description = "Phim kinh dị" },
                new Movie { Name = "THỎ ƠI", Genre = "Hài, kinh dị", Duration = 120, ReleaseDate = new DateTime(2026, 5, 22), Status = "Sắp chiếu", Language = "Tiếng Việt", Image = "/Content/Img/thooiposter.jpg", Description = "Phim hài, kinh dị" }
            };

            context.Movies.AddOrUpdate(x => x.Name, movies);
            context.SaveChanges();

            var dbMovies = context.Movies
                .OrderBy(x => x.Id)
                .ToList();

            int[][] movieHours =
            {
                new[] { 8, 10, 12, 14, 16, 18, 20, 22 },
                new[] { 9, 11, 13, 15, 17, 19, 21, 23 },
                new[] { 8, 11, 14, 16, 18, 20, 22, 23 },
                new[] { 9, 10, 12, 15, 17, 19, 21, 22 },
                new[] { 8, 10, 13, 15, 18, 20, 21, 23 },
                new[] { 9, 11, 14, 16, 18, 19, 21, 23 },
                new[] { 8, 10, 12, 14, 17, 19, 20, 22 },
                new[] { 9, 11, 13, 15, 16, 18, 21, 23 },
                new[] { 8, 10, 12, 15, 17, 19, 21, 22 },
                new[] { 9, 11, 14, 16, 18, 20, 22, 23 },
                new[] { 8, 10, 13, 15, 17, 19, 21, 23 },
                new[] { 9, 12, 14, 16, 18, 20, 22, 23 }
            };

            for (int d = 0; d < 7; d++)
            {
                DateTime day = DateTime.Today.AddDays(d);

                for (int m = 0; m < dbMovies.Count; m++)
                {
                    for (int i = 0; i < movieHours[m % movieHours.Length].Length; i++)
                    {
                        context.Showtimes.Add(new Showtime
                        {
                            MovieId = dbMovies[m].Id,
                            StartTime = new DateTime(
                                day.Year,
                                day.Month,
                                day.Day,
                                movieHours[m % movieHours.Length][i],
                                0,
                                0),
                            Room = "Phòng " + (((m + 1 + i) % 5) + 1)
                        });
                    }
                }
            }

            context.SaveChanges();
        }
    }
}