using System;

namespace ComprasVentas.Dto;

public class RolDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    // Relacion many to many
    public List<int> Permisos { get; set; } = [];
}
