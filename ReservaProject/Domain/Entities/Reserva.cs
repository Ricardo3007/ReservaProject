using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class Reserva
{
    public int Id { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? Cliente { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual Cliente? ClienteNavigation { get; set; }

    public virtual ICollection<ServicioReservado> ServicioReservados { get; set; } = new List<ServicioReservado>();
}
