using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchGoalAPI.Migrations
{
    /// <inheritdoc />
    public partial class addwinnertoMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Matches");
        }
    }
}
