using Microsoft.IdentityModel.Tokens;
using ReservaProject.Applications.Contrats;
using ReservaProject.Domain;
using ReservaProject.Domain.Contrats;
using ReservaProject.DTo;
using ReservaProject.Helpers;

namespace ReservaProject.Applications
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDomain _usuarioDomain;
        private readonly ILogger<UsuarioService> _logger;
        public UsuarioService(IUsuarioDomain usuarioDomain, ILogger<UsuarioService> logger) { 
            _usuarioDomain = usuarioDomain;
            _logger = logger;
        }
        public Request<bool> GetUsuario(string nombre, string password)
        {
            try
            {

                if (nombre.IsNullOrEmpty() || password.IsNullOrEmpty())
                {
                    return Request<bool>.NoSucces("Usuario o contraseña incorrecta.");
                }

                bool usuarioDTO = _usuarioDomain.GetUsuario(nombre, password);
                if (!usuarioDTO)

                {
                    return Request<bool>.NoSucces("Usuario o contraseña incorrecta.");
                }

                return Request<bool>.Succes(usuarioDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en GetUsuario" + ex);
                return Request<bool>.Error(ex.ToString());
            }
        }


        public Request<List<ClienteDTO>> GetCliente()
        {
            try
            {

                List<ClienteDTO> clienteDTO = _usuarioDomain.GetCliente();
                if (clienteDTO.Count == 0)
                {
                    return Request<List<ClienteDTO>>.NoSucces("No existen Registros");
                }

                return Request<List<ClienteDTO>>.Succes(clienteDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en clienteDTO" + ex);
                return Request<List<ClienteDTO>>.Error(ex.ToString());
            }
        }

    }
}
