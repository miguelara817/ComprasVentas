using System;

namespace ComprasVentas.Dto.auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiracion { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public int identifier { get; set; }

    // DATA ADICIONAL (RECOMENDACION NO AÑADIR DATOS SENSIBLES)
    //public string Username { get; set; } = string.Empty;

    //public string Role { get; set; } = string.Empty;
}
