using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int TipoServicio { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<ServicioReservado> ServicioReservados { get; set; } = new List<ServicioReservado>();

    public virtual TipoServicio TipoServicioNavigation { get; set; } = null!;
}
