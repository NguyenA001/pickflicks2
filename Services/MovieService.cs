using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pickflicks2.Services
{
    public class MovieService
    {
        private readonly DataContext _context;
        public MovieService(DataContext context)
        {
            _context = context;
        }

        public bool AddMovie(MovieModel newMovie)
        {
            
        }


    }
}