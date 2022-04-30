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
               

            //sumsIndex = sumsList.Add(sumsList.Where(IndexOf(number) => number == 2)));

            //return sumsIndex;
            // return sumsIndex;
            //return final;
            // int MovieTotal1 = 0;
            // int MovieTotal2 = 0;
            // int MovieTotal3 = 0;
            // int MovieTotal4 = 0;
            // int MovieTotal5 = 0;
            // int MovieTotal6 = 0;
            // int MovieTotal7 = 0;
            // int MovieTotal8 = 0;
            // int MovieTotal9 = 0;
            // int MovieTotal10 = 0;
            // int MovieTotal11 = 0;
            // int MovieTotal12 = 0;
            // int MovieTotal13 = 0;
            // int MovieTotal14 = 0;
            // int MovieTotal15 = 0;

            // int[] highestArr = new int[totalNumberOfVotes];
            // int indexOfHighestGenre = 0; 
            // int highestGenre;

            // for (int i = 0; i < AllRecentGenreRankingsWithMWGId.Count; i++) { 
            //     int firstGenre = AllRecentGenreRankingsWithMWGId[i].Genre1;  // set firstGenere to drama's int value 
            //     int secondGenre = AllRecentGenreRankingsWithMWGId[i].Genre2; 
            //     int thirdGenre = AllRecentGenreRankingsWithMWGId[i].Genre3; 
            //     // int fourthGenre = AllGenreRankingsWithMWGId[i].Genre4; 
            //     // int fithGenre = AllGenreRankingsWithMWGId[i].Genre5; 

            //     firstGenreTotal += firstGenre;
            //     secondGenreTotal += secondGenre;
            //     thirdGenreTotal += thirdGenre;
            //     // fourthGenreTotal += fourthGenre;
            //     // fithGenreTotal += fithGenre;
                
            // }
            // highestArr[0] = (firstGenreTotal);
            // highestArr[1] = (secondGenreTotal);
            // highestArr[2] = (thirdGenreTotal);
            // // highestArr[3] = (fourthGenreTotal);
            // // highestArr[4] = (fithGenreTotal);

            // highestGenre = highestArr.Max();
            // indexOfHighestGenre = Array.IndexOf(highestArr, highestGenre);

            // List<string> genrelist = new List<string>();

            // foreach (string genre in chosenGenres.Split(','))
            //     genrelist.Add(genre);

            // string genreString = genrelist[indexOfHighestGenre].ToString();

            // int genreId;
            // switch(genreString)
            // {
            //     case "Horror" :
            //         genreId = 10;
            //         break;
            //     case "Drama" :
            //         genreId = 7;
            //         break;
            //     case "Action" :
            //         genreId = 1;
            //         break;
            //     // case "Action & Adventure" :
            //     //     genreId = 39;
            //     //     break;
            //     // case "Adult" :
            //     //     genreId = 30;
            //     //     break;
            //     case "Animation" :
            //         genreId = 3;
            //         break;
            //     // case "Anime" :
            //     //     genreId = 33;
            //     //     break;
            //     // case "Biography" :
            //     //     genreId = 31;
            //     //     break;
            //     case "Comedy" :
            //         genreId = 4;
            //         break;
            //     case "Crime" :
            //         genreId = 5;
            //         break;
            //     case "Documentary" :
            //         genreId = 6;
            //         break;
            //     case "Family" :
            //         genreId = 8;
            //         break;
            //     case "Fantasy" :
            //         genreId = 9;
            //         break;
            //     case "History" :
            //         genreId = 10;
            //         break;
            //     // case "Kids" :
            //     //     genreId = 21;
            //     //     break;
            //     case "Music" :
            //         genreId = 12;
            //         break;
            //     case "Mystery" :
            //         genreId = 13;
            //         break;
            //     case "Romance" :
            //         genreId = 14;
            //         break;
            //     // case "Sci-Fi & Fantasy" :
            //     //     genreId = 40;
            //     //     break;
            //     case "Science Fiction" :
            //         genreId = 15;
            //         break;
            //     // case "Sports" :
            //     //     genreId = 29;
            //     //     break;
            //     // case "Supernatural" :
            //     //     genreId = 37;
            //     //     break;
            //     case "Thriller" :
            //         genreId = 17;
            //         break;
            //     case "War" :
            //         genreId = 18;
            //         break;
            //     case "Western" :
            //         genreId = 19;
            //         break;
            //     default :
            //         genreId = 0;
            //         break;
            // }

            // return genreId;


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