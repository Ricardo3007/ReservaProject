using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int Usuario { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
