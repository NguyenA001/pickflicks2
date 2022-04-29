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
        public DbSet<MoviesModel> MoviesInfo {get; set;}
        public DbSet<MWGMatchModel> MWGMatchInfo {get; set;}
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
                    Icon = "boy1",
                    MyMWGId = "1",
                    ListOfMWGId="1,2,3",
                    FavoritedMWGId = "1,3",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                }, 
                new UserModel() {
                    Id = 2,
                    Username = "Sophie",
                    Icon = "girl5",
                    MyMWGId = "2",
                    ListOfMWGId="1,2,3",
                    FavoritedMWGId = "1,2",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                }, 
                new UserModel() {
                    Id = 3,
                    Username = "An",
                    Icon = "boy2",
                    MyMWGId = "3",
                    ListOfMWGId="1,3",
                    FavoritedMWGId = "3",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                }, 
                new UserModel() {
                    Id = 4,
                    Username = "Angel",
                    Icon = "boy3",
                    MyMWGId = "",
                    ListOfMWGId="3",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                },
                new UserModel() {
                    Id = 5,
                    Username = "JT",
                    Icon = "boy6",
                    MyMWGId = "",
                    ListOfMWGId="2",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                },
                new UserModel() {
                    Id = 6,
                    Username = "Jose",
                    Icon = "boy4",
                    MyMWGId = "",
                    ListOfMWGId="3",
                    FavoritedMWGId = "",
                    Salt = "6oyIFN+J/zb3uaje2+GP98c/WdMgNb9Rwbn3Wyi51i+OUta55QsZFkrTqbJy9hiothKp95mleCPySEZOIOcPIg==",
                    Hash = "BBvCTG4MF3e0YvDBW7mPkimgpAOpbP7HUkNb8YRmHjM8KdNBKFRRgctlBZV/WZ0GfasEBS8qJCuPB0Z4rOalX0UuAxkqmoBznghkV+lmxmIod+25vzFPjuSYhN7QvWsfPvGf7Ze1w/qG3xQ6KBTgjTc+yKe9bcaQPFBjiYPVnM1RYsUPC3RG47Q27K2xqCOQTtdvaAUEhoDgaCLi3zyi+e750FEZ6e9y1HObs4Zsnl2Yk4AwkFZ8IZBnHlXoDyy1b1ICIG7+CggJdDkiNJxv57BRlNqijDwfE+6vBujqvr7a5kgzWgGoEMBsqLpFNv7j7sIJVBjevmyV3X8eDAY68g==",
                },
                new UserModel() {
                    Id = 7,
                    Username = "Demetri",
                    Icon = "boy5",
                    MyMWGId = "",
                    ListOfMWGId="1",
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
                    MembersIcons = "boy1,girl5,boy2",
                    UserSuggestedMovies = "Toy Story 3, Wolf of Wallstreet",
                    ChosenGenres = "Drama,Thriller,Comedy",
                    IsDeleted = false
                },
                new MWGModel() {
                    Id = 2,
                    MWGName = "Second MWG",
                    GroupCreatorId = 2,
                    MembersId = "1,2,4",
                    MembersNames = "Dylan,Sophie,Angel",
                    MembersIcons = "boy1,girl5,boy3",
                    UserSuggestedMovies = "Encanto",
                    ChosenGenres = "Drama,Thriller,Comedy",
                    IsDeleted = false
                },
                new MWGModel() {
                    Id = 3,
                    MWGName = "Third MWG",
                    GroupCreatorId = 3,
                    MembersId = "1,2,3,5",
                    MembersNames = "Dylan,Sophie,An,JT",
                    MembersIcons = "boy1,girl5,boy2,boy6",
                    UserSuggestedMovies = "Frozen lol",
                    ChosenGenres = "Drama,Thriller,Comedy",
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
                    MembersId = "1,2,3",
                    Genre1 = 1,
                    Genre2 = 1,
                    Genre3= 1,
                    // Genre4 = 1,
                    // Genre5 = 2,
                },
                new GenreRankingModel() {
                    Id = 2,
                    MWGId = 1,
                    UserId = 2,
                    MembersId = "1,2,3",
                    Genre1 = 1,
                    Genre2 = 1,
                    Genre3= 1,
                    // Genre4 = 1,
                    // Genre5 = 2,
                },
                new GenreRankingModel() {
                    Id = 3,
                    MWGId = 1,
                    UserId = 3,
                    MembersId = "1,2,3",
                    Genre1 = 5,
                    Genre2 = 2,
                    Genre3= 4,
                    // Genre4 = 1,
                    // Genre5 = 2,
                },
            };
           builder.Entity<GenreRankingModel>().HasData(GenreRankingData);
        }
    }
}