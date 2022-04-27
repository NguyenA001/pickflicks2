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
    public class MovieController : ControllerBase
    {
        private readonly MovieService _data;

        public MovieController(MovieService dataFromService) {
            _data = dataFromService;
        }

        // Create a MWG by MWGModel (will return a bool)
        [HttpPost("AddMovie")]
        public bool AddMovie(MoviesModel newMovie)
        {
            return _data.AddMovie(newMovie);
        }


        [HttpGet("GetMoviesByMWGId/{MWGId}/{SessionId}")]
        public IEnumerable<MoviesModel> GetMoviesByMWGId(int MWGId, int SessionId)
        {
            return _data.GetMoviesByMWGId(MWGId, SessionId);
        } 

        // [HttpGet("GetMovies")]
        // public IEnumerable<MoviesModel> GetMovies()
        // {
        //     return _data.GetMovies();
        // } 

        // [HttpPost("ClearMoviesByMWGId/{MWGID}/{SessionId}")]
        // public bool ClearMoviesByMWGId(int MWGId, int SessionId)
        // {
        //     return _data.ClearMoviesByMWGId(MWGId, SessionId);
        // } 

        //need to get genre and change base URL
        [HttpGet("TestPageNumber")]
        public async Task<int> TestPageNumber()
        {
            return await _data.TestPageNumber();
        } 

        [HttpGet("UseRandomPageNumberToGetRandomListOfMovieTitles")]
        public async Task<List<string>> UseRandomPageNumberToGetRandomListOfMovieTitles()
        {
            return await _data.UseRandomPageNumberToGetRandomListOfMovieTitles();
        } 


    }
}