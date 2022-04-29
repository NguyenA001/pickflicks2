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

        public List<string> GetTopMovieByMWGId(int MWGId)
        {
            List<MWGMatchModel> AllLikeDislikesOfMWGId = new List<MWGMatchModel>();
            AllLikeDislikesOfMWGId =  _context.MWGMatchInfo.Where(item => item.MWGId == MWGId).ToList();
           
            MWGModel foundMWG =  _context.MWGInfo.SingleOrDefault(item => item.Id == MWGId);
            string membersId = foundMWG.MembersId;

            List<int> MWGmembersIdlist = new List<int>();
            foreach (string memberId in membersId.Split(','))
            
            MWGmembersIdlist.Add(Int32.Parse(memberId));
            

            List<MWGMatchModel> mostRecentModelsForEachUser = new List<MWGMatchModel>();

            foreach(int Id in MWGmembersIdlist)
            {
                List<MWGMatchModel> AllLikesDislikesWithUserId = new List<MWGMatchModel>();
                AllLikesDislikesWithUserId = AllLikeDislikesOfMWGId.Where(item => item.UserId == Id).ToList();
                int lastIndex = AllLikesDislikesWithUserId.Count;
                mostRecentModelsForEachUser.Add(AllLikesDislikesWithUserId[lastIndex-1]);
            }

             //List<GenreRankingModel> AllGenreRankingsWithUserIdLAST = new List<GenreRankingModel>();
            List<string> LikesDislikesList = new List<string>();

            for(int i = 0; i<mostRecentModelsForEachUser.Count; i++)
            {
                string temp = mostRecentModelsForEachUser[i].LikesDislikesIndexValues;
                LikesDislikesList.Add(temp);
            }
            return LikesDislikesList;

            // string[] array = LikesDislikesList.ToArray();
            // return array;
            // List<int[]> arrList = new List<int[]>();

            // char[] temp2;

            // for(int i = 0; i<LikesDislikesList.Count; i++)
            // {
            //     //  foreach(string number in LikesDislikesList[i].Split(","))
            //     //  arrList.Add(Int32.Parse(number));
            //     temp2 = LikesDislikesList[i].ToArray();
                
            // }
            // return temp2;
        //1,0,1,1,1,0

        //create test MWGmatchmodels

        //use logic to find latest/highest ID of MWGmatchmodel per member of MWG

        //add to a list of all members(latest MWGmatchmodels only)

        //get string of likes/dislikes of each member, convert to a list and add each list to 
        //another list of MWGmemberslikes/dislikes

        //loop thru list of all members
            // loop thru MWGmemberslikes/dislikes
                //find total of sum at each index
                //return 15 different sums
                //return 1 highest sum

        //turn into list

        //map through
        }
    }
}