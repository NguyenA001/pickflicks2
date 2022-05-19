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
    public class MWGController : ControllerBase
    {
        private readonly MWGService _data;

        public MWGController(MWGService dataFromService) {
            _data = dataFromService;
        }

        // Create a MWG by MWGModel (will return a bool)
        [HttpPost("AddMWG")]
        public bool AddMWG(MWGModel newMWG)
        {
            return _data.AddMWG(newMWG);
        }

        // Get all MWGs from table (will return a collection)
        [HttpGet("GetAllMWG")]
        public IEnumerable<MWGModel> GetAllMWG()
        {
            return _data.GetAllMWG();
        }

        // Get a MWG by the specific id (will return MWGModel)
        [HttpGet("GetMWGById/{id}")]
        public MWGModel GetMWGById(int id)
        {
            return _data.GetMWGById(id);
        }

        // Get a MWG by the MWGName (will return MWGModel)
        [HttpGet("GetMWGByMWGName/{MWGName}")]
        public MWGModel GetMWGByMWGName(string? MWGName)
        {
            return _data.GetMWGByMWGName(MWGName);
        }

        // Get all the MWGs a user created by userId (will return a collection of MWGModels)
        [HttpGet("GetAllCreatedMWGByUserId/{userId}")]
        public IEnumerable<MWGModel> GetAllCreatedMWGByUserId(int userId)
        {
            return _data.GetAllCreatedMWGByUserId(userId);
        }

        // Get all the MWGs a user is a member of by userId (will return a collection of MWGModels)
        [HttpGet("GetAllMWGAUserIsMemberOf/{userId}")]
        public IEnumerable<MWGModel> GetAllMWGAUserIsMemberOf(int userId)
        {
            return _data.GetAllMWGAUserIsMemberOf(userId);
        }

        // Edit a MWG name (will return a bool), edits MWGStatus models too
        [HttpPost("EditMWGName/{oldMWGName}/{updatedMWGName}")]
        public bool EditMWGName(string? oldMWGName, string? updatedMWGName)
        {
            return _data.EditMWGName(oldMWGName, updatedMWGName);
        }


        // Add a members to MWG only the GrouoCreator can do this (will return a bool), will upate MWGStatus models too
        [HttpPost("AddMemberToMWG/{MWGId}/{newMemberId}/{newMemberName}")]
        public bool AddMemberToMWG(int MWGId, int newMemberId, string? newMemberName)
        {
            return _data.AddMemberToMWG(MWGId, newMemberId, newMemberName);
        }

        // Add a user suggested movie to a MWG (will return a bool)
        [HttpPost("suggestedMovieNames/{MWGId}/{newMovie}")]
        public bool suggestedMovieNames(int MWGId, string? newMovie)
        {
            return _data.suggestedMovieNames(MWGId, newMovie);
        }
        [HttpPost("emptysuggestedMovieNames/{MWGId}")]
        public bool emptysuggestedMovieNames(int MWGId)
        {
            return _data.emptysuggestedMovieNames(MWGId);
        }
        [HttpPost("suggestedMovieGenres/{MWGId}/{newGenre}")]
        public bool suggestedMovieGenres(int MWGId, string? newGenre)
        {
            return _data.suggestedMovieGenres(MWGId, newGenre);
        }

        //updates MWGStatus models and permanently deletes old members MWGstatus model
        [HttpPost("DeleteMemberFromMWG/{MWGId}/{deletedMemberId}/{deleteMemberName}")]
        public bool DeleteMemberFromMWG(int MWGId, int deletedMemberId, string? deleteMemberName)
        {
            return _data.DeleteMemberFromMWG(MWGId, deletedMemberId, deleteMemberName);
        }

        // Delete a MWG by MWGName, changes isDeleted on MWGStatus models to true too
        [HttpPost("DeleteByMWGName/{MWGName}")]
        public bool DeleteByMWGName(string? MWGName)
        {
            return _data.DeleteByMWGName(MWGName);
        }

        // Delete a MWG by id of MWG, changes isDeleted on MWGStatus models to true too
        [HttpPost("DeleteByMWGId/{MWGId}")]
        public bool DeleteByMWGId(int MWGId)
        {
            return _data.DeleteByMWGId(MWGId);
        }

        [HttpPost("EditMWG")]
        public bool EditMWG(MWGModel MWG)
        {
            return _data.EditMWG(MWG);
        }

        //add the three genres the admin chooses initially for everyone else to vote on
        //used to display the genre names on the genreRankingScreen on front end
        [HttpPost("AddChosenGenres/{MWGId}/{chosenGenres}")]
        public bool AddChosenGenres(int MWGId, string chosenGenres)
        {
            return _data.AddChosenGenres(MWGId, chosenGenres);
        }

        //adds service id
        [HttpPost("AddStreamingService/{MWGId}/{serviceId}")]
        public bool AddStreamingService(int MWGId, string serviceId)
        {
            return _data.AddStreamingService(MWGId, serviceId);
        }
        [HttpPost("AddFinalGenre/{MWGId}/{GenreName}")]
        public bool AddFinalGenre(int MWGId, string GenreName)
        {
            return _data.AddFinalGenre(MWGId, GenreName);
        }
        [HttpPost("AddFinalMovieIndex/{MWGId}/{index}")]
        public bool AddFinalMovieIndex(int MWGId, int index)
        {
            return _data.AddFinalMovieIndex(MWGId, index);
        }
    }
}