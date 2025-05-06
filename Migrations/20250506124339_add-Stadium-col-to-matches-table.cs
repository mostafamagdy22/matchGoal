using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchGoalAPI.Migrations
{
    /// <inheritdoc />
    public partial class addStadiumcoltomatchestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stadium",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stadium",
                table: "Matches");
        }
    }
}
