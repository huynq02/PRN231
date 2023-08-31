using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Q1.Dtos;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ODataController
    {
        private readonly PE_PRN_Fall22B1Context _context;
        private readonly IMapper _mapper;

        public DirectorsController(PE_PRN_Fall22B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            var directors = _context.Directors.ToList();
            var dir = _mapper.Map<List<DirectDto>>(directors);
            return Ok(directors);
        }
    }
}
