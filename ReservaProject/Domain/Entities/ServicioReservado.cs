using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class ServicioReservado
{
    public int Id { get; set; }

    public int? Reserva { get; set; }

    public int? Servicio { get; set; }

    public int? Habitacion { get; set; }

    public int? Mesa { get; set; }

    public decimal? PrecioReal { get; set; }

    public bool Estado { get; set; }

    public virtual Habitacion? HabitacionNavigation { get; set; }

    public virtual Mesa? MesaNavigation { get; set; }

    public virtual Reserva? ReservaNavigation { get; set; }

    public virtual Servicio? ServicioNavigation { get; set; }
}
