using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollingApplication.Migrations
{
    /// <inheritdoc />
    public partial class SurveySessionIdfixingIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "completed",
                table: "SurveySessions",
                newName: "Completed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "SurveySessions",
                newName: "completed");
        }
    }
}
