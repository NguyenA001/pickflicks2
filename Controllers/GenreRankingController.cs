using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pickflicks2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreRankingController : ControllerBase
    {
        private readonly GenreRankingService _data;

        public GenreRankingController(GenreRankingService dataFromService) {
            _data = dataFromService;
        }

        // Create a Genre Ranking by GenreRankingModel (will return a bool)
        [HttpPost("AddGenreRankings")]
        public bool AddGenreRankings(GenreRankingModel newGenreRankingModel)
        {
            return _data.AddGenreRankings(newGenreRankingModel);
        }

        [HttpGet("GetGenreRankingsByMWGId/{MWGId}")]
        public IEnumerable<GenreRankingModel> GetGenreRankingsByMWGId(int MWGId)
        {
            return _data.GetGenreRankingsByMWGId(MWGId);
        } 

        [HttpGet("GetGenreRankingsByUserId/{UserId}")]
        public IEnumerable<GenreRankingModel> GetGenreRankingsByUserId(int UserId)
        {
            return _data.GetGenreRankingsByUserId(UserId);
        } 

        [HttpPost("UpdateGenreRankingsByMWGId/{MWGId}")]
        public bool UpdateGenreRankingsByMWGId(int MWGId)
        {
            return _data.UpdateGenreRankingsByMWGId(MWGId);
        } 
    }
}