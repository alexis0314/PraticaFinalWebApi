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
    public class SeccionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Secciones.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var seccion = await _context.Secciones.FindAsync(id);

            if (seccion == null)
                return NotFound();

            return Ok(seccion);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Seccion seccion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Secciones.Add(seccion);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = seccion.Id }, seccion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Seccion seccion)
        {
            if (id != seccion.Id)
                return BadRequest();

            var seccionBD = await _context.Secciones.FindAsync(id);

            if (seccionBD == null)
                return NotFound();

            seccionBD.Nombre = seccion.Nombre;

            await _context.SaveChangesAsync();

            return Ok("Sección actualizada.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var seccion = await _context.Secciones.FindAsync(id);

            if (seccion == null)
                return NotFound();

            _context.Secciones.Remove(seccion);

            await _context.SaveChangesAsync();

            return Ok("Sección eliminada.");
        }
    }
}