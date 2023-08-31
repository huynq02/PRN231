using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Q2.Pages
{
    public class CreateMovieModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public CreateMovieModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [BindProperty]
        public int MovieId { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public DateTime PublishDate { get; set; }

        public List<Studio> AllStudio { get; set; }  // Assuming you have a list of available books

        [BindProperty]
        public int SelectedStudios { get; set; }  // To bind selected book ids from the form

        public async void OnGet()
        {
            AllStudio = GetAvailableAuthos().Result;

            string authurl = @"http://localhost:5000/api/Studio/List";
            HttpResponseMessage authResult = await _httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Studio> studioList = JsonSerializer.Deserialize<List<Studio>>(authJsonStr);
            AllStudio = studioList;
        }

        public async Task<IActionResult> OnPost()
        {
            string authurl = @"http://localhost:5000/api/Studio/List";
            HttpResponseMessage authResult = await _httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Studio> studioList = JsonSerializer.Deserialize<List<Studio>>(authJsonStr);
            AllStudio = studioList;
            if (!ModelState.IsValid)
            {

                return Page();
            }
            var movie = new Movie
            {
                movieId = MovieId,
                title = Title,
                publishDate = PublishDate,
                studioId = SelectedStudios
            };

            // Serialize the book object to JSON
            var json = JsonSerializer.Serialize(movie);

            // Define the API endpoint URL for creating a book
            var apiUrl = "http://localhost:5000/api/Movie/Add";
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(movie.movieId.ToString()), "MovieId");
            content.Add(new StringContent(movie.title), "Title");
            content.Add(new StringContent(movie.publishDate.ToString("yyyy-MM-dd")), "PublishDate");
            content.Add(new StringContent(movie.studioId.ToString()), "StudioId");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Check if the response content equals 1 to determine success
                if (responseContent == "1")
                {
                    // Book creation successful, redirect to the index page
                    return RedirectToPage("/movies");
                }
            }
            else
            {
                // Book creation failed, display an error message
                ModelState.AddModelError("", "Failed to create the movie.");
                return Page();
            }


            return RedirectToPage("/Index");
        }
        private async Task<List<Studio>> GetAvailableAuthos()
        {
            string authurl = @"http://localhost:5000/api/Studio/List";
            HttpResponseMessage authResult = await _httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Studio> studioList = JsonSerializer.Deserialize<List<Studio>>(authJsonStr);
            AllStudio = studioList;
            return AllStudio;

        }

    }
}
