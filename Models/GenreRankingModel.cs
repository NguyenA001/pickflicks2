namespace pickflicks2.Models
{
    public class GenreRankingModel
    {
        public int Id { get; set; }
        public int MWGId { get; set; }
        public string MembersId { get; set; }
        public int UserId { get; set; }  
        public int Genre1 { get; set; }    
        public int Genre2 { get; set; }    
        public int Genre3 { get; set; }    
    }
}