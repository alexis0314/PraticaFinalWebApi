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
    public class PeriodosAcademicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeriodosAcademicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.PeriodosAcademicos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var periodo = await _context.PeriodosAcademicos.FindAsync(id);

            if (periodo == null)
                return NotFound("Período académico no encontrado.");

            return Ok(periodo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PeriodoAcademico periodo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.PeriodosAcademicos.Add(periodo);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = periodo.Id }, periodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PeriodoAcademico periodo)
        {
            if (id != periodo.Id)
                return BadRequest();

            var periodoBD = await _context.PeriodosAcademicos.FindAsync(id);

            if (periodoBD == null)
                return NotFound();

            periodoBD.Nombre = periodo.Nombre;

            await _context.SaveChangesAsync();

            return Ok("Período académico actualizado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var periodo = await _context.PeriodosAcademicos.FindAsync(id);

            if (periodo == null)
                return NotFound();

            _context.PeriodosAcademicos.Remove(periodo);

            await _context.SaveChangesAsync();

            return Ok("Período académico eliminado.");
        }
    }
}