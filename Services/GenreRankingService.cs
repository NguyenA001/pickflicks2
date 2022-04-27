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

        public List<GenreRankingModel> GetMostRecentGenreRankingsByMWGId(int MWGId)
        {
            List<GenreRankingModel> AllGenreRankingsWithMWGId = new List<GenreRankingModel>();
            AllGenreRankingsWithMWGId =  _context.GenreRankingInfo.Where(item => item.MWGId == MWGId).ToList();
           
            MWGModel foundMWG =  _context.MWGInfo.SingleOrDefault(item => item.Id == MWGId);
            string membersId = foundMWG.MembersId;

            List<int> MWGmembersIdlist = new List<int>();
            foreach (string memberId in membersId.Split(','))
            
            MWGmembersIdlist.Add(Int32.Parse(memberId));
            

            List<GenreRankingModel> final = new List<GenreRankingModel>();

            foreach(int Id in MWGmembersIdlist)
            {
                List<GenreRankingModel> AllGenreRankingsWithUserId = new List<GenreRankingModel>();
                AllGenreRankingsWithUserId = AllGenreRankingsWithMWGId.Where(item => item.UserId == Id).ToList();
                int lastIndex = AllGenreRankingsWithUserId.Count;
                final.Add(AllGenreRankingsWithUserId[lastIndex-1]);
            }

             //List<GenreRankingModel> AllGenreRankingsWithUserIdLAST = new List<GenreRankingModel>();
            return final;

        
        //     return AllGenreRankingsWithMWGId;

            //find the most recent GR of each member in the MWG
            //get list of members id
            //loop thru AllGenreRankingsWithMWGId and find userId == membersId
            // string MWGmembersId = 
            // List<int> MWGmembersIdlist = new List<int>();
            // foreach (string membersId in MWGmembersId.Split(','))
            // {
            // MWGmembersIdlist.Add(Int32.Parse(membersId));
            // }

            // List<GenreRankingModel> AllGenreRankingsWithOfUser = new List<GenreRankingModel>();
            //turn string into array, map thru it, find all genreRankings of that id with the same MWGid and most recent (.Length?), andddd add it to a list of object

            // foreach (int Id in MWGmembersIdlist)
            // {
            //     //returns a list of all GR of a user
            //     // need to go thru the list and find the ones that match the MWGId
            //     //
            //     AllGenreRankingsWithOfUser = GetGenreRankingsByUserId(Id);

            // }
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
                // Group.Genre4 = 0;
                // Group.Genre5 = 0;

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
            string membersIds = foundMWG.MembersId;
            // chosenGenres = "Drama,Thriller,Comedy,Romance,ScienceFiction"
            
            List<GenreRankingModel> AllRecentGenreRankingsWithMWGId = new List<GenreRankingModel>();
            AllRecentGenreRankingsWithMWGId = GetMostRecentGenreRankingsByMWGId(MWGId);

            int firstGenreTotal = 0;
            int secondGenreTotal = 0;
            int thirdGenreTotal = 0;
            // int fourthGenreTotal = 0;
            // int fithGenreTotal = 0;

            int[] highestArr = new int[3];
            int indexOfHighestGenre = 0; 
            int highestGenre;

            for (int i = 0; i < AllRecentGenreRankingsWithMWGId.Count; i++) { 
                int firstGenre = AllRecentGenreRankingsWithMWGId[i].Genre1;  // set firstGenere to drama's int value 
                int secondGenre = AllRecentGenreRankingsWithMWGId[i].Genre2; 
                int thirdGenre = AllRecentGenreRankingsWithMWGId[i].Genre3; 
                // int fourthGenre = AllGenreRankingsWithMWGId[i].Genre4; 
                // int fithGenre = AllGenreRankingsWithMWGId[i].Genre5; 

                firstGenreTotal += firstGenre;
                secondGenreTotal += secondGenre;
                thirdGenreTotal += thirdGenre;
                // fourthGenreTotal += fourthGenre;
                // fithGenreTotal += fithGenre;
                
            }
            highestArr[0] = (firstGenreTotal);
            highestArr[1] = (secondGenreTotal);
            highestArr[2] = (thirdGenreTotal);
            // highestArr[3] = (fourthGenreTotal);
            // highestArr[4] = (fithGenreTotal);

            highestGenre = highestArr.Max();
            indexOfHighestGenre = Array.IndexOf(highestArr, highestGenre);

            List<string> genrelist = new List<string>();

            foreach (string genre in chosenGenres.Split(','))
                genrelist.Add(genre);

            return genrelist[indexOfHighestGenre].ToString();
        }
    }
}