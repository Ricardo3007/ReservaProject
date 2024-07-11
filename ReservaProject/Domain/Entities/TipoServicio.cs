using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class TipoServicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool IsHabitacion { get; set; }

    public bool IsRestaurante { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
