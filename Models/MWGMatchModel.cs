using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pickflicks2.Models
{
    public class MWGMatchModel
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int MWGId { get; set; }
        public int UserId { get; set; }
        public string LikesDislikesIndexValues { get; set; }

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