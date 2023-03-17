using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentSchedulerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedAppointmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "Reminder", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), true, "Appointment with doctor" },
                    { 2, new DateTime(2023, 3, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), true, "Appointment with professor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
