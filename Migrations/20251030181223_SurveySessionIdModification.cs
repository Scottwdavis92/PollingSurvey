using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollingApplication.Migrations
{
    /// <inheritdoc />
    public partial class SurveySessionIdModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "completed",
                table: "SurveySessions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completed",
                table: "SurveySessions");
        }
    }
}
