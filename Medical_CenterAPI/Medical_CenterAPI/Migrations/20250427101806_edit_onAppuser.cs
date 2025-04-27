using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_CenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class edit_onAppuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmToken",
                table: "AspNetUsers");
        }
    }
}
