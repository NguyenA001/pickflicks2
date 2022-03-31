using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Services.Context;

namespace pickflicks2.Services
{
    public class GenreRankingService
    {
        private readonly DataContext _context;
        public GenreRankingService(DataContext context)
        {
            _context = context;
        }

        public bool AddGenreRankings(GenreRankingModel newGenreRankingModel)
        {
            bool result = false;
            bool doesGenreRankingExist = _context.GenreRankingInfo.SingleOrDefault(GR => GR.Id == newGenreRankingModel.Id) != null;
            if (!doesGenreRankingExist)
            {
                _context.Add(newGenreRankingModel);
                result = _context.SaveChanges() != 0;
            }
            return result; 
        }

        public IEnumerable<GenreRankingModel> GetGenreRankingsByMWGId(int MWGId)
        {
            return _context.GenreRankingInfo.Where(item => item.MWGId == MWGId);
        } 

        public IEnumerable<GenreRankingModel> GetGenreRankingsByUserId(int UserId)
        {
            return _context.GenreRankingInfo.Where(item => item.UserId == UserId);
        } 

        public bool UpdateGenreRankingsByMWGId(int MWGId)
        {
            // Get each GenreRankingModel with that MWGId
            // Then se each GenreRankingModel to empty strings
            // Return new list of GenreRankingModels with that MWGId?

            bool result = false;

            List<GenreRankingModel> AllGenreRankingsWithMWGId = new List<GenreRankingModel>();
            AllGenreRankingsWithMWGId = _context.GenreRankingInfo.Where(item => item.MWGId == MWGId).ToList();

            foreach (GenreRankingModel Group in AllGenreRankingsWithMWGId)
            {
                Group.Biography = 0;
                Group.FilmNoir = 0;
                Group.Musical = 0;
                Group.Sport = 0;
                Group.Short = 0;
                Group.Adult = 0;
                Group.Fantasy = 0;
                Group.Animation = 0;
                Group.Drama = 0;
                Group.Horror = 0;
                Group.Action = 0;
                Group.Comedy = 0;
                Group.History = 0;
                Group.Western = 0;
                Group.Thriller = 0;
                Group.Crime = 0;
                Group.Documentary = 0;
                Group.ScienceFiction = 0;
                Group.Mystery = 0;
                Group.Music = 0;
                Group.Romance = 0;
                Group.Family = 0;
                Group.War = 0;

                _context.Update<GenreRankingModel>(Group);
                result = _context.SaveChanges() != 0;
            }
            return result;
        } 

        public string GetTopRankedGenre(int MWGId, string genre) 
        {  
            // genres = 'drama,action,horror,comedey,history'

            List<GenreRankingModel> AllGenreRankingsWithMWGId = new List<GenreRankingModel>();
            AllGenreRankingsWithMWGId = _context.GenreRankingInfo.Where(item => item.MWGId == MWGId).ToList();

            // Get five voted genre for from each Genre

            int firstGenreTotal = 0;
            int secondGenreTotal = 0;
            int thirdGenreTotal = 0;
            int fourthGenreTotal = 0;
            int fithGenreTotal = 0;

            int[] highestArr = new int[5];
            int indexOfHighestGenre = 0; 

            for (int i = 0; i < AllGenreRankingsWithMWGId.Count; i++) { 
                int firstGenre = AllGenreRankingsWithMWGId[i].genre[0];  // set firstGenere to drama's int value 
                int secondGenre = AllGenreRankingsWithMWGId[i].genre[1]; 
                int thirdGenre = AllGenreRankingsWithMWGId[i].genre[2]; 
                int fourthGenre = AllGenreRankingsWithMWGId[i].genre[3]; 
                int fithGenre = AllGenreRankingsWithMWGId[i].genre[4]; 

                firstGenreTotal += firstGenere;
                secondGenreTotal += secondGenre;
                thirdGenreTotal += thirdGenre;
                fourthGenreTotal += fourthGenre;
                fithGenreTotal += fithGenre;
                
                highestArr.Add(firstGenreTotal, secondGenreTotal, thirdGenreTotal, fourthGenreTotal, fithGenreTotal);
                indexOfHighestGenre = highestArr.indexOf(Max());

                return genre[indexOfHighestGenre];
            }
        }
    }
}