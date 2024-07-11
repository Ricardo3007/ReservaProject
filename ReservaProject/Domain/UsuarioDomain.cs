using Microsoft.EntityFrameworkCore;
using ReservaProject.Domain.Contrats;
using ReservaProject.DTo;
using ReservaProject.Infraestructura.Context;

namespace ReservaProject.Domain
{
    public class UsuarioDomain : IUsuarioDomain
    {
        private readonly ReservasContext _context;
        public UsuarioDomain(ReservasContext context) {
            _context = context; 
        }

        public bool GetUsuario(string nombre, string password)
        {
            return _context.Usuarios.Any(x => x.Estado == true && x.Nombre == nombre && x.Password == password);

        }

        public List<ClienteDTO> GetCliente()
        {
            var query = _context.Clientes.AsQueryable().Where(r => r.Estado);


            var cliente = query
             .Select(r => new ClienteDTO
             {
                 Id = r.Id,
                 Nombre = r.Nombre,
                 Email = r.Email,

             })
             .ToList();

            return cliente;
        }

    }
}
