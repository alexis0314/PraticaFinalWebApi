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
    public class CalificacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalificacionesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]  
        public async Task<IActionResult> Get()
        {
            var calificaciones = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Include(c => c.Materia)
                .Include(c => c.PeriodoAcademico)
                .Include(c => c.TipoEvaluacion)
                .ToListAsync();

            return Ok(calificaciones);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var calificacion = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Include(c => c.Materia)
                .Include(c => c.PeriodoAcademico)
                .Include(c => c.TipoEvaluacion)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (calificacion == null)
                return NotFound("Calificación no encontrada.");

            return Ok(calificacion);
        }
        [HttpGet("Estudiante/{estudianteId}")]
        public async Task<IActionResult> GetPorEstudiante(int estudianteId)
        {
            var calificaciones = await _context.Calificaciones
                .Include(c => c.Materia)
                .Include(c => c.PeriodoAcademico)
                .Include(c => c.TipoEvaluacion)
                .Where(c => c.EstudianteId == estudianteId)
                .ToListAsync();

            return Ok(calificaciones);
        }
        // GET: api/Calificaciones/Historial/5
        [HttpGet("Historial/{estudianteId}")]
        public async Task<IActionResult> Historial(int estudianteId)
        {
            var historial = await _context.Calificaciones
                .Where(c => c.EstudianteId == estudianteId)
                .Include(c => c.Materia)
                .Include(c => c.PeriodoAcademico)
                .Include(c => c.TipoEvaluacion)
                .Select(c => new
                {
                    c.Id,
                    Materia = c.Materia!.Nombre,
                    Periodo = c.PeriodoAcademico!.Nombre,
                    TipoEvaluacion = c.TipoEvaluacion!.Nombre,
                    c.Calificacion1,
                    c.Calificacion2,
                    c.Calificacion3,
                    c.Calificacion4,
                    c.Examen,
                    c.TotalCalificacion,
                    c.Clasificacion,
                    c.Estado
                })
                .ToListAsync();

            if (!historial.Any())
                return NotFound("El estudiante no posee calificaciones.");

            return Ok(historial);
        }
        // GET: api/Calificaciones/Aprobados
        [HttpGet("Aprobados")]
        public async Task<IActionResult> Aprobados()
        {
            var lista = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Include(c => c.Materia)
                .Where(c => c.Estado == "Aprobado")
                .Select(c => new
                {
                    Estudiante = c.Estudiante!.Nombre,
                    Materia = c.Materia!.Nombre,
                    c.TotalCalificacion,
                    c.Clasificacion
                })
                .ToListAsync();

            return Ok(lista);
        }
        // GET: api/Calificaciones/Reprobados
        [HttpGet("Reprobados")]
        public async Task<IActionResult> Reprobados()
        {
            var lista = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Include(c => c.Materia)
                .Where(c => c.Estado == "Reprobado")
                .Select(c => new
                {
                    Estudiante = c.Estudiante!.Nombre,
                    Materia = c.Materia!.Nombre,
                    c.TotalCalificacion,
                    c.Clasificacion
                })
                .ToListAsync();

            return Ok(lista);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Calificacion calificacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!await _context.Estudiantes.AnyAsync(e => e.Id == calificacion.EstudianteId))
                return BadRequest("El estudiante no existe.");

            
            if (!await _context.Materias.AnyAsync(m => m.Id == calificacion.MateriaId))
                return BadRequest("La materia no existe.");

            
            if (!await _context.PeriodosAcademicos.AnyAsync(p => p.Id == calificacion.PeriodoAcademicoId))
                return BadRequest("El período académico no existe.");
      
            if (!await _context.TiposEvaluacion.AnyAsync(t => t.Id == calificacion.TipoEvaluacionId))
                return BadRequest("El tipo de evaluación no existe.");
            
            decimal promedio =
                (calificacion.Calificacion1 +
                 calificacion.Calificacion2 +
                 calificacion.Calificacion3 +
                 calificacion.Calificacion4) / 4;
 
            calificacion.TotalCalificacion =
                (promedio * 0.70m) +
                (calificacion.Examen * 0.30m);

            // Clasificación
            if (calificacion.TotalCalificacion >= 90)
                calificacion.Clasificacion = "A";
            else if (calificacion.TotalCalificacion >= 80)
                calificacion.Clasificacion = "B";
            else if (calificacion.TotalCalificacion >= 70)
                calificacion.Clasificacion = "C";
            else
                calificacion.Clasificacion = "F";

            // Estado
            calificacion.Estado =
                calificacion.TotalCalificacion >= 70
                ? "Aprobado"
                : "Reprobado";

            _context.Calificaciones.Add(calificacion);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = calificacion.Id }, calificacion);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Calificacion calificacion)
        {
            if (id != calificacion.Id)
                return BadRequest("El Id no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var calificacionBD = await _context.Calificaciones.FindAsync(id);

            if (calificacionBD == null)
                return NotFound("La calificación no existe.");

            // Verificar relaciones
            if (!await _context.Estudiantes.AnyAsync(e => e.Id == calificacion.EstudianteId))
                return BadRequest("El estudiante no existe.");

            if (!await _context.Materias.AnyAsync(m => m.Id == calificacion.MateriaId))
                return BadRequest("La materia no existe.");

            if (!await _context.PeriodosAcademicos.AnyAsync(p => p.Id == calificacion.PeriodoAcademicoId))
                return BadRequest("El período académico no existe.");

            if (!await _context.TiposEvaluacion.AnyAsync(t => t.Id == calificacion.TipoEvaluacionId))
                return BadRequest("El tipo de evaluación no existe.");

            // Actualizar datos
            calificacionBD.EstudianteId = calificacion.EstudianteId;
            calificacionBD.MateriaId = calificacion.MateriaId;
            calificacionBD.PeriodoAcademicoId = calificacion.PeriodoAcademicoId;
            calificacionBD.TipoEvaluacionId = calificacion.TipoEvaluacionId;

            calificacionBD.Calificacion1 = calificacion.Calificacion1;
            calificacionBD.Calificacion2 = calificacion.Calificacion2;
            calificacionBD.Calificacion3 = calificacion.Calificacion3;
            calificacionBD.Calificacion4 = calificacion.Calificacion4;
            calificacionBD.Examen = calificacion.Examen;

            // Recalcular promedio
            decimal promedio =
                (calificacionBD.Calificacion1 +
                 calificacionBD.Calificacion2 +
                 calificacionBD.Calificacion3 +
                 calificacionBD.Calificacion4) / 4;

            // Recalcular nota final
            calificacionBD.TotalCalificacion =
                (promedio * 0.70m) +
                (calificacionBD.Examen * 0.30m);

            // Recalcular clasificación
            if (calificacionBD.TotalCalificacion >= 90)
                calificacionBD.Clasificacion = "A";
            else if (calificacionBD.TotalCalificacion >= 80)
                calificacionBD.Clasificacion = "B";
            else if (calificacionBD.TotalCalificacion >= 70)
                calificacionBD.Clasificacion = "C";
            else
                calificacionBD.Clasificacion = "F";

            // Recalcular estado
            calificacionBD.Estado =
                calificacionBD.TotalCalificacion >= 70
                ? "Aprobado"
                : "Reprobado";

            await _context.SaveChangesAsync();

            return Ok("Calificación actualizada correctamente.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);

            if (calificacion == null)
                return NotFound("La calificación no existe.");

            _context.Calificaciones.Remove(calificacion);

            await _context.SaveChangesAsync();

            return Ok("Calificación eliminada correctamente.");
        }
    }
}