using Microsoft.AspNetCore.Mvc;

namespace IdentityAjaxClient.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}
