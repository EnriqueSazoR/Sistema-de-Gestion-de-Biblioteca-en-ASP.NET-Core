using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using APIBiblioteca.Services.IServices;
using APIBiblioteca.DTO.Autenticacion;
using APIBiblioteca.DTO.Roles;

namespace APIBiblioteca.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutenticacionRepository _authrepository;
        private readonly ITokenService _tokenservice;

        public AuthController(IAutenticacionRepository authrepository, ITokenService tokenService)
        {
            _authrepository = authrepository;
            _tokenservice = tokenService;
        }

        // Enpoint para login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            var usuario = await _authrepository.InicioSesion(loginDTO.Correo, loginDTO.Password);

            if (usuario == null)
            {
                return Unauthorized("Credenciales Inválidas");
            }

            var token = _tokenservice.GenerarToken(usuario);
            return Ok(new { Token = token });
        }

        // Enpoint para registro
        [HttpPost("registro")]
        public async Task<ActionResult<Usuario>> Registro([FromBody] UsuarioRegistroDTO usuarioRegistroDTO)
        {

            try
            {
                var usuario = new Usuario
                {
                    NombreUsuario = usuarioRegistroDTO.NombreUsuario,
                    Correo = usuarioRegistroDTO.Correo,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuarioRegistroDTO.Password)

                };

                await _authrepository.Registro(usuario);

                var token = _tokenservice.GenerarToken(usuario);
                

                var respuestaDTO = new RespuestaUsuarioDTO
                {
                    NombreUsuario = usuario.NombreUsuario,
                    Correo = usuario.Correo
                };

                return Ok(new
                {
                    mensaje = "Registro Exitoso",
                    token = token
                });

            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        // Endpoint para asignar rol
        [Authorize(Roles = "Admin")]
        [HttpPost("asignar-rol")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDTO rolDTO)
        {
            var resultado = await _authrepository.AsignarRol(rolDTO.UsuarioID, rolDTO.RolId);

            if (!resultado)
            {
                return BadRequest("No se pudo asignar el rol. Verifica si el usuario y el rol existen, o si ya tiene ese rol.");
            }

            return Ok("Rol asignado correctamente");
        }
    }
}
