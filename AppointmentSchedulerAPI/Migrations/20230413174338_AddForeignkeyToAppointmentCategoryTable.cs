using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentSchedulerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignkeyToAppointmentCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "CategoryNo",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CategoryNo",
                table: "Appointments",
                column: "CategoryNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentCategeroies_CategoryNo",
                table: "Appointments",
                column: "CategoryNo",
                principalTable: "AppointmentCategeroies",
                principalColumn: "AppointmentCategoryNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentCategeroies_CategoryNo",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CategoryNo",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CategoryNo",
                table: "Appointments");

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "Reminder", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), true, "Appointment with doctor" },
                    { 2, new DateTime(2023, 3, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), false, "Appointment with professor" }
                });
        }
    }
}
