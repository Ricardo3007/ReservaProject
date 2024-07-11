namespace ReservaProject.DTo
{


    public class ReservaGetDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public ClienteDTO Cliente { get; set; }
        public List<ServicioDTO> ServiciosReservados { get; set; }
    }

    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }

    public class ServicioDTO
    {
        public int Id { get; set; }
        public string NombreServicio { get; set; }
        public decimal? PrecioReal { get; set; }
        public DateTime FechaServicio { get; set; }
        public HabitacionDTO Habitacion { get; set; }
        public MesaDTO Mesa { get; set; }
        public TipoServicioDTO TipoServicio { get; set; }
    }

    public class HabitacionDTO
    {
        public int Id { get; set; }
        public string NumeroCuarto { get; set; }
        public int NumeroCamas { get; set; }
        public int? NumeroBanos { get; set; }
        public decimal Precio { get; set; }
    }

    public class MesaDTO
    {
        public int Id { get; set; }
        public string NumeroMesa { get; set; }
        public int NumeroSillas { get; set; }
    }

    public class TipoServicioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool IsHabitacion { get; set; }

        public bool IsRestaurante { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Estado { get; set; }
    }


}
