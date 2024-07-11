using ReservaProject.Domain.Entities;
using ReservaProject.DTo;
using ReservaProject.Helpers;

namespace ReservaProject.Domain.Contrats
{
    public interface IServicioReservadoDomain
    {
        List<ReservaGetDTO> GetReservas(DateTime? fechaInicio, DateTime? fechaFin, int? servicioId, int? clienteId, int? reservaId, int? habitacionId, int? mesaId);

        int AddReserva(ReservaSetDTO crearReservaDto);
        bool UpdateReserva(ReservaSetDTO reservaSetDTO);
        bool CancelarReserva(ReservaSetDTO reservaSetDTO);
        List<ServicioDTO> GetServicio();
        List<HabitacionDTO> GetHabitacion();
        List<MesaDTO> GetMesa();
    }
}
