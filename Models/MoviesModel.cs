using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pickflicks2.Models
{
    public class MoviesModel
    {
        public int Id { get; set; }
        public int MWGId { get; set; }
        public int SessionId { get; set; }
        public string? MovieName { get; set; }
        public string? MovieOverview { get; set; }
        public int MovieReleaseYear { get; set; }
        public double MovieIMDBRating { get; set; }
        public string? MovieImage { get; set; }

        public MoviesModel() {}
    }
}