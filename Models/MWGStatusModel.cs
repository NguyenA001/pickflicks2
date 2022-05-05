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
        public string? MWGName { get; set; }
        public int GroupCreatorId { get; set; }
        public string? MembersId { get; set; }
        public string? MembersNames { get; set; }
        public int UserId { get; set; }
        public bool IsStarted { get; set; }
        public bool UserDoneWithGenreRankings { get; set; }
        public bool AreAllMembersDoneWithGenre { get; set; }
        public bool UserDoneWithSwipes { get; set; }
        public bool AreAllMembersDoneWithSwipes { get; set; }
        public bool IsDeleted { get; set; }

        public MWGStatusModel(){}

    }
}