using System;

namespace ComprasVentas.Models;

public class Rol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    // Relacion many to many
    public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();

    public ICollection<Usuario> Usuario { get; set; } = [];
}  
