using System;

namespace ComprasVentas.Dto;

public class ProductoFilterDto
{
    public int Page { get; set; } = 1; // 1 por defecto

    public int Size { get; set; } = 10; // 10 por defecto

    public string SortOrder { get; set; } = "asc";

    public string? SortField { get; set; } = "Id";

    //Filtro global

    public string? filterValue { get; set; }

    //Filtros especificos
    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
    
    public string? Marca { get; set; }

    public string? NombreCategoria { get; set; }

}
