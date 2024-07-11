using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class Habitacion
{
    public int Id { get; set; }

    public string NumeroCuarto { get; set; } = null!;

    public int NumeroCamas { get; set; }

    public int NumeroPersonas { get; set; }

    public int? NumeroBanos { get; set; }

    public decimal Precio { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<ServicioReservado> ServicioReservados { get; set; } = new List<ServicioReservado>();
}
