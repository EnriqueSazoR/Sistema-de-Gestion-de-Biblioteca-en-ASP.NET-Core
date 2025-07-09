using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using APIBiblioteca.Services.IServices;

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
            return Ok(new {Token = token});
        }

        // Enpoint para registro
        [HttpPost("regitro")]
        public async Task<ActionResult<Usuario>> Registro([FromBody] UsuarioRegistroDTO usuarioRegistroDTO)
        {

            var usuario = new Usuario
            {
                NombreUsuario = usuarioRegistroDTO.NombreUsuario,
                Correo = usuarioRegistroDTO.Correo,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuarioRegistroDTO.Password)

            };
            
            await _authrepository.Registro(usuario);

            var respuestaDTO = new RespuestaUsuarioDTO
            {
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo
            };

            return Ok(respuestaDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("asignar-rol")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDTO rolDTO)
        {
            var resultado = await _authrepository.AsignarRol(rolDTO.UsuarioID, rolDTO.RolId);

            if(!resultado)
            {
                return BadRequest("No se pudo asignar el rol. Verifica si el usuario y el rol existen, o si ya tiene ese rol.");
            }

            return Ok("Rol asignado correctamente");
        }






        // Clases Dto
        public class UsuarioRegistroDTO
        {
            [Required(ErrorMessage = "El nombre de ususario es obligatorio")]
            public string NombreUsuario { get; set; }

            [Required]
            [EmailAddress(ErrorMessage = "El correo no tiene el formato válido")]
            public string Correo { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria")]
            public string Password { get; set; }

        }

        public class RespuestaUsuarioDTO
        {
            public string NombreUsuario { get; set; }
            public string Correo { get; set; }
        }

        public class AsignarRolDTO
        {
            public int UsuarioID { get; set; }
            public int RolId { get; set; }
        }

        public class UsuarioLoginDTO
        {
            [Required]
            [EmailAddress(ErrorMessage = "El campo email no cumple con el formato válido")]
            public string Correo { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria")]
            public string Password { get; set; }
        }
    }
}
