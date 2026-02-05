using System;
using ComprasVentas.Dto;
using ComprasVentas.Exceptions;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql.Internal;

namespace ComprasVentas.Services.impl;

public class UsuarioService(UsuarioRepository usuarioRepository, RolRepository rolRepository) : IUsuarioService
{   
    private readonly UsuarioRepository _usuarioRepository = usuarioRepository;

    private readonly RolRepository _rolRepository = rolRepository;

    public async Task<List<UsuarioDto>> GetAllAsync()
    {
        try
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => MapToDto(u)).ToList();
        }
        catch (Exception ex)
        {
            
            throw new Exception("Error al obtener los usuarios", ex);
        }
    }

    public async Task<UsuarioDto> GetByIdAsync(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                // TODO add custom exception NotFoundException
                throw new NotFoundException("Usuario no econtrado");
            }
            return MapToDto(usuario);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        try
        {
            var roles = new List<Rol>();
            if (dto.RolIds != null && dto.RolIds.Count > 0)
            {
                foreach (var rolId in dto.RolIds)
                {
                    var rol = await _rolRepository.GetByIdAsync(rolId);
                    if(rol != null) roles.Add(rol);
                }
            }
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                //TODO add password hashing
                Password = dto.Password,
                Persona = new Persona
                {
                    Nombres = dto.Nombres,
                    Apellidos = dto.Apellidos,
                    Fechanacimiento = DateTime.ParseExact(dto.Fechanacimiento, "dd/mm/yyyy", null),
                    Genero = dto.Genero,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    Nacionalidad = dto.Nacionalidad
                },

                Roles = roles
            };

            await _usuarioRepository.CreateAsync(usuario);
            return MapToDto(usuario);
        }
        catch (Exception)
        {
            
            throw new Exception("Error al crear usuario");
        }
    }

    public async Task UpdateAsync(CreateUsuarioDto dto, int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null) throw new Exception("Usuario no encontrado");
            usuario.Nombre = dto.Nombre;
            usuario.Correo = dto.Correo;
            usuario.Password = dto.Password;
            //Actualizar datos persona
            if (usuario.Persona != null)
            {
                usuario.Persona.Nombres = dto.Nombre != null ? dto.Nombres : usuario.Persona.Nombres;
                usuario.Persona.Apellidos = dto.Apellidos != null ? dto.Apellidos : usuario.Persona.Apellidos;
                usuario.Persona.Fechanacimiento = DateTime.ParseExact(dto.Fechanacimiento, "dd/mm/yyyy", null);
                usuario.Persona.Genero = dto.Genero;
                usuario.Persona.Telefono = dto.Telefono;
                usuario.Persona.Direccion = dto.Direccion;
                usuario.Persona.Nacionalidad = dto.Nacionalidad;
            }

            // Actualizar roles
            if (dto.RolIds != null && dto.RolIds.Count > 0)
            {
                usuario.Roles.Clear();

                foreach (var rolId in dto.RolIds)
                {
                    var rol = await _rolRepository.GetByIdAsync(rolId);
                    if(rol != null) usuario.Roles.Add(rol);
                }

            }
            
            await _usuarioRepository.UpdateAsync(usuario);
        }
        catch (Exception)
        {
            
            throw new Exception($"Error al actualizar usuario {id}");
        }
    }
    public async Task DeleteAsync(int id)
    {
        try
        {
            await _usuarioRepository.DeleteAsync(id);
        }
        catch (Exception)
        {
            
            throw new Exception("Error al eliminar el usuario");
        }
    }

    //MAP TO DTO
    private UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Nombres = usuario.Persona?.Nombres ?? string.Empty,
            Apellidos = usuario.Persona?.Apellidos ?? string.Empty,
            Fechanacimiento = usuario.Persona?.Fechanacimiento.ToString("dd/mm/yyyy") ?? string.Empty,
            Genero = usuario.Persona?.Genero,
            Telefono = usuario.Persona?.Telefono,
            Direccion = usuario.Persona?.Direccion,
            Nacionalidad = usuario.Persona?.Nacionalidad,
            RolIds = usuario.Roles.Select(r=>r.Id).ToList()
        };
    }
}
