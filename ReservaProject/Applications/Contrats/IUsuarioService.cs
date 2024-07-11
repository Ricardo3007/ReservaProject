using ReservaProject.DTo;
using ReservaProject.Helpers;

namespace ReservaProject.Applications.Contrats
{
    public interface IUsuarioService
    {
        Request<bool> GetUsuario(string nombre, string password);
        Request<List<ClienteDTO>> GetCliente();
    }
}
