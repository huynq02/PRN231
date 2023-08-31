using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class BookController : Controller
    {
        public BookController()
        {
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
    }
}
