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


        [HttpGet("GetMoviesByMWGId/{MWGId}")]
        public IEnumerable<MoviesModel> GetMoviesByMWGId(int MWGId)
        {
            return _data.GetMoviesByMWGId(MWGId);
        } 

        //need to get genre and change base URL
        [HttpGet("TestPageNumber/{genreId}")]
        public async Task<int> TestPageNumber(int genreId)
        {
            return await _data.TestPageNumber(genreId);
        } 

        [HttpGet("UseRandomPageNumberToGetRandomListOfMovieTitles/{genreId}")]
        public async Task<List<string>> UseRandomPageNumberToGetRandomListOfMovieTitles(int genreId)
        {
            return await _data.UseRandomPageNumberToGetRandomListOfMovieTitles(genreId);
        } 


        [HttpGet("AddAll15Movies/{MWGId}/{genreId}")]
        public async Task<bool> AddAll15Movies(int MWGId, int genreId)
        {
            return await _data.AddAll15Movies(MWGId, genreId);
        } 


    }
}