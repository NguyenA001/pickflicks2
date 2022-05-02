using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pickflicks2.Models
{
    public class MWGStatusModel
    {
        public int Id { get; set; }
        public int MWGId { get; set; }
        public string MembersId { get; set; }
        public int UserId { get; set; }
        public bool UserDoneWithGenreRankings { get; set; }
        public bool UserDoneWithSwipes { get; set; }

    }
}