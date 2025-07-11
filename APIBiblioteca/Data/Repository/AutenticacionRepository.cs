using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Data.Repository
{
    public class AutenticacionRepository : IAutenticacionRepository
    {
        private readonly ApplicationDBContext _db;

        public AutenticacionRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> AsignarRol(int usuarioId, int rolId)
        {
            // Buscamos al usuario por el ID y cargarmos sus roles actuales
            var usuario = await _db.Usuarios
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null) return false;

            // Buscamos el rol por el ID
            var rol = await _db.Roles.FirstOrDefaultAsync(u => u.Id == rolId);
            if (rol == null) return false;

            // Verifacamos si el usuario ya tiene ese rol
            if(usuario.Roles.Any(r => r.Id == rolId)) return false;

            usuario.Roles.Add(rol);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> Registro(Usuario usuario)
        {
            var rolCliente = await _db.Roles.FirstOrDefaultAsync(r => r.NombreRol == "Cliente");
            
            if(await _db.Usuarios.AnyAsync(u => u.Correo == usuario.Correo))
            {
                throw new Exception("El correo eléctronico ya está asociado a una cuenta");
            }

            if (rolCliente == null)
            {
                throw new Exception("El rol 'Cliente' no existe en la base de datos");
            }
            usuario.Roles = new List<Rol> { rolCliente };

            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> InicioSesion(string correo, string password)
        {
            var usuario = await _db.Usuarios
                .Include(r => r.Roles)
                .FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null)
            {
                return null;
            }

            // Comparamos contraseñas
            bool passwordValida = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);

            if (!passwordValida)
            {
                return null;
            }

            return usuario;
        }
    }
}
