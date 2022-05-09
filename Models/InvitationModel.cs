using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pickflicks2.Models
{
    public class InvitationModel
    {
        public int Id { get; set; }
        public int MWGId { get; set; }
        public string? MWGName { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserIcon { get; set; }
        public bool HasAccepted { get; set; }
    }
}