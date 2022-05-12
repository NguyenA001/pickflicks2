using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pickflicks2.Models;
using pickflicks2.Services;

namespace pickflicks2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvitationController : Controller
    {
        private readonly InvitationService _data;

        public InvitationController(InvitationService dataFromService) {
            _data = dataFromService;
        }

        [HttpPost("AddInvitations/{MWGId}/{MWGName}/{stringOfInvitedUserNames}")]
        public bool AddInvitations(int MWGId, string? MWGName, string? stringOfInvitedUserNames)
        {
            return _data.AddInvitations(MWGId, MWGName, stringOfInvitedUserNames);
        }

        [HttpGet("GetAllInvitationsByMWGId/{MWGId}")]
        public IEnumerable<InvitationModel> GetAllInvitationsByMWGId(int MWGId)
        {
            return _data.GetAllInvitationsByMWGId(MWGId);
        } 

        [HttpGet("GetAllUnacceptedInvitationsByMWGId/{MWGId}")]
        public IEnumerable<InvitationModel> GetAllUnacceptedInvitationsByMWGId(int MWGId)
        {
            return _data.GetAllUnacceptedInvitationsByMWGId(MWGId);
        } 


        [HttpGet("GetAllAcceptedInvitationsByMWGId/{MWGId}")]
        public IEnumerable<InvitationModel> GetAllAcceptedInvitationsByMWGId(int MWGId)
        {
            return _data.GetAllAcceptedInvitationsByMWGId(MWGId);
        } 

        [HttpGet("GetAllInvitationsByUserId/{UserId}")]
        public IEnumerable<InvitationModel> GetAllInvitationsByUserId(int UserId)
        {
            return _data.GetAllInvitationsByUserId(UserId);
        } 

        [HttpGet("GetAllUnacceptedInvitationsByUserId/{UserId}")]
        public IEnumerable<InvitationModel> GetAllUnacceptedInvitationsByUserId(int UserId)
        {
            return _data.GetAllUnacceptedInvitationsByUserId(UserId);
        } 

        [HttpGet("GetAllAcceptedInvitationsByUserId/{UserId}")]
        public IEnumerable<InvitationModel> GetAllAcceptedInvitationsByUserId(int UserId)
        {
            return _data.GetAllAcceptedInvitationsByUserId(UserId);
        } 


        [HttpPost("AcceptInvitation/{MWGId}/{UserId}")]
        public bool AcceptInvitation(int MWGId, int UserId)
        {
            return _data.AcceptInvitation(MWGId, UserId);
        } 

        [HttpPost("DeleteInvitation/{MWGId}/{UserId}")]
        public bool DeleteInvitation(int MWGId, int UserId)
        {
            return _data.DeleteInvitation(MWGId, UserId);
        } 

        //groupCreator creates new MWG
            //MWGmodel is created with user as groupCreator, and their username, icon, and id already in model
        //groupCreator sends invite to users
            //every time the user searches a name in the front end, it adds the the users name, id, and icon in a list
    }
}