using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtaLearning.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewSmartDeviceId", // Change the column name to a unique name
                table: "Clients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewSmartDeviceId", // Use the same column name used in the Up method
                table: "Clients");
        }
    }
}
