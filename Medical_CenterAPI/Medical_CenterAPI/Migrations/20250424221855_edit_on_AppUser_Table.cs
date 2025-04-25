using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_CenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class edit_on_AppUser_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccountConfirmed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccountConfirmed",
                table: "AspNetUsers");
        }
    }
}
