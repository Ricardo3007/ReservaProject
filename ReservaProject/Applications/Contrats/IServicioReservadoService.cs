using ReservaProject.DTo;
using ReservaProject.Helpers;

namespace ReservaProject.Applications.Contrats
{
    public interface IServicioReservadoService
    {
        Request<List<ReservaGetDTO>> GetReservas(DateTime? fechaInicio, DateTime? fechaFin, int? servicioId, int? clienteId, int? reservaId, int? habitacionId, int? mesaId);
        Request<List<ReservaGetDTO>> AddReserva(ReservaSetDTO movCanguroDTO);
        Request<List<ReservaGetDTO>> UpdateReserva(ReservaSetDTO movCanguroDTO);
        Request<bool> CancelarReserva(ReservaSetDTO reservaSetDTO);
        Request<List<ServicioDTO>> GetServicio();
        Request<List<HabitacionDTO>> GetHabitacion();
        Request<List<MesaDTO>> GetMesa();
    }
}
