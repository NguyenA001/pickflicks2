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

        public IEnumerable<MoviesModel> GetMoviesByMWGId(int MWGId)
        {
            return _context.MoviesInfo.Where(item => item.MWGId == MWGId);
        } 

        public async Task<int> TestPageNumber(int genreId, int streamingServiceId)
        {
            string baseUrl = $"https://api.watchmode.com/v1/list-titles/?apiKey=mETUm6GZ5uGvydlOLpOdvzHNucdbuaZ9LJDp9Flw&types=movie&genres={genreId}&page=0&source_ids={streamingServiceId}&regions=US";
            //Have your using statements within a try/catch blokc that will catch any exceptions.
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    //System.Console.WriteLine("This is running");
                    //Now get your response from the client from get request to baseurl.
                    //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        //Now we will retrieve content from our response, which would be HttpContent, retrieve from the response Content property.
                        using (HttpContent content = res.Content)
                        {
                            //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                            string data = await content.ReadAsStringAsync();
                            //If the data is not null, parse the data to a C# object, then create a new instance of PokeItem.
                            if (data != null)
                            {
                                //Parse your data into a object.
                                var dataObj = JObject.Parse(data);
                                //Then create a new instance of PokeItem, and string interpolate your name property to your JSON object.
                                //Which will convert it to a string, since each property value is a instance of JToken.
                                // PokeItem pokeItem = new PokeItem(name: $"{dataObj["name"]}");
                                //Log your pokeItem's name to the Console.
                                var totalPages = dataObj["total_pages"];
                                //Console.WriteLine(totalPages);
                                var rand = new Random();
                                //removed +1 to prevent last page from appearing
                                int result = rand.Next(Convert.ToInt32(totalPages));
                                return result;
                            }
                            else
                            {
                                //If data is null log it into console.
                                //Console.WriteLine("Data is null!");
                                int result = 1111;
                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return 111;
            }
        } 



        //fetch using random page number
        public async Task<List<string>> UseRandomPageNumberToGetRandomListOfMovieIds(int genreId, int streamingServiceId)
        {
            int pageNum = await TestPageNumber(genreId, streamingServiceId);
            string baseUrl = $"https://api.watchmode.com/v1/list-titles/?apiKey=mETUm6GZ5uGvydlOLpOdvzHNucdbuaZ9LJDp9Flw&types=movie&genres={genreId}&page={pageNum}&source_ids={streamingServiceId}&regions=US";
            //Have your using statements within a try/catch blokc that will catch any exceptions.
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                //Parse your data into a object.
                                var dataObj = JObject.Parse(data);

                                List<int> listNumbers = new List<int>();
                                int number;
                                int totalResults = (int)dataObj["titles"].Count();
                                var rand = new Random();
                                for (int i = 0; i < 15; i++)
                                {
                                    do {
                                        number = rand.Next(0, totalResults);
                                    } while (listNumbers.Contains(number));
                                    listNumbers.Add(number);
                                }
                                // int [] randomArr = listNumbers.ToArray();
                                List<string> randomMovies = new List<string>();
                                foreach(int i in listNumbers)
                                {
                                    randomMovies.Add(dataObj["titles"][i]["id"].ToString());
                                }
                                return randomMovies;
                            }
                            else
                            {
                                List<string> randomMovies = new List<string>();
                                return randomMovies;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                List<string> randomMovies = new List<string>();
                return randomMovies;
            }
        }


        public async Task<bool> AddAll15Movies(int MWGId, int genreId, int streamingServiceId)
        {
            bool result = false;
            var randomMovies= await UseRandomPageNumberToGetRandomListOfMovieIds(genreId, streamingServiceId);

            foreach (string movie in randomMovies)
            {
                string baseUrl = $"https://api.watchmode.com/v1/title/{movie}/details/?apiKey=mETUm6GZ5uGvydlOLpOdvzHNucdbuaZ9LJDp9Flw";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                //Parse your data into a object.
                                var dataObj = JObject.Parse(data);
                                MoviesModel newMM = new MoviesModel();
                                newMM.Id = 0;
                                newMM.MWGId = MWGId;
                                newMM.SessionId = 0;
                                newMM.MovieName = dataObj["title"].ToString();
                                newMM.MovieOverview = dataObj["plot_overview"].ToString();
                                newMM.MovieReleaseYear = (int)dataObj["year"];
                                newMM.MovieIMDBRating = (double)dataObj["user_rating"];
                                newMM.MovieImage = dataObj["poster"].ToString();

                                _context.Add(newMM);
                                result = _context.SaveChanges() != 0;
                            }
                            else
                            {
                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
                {
                    return result;
                }
            }
            result = true;
            return result;
        }
    }
}