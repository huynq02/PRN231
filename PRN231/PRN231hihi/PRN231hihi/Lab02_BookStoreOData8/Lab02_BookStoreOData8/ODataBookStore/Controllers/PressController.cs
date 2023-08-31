using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.DataContext;
using ODataBookStore.DataContext.IRepository;

namespace ODataBookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PressController : ODataController
    {

        private readonly IPressRepo repo;
        public PressController(IPressRepo pressRepo)
        {
            repo = pressRepo;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var press = await repo.GetAllPress();
            return Ok(press);
        }
    }
}
