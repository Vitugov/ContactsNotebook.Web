using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class TokenAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastToken",
                table: "AspNetUsers");
        }
    }
}
