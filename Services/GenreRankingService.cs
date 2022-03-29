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

        public IEnumerable<GenreRankingModel> UpdateGenreRankingsByMWGId(int MWGId)
        {
            bool result = false;
            List<GenreRankingModel> AllGenreRankingsWithMWGId = new List<GenreRankingModel>();
            AllGenreRankingsWithMWGId = GetGenreRankingsByMWGId(MWGId);
            GenreRankingModel isInMWG = 



            if (foundMWG != null)
            {
                foundMWG.MWGName = updatedMWGName;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
            }
            return result;
        } 

    }
}