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
    public class ProgramasAcademicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProgramasAcademicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProgramasAcademicos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var programas = await _context.ProgramasAcademicos.ToListAsync();
            return Ok(programas);
        }

        // GET: api/ProgramasAcademicos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var programa = await _context.ProgramasAcademicos.FindAsync(id);

            if (programa == null)
                return NotFound("Programa académico no encontrado.");

            return Ok(programa);
        }

        // POST: api/ProgramasAcademicos
        [HttpPost]
        public async Task<IActionResult> Post(ProgramaAcademico programa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.ProgramasAcademicos.Add(programa);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = programa.Id }, programa);
        }

        // PUT: api/ProgramasAcademicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProgramaAcademico programa)
        {
            if (id != programa.Id)
                return BadRequest("El Id no coincide.");

            var programaBD = await _context.ProgramasAcademicos.FindAsync(id);

            if (programaBD == null)
                return NotFound("Programa académico no encontrado.");

            programaBD.Nombre = programa.Nombre;

            await _context.SaveChangesAsync();

            return Ok("Programa académico actualizado correctamente.");
        }

        // DELETE: api/ProgramasAcademicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var programa = await _context.ProgramasAcademicos.FindAsync(id);

            if (programa == null)
                return NotFound("Programa académico no encontrado.");

            _context.ProgramasAcademicos.Remove(programa);

            await _context.SaveChangesAsync();

            return Ok("Programa académico eliminado correctamente.");
        }
    }
}