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
    public class MateriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MateriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var materias = await _context.Materias
                .Include(m => m.Profesor)
                .ToListAsync();

            return Ok(materias);
        }

        // GET: api/Materias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var materia = await _context.Materias
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (materia == null)
                return NotFound("Materia no encontrada.");

            return Ok(materia);
        }

        // POST: api/Materias
        [HttpPost]
        public async Task<IActionResult> Post(Materia materia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profesorExiste = await _context.Profesores
                .AnyAsync(p => p.Id == materia.ProfesorId);

            if (!profesorExiste)
                return BadRequest("El profesor seleccionado no existe.");

            _context.Materias.Add(materia);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = materia.Id }, materia);
        }

        // PUT: api/Materias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Materia materia)
        {
            if (id != materia.Id)
                return BadRequest();

            var materiaBD = await _context.Materias.FindAsync(id);

            if (materiaBD == null)
                return NotFound("Materia no encontrada.");

            materiaBD.Nombre = materia.Nombre; 
            materiaBD.ProfesorId = materia.ProfesorId;

            await _context.SaveChangesAsync();

            return Ok("Materia actualizada correctamente.");
        }

        // DELETE: api/Materias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var materia = await _context.Materias.FindAsync(id);

            if (materia == null)
                return NotFound("Materia no encontrada.");

            _context.Materias.Remove(materia);

            await _context.SaveChangesAsync();

            return Ok("Materia eliminada correctamente.");
        }
    }
}