using APIBiblioteca.Models;

namespace APIBiblioteca.Services.IServices
{
    public interface ITokenService
    {
        string GenerarToken (Usuario usuario);
    }
}
