using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Services;
using pickflicks2.Services.Context;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace pickflicks2.Services
{
    public class MovieService
    {
        private readonly DataContext _context;
        public MovieService(DataContext context)
        {
            _context = context;
        }

        public bool AddMovie(MoviesModel newMovie)
        {
            bool result = false;

            _context.Add(newMovie);
            result = _context.SaveChanges() != 0;

            return result; 
        }

        public IEnumerable<MoviesModel> GetMoviesByMWGId(int MWGId, int SessionId)
        {
            return _context.MoviesInfo.Where(item => item.MWGId == MWGId && item.SessionId == SessionId);
        } 

        
        public IEnumerable<MoviesModel> GetMovies()
        {
           
        } 

        
        
    }
}