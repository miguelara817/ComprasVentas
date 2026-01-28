using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;

namespace ComprasVentas.Services.spec;

public interface IRolService
{
    Task<List<Rol>> GetAllAsync();

    Task CreateAsync(Rol rol);
}
