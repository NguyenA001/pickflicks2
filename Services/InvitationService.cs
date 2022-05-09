using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Services.Context;

namespace pickflicks2.Services
{
    public class InvitationService
    {
        private readonly DataContext _context;
        public InvitationService(DataContext context)
        {
            _context = context;
        }
        public bool AddInvitations(InvitationModel newInvitation)
        {
            bool result = false;
            bool doesInvitationExist = _context.InvitationInfo.SingleOrDefault(invite => invite.Id == newInvitation.Id) != null;
            if (!doesInvitationExist)
            {
                _context.Add(doesInvitationExist);
                result = _context.SaveChanges() != 0;
            }
            return result; 
        }

        public IEnumerable<InvitationModel> GetAllInvitationsByMWGId(int MWGId)
        {
            return _context.InvitationInfo.Where(item => item.MWGId == MWGId);
        }

        public IEnumerable<InvitationModel> GetAllUnacceptedInvitationsByMWGId(int MWGId)
        {
            return _context.InvitationInfo.Where(item => item.MWGId == MWGId && item.HasAccepted == false);
        } 

        public IEnumerable<InvitationModel> GetAllAcceptedInvitationsByMWGId(int MWGId)
        {
            return _context.InvitationInfo.Where(item => item.MWGId == MWGId && item.HasAccepted == true);
        } 

        public IEnumerable<InvitationModel> GetAllInvitationsByUserId(int UserId)
        {
            return _context.InvitationInfo.Where(item => item.UserId == UserId);
        }  

        public IEnumerable<InvitationModel> GetAllUnacceptedInvitationsByUserId(int UserId)
        {
            return _context.InvitationInfo.Where(item => item.UserId == UserId && item.HasAccepted == false);
        } 

        public IEnumerable<InvitationModel> GetAllAcceptedInvitationsByUserId(int UserId)
        {
            return _context.InvitationInfo.Where(item => item.UserId == UserId && item.HasAccepted == true);
        } 

        public bool AcceptInvitation(int MWGId, int UserId)
        {
            bool result = false;
            var foundInvitation = GetAllUnacceptedInvitationsByUserId(UserId).SingleOrDefault(item => item.UserId == UserId && item.MWGId == MWGId);
            if(foundInvitation != null)
            {
                foundInvitation.HasAccepted = true;
                _context.Update<InvitationModel>(foundInvitation);
                result = _context.SaveChanges() != 0;
            }
            return result;
        } 
    }
}