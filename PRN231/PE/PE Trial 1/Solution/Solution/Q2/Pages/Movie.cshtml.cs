using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Q2.Pages
{
    public class MovieModel : PageModel
    {
        private HttpClient httpClient;
        public MovieModel(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
        }
        public List<Movie> movies { get; set; }
        public List<MovieDto> moviesDto { get; set; } = new List<MovieDto>();
        [BindProperty]
        public int MovieId { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public DateTime PublishDate { get; set; }

        public List<Studio> AllStudio { get; set; }  // Assuming you have a list of available books

        [BindProperty]
        public int SelectedStudios { get; set; }  // To bind selected book ids from the form

        public async Task OnGet(int? id)
        {
            AllStudio = GetAvailableAuthos().Result;

            string authurl = @"http://localhost:5000/api/Studio/List";
            HttpResponseMessage authResult = await httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Studio> studioList = JsonSerializer.Deserialize<List<Studio>>(authJsonStr);
            AllStudio = studioList;



            string url = @"http://localhost:5000/api/Movie/List/" + id;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            movies = JsonSerializer.Deserialize<List<Movie>>(jsonStr);

            foreach (var movie in movies)
            {
                moviesDto.Add(new MovieDto { MovieId = movie.movieId, Title = movie.title, PublishDate = movie.publishDate, StudioId = movie.movieId });
            }

            string authurls = @"http://localhost:5000/api/Studio/List";
            HttpResponseMessage authResults = await httpClient.GetAsync(authurl);
            string authJsonStrs = await authResult.Content.ReadAsStringAsync();

            // Author author = JsonSerializer.Deserialize<Author>(authJsonStr);
            List<Studio> studioListss = JsonSerializer.Deserialize<List<Studio>>(authJsonStrs);
            foreach (var movie in moviesDto)
            {
                foreach (var item in studioListss)
                {
                    if (movie.StudioId == item.studioId)
                    {
                        movie.StudioName = item.studioName;
                    }
                }


            }
        }
        private async Task<List<Studio>> GetAvailableAuthos()
        {
            string authurl = @"http://localhost:5000/api/Studio/List";
            HttpResponseMessage authResult = await httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Studio> studioList = JsonSerializer.Deserialize<List<Studio>>(authJsonStr);
            AllStudio = studioList;
            return AllStudio;
        }
    }
    public class Movie
    {
        public int movieId { get; set; }
        public string title { get; set; }
        public DateTime publishDate { get; set; }
        public int studioId { get; set; }
    }
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int StudioId { get; set; }
        public string StudioName { get; set; } = string.Empty;
    }
    public class Studio
    {
        public int studioId { get; set; }
        public string studioName { get; set; }
    }
    public class StudioDto
    {
        public int StudioId { get; set; }
        public string StudioName { get; set; }
        public List<Movie>? movies { get; set; }
    }

}

