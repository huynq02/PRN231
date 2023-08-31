using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        public readonly PE_PRN231_Trial_02Context _context;
        public readonly IMapper _mapper;

        public DirectorController(PE_PRN231_Trial_02Context context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetDirectors/{nationality}/{gender}")]
        public ActionResult GetDirectors(string nationality, string gender)
        {
            var directors = _context.Directors.Where(x => ((gender == "Male") ? x.Male == true : x.Male == false) && x.Nationality == nationality)
                .ToList();
            var result = directors.Select(x => new { 
                id = x.Id,
                fullName = x.FullName,
                gender = x.Male ? "Male" : "Female",
                dob = x.Dob,
                dobString = x.Dob.ToShortDateString(),
                nationality = x.Nationality,
                description = x.Description,
            }).ToList();
            //var mapper = _mapper.Map<List<DirectorsDTO>>(directors);
            return Ok(result);
        }
        [HttpGet("GetDirector/{id}")]
        public ActionResult Dir (int id) 
        {
            var dir = _context.Directors.Include(x => x.Movies).ThenInclude(x=>x.Producer)
                .FirstOrDefault(x =>x.Id == id);
            var result = new {
                id = dir.Id,
                fullName = dir.FullName,
                gender = dir.Male ? "Male" : "Female",
                dob = dir.Dob,
                dobString = dir.Dob.ToShortDateString(),
                nationality = dir.Nationality,
                description = dir.Description,
                movies = dir.Movies.Select(x => new 
                {
                    id = x.Id,
                    title = x.Title,
                    releaseDate = x.ReleaseDate,
                    releaseYear = x.ReleaseDate.Value.Year,
                    description = x.Description,
                    language =  x.Language,
                    producerId = x.ProducerId,
                    directorId = x.DirectorId,
                    producerName = x.Producer.Name,
                    directorName = dir.FullName,
                    genres = new List<Genre>(),
                    stars = new List<Star>(),
                }),
            };

            return Ok(result);
        }
        
    }
}
