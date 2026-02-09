using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ComprasVentas.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuario_rol_Usuarios_UsuarioId",
                table: "usuario_rol");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "usuario_rol",
                newName: "UsuariosId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_rol_UsuarioId",
                table: "usuario_rol",
                newName: "IX_usuario_rol_UsuariosId");

            migrationBuilder.RenameColumn(
                name: "Fechanacimiento",
                table: "Personas",
                newName: "FechaNacimiento");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UsuarioId",
                table: "RefreshTokens",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_rol_Usuarios_UsuariosId",
                table: "usuario_rol",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuario_rol_Usuarios_UsuariosId",
                table: "usuario_rol");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "usuario_rol",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_rol_UsuariosId",
                table: "usuario_rol",
                newName: "IX_usuario_rol_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Personas",
                newName: "Fechanacimiento");

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_rol_Usuarios_UsuarioId",
                table: "usuario_rol",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
