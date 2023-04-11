using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedulerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentCatgoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentCategeroies",
                columns: table => new
                {
                    AppointmentCategoryNo = table.Column<int>(type: "int", nullable: false),
                    AppointmentCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentCategeroies", x => x.AppointmentCategoryNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentCategeroies");
        }
    }
}
