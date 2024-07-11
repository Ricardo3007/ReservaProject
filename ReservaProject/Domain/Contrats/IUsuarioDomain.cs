using ReservaProject.DTo;

namespace ReservaProject.Domain.Contrats
{
    public interface IUsuarioDomain
    {
        bool GetUsuario(string nombre, string password);

        List<ClienteDTO> GetCliente();
    }
}
