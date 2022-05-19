namespace pickflicks2.Models
{
    public class MWGModel
    {
        public int Id { get; set; } // Own id for MWG
        public string? MWGName { get; set; } // String name for MWG 
        public int GroupCreatorId { get; set; } // The groupCreator's id
        public string? MembersId { get; set; } // String of each members' id
        public string? MembersNames { get; set; } // String of each members' username
        public string? MembersIcons { get; set; }
        public string? suggestedMovieNames { get; set; }
        public string? suggestedMovieGenres { get; set; }
        public string? ChosenGenres {get; set;} //String of chosen genres before watching movie
        public string? StreamingService {get; set; } 
        public string? FinalGenre { get; set; }
        public int FinalMovieIndex { get; set; }
        public bool IsDeleted { get; set; } // Soft delete

        //tie to member and check if their status
        // make another table that has MWGID, membersID and check if they did it
        //track  for genres
        //another track for swipes


    }
}

