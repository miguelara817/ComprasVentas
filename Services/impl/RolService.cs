using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class RolService : IRolService
{
    private readonly RolRepository _rolRepository;

    public RolService(RolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }
    public Task<List<Rol>> GetAllAsync()
    {
        return _rolRepository.GetAllAsync();
    }

    public async Task CreateAsync(Rol rol)
    {  
        await _rolRepository.CreateAsync(rol);
    }

}
