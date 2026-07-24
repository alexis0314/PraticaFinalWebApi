using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaFinalWebApi.Data;
using PracticaFinalWebApi.Models;

namespace PracticaFinalWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfesoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Profesores
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profesores = await _context.Profesores.ToListAsync();
            return Ok(profesores);
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);

            if (profesor == null)
                return NotFound("Profesor no encontrado.");

            return Ok(profesor);
        }

        // POST: api/Profesores
        [HttpPost]
        public async Task<IActionResult> Post(Profesor profesor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool existe = await _context.Profesores
                .AnyAsync(p => p.Correo == profesor.Correo);

            if (existe)
                return BadRequest("Ya existe un profesor con ese correo.");

            _context.Profesores.Add(profesor);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = profesor.Id }, profesor);
        }

        // PUT: api/Profesores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Profesor profesor)
        {
            if (id != profesor.Id)
                return BadRequest("El Id no coincide.");

            var profesorBD = await _context.Profesores.FindAsync(id);

            if (profesorBD == null)
                return NotFound("Profesor no encontrado.");

            profesorBD.Nombre = profesor.Nombre;
            profesorBD.Apellido = profesor.Apellido;
            profesorBD.Correo = profesor.Correo;

            await _context.SaveChangesAsync();

            return Ok("Profesor actualizado correctamente.");
        }

        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);

            if (profesor == null)
                return NotFound("Profesor no encontrado.");

            _context.Profesores.Remove(profesor);

            await _context.SaveChangesAsync();

            return Ok("Profesor eliminado correctamente.");
        }
    }
}