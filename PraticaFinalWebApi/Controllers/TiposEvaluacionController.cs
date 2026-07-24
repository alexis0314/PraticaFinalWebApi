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
    public class TiposEvaluacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiposEvaluacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposEvaluacion
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.TiposEvaluacion.ToListAsync());
        }

        // GET: api/TiposEvaluacion/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tipo = await _context.TiposEvaluacion.FindAsync(id);

            if (tipo == null)
                return NotFound("Tipo de evaluación no encontrado.");

            return Ok(tipo);
        }

        // POST: api/TiposEvaluacion
        [HttpPost]
        public async Task<IActionResult> Post(TipoEvaluacion tipo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.TiposEvaluacion.Add(tipo);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = tipo.Id }, tipo);
        }

        // PUT: api/TiposEvaluacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TipoEvaluacion tipo)
        {
            if (id != tipo.Id)
                return BadRequest("El Id no coincide.");

            var tipoBD = await _context.TiposEvaluacion.FindAsync(id);

            if (tipoBD == null)
                return NotFound("Tipo de evaluación no encontrado.");

            tipoBD.Nombre = tipo.Nombre;

            await _context.SaveChangesAsync();

            return Ok("Tipo de evaluación actualizado correctamente.");
        }

        // DELETE: api/TiposEvaluacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipo = await _context.TiposEvaluacion.FindAsync(id);

            if (tipo == null)
                return NotFound("Tipo de evaluación no encontrado.");

            _context.TiposEvaluacion.Remove(tipo);

            await _context.SaveChangesAsync();

            return Ok("Tipo de evaluación eliminado correctamente.");
        }
    }
}