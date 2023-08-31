using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Reservations.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IRepository repository;
        private IWebHostEnvironment webHostEnvironment;
        public ReservationController(IRepository repo, IWebHostEnvironment environment)
        {
            repository = repo;
            webHostEnvironment = environment;
        }

        // GET: api/<ReservationController>

        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;



        // GET api/<ReservationController>/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must ve pass in the request body.");
            return Ok(repository[id]);
        }

        // POST api/<ReservationController>
        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            repository.AddReservation(new Reservation
            {
                Name = res.Name,
                StartLocation = res.StartLocation,
                EndLocation = res.EndLocation
            });



        // PUT api/<ReservationController>/5
        [HttpPut]
        public Reservation Put([FromForm] Reservation res) => repository.UpdateReservation(res);

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
        bool Authenticate()
        {
            var allowedKey = new[] { "Secret@123", "Secret#12", "SecretABC" };
            StringValues key = Request.Headers["Key"];
            int cout = (from t in allowedKey where t == key select t).Count();
            return cout == 0 ? false : true;
        }
        [HttpPost("PostXml")]
        public Reservation PostXml([FromBody] System.Xml.Linq.XElement res)
        {

            return repository.AddReservation(new Reservation
            {
                Name = res.Element("Name").Value,
                StartLocation = res.Element("StartLocation").Value,
                EndLocation = res.Element("EndLocation").Value
            });
        }
        [HttpGet("ShowReservation.{format}"), FormatFilter]
        public IEnumerable<Reservation> ShowReservation() => repository.Reservations;
        
    }
}
