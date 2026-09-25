using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsychologistSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Psychologist_Index_Key_Fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PsychologistCategory",
                columns: table => new
                {
                    PsychologistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologistCategory", x => new { x.PsychologistId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PsychologistCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PsychologistCategory_Psychologist_PsychologistId",
                        column: x => x.PsychologistId,
                        principalTable: "Psychologist",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistCategory_CategoryId",
                table: "PsychologistCategory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PsychologistCategory");
        }
    }
}
