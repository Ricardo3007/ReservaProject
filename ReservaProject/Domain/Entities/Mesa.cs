using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class Mesa
{
    public int Id { get; set; }

    public string NumeroMesa { get; set; } = null!;

    public int NumeroSillas { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<ServicioReservado> ServicioReservados { get; set; } = new List<ServicioReservado>();
}
