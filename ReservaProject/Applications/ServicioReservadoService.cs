using AutoMapper;
using ReservaProject.Applications.Contrats;
using ReservaProject.Domain.Contrats;
using ReservaProject.Domain.Entities;
using ReservaProject.DTo;
using ReservaProject.Helpers;

namespace ReservaProject.Applications
{
    public class ServicioReservadoService : IServicioReservadoService
    {
        private readonly IServicioReservadoDomain _servicioReservadoDomain;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicioReservadoService> _logger;

        public ServicioReservadoService(IServicioReservadoDomain servicioReservadoDomain, IMapper mapper, ILogger<ServicioReservadoService> logger) { 
            _servicioReservadoDomain = servicioReservadoDomain;
            _mapper = mapper;
            _logger = logger;
        }

        public Request<List<ReservaGetDTO>> GetReservas(DateTime? fechaInicio, DateTime? fechaFin, int? servicioId, int? clienteId, int? reservaId, int? habitacionId, int? mesaId)
        {
            try
            {

                List<ReservaGetDTO> reservaDTO = _servicioReservadoDomain.GetReservas(fechaInicio, fechaFin, servicioId, clienteId, reservaId, habitacionId, mesaId);
                if (reservaDTO.Count == 0)
                {
                    return Request<List<ReservaGetDTO>>.NoSucces("No existen Registros");
                }

                return Request<List<ReservaGetDTO>>.Succes(reservaDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en GetReservas" + ex);
                return Request<List<ReservaGetDTO>>.Error(ex.ToString());
            }
        }

        public Request<List<ReservaGetDTO>> AddReserva(ReservaSetDTO reservaSetDTO)
        {
            try
            {
                if (reservaSetDTO.ClienteId == 0 || reservaSetDTO.FechaInicio == null)
                {
                    return Request<List<ReservaGetDTO>>.NoSucces("Existen campos obligatorios por llenar");
                }

                int rstaReserva = _servicioReservadoDomain.AddReserva(reservaSetDTO);
                if (rstaReserva == 0)

                {
                    return Request<List<ReservaGetDTO>>.NoSucces("No se pudo insertar el regsitro.");
                }

                List<ReservaGetDTO> reservaGetDTO = _servicioReservadoDomain.GetReservas(null, null, null, null, rstaReserva, null, null);

                return Request<List<ReservaGetDTO>>.Succes(reservaGetDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en AddReserva " + ex);
                return Request<List<ReservaGetDTO>>.Error(ex.ToString());
            }

        }

        public Request<List<ReservaGetDTO>> UpdateReserva(ReservaSetDTO reservaSetDTO)
        {
            try
            {
                if (reservaSetDTO.ClienteId == 0 || reservaSetDTO.FechaInicio == null)
                {
                    return Request<List<ReservaGetDTO>>.NoSucces("Existen campos obligatorios por llenar");
                }

                //Reserva reserva = _mapper.Map<Reserva>(reservaSetDTO);
                bool rstaReserva = _servicioReservadoDomain.UpdateReserva(reservaSetDTO);
                if (!rstaReserva)

                {
                    return Request<List<ReservaGetDTO>>.NoSucces("No se pudo actualizar el regsitro.");
                }

                List<ReservaGetDTO> reservaGetDTO = _servicioReservadoDomain.GetReservas(null, null, null, null, reservaSetDTO.Id, null, null);

                return Request<List<ReservaGetDTO>>.Succes(reservaGetDTO);


            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en UpdateReserva " + ex);
                return Request<List<ReservaGetDTO>>.Error(ex.ToString());
            }
        }



       

        public Request<List<ServicioDTO>> GetServicio()
        {
            try
            {

                List<ServicioDTO> servicioDTO = _servicioReservadoDomain.GetServicio();
                if (servicioDTO.Count == 0)
                {
                    return Request<List<ServicioDTO>>.NoSucces("No existen Registros");
                }

                return Request<List<ServicioDTO>>.Succes(servicioDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en GetServicio" + ex);
                return Request<List<ServicioDTO>>.Error(ex.ToString());
            }
        }

        public Request<List<HabitacionDTO>> GetHabitacion()
        {
            try
            {

                List<HabitacionDTO> habitacionDTO = _servicioReservadoDomain.GetHabitacion();
                if (habitacionDTO.Count == 0)
                {
                    return Request<List<HabitacionDTO>>.NoSucces("No existen Registros");
                }

                return Request<List<HabitacionDTO>>.Succes(habitacionDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en HabitacionDTO" + ex);
                return Request<List<HabitacionDTO>>.Error(ex.ToString());
            }
        }

        public Request<List<MesaDTO>> GetMesa()
        {
            try
            {

                List<MesaDTO> mesaDTO = _servicioReservadoDomain.GetMesa();
                if (mesaDTO.Count == 0)
                {
                    return Request<List<MesaDTO>>.NoSucces("No existen Registros");
                }

                return Request<List<MesaDTO>>.Succes(mesaDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en MesaDTO" + ex);
                return Request<List<MesaDTO>>.Error(ex.ToString());
            }
        }

        public Request<bool> CancelarReserva(ReservaSetDTO reservaSetDTO)
        {
            try
            {
               

                bool rstaReserva = _servicioReservadoDomain.CancelarReserva(reservaSetDTO);
                if (!rstaReserva)

                {
                    return Request<bool>.NoSucces("No se pudo cancelar el regsitro.");
                }

                return Request<bool>.Succes(rstaReserva);


            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en CancelarReserva " + ex);
                return Request<bool>.Error(ex.ToString());
            }
        }
    }
}
