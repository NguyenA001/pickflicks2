using pickflicks2.Models;
using pickflicks2.Services;
using pickflicks2.Services.Context;

namespace pickflicks2.Services
{
    public class MWGMatchService
    {
        private readonly DataContext _context;
        public MWGMatchService(DataContext context)
        {
            _context = context;
        }

        //use to add and update
        public bool AddLikeOrDislike(MWGMatchModel updatedModel)
        {
            _context.Update<MWGMatchModel>(updatedModel);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<MWGMatchModel> GetMWGMatchModelsByUserId(int userId)
        {
            return _context.MWGMatchInfo.Where(item => item.UserId == userId);
        }

        public IEnumerable<MWGMatchModel> GetMWGMatchModelsByMWGId(int MWGId)
        {
            return _context.MWGMatchInfo.Where(item => item.MWGId == MWGId);
        }

        public IEnumerable<MWGMatchModel> GetAllMWGMatchModels()
        {
            return _context.MWGMatchInfo;
        }
    }
}