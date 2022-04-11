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
                Group.Genre1 = 0;
                Group.Genre2 = 0;
                Group.Genre3 = 0;
                Group.Genre4 = 0;
                Group.Genre5 = 0;

                _context.Update<GenreRankingModel>(Group);
                result = _context.SaveChanges() != 0;
            }
            return result;
        } 

        public string GetTopRankedGenre(int MWGId) 
        {  
            // Get MWG from MWGID
            MWGModel foundMWG =  _context.MWGInfo.SingleOrDefault(item => item.Id == MWGId);

            string chosenGenres = foundMWG.ChosenGenres;
            // chosenGenres = 'drama,action,horror,comedey,history'
            
            List<GenreRankingModel> AllGenreRankingsWithMWGId = new List<GenreRankingModel>();
            AllGenreRankingsWithMWGId = _context.GenreRankingInfo.Where(item => item.MWGId == MWGId).ToList();

            int firstGenreTotal = 0;
            int secondGenreTotal = 0;
            int thirdGenreTotal = 0;
            int fourthGenreTotal = 0;
            int fithGenreTotal = 0;

            int[] highestArr = new int[5];
            int indexOfHighestGenre = 0; 
            int highestGenre;

            for (int i = 0; i < AllGenreRankingsWithMWGId.Count; i++) { 
                int firstGenre = AllGenreRankingsWithMWGId[i].Genre1;  // set firstGenere to drama's int value 
                int secondGenre = AllGenreRankingsWithMWGId[i].Genre2; 
                int thirdGenre = AllGenreRankingsWithMWGId[i].Genre3; 
                int fourthGenre = AllGenreRankingsWithMWGId[i].Genre4; 
                int fithGenre = AllGenreRankingsWithMWGId[i].Genre5; 

                firstGenreTotal += firstGenre;
                secondGenreTotal += secondGenre;
                thirdGenreTotal += thirdGenre;
                fourthGenreTotal += fourthGenre;
                fithGenreTotal += fithGenre;
                
            }
            highestArr[0] = (firstGenreTotal);
            highestArr[1] = (secondGenreTotal);
            highestArr[2] = (thirdGenreTotal);
            highestArr[3] = (fourthGenreTotal);
            highestArr[4] = (fithGenreTotal);

            highestGenre = highestArr.Max();
            indexOfHighestGenre = Array.IndexOf(highestArr, highestGenre);

            return foundMWG.ChosenGenres[highestGenre].ToString();
        }
    }
}