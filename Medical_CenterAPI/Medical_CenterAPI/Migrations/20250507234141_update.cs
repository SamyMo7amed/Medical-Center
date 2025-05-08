using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_CenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_AssistantId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AssistantId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssistantId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AssistantId",
                table: "Appointments",
                column: "AssistantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_AssistantId",
                table: "Appointments",
                column: "AssistantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
