using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCareAlliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteWeekendsFromWorkSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weekends",
                table: "WorkSchedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Weekends",
                table: "WorkSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
