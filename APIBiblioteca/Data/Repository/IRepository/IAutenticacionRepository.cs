using APIBiblioteca.Models;

namespace APIBiblioteca.Data.Repository.IRepository
{
    public interface IAutenticacionRepository
    {
        Task<Usuario> Registro (Usuario usuario);
        Task<Usuario> InicioSesion (string correo, string password);
        Task<bool> AsignarRol(int usuarioId, int rolId);
    }
}
