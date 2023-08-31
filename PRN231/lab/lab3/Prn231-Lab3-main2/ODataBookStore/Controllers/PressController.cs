using BussinessObject.Models;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Drawing.Printing;

namespace ODataBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PressController : ControllerBase
    {
        private readonly IPressService _pressService;
        private readonly MyDBContext db;

        public PressController(IPressService pressService)
        {
            this._pressService = pressService;
        }

        [EnableQuery(PageSize = 10)]
        [HttpGet]
        public async Task<ActionResult> GetPress()
        {
            try
            {
                return Ok(_pressService.GetAllAsync().Result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
