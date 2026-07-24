using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaFinalWebApi.Data;
using PracticaFinalWebApi.Models;

namespace PracticaFinalWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosAcademicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstadosAcademicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EstadosAcademicos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.EstadosAcademicos.ToListAsync());
        }

        // GET: api/EstadosAcademicos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var estado = await _context.EstadosAcademicos.FindAsync(id);

            if (estado == null)
                return NotFound("Estado académico no encontrado.");

            return Ok(estado);
        }

        // POST: api/EstadosAcademicos
        [HttpPost]
        public async Task<IActionResult> Post(EstadoAcademico estado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.EstadosAcademicos.Add(estado);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = estado.Id }, estado);
        }

        // PUT: api/EstadosAcademicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EstadoAcademico estado)
        {
            if (id != estado.Id)
                return BadRequest("El Id no coincide.");

            var estadoBD = await _context.EstadosAcademicos.FindAsync(id);

            if (estadoBD == null)
                return NotFound("Estado académico no encontrado.");

            estadoBD.Nombre = estado.Nombre;

            await _context.SaveChangesAsync();

            return Ok("Estado académico actualizado correctamente.");
        }

        // DELETE: api/EstadosAcademicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var estado = await _context.EstadosAcademicos.FindAsync(id);

            if (estado == null)
                return NotFound("Estado académico no encontrado.");

            _context.EstadosAcademicos.Remove(estado);

            await _context.SaveChangesAsync();

            return Ok("Estado académico eliminado correctamente.");
        }
    }
}