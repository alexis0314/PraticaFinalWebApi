using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaFinalWebApi.Data;
using PracticaFinalWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticaFinalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        // GET: api/<EstudiantesController>
        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class EstudianteController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public EstudianteController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET api/<EstudiantesController>/5
            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var estudiantes = await _context.Estudiantes.ToListAsync();
                return Ok(estudiantes);
            }

            // Obtener un estudiante por Id
            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var estudiante = await _context.Estudiantes.FindAsync(id);

                if (estudiante == null)
                    return NotFound("Estudiante no encontrado.");

                return Ok(estudiante);
            }

            // POST api/<EstudiantesController>
            [HttpPost]
            public async Task<IActionResult> Post(Estudiante estudiante)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Estudiantes.Add(estudiante);
                await _context.SaveChangesAsync();

                return Ok(estudiante);
            }
            // PUT api/<EstudiantesController>/5
            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, Estudiante estudiante)
            {
                if (id != estudiante.Id)
                    return BadRequest();

                _context.Entry(estudiante).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Estudiante actualizado correctamente.");
            }
            // DELETE api/<EstudiantesController>/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var estudiante = await _context.Estudiantes.FindAsync(id);

                if (estudiante == null)
                    return NotFound();

                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();

                return Ok("Estudiante eliminado correctamente.");
            }
        }
    }
}
