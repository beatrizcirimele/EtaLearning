using Microsoft.EntityFrameworkCore.Migrations;

namespace EtaLearning.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSmartDeviceClientIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "SmartDevices",
                type: "int", // New data type
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the column type back to uniqueidentifier if needed
            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "SmartDevices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
