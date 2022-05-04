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
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<MWGModel> MWGInfo { get; set; }
        public DbSet<GenreRankingModel> GenreRankingInfo { get; set; }
        public DbSet<MoviesModel> MoviesInfo { get; set; }
        public DbSet<MWGMatchModel> MWGMatchInfo { get; set; }
        public DbSet<MWGStatusModel> MWGStatusInfo { get; set; }
        public DataContext(DbContextOptions options) : base(options)
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

            var MWGStatusData = new List<MWGStatusModel>()
            {
                new MWGStatusModel() {
                    Id = 1,
                    MWGId = 1,
                    MWGName = "First MWG",
                    MembersNames = "Dylan,Sophie,An",
                    UserId = 1,
                    GroupCreatorId = 1,
                    MembersId = "1,2,3",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 2,
                    MWGId = 1,
                    MWGName = "First MWG",
                    MembersNames = "Dylan,Sophie,An",
                    UserId = 2,
                    GroupCreatorId = 1,
                    MembersId = "1,2,3",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 3,
                    MWGId = 1,
                    MWGName = "First MWG",
                    MembersNames = "Dylan,Sophie,An",
                    UserId = 3,
                    GroupCreatorId = 1,
                    MembersId = "1,2,3",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 4,
                    MWGId = 2,
                    MWGName ="Second MWG",
                    MembersNames = "Dylan,Sophie,Angel",
                    UserId = 1,
                    GroupCreatorId = 2,
                    MembersId = "1,2,4",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 5,
                    MWGId = 2,
                    MWGName ="Second MWG",
                    MembersNames = "Dylan,Sophie,Angel",
                    UserId = 2,
                    GroupCreatorId = 2,
                    MembersId = "1,2,4",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 6,
                    MWGId = 2,
                    MWGName ="Second MWG",
                    MembersNames = "Dylan,Sophie,Angel",
                    UserId = 4,
                    GroupCreatorId = 2,
                    MembersId = "1,2,4",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 7,
                    MWGId = 3,
                    MWGName="Third MWG",
                    MembersNames = "Dylan,Sophie,An,JT",
                    UserId = 1,
                    GroupCreatorId = 3,
                    MembersId = "1,2,3,5",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 8,
                    MWGId = 3,
                    MWGName="Third MWG",
                    MembersNames = "Dylan,Sophie,An,JT",
                    UserId = 2,
                    GroupCreatorId = 3,
                    MembersId = "1,2,3,5",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 9,
                    MWGId = 3,
                    MWGName="Third MWG",
                    MembersNames = "Dylan,Sophie,An,JT",
                    UserId = 3,
                    GroupCreatorId = 3,
                    MembersId = "1,2,3,5",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
                new MWGStatusModel() {
                    Id = 10,
                    MWGId = 3,
                    MWGName="Third MWG",
                    MembersNames = "Dylan,Sophie,An,JT",
                    UserId = 5,
                    GroupCreatorId = 3,
                    MembersId = "1,2,3,5",
                    UserDoneWithGenreRankings = false,
                    UserDoneWithSwipes = false,
                    IsDeleted = false
                },
            };
            builder.Entity<MWGStatusModel>().HasData(MWGStatusData);

            var GenreRankingData = new List<GenreRankingModel>()
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

            var MoviesData = new List<MoviesModel>()
            {
                new MoviesModel(){
        Id = 5,
        MWGId = 2,
        MovieName = "Bloodline",
        MovieOverview = "Evan values family above all else, and anyone who gets between him, his wife, and newborn son learns that the hard way. But when it comes to violent tendencies, it seems the apple doesn’t fall far from the tree.",
        MovieReleaseYear = 2019,
        MovieIMDBRating = 5.9,
        MovieImage = "https://cdn.watchmode.com/posters/01486484_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 6,
        MWGId = 2,
        MovieName = "Alone",
        MovieOverview = "A recently widowed traveler is kidnapped by a cold blooded killer, only to escape into the wilderness where she is forced to battle against the elements as her pursuer closes in on her.",
        MovieReleaseYear = 2020,
        MovieIMDBRating = 6.2,
        MovieImage = "https://cdn.watchmode.com/posters/0123789_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 7,
        MWGId = 2,
        MovieName = "The Dustwalker",
        MovieOverview = "An alien spaceship crash-lands in an isolated town in the middle of the Australian desert, releasing an insidious parasite that attacks the brain of all creatures including humans, making them disorientated, unnaturally strong and violent. Sargeant Jo Sharp, readying for a return to the big city beat, wakes to find the township, her family and friends falling victim to an evil that is spreading faster than it can be contained.",
        MovieReleaseYear = 2020,
        MovieIMDBRating = 3.1,
        MovieImage = "https://cdn.watchmode.com/posters/01583583_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 8,
        MWGId = 2,
        MovieName = "Parasite",
        MovieOverview = "All unemployed, Ki-taek's family takes peculiar interest in the wealthy and glamorous Parks for their livelihood until they get entangled in an unexpected incident.",
        MovieReleaseYear = 2019,
        MovieIMDBRating = 8.6,
        MovieImage = "https://cdn.watchmode.com/posters/01295258_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 9,
        MWGId = 2,
        MovieName = "The Tuxedo",
        MovieOverview = "Cabbie-turned-chauffeur Jimmy Tong learns there is really only one rule when you work for playboy millionaire Clark Devlin : Never touch Devlin's prized tuxedo. But when Devlin is temporarily put out of commission in an explosive accident, Jimmy puts on the tux and soon discovers that this extraordinary suit may be more black belt than black tie. Paired with a partner as inexperienced as he is, Jimmy becomes an unwitting secret agent.",
        MovieReleaseYear = 2002,
        MovieIMDBRating = 5.7,
        MovieImage = "https://cdn.watchmode.com/posters/01424722_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 10,
        MWGId = 2,
        MovieName = "Stay",
        MovieOverview = "Psychiatrist Sam Foster has a new patient, Henry Letham, who claims to be suicidal. In trying to diagnose him, Sam visits Henry's prior therapist and also finds Henry's mother -- even though Henry has said that he murdered both of his parents. As reality starts to contradict fact, Sam spirals into an unstable mental state. Then he finds a clue as to how and when Henry may try to kill himself, and races to try to stop him.",
        MovieReleaseYear = 2005,
        MovieIMDBRating = 6.9,
        MovieImage = "https://cdn.watchmode.com/posters/01359873_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 11,
        MWGId = 2,
        MovieName = "Death on the Nile",
        MovieOverview = "Belgian sleuth Hercule Poirot boards a glamorous river steamer with enough champagne to fill the Nile. But his Egyptian vacation turns into a thrilling search for a murderer when a picture-perfect couple’s idyllic honeymoon is tragically cut short.",
        MovieReleaseYear = 2022,
        MovieIMDBRating = 7,
        MovieImage = "https://cdn.watchmode.com/posters/0194978_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 12,
        MWGId = 2,
        MovieName = "Feral State",
        MovieOverview = "A misfit gang of runaways and orphans are taken in by a dark and charismatic father figure, who together wreak havoc throughout swamps and trailer parks of central Florida.",
        MovieReleaseYear = 2021,
        MovieIMDBRating = 5.7,
        MovieImage = "https://cdn.watchmode.com/posters/01628143_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 13,
        MWGId = 2,
        MovieName = "Alone",
        MovieOverview = "\"Alone\" follows a writer seeking peace and solitude in the countryside in an attempt to recover from tragedy and finish her book. However, as the welcoming country house turns into a living hell, she soon realizes that her inner demons are not the worst of her problems.",
        MovieReleaseYear = 2020,
        MovieIMDBRating = 4.8,
        MovieImage = "https://cdn.watchmode.com/posters/01597024_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 14,
        MWGId = 2,
        MovieName = "Luce",
        MovieOverview = "A star athlete and top student, Luce's idealized image is challenged by one of his teachers when his unsettling views on political violence come to light, putting a strain on family bonds while igniting intense debates on race and identity.",
        MovieReleaseYear = 2019,
        MovieIMDBRating = 7.2,
        MovieImage = "https://cdn.watchmode.com/posters/01238045_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 15,
        MWGId = 2,
        MovieName = "47 Meters Down",
        MovieOverview = "Two sisters on Mexican vacation are trapped in a shark observation cage at the bottom of the ocean, with oxygen running low and great whites circling nearby, they have less than an hour of air left to figure out how to get to the surface.",
        MovieReleaseYear = 2017,
        MovieIMDBRating = 6,
        MovieImage = "https://cdn.watchmode.com/posters/013820_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 16,
        MWGId = 2,
        MovieName = "Black Swan",
        MovieOverview = "A journey through the psyche of a young ballerina whose starring role as the duplicitous swan queen turns out to be a part for which she becomes frighteningly perfect.",
        MovieReleaseYear = 2010,
        MovieIMDBRating = 8,
        MovieImage = "https://cdn.watchmode.com/posters/0153435_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 17,
        MWGId = 2,
        MovieName = "12 Hour Shift",
        MovieOverview = "It's 1999 and over the course of one 12-hour shift at an Arkansas hospital, a junkie nurse, her scheming cousin and a group of black market organ-trading criminals start a heist that could lead to their collective demise.",
        MovieReleaseYear = 2020,
        MovieIMDBRating = 5.3,
        MovieImage = "https://cdn.watchmode.com/posters/01597757_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 18,
        MWGId = 2,
        MovieName = "Songbird",
        MovieOverview = "During a pandemic lockdown, Nico, a young man with rare immunity, must overcome martial law, murderous vigilantes and a powerful family to reunite with his love, Sara.",
        MovieReleaseYear = 2020,
        MovieIMDBRating = 5.3,
        MovieImage = "https://cdn.watchmode.com/posters/01620358_poster_w185.jpg"
    },
    new MoviesModel(){
        Id = 19,
        MWGId = 2,
        MovieName = "Armored",
        MovieOverview = "A crew of officers at an armored transport security firm risk their lives when they embark on the ultimate heist against their own company. Armed with a seemingly fool-proof plan, the men plan on making off with a fortune with harm to none. But when an unexpected witness interferes, the plan quickly unravels and all bets are off.",
        MovieReleaseYear = 2009,
        MovieIMDBRating = 5.8,
        MovieImage = "https://cdn.watchmode.com/posters/0133594_poster_w185.jpg"
    },

            };
            builder.Entity<MoviesModel>().HasData(MoviesData);


            var  MWGMatchData = new List<MWGMatchModel>()
            {
                new MWGMatchModel() {
                    Id = 1,
                    MWGId = 1,
                    UserId = 1,
                    LikesDislikesIndexValues = "1,0,1,0,0,0,1,0,0,1,0,0,1,1,1",
                },
                new MWGMatchModel() {
                    Id = 2,
                    MWGId = 1,
                    UserId = 2,
                    LikesDislikesIndexValues = "1,0,1,0,1,1,1,0,0,1,0,1,1,1,1",
                },
                new MWGMatchModel() {
                    Id = 3,
                    MWGId = 1,
                    UserId = 3,
                    LikesDislikesIndexValues = "0,0,1,0,1,0,0,0,0,1,0,0,1,0,1",
                },
            };
           builder.Entity<MWGMatchModel>().HasData(MWGMatchData);
        }
    }
}