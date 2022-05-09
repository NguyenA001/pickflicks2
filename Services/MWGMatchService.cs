using pickflicks2.Models;
using pickflicks2.Services;
using pickflicks2.Services.Context;
using System.Linq;

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

        public int GetTopMovieByMWGId(int MWGId)
        {
            List<MWGMatchModel> AllLikeDislikesOfMWGId = new List<MWGMatchModel>();
            AllLikeDislikesOfMWGId =  _context.MWGMatchInfo.Where(item => item.MWGId == MWGId).ToList();
           
            MWGModel foundMWG =  _context.MWGInfo.SingleOrDefault(item => item.Id == MWGId);
            string membersId = foundMWG.MembersId;

            List<int> MWGmembersIdlist = new List<int>();
            //turns string membersId into a list of ints
            foreach (string memberId in membersId.Split(','))
            MWGmembersIdlist.Add(Int32.Parse(memberId));
            

            List<MWGMatchModel> mostRecentModelsForEachUser = new List<MWGMatchModel>();

            //finds the most recent models for each user in MWG
            foreach(int Id in MWGmembersIdlist)
            {
                List<MWGMatchModel> AllLikesDislikesWithUserId = new List<MWGMatchModel>();
                //finds all models for one user and adds to a list
                AllLikesDislikesWithUserId = AllLikeDislikesOfMWGId.Where(item => item.UserId == Id).ToList();
                //finds amount of objects in list
                int lastIndex = AllLikesDislikesWithUserId.Count;
                //adds the most latest model for the user (highest ID)
                // lastIndex-1 to get index value
                mostRecentModelsForEachUser.Add(AllLikesDislikesWithUserId[lastIndex-1]);
            }

             //List<GenreRankingModel> AllGenreRankingsWithUserIdLAST = new List<GenreRankingModel>();
            List<List<int>> LikesDislikesList = new List<List<int>>();

            //loops through all the most recent models
            for(int i = 0; i<mostRecentModelsForEachUser.Count; i++)
            {
                List<int> tempList = new List<int>();
                //saves string index values as temp
                string temp = mostRecentModelsForEachUser[i].LikesDislikesIndexValues;
                //loops thru string temp likesdislikes
                for(int j =0; j < temp.Length; j++){
                    //adds the  numeric value of each character into temp list
                    tempList.Add((int)Char.GetNumericValue(temp[j]));
                }
                //removes all instances of -1 (all commas in string temp were returned as -1)
                LikesDislikesList.Add(tempList.Where(item => item != -1).ToList());


            }
            // return LikesDislikesList;
            //like this:
            // [
            //     [1,0,1,0],
            //     [0,0,1,1],
            //     [0,1,0,0]
            // ]
            int totalNumberOfVotes = LikesDislikesList[0].Count;
            List<int> sumsList = new List<int>();

            //goes thru the number of votes -1
            for(int i =0; i<= LikesDislikesList[0].Count-1; i++)
            {
                int HighestMovieScoreTemp = 0;
                //goes thru the number of members in MWG
                for(int j=0; j<LikesDislikesList.Count;j++)
                {
                    //updates highestScoreTemp with every vote
                    HighestMovieScoreTemp += LikesDislikesList[j][i];           
                }
                    //adds the highest score to sumsList
                    sumsList.Add(HighestMovieScoreTemp);
            }

            List<int> sumsIndex = new List<int>();
            int maxValue = sumsList.Max();
            //goes thru sumList
            for(int k = 0; k < sumsList.Count; k++)
            {
                //check to see if number in list is maxValue
                //if it is, add to sumsIndex list
                if(sumsList[k] == maxValue)
                {
                    sumsIndex.Add(k);
                }
            }
            
            var rand = new Random();          
            //finds random num from 0 to number of items in sumsIndex                        
            int result = rand.Next(sumsIndex.Count);

            
            return sumsIndex[result];
        }
    }
}