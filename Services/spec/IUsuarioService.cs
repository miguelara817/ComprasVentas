using System;
using ComprasVentas.Dto;

namespace ComprasVentas.Services.spec;

public interface IUsuarioService
{
    Task<List<UsuarioDto>> GetAllAsync();

    Task<UsuarioDto> GetByIdAsync(int id);

    Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);

    Task UpdateAsync(CreateUsuarioDto dto, int id);

    Task DeleteAsync(int id);
}
