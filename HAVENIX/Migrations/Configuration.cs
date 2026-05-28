namespace HAVENIX.Migrations
{
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
            // XÓA SHOWTIME CŨ
            context.Showtimes.RemoveRange(context.Showtimes.ToList());
            context.SaveChanges();

            // MOVIES
            var movies = new[]
            {
                new havenix.Models.Movie { Name = "MAI", Genre = "Tình cảm", Duration = "131 phút", Image = "/Content/Img/maipos.jpg", Description = "Phim tình cảm Việt Nam" },

                new havenix.Models.Movie { Name = "BỐ GIÀ", Genre = "Gia đình", Duration = "128 phút", Image = "/Content/Img/bogiaposter.jpg", Description = "Câu chuyện gia đình cảm động" },

                new havenix.Models.Movie { Name = "Nhà Bà Nữ", Genre = "Drama", Duration = "102 phút", Image = "/Content/Img/nhabanuposter.jpg", Description = "Phim drama xã hội Việt Nam" },

                new havenix.Models.Movie { Name = "LẬT MẶT 7: MỘT ĐIỀU ƯỚC", Genre = "Hành động", Duration = "130 phút", Image = "/Content/Img/latmat7poster.jpg", Description = "Phim tình cảm gia đình" },

                new havenix.Models.Movie { Name = "DORAEMON: KHÁM PHÁ ĐÁY ĐẠI DƯƠNG", Genre = "Anime", Duration = "109 phút", Image = "/Content/Img/doraemonposter.jpg", Description = "Phim hoạt hình tuổi thơ" },

                new havenix.Models.Movie { Name = "THANH GƯƠM DIỆT QUỶ: VÔ HẠN THÀNH", Genre = "Anime", Duration = "117 phút", Image = "/Content/Img/tgdqposter.jpg", Description = "Anime hành động nổi tiếng" },

                new havenix.Models.Movie { Name = "ATTACK ON TITAN 4", Genre = "Anime", Duration = "120 phút", Image = "/Content/Img/attachposter.jpg", Description = "Cuộc chiến nhân loại và Titan" },

                new havenix.Models.Movie { Name = "SIÊU LỪA GẶP SIÊU LẦY", Genre = "Hài", Duration = "113 phút", Image = "/Content/Img/sieuluaposter.jpg", Description = "Phim hài Việt Nam" },

                new havenix.Models.Movie { Name = "THÁM TỬ KIÊN: KỲ ÁN KHÔNG ĐẦU", Genre = "Trinh thám", Duration = "115 phút", Image = "/Content/Img/thamtukienposter.jpg", Description = "Phim điều tra phá án" },

                new havenix.Models.Movie { Name = "LẬT MẶT 8: VÒNG TAY NẮNG", Genre = "Gia đình", Duration = "120 phút", Image = "/Content/Img/latmat8poster.jpg", Description = "Phim tình cảm gia đình" },

                new havenix.Models.Movie { Name = "QUỶ ĂN TẠNG 2", Genre = "Kinh dị", Duration = "120 phút", Image = "/Content/Img/quyposter.jpg", Description = "Phim kinh dị" },

                new havenix.Models.Movie { Name = "THỎ ƠI", Genre = "Hài, kinh dị", Duration = "120 phút", Image = "/Content/Img/thooiposter.jpg", Description = "Phim hài, kinh dị" }
            };

            // KHÔNG BỊ TRÙNG PHIM
            context.Movies.AddOrUpdate(
                x => x.Name,
                movies
            );

            context.SaveChanges();

            // LẤY ID THẬT TRONG DATABASE
            var dbMovies = context.Movies
                .OrderBy(x => x.Id)
                .ToList();

            // GIỜ CHIẾU
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
                new[] { 9, 12, 16, 19, 23 },
                new[] { 10, 14, 17, 20, 22 },
                new[] { 9, 13, 16, 18, 21 }
            };

            // THÊM SHOWTIME
            for (int d = 0; d < 7; d++)
            {
                DateTime day = new DateTime(2026, 5, 22).AddDays(d);

                for (int m = 0; m < dbMovies.Count; m++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        context.Showtimes.Add(new havenix.Models.Showtime
                        {
                            MovieId = dbMovies[m].Id,
                            StartTime = new DateTime(
                                day.Year,
                                day.Month,
                                day.Day,
                                movieHours[m][i],
                                0,
                                0
                            ),
                            Room = "Phòng " + (((m + 1 + i) % 5) + 1)
                        });
                    }
                }
            }

            context.SaveChanges();
        }
    }
}