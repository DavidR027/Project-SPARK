using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class EditColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_events_tb_m_users_UserGuid",
                table: "tb_m_events");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_events_UserGuid",
                table: "tb_m_events");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "tb_m_events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "tb_m_events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_events_UserGuid",
                table: "tb_m_events",
                column: "UserGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_events_tb_m_users_UserGuid",
                table: "tb_m_events",
                column: "UserGuid",
                principalTable: "tb_m_users",
                principalColumn: "guid");
        }
    }
}
