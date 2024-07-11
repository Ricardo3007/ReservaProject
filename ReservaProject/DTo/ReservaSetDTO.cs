namespace ReservaProject.DTo
{

    public class ReservaSetDTO
    {
        public int Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int ClienteId { get; set; }
        public bool Estado { get; set; } 
        public List<CrearServicioReservadoDTO> ServiciosReservados { get; set; }
    }

    public class CrearServicioReservadoDTO
    {
        public int ServicioId { get; set; }
        public int? HabitacionId { get; set; }
        public int? MesaId { get; set; }
        public decimal PrecioReal { get; set; }
    }
}
