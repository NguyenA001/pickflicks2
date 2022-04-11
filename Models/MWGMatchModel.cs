using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pickflicks2.Models
{
    public class MWGMatchModel
    {
        public int Id { get; set; }
        public int MWGId { get; set; }
        public int UserId { get; set; }
        public string LikesDislikesIndexValues { get; set; }

    }
}