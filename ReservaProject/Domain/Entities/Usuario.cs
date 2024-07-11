using System;
using System.Collections.Generic;

namespace ReservaProject.Domain.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Password { get; set; }

    public bool IsAdmin { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
