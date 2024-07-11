using ReservaProject.Domain.Contrats;
using ReservaProject.Domain.Entities;
using ReservaProject.DTo;
using ReservaProject.Helpers;
using ReservaProject.Infraestructura.Context;

namespace ReservaProject.Domain
{
    public class ServicioReservadoDomain:IServicioReservadoDomain
    {
        private readonly ReservasContext _context;
        public ServicioReservadoDomain(ReservasContext context)
        {
            _context = context;
        }

        public List<ReservaGetDTO> GetReservas(DateTime? fechaInicio, DateTime? fechaFin, int? servicioId, int? clienteId, int? reservaId, int? habitacionId, int? mesaId)
        {
            var query = _context.Reservas.AsQueryable().Where(r => r.Estado);

            if (reservaId.HasValue)
                query = query.Where(r => r.Id == reservaId);

            if (fechaInicio.HasValue)
                query = query.Where(r => r.FechaInicio >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(r => r.FechaInicio <= fechaFin.Value);

            if (servicioId.HasValue)
                query = query.Where(r => r.ServicioReservados.Any(sr => sr.Id == servicioId.Value));

            if (clienteId.HasValue)
                query = query.Where(r => r.Cliente == clienteId.Value);

            if (habitacionId.HasValue)
                query = query.Where(r => r.ServicioReservados.Any(sr => sr.Habitacion == habitacionId.Value));

            if (mesaId.HasValue)
                query = query.Where(r => r.ServicioReservados.Any(sr => sr.Mesa == mesaId.Value));

          

            var reservas =  query
             .Select(r => new ReservaGetDTO
             {
                 Id = r.Id,
                 FechaInicio = r.FechaInicio,
                 FechaFin = r.FechaFin,
                 Cliente = new ClienteDTO
                 {
                     Id = r.ClienteNavigation.Id,
                     Nombre = r.ClienteNavigation.Nombre,
                     Email = r.ClienteNavigation.Email,
                     Telefono = r.ClienteNavigation.Telefono
                 },
                 ServiciosReservados = r.ServicioReservados.Where(s => s.Estado).Select(sr => new ServicioDTO
                 {
                     Id = sr.ServicioNavigation.Id,
                     NombreServicio = sr.ServicioNavigation.Nombre,
                     PrecioReal = sr.PrecioReal,
                     FechaServicio = sr.ServicioNavigation.FechaCreacion,
                     Habitacion = sr.Habitacion == null ? null : new HabitacionDTO
                     {
                         Id = sr.HabitacionNavigation.Id,
                         NumeroCuarto = sr.HabitacionNavigation.NumeroCuarto,
                         NumeroCamas = sr.HabitacionNavigation.NumeroCamas,
                         NumeroBanos = sr.HabitacionNavigation.NumeroBanos
                     },
                     Mesa = sr.Mesa == null ? null : new MesaDTO
                     {
                         Id = sr.MesaNavigation.Id,
                         NumeroMesa = sr.MesaNavigation.NumeroMesa,
                         NumeroSillas = sr.MesaNavigation.NumeroSillas
                     }
                 }).ToList()
             })
             .ToList();



            return reservas;
        }

        public int AddReserva(ReservaSetDTO crearReservaDto)
        {

            var reserva = new Reserva
            {
                FechaInicio = crearReservaDto.FechaInicio ?? DateTime.Now,
                FechaFin = crearReservaDto.FechaFin,
                FechaCreacion = DateTime.Now,
                Cliente = crearReservaDto.ClienteId,
                Estado = true
            };

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            foreach (var servicio in crearReservaDto.ServiciosReservados)
            {
                var servicioReservado = new ServicioReservado
                {
                    Reserva = reserva.Id,
                    Servicio = servicio.ServicioId,
                    Habitacion = servicio.HabitacionId,
                    Mesa = servicio.MesaId,
                    PrecioReal = servicio.PrecioReal,
                    Estado = true
                };
                _context.ServicioReservados.Add(servicioReservado);
            }

            _context.SaveChanges();

            return reserva.Id;
        }

        public bool UpdateReserva(ReservaSetDTO reservaUpdate)
        {
            Reserva reservaExistente = GetReservaExistById(reservaUpdate.Id);
            if (reservaExistente == null)
                return false;

            // Actualizar registros
            _context.Entry(reservaExistente).CurrentValues.SetValues(reservaUpdate);

            var serviciosExistentes = _context.ServicioReservados.Where(sr => sr.Reserva == reservaExistente.Id).ToList();
            foreach (var servicioExistente in serviciosExistentes)
            {
                servicioExistente.Estado = false; 
            }

            // Aplicar los cambios a la base de datos
            _context.SaveChanges();


            foreach (var servicio in reservaUpdate.ServiciosReservados)
            {
                var servicioReservado = new ServicioReservado
                {
                    Reserva = reservaExistente.Id,
                    Servicio = servicio.ServicioId,
                    Habitacion = servicio.HabitacionId,
                    Mesa = servicio.MesaId,
                    PrecioReal = servicio.PrecioReal,
                    Estado = true
                };
                _context.ServicioReservados.Add(servicioReservado);
            }

            _context.SaveChanges();

            return true;
        }


        public bool CancelarReserva(ReservaSetDTO reservaUpdate)
        {
            Reserva reservaExistente = GetReservaExistById(reservaUpdate.Id);
            if (reservaExistente == null)
                return false;

            reservaExistente.Estado = false;
            // Actualizar registros
            _context.Entry(reservaExistente).CurrentValues.SetValues(reservaExistente);

            // Aplicar los cambios a la base de datos
            _context.SaveChanges();

            return true;
        }

        public List<ServicioDTO> GetServicio()
        {
            var query = _context.Servicios.AsQueryable().Where(r => r.Estado);


            var servicio = query
             .Select(r => new ServicioDTO
             {
                 Id = r.Id,
                 NombreServicio = r.Nombre,
                 PrecioReal = r.Precio,
                 TipoServicio = new TipoServicioDTO
                 {
                     Id = r.TipoServicioNavigation.Id,
                     Nombre = r.TipoServicioNavigation.Nombre,
                     IsHabitacion = r.TipoServicioNavigation.IsHabitacion,
                     IsRestaurante = r.TipoServicioNavigation.IsRestaurante
                 }

             })
             .ToList();

            return servicio;
        }

        public List<HabitacionDTO> GetHabitacion()
        {
            var query = _context.Habitacions.AsQueryable().Where(r => r.Estado);


            var habitacion = query
             .Select(r => new HabitacionDTO
             {
                 Id = r.Id,
                 NumeroCuarto = r.NumeroCuarto,
                 NumeroCamas = r.NumeroCamas,
                 Precio = r.Precio,

             })
             .ToList();

            return habitacion;
        }

        public List<MesaDTO> GetMesa()
        {
            var query = _context.Mesas.AsQueryable().Where(r => r.Estado);


            var mesa = query
             .Select(r => new MesaDTO
             {
                 Id = r.Id,
                 NumeroMesa = r.NumeroMesa,
                 NumeroSillas = r.NumeroSillas,

             })
             .ToList();

            return mesa;
        }

        private Reserva GetReservaExistById(int reservaId)
        {
            return _context.Reservas.FirstOrDefault(x => x.Id.Equals(reservaId));

        }


    }
}
