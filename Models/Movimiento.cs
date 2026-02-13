using System;

namespace ComprasVentas.Models;

public class Movimiento
{
    public int Id { get; set; }

    public int Cantidad { get; set; }

    public int TipoMovimiento { get; set; }

    public decimal PrecioUnitarioCompra { get; set; }

    public decimal PrecioUnitarioVenta { get; set; }

    public string? Observaciones { get; set; }

    public Nota? Nota { get; set; }

    public Producto? Producto { get; set; }

    public Almacen? Almacen { get; set; }
}
