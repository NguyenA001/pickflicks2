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

        
        // public IEnumerable<MoviesModel> GetMovies()
        // {
           
        // } 
        public async Task<int> TestPageNumber()
        {
            string baseUrl = "https://api.watchmode.com/v1/list-titles/?apiKey=h4xYuoaDgHHU19yy6I3jDqjH7ZoPQ9ruXtNJ6buj&types=movie&genres=4&page=1&source_ids=203&regions=US";
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
                                int result = 0;
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
        public async Task<List<string>> UseRandomPageNumberToGetRandomListOfMovieTitles()
        {
            Task<int> pageNum = TestPageNumber();
            string baseUrl = $"https://api.watchmode.com/v1/list-titles/?apiKey=h4xYuoaDgHHU19yy6I3jDqjH7ZoPQ9ruXtNJ6buj&types=movie&genres=4&page={pageNum}&source_ids=203&regions=US";
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
                                var rand = new Random();
                                for (int i = 0; i < 15; i++)
                                {
                                    do {
                                        number = rand.Next(0, 250);
                                    } while (listNumbers.Contains(number));
                                    listNumbers.Add(number);
                                }
                                // int [] randomArr = listNumbers.ToArray();
                                List<string> randomMovies = new List<string>();
                                foreach(int i in listNumbers)
                                {
                                    //casting
                                    //var randTitle = dataObj["titles"][i]["title"].ToString();
                                    randomMovies.Add(dataObj["titles"][i]["title"].ToString());

                                    //var randTitle = (string)dataObj["titles"][i]["title"];
                                    //randomMovies.Add(randTitle);
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

        //return list, turn into array, map through it and create a new MovieModel
        // public bool AddMovie1(MoviesModel newMovie)
        // {
        //     Task<List<int>> randomList = UseRandomPageNumberToGetRandomListOfNums();
        //     int [] randomArr = randomList.ToArray();
        //     bool result = false;

        //     foreach(int i in randomArr)
        //     {
        //         MoviesModel newMoviesModel = new MoviesModel();


        //         newMoviesModel.Id = 0;
        //         newMoviesModel.MWGId = 1;
        //         newMoviesModel.SessionId = 2;
        //         newMoviesModel.MovieName = 
        //         newMoviesModel
        //         newMoviesModel
        //     }

        //     _context.Add(newMovie);
        //     result = _context.SaveChanges() != 0;

        //     return result; 
        // }

    }
}