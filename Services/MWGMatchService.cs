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

        public bool AddLikeOrDislike(MWGMatchModel updatedModel)
        {
            _context.Update<MWGMatchModel>(updatedModel);
            return _context.SaveChanges() != 0;
        }
    }
}