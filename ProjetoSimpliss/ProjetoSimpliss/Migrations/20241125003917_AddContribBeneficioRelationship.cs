using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoSimpliss.Migrations
{
    /// <inheritdoc />
    public partial class AddContribBeneficioRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContribuintesBeneficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContribuinteId = table.Column<int>(type: "integer", nullable: false),
                    BeneficioId = table.Column<int>(type: "integer", nullable: false),
                    DataVinculo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContribuintesBeneficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContribuintesBeneficios_Beneficios_BeneficioId",
                        column: x => x.BeneficioId,
                        principalTable: "Beneficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContribuintesBeneficios_Contribuintes_ContribuinteId",
                        column: x => x.ContribuinteId,
                        principalTable: "Contribuintes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContribuintesBeneficios_BeneficioId",
                table: "ContribuintesBeneficios",
                column: "BeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_ContribuintesBeneficios_ContribuinteId",
                table: "ContribuintesBeneficios",
                column: "ContribuinteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContribuintesBeneficios");
        }
    }
}
