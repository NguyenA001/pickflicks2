using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using Microsoft.EntityFrameworkCore;

namespace pickflicks2.Services.Context
{
    public class DataContext : DbContext 
    {
        public DbSet<UserModel> UserInfo { get; set;}
        public DbSet<MWGModel> MWGInfo { get; set; }
        public DbSet<GenreRankingModel> GenreRankingInfo {get; set;}
        public DataContext(DbContextOptions options ): base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            var UserData = new List<UserModel>()
            {
                new UserModel() {
                    Id = 1,
                    Username = "Dylan",
                    Icon = "",
                    MyMWGId = "",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                }, 
                new UserModel() {
                    Id = 2,
                    Username = "Sophie",
                    Icon = "",
                    MyMWGId = "",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                }, 
                new UserModel() {
                    Id = 3,
                    Username = "An",
                    Icon = "",
                    MyMWGId = "",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                }, 
                new UserModel() {
                    Id = 4,
                    Username = "Angel",
                    Icon = "",
                    MyMWGId = "",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                },
                new UserModel() {
                    Id = 5,
                    Username = "JT",
                    Icon = "",
                    MyMWGId = "",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                },
                new UserModel() {
                    Id = 6,
                    Username = "Jose",
                    Icon = "",
                    MyMWGId = "",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                },
                new UserModel() {
                    Id = 7,
                    Username = "Demetri",
                    Icon = "",
                    MyMWGId = "",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                }
            };
            builder.Entity<UserModel>().HasData(UserData);

            var MWGData = new List<MWGModel>()
            {
                new MWGModel() {
                    Id = 1,
                    MWGName = "First MWG",
                    GroupCreatorId = 1,
                    MembersId = "1,2,3",
                    MembersNames = "Dylan,Sophie,An",
                    UserSuggestedMovies = "Toy Story 3, Wolf of Wallstreet",
                    ChosenGenres = "Drama,Thriller,Comedy,Romance,ScienceFiction",
                    IsDeleted = false
                },
                new MWGModel() {
                    Id = 2,
                    MWGName = "Second MWG",
                    GroupCreatorId = 2,
                    MembersId = "1,2,4",
                    MembersNames = "Dylan,Sophie,Angel",
                    UserSuggestedMovies = "Encanto",
                    ChosenGenres = "Drama,Thriller,Comedy,Romance,ScienceFiction",
                    IsDeleted = false
                },
                new MWGModel() {
                    Id = 3,
                    MWGName = "Third MWG",
                    GroupCreatorId = 3,
                    MembersId = "1,2,3,5",
                    MembersNames = "Dylan,Sophie,An,JT",
                    UserSuggestedMovies = "Frozen lol",
                    ChosenGenres = "Drama,Thriller,Comedy,Romance,ScienceFiction",
                    IsDeleted = false
                },
            };
           builder.Entity<MWGModel>().HasData(MWGData);

        var  GenreRankingData = new List<GenreRankingModel>()
            {
                new GenreRankingModel() {
                    Id = 1,
                    MWGId = 1,
                    UserId = 1,
                    // Biography = 0,
                    // FilmNoir = 0,
                    // Musical = 0,
                    // Sport = 0,
                    // Short = 0,
                    // Adult = 0,
                    // Adventure = 0,
                    // Fantasy = 0,
                    // Animation = 0,
                    // Drama = 2,
                    // Horror = 0,
                    // Action = 0,
                    // Comedy = 3,
                    // History = 0,
                    // Western = 0,
                    // Thriller = 4,
                    // Crime = 0,
                    // Documentary = 0,
                    // ScienceFiction = 2,
                    // Mystery = 0,
                    // Music = 0,
                    // Romance = 3,
                    // Family = 0,
                    // War = 0
                },
                new GenreRankingModel() {
                    Id = 2,
                    MWGId = 1,
                    UserId = 2,
                //     Biography = 0,
                //     FilmNoir = 0,
                //     Musical = 0,
                //     Sport = 0,
                //     Short = 0,
                //     Adult = 0,
                //     Adventure = 0,
                //     Fantasy = 0,
                //     Animation = 0,
                //     Drama = 5,
                //     Horror = 0,
                //     Action = 0,
                //     Comedy = 5,
                //     History = 0,
                //     Western = 0,
                //     Thriller = 1,
                //     Crime = 0,
                //     Documentary = 0,
                //     ScienceFiction = 4,
                //     Mystery = 0,
                //     Music = 0,
                //     Romance = 2,
                //     Family = 0,
                //     War = 0
                // },
                // new GenreRankingModel() {
                //     Id = 3,
                //     MWGId = 1,
                //     UserId = 3,
                //     Biography = 0,
                //     FilmNoir = 0,
                //     Musical = 0,
                //     Sport = 0,
                //     Short = 0,
                //     Adult = 0,
                //     Adventure = 0,
                //     Fantasy = 0,
                //     Animation = 0,
                //     Drama = 5,
                //     Horror = 0,
                //     Action = 0,
                //     Comedy = 4,
                //     History = 0,
                //     Western = 0,
                //     Thriller = 1,
                //     Crime = 0,
                //     Documentary = 0,
                //     ScienceFiction = 2,
                //     Mystery = 0,
                //     Music = 0,
                //     Romance = 1,
                //     Family = 0,
                //     War = 0
                }
            };
           builder.Entity<GenreRankingModel>().HasData(GenreRankingData);
        }
    }
}