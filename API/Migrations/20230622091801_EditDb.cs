using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class EditDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_by",
                table: "tb_m_events");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountGuid",
                table: "tb_m_events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "organizer",
                table: "tb_m_events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_events_AccountGuid",
                table: "tb_m_events",
                column: "AccountGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_events_tb_m_accounts_AccountGuid",
                table: "tb_m_events",
                column: "AccountGuid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_events_tb_m_accounts_AccountGuid",
                table: "tb_m_events");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_events_AccountGuid",
                table: "tb_m_events");

            migrationBuilder.DropColumn(
                name: "AccountGuid",
                table: "tb_m_events");

            migrationBuilder.DropColumn(
                name: "organizer",
                table: "tb_m_events");

            migrationBuilder.AddColumn<Guid>(
                name: "create_by",
                table: "tb_m_events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
