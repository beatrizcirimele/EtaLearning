using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtaLearning.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddClientIdColumnToSmartDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    migrationBuilder.AddColumn<int>(
    name: "ClientId",
    table: "SmartDevices",
    type: "int",
    nullable: false,
    defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
