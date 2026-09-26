using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsychologistSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Psychoklogist_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Psychologist_PsychologistId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PsychologistCategory_Psychologist_PsychologistId",
                table: "PsychologistCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingSchedule_Psychologist_PsychologistId",
                table: "WorkingSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Psychologist",
                table: "Psychologist");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Psychologist",
                table: "Psychologist",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Psychologist_UserId",
                table: "Psychologist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Psychologist_PsychologistId",
                table: "Appointment",
                column: "PsychologistId",
                principalTable: "Psychologist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PsychologistCategory_Psychologist_PsychologistId",
                table: "PsychologistCategory",
                column: "PsychologistId",
                principalTable: "Psychologist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingSchedule_Psychologist_PsychologistId",
                table: "WorkingSchedule",
                column: "PsychologistId",
                principalTable: "Psychologist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Psychologist_PsychologistId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PsychologistCategory_Psychologist_PsychologistId",
                table: "PsychologistCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingSchedule_Psychologist_PsychologistId",
                table: "WorkingSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Psychologist",
                table: "Psychologist");

            migrationBuilder.DropIndex(
                name: "IX_Psychologist_UserId",
                table: "Psychologist");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Psychologist",
                table: "Psychologist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Psychologist_PsychologistId",
                table: "Appointment",
                column: "PsychologistId",
                principalTable: "Psychologist",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PsychologistCategory_Psychologist_PsychologistId",
                table: "PsychologistCategory",
                column: "PsychologistId",
                principalTable: "Psychologist",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingSchedule_Psychologist_PsychologistId",
                table: "WorkingSchedule",
                column: "PsychologistId",
                principalTable: "Psychologist",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
