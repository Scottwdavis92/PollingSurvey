using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollingApplication.Migrations
{
    /// <inheritdoc />
    public partial class ScaleInitiation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ResponseScales",
                columns: new[] { "ScaleValue", "ScaleDescription" },
                values: new object[,]
                {
                    { 1, "Strongly Disagree" },
                    { 2, "Disagree" },
                    { 3, "Neutral" },
                    { 4, "Agree" },
                    { 5, "Strongly Agree" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResponseScales",
                keyColumn: "ScaleValue",
                keyValues: new object[] { 1, 2, 3, 4, 5 });
        }
    }
}
