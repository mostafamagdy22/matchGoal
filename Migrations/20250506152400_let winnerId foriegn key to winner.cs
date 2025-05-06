using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchGoalAPI.Migrations
{
    /// <inheritdoc />
    public partial class letwinnerIdforiegnkeytowinner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "WinnerID",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerID",
                table: "Matches",
                column: "WinnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_WinnerID",
                table: "Matches",
                column: "WinnerID",
                principalTable: "Teams",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_WinnerID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinnerID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "WinnerID",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
