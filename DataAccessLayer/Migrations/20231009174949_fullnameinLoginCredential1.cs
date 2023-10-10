using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class fullnameinLoginCredential1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "LoginCredentials");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "LoginCredentials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "LoginCredentials");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "LoginCredentials",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
