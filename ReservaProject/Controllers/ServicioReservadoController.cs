
using Microsoft.AspNetCore.Mvc;
using ReservaProject.Applications.Contrats;
using ReservaProject.DTo;
using ReservaProject.Helpers;

namespace ReservaProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ServicioReservadoController : ControllerBase
    {
        private readonly IServicioReservadoService _servicioReservadoService;

        public ServicioReservadoController(IServicioReservadoService servicioReservadoService)
        {
            _servicioReservadoService = servicioReservadoService;
        }


        /// <summary>
        /// consultar reservas
        /// </summary>
        [HttpGet("[action]")]
        public Request<List<ReservaGetDTO>> GetReservas(DateTime? fechaInicio, DateTime? fechaFin, int? servicioId, int? clienteId, int? reservaId, int? habitacionId, int? mesaId)
        {
            return _servicioReservadoService.GetReservas(fechaInicio, fechaFin, servicioId, clienteId, reservaId, habitacionId, mesaId);
        }


        /// <summary>
        /// agregar reservas
        /// </summary>
        [HttpPost("[action]")]
        public Request<List<ReservaGetDTO>> AddReserva(ReservaSetDTO reservaSetDTO)
        {
            return _servicioReservadoService.AddReserva(reservaSetDTO);
        }


        /// <summary>
        /// Actualizar reserva
        /// </summary>
        [HttpPut("[action]")]
        public Request<List<ReservaGetDTO>> UpdateReserva(ReservaSetDTO reservaSetDTO)
        {
            return _servicioReservadoService.UpdateReserva(reservaSetDTO);
        }

        /// <summary>
        /// Cancelar reserva
        /// </summary>
        [HttpPut("[action]")]
        public Request<bool> CancelarReserva(ReservaSetDTO reservaSetDTO)
        {
            return _servicioReservadoService.CancelarReserva(reservaSetDTO);
        }

        /// <summary>
        /// consultar servicio
        /// </summary>
        [HttpGet("[action]")]
        public Request<List<ServicioDTO>> GetServicio()
        {
            return _servicioReservadoService.GetServicio();
        }

        /// <summary>
        /// consultar Habitacion
        /// </summary>
        [HttpGet("[action]")]
        public Request<List<HabitacionDTO>> GetHabitacion()
        {
            return _servicioReservadoService.GetHabitacion();
        }

        /// <summary>
        /// consultar Mesa
        /// </summary>
        [HttpGet("[action]")]
        public Request<List<MesaDTO>> GetMesa()
        {
            return _servicioReservadoService.GetMesa();
        }

    }
}
