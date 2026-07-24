using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaFinalWebApi.Data;
using PracticaFinalWebApi.DTOs;
using PracticaFinalWebApi.Helpers;
using PracticaFinalWebApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticaFinalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        // GET: api/<UsuariosController>
       
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Correo = u.Correo
                })
                .ToListAsync();

            return Ok(usuarios);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Correo = u.Correo
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            return Ok(usuario);
        }
        private readonly JwtHelper _jwtHelper;
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        // Registrar usuario
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar(RegistroUsuarioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool existe = await _context.Usuarios
                .AnyAsync(u => u.Correo == dto.Correo);

            if (existe)
                return BadRequest("El correo ya está registrado.");

            Usuario usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Password = dto.Password
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u =>
                u.Correo == dto.Correo &&
                u.Password == dto.Password);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }

            var token = _jwtHelper.GenerarToken(usuario);

            return Ok(new
            {
                token = token
            });
        }

        // PUT api/<UsuariosController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, RegistroUsuarioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            usuario.Nombre = dto.Nombre;
            usuario.Correo = dto.Correo;
            usuario.Password = dto.Password;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario actualizado correctamente."
            });
        }
        // DELETE api/<UsuariosController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario eliminado correctamente."
            });
        }
    }
}
