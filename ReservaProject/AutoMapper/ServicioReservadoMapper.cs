using AutoMapper;
using ReservaProject.Domain.Entities;
using ReservaProject.DTo;

namespace ReservaProject.AutoMapper
{
    public class ServicioReservadoMapper:Profile
    {
        public ServicioReservadoMapper()
        {
            CreateMap<Reserva, ReservaGetDTO>().ReverseMap();
        }
    }
}
