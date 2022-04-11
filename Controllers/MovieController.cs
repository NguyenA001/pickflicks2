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
        //genre is selected
        //do initial fetch using genre and streaming service to get the total number of pages
        //get a random number between 0-total then fetch using that number as a page parameter
        //get 15 random numbers from 0-250, each number will be the index value of a random movie
        //push the movie title name, or id, into a temp array
        //loop through array and fetch the movie object from API
        //save the movie object into movie Model at the end of each loop
        // for(int i = 0; i< tempArr.Length; i++)
        // {
        //     MoviesModel newMovieModel = new MoviesModel();
        //     var FetchedMovieObject = 

        // }

        [HttpGet("GetMoviesByMWGId/{MWGID}")]
        public IEnumerable<MoviesModel> GetMoviesByMWGId(int MWGId, int SessionId)
        {
            return _data.GetMoviesByMWGId(MWGId, SessionId);
        } 

        // [HttpPost("ClearMoviesByMWGId/{MWGID}/{SessionId}")]
        // public bool ClearMoviesByMWGId(int MWGId, int SessionId)
        // {
        //     return _data.ClearMoviesByMWGId(MWGId, SessionId);
        // } 
    }
}