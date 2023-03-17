using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedulerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedTableWithReminderFalse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Reminder",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Reminder",
                value: true);
        }
    }
}
