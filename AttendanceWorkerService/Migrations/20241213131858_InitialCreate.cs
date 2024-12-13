using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceWorkerService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Execute CreateTables.sql to create the required tables
            var createTablesScript = File.ReadAllText("Scripts/CreateTables.sql");
            migrationBuilder.Sql(createTablesScript);

            // Execute SeedData.sql to populate initial data
            var seedDataScript = File.ReadAllText("Scripts/SeedData.sql");
            migrationBuilder.Sql(seedDataScript);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AttendanceRecords");
            migrationBuilder.DropTable(name: "AttendanceStatus");
            migrationBuilder.DropTable(name: "Employees");
            migrationBuilder.DropTable(name: "ShiftSchedules");
        }
    }
}
