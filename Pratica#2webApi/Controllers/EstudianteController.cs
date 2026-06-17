using Microsoft.AspNetCore.Mvc;
using Pratica_2webApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pratica_2webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private static List<Estudiante> estudiantes = new List<Estudiante>();

        // GET: api/<EstudianteController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EstudianteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EstudianteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EstudianteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EstudianteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
