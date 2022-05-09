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

        [HttpPost("AddInvitations")]
        public bool AddInvitations(InvitationModel newInvitation)
        {
            return _data.AddInvitations(newInvitation);
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
        public IEnumerable<InvitationModel> AcceptInvitation(int MWGId, int UserId)
        {
            return _data.AcceptInvitation(MWGId, UserId);
        } 
    }
}