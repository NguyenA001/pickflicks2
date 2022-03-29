using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pickflicks2.Models.DTO
{
    public class CreateAccountDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Icon { get; set; } // Preset/changable icon
        public string? MyMWGId { get; set; } // String of the the user's MWG they created by id
        public string? ListOfMWGId { get; set; } // This is the string of each MWG name they are members of by id
        public string? FavoritedMWGId { get; set; }
    }
}