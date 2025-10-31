using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollingApplication.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees", // The name of your employee table
                columns: new[] { "EmployeeId", "FirstName", "LastName", "Email", "Departmentid", "ApplicationUserId" },
                values: new object[,]
                {
             { 2, "Nick", "Barry", "nbarry@nebraska.edu", 1, null },
             { 3, "Stephanie", "Canny", "scanny@nebraska.edu", 2, null },
             { 4, "Scott", "Davis", "scottwdavis@nebraska.edu", 3, null },
             { 5, "Casey", "nugent", "Casey.nugent@nebraska.edu", 4, null },
             { 6, "David", "Irvine", "dirvine@nebraska.edu", 5, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValues: new object[] { 2, 3, 4, 5, 6 });
        }
    }
}
