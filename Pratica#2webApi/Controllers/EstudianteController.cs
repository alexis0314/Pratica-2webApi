using Microsoft.AspNetCore.Mvc;
using Pratica_2webApi.Models;

namespace Pratica_2webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private static List<Estudiante> estudiantes = new List<Estudiante>();

        // GET: api/estudiante
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(estudiantes);
        }

        // GET: api/estudiante/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
                return NotFound();

            return Ok(estudiante);
        }

        // POST: api/estudiante
        [HttpPost]
        public IActionResult Post([FromBody] Estudiante estudiante)
        {
            estudiante.Id = estudiantes.Count == 0
                ? 1
                : estudiantes.Max(e => e.Id) + 1;

            estudiantes.Add(estudiante);

            return CreatedAtAction(nameof(Get),
                new { id = estudiante.Id },
                estudiante);
        }

        // PUT: api/estudiante/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Estudiante actualizado)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
                return NotFound();

            estudiante.Nombre = actualizado.Nombre;
            estudiante.Apellido = actualizado.Apellido;
            estudiante.Correo = actualizado.Correo;
            estudiante.Carrera = actualizado.Carrera;
            estudiante.Edad = actualizado.Edad;
            estudiante.Promedio = actualizado.Promedio;
            estudiante.Activo = actualizado.Activo;

            return NoContent();
        }

        // DELETE: api/estudiante/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
                return NotFound();

            estudiantes.Remove(estudiante);

            return NoContent();
        }

        // GET: api/estudiante/buscar?texto=ana
        [HttpGet("buscar")]
        public IActionResult Buscar(string texto)
        {
            var resultado = estudiantes.Where(e =>
                e.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                e.Apellido.Contains(texto, StringComparison.OrdinalIgnoreCase));

            return Ok(resultado);
        }

        // GET: api/estudiante/carrera/Sistemas
        [HttpGet("carrera/{carrera}")]
        public IActionResult Carrera(string carrera)
        {
            var resultado = estudiantes.Where(e =>
                e.Carrera.Equals(carrera,
                StringComparison.OrdinalIgnoreCase));

            return Ok(resultado);
        }

        // GET: api/estudiante/aprobados?promedioMinimo=70
        [HttpGet("aprobados")]
        public IActionResult Aprobados(decimal promedioMinimo = 70)
        {
            var resultado = estudiantes.Where(e =>
                e.Promedio >= promedioMinimo);

            return Ok(resultado);
        }

        // GET: api/estudiante/ordenar?por=promedio&direccion=desc
        [HttpGet("ordenar")]
        public IActionResult Ordenar(string por = "nombre", string direccion = "asc")
        {
            IEnumerable<Estudiante> resultado = estudiantes;

            switch (por.ToLower())
            {
                case "nombre":
                    resultado = direccion.ToLower() == "desc"
                        ? estudiantes.OrderByDescending(e => e.Nombre)
                        : estudiantes.OrderBy(e => e.Nombre);
                    break;

                case "promedio":
                    resultado = direccion.ToLower() == "desc"
                        ? estudiantes.OrderByDescending(e => e.Promedio)
                        : estudiantes.OrderBy(e => e.Promedio);
                    break;

                case "edad":
                    resultado = direccion.ToLower() == "desc"
                        ? estudiantes.OrderByDescending(e => e.Edad)
                        : estudiantes.OrderBy(e => e.Edad);
                    break;
            }

            return Ok(resultado);
        }

        // GET: api/estudiante/rango?promedioDesde=70&promedioHasta=90
        [HttpGet("rango")]
        public IActionResult Rango(decimal promedioDesde,
                                   decimal promedioHasta)
        {
            var resultado = estudiantes.Where(e =>
                e.Promedio >= promedioDesde &&
                e.Promedio <= promedioHasta);

            return Ok(resultado);
        }

        // GET: api/estudiante/estadisticas
        [HttpGet("estadisticas")]
        public IActionResult Estadisticas()
        {
            if (!estudiantes.Any())
            {
                return Ok(new
                {
                    Total = 0,
                    Aprobados = 0,
                    Reprobados = 0,
                    PromedioGeneral = 0,
                    MejorPromedio = 0,
                    PeorPromedio = 0
                });
            }

            return Ok(new
            {
                Total = estudiantes.Count,
                Aprobados = estudiantes.Count(e => e.Promedio >= 70),
                Reprobados = estudiantes.Count(e => e.Promedio < 70),
                PromedioGeneral = estudiantes.Average(e => e.Promedio),
                MejorPromedio = estudiantes.Max(e => e.Promedio),
                PeorPromedio = estudiantes.Min(e => e.Promedio)
            });
        }

        // PUT: api/estudiante/5/estado?activo=false
        [HttpPut("{id}/estado")]
        public IActionResult CambiarEstado(int id, bool activo)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
                return NotFound();

            estudiante.Activo = activo;

            return NoContent();
        }

        // GET: api/estudiante/activos
        [HttpGet("activos")]
        public IActionResult Activos()
        {
            var resultado = estudiantes.Where(e => e.Activo);

            return Ok(resultado);
        }
    }
}