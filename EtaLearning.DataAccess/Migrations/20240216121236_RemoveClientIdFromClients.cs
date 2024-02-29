using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtaLearning.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClientIdFromClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

    }

}
