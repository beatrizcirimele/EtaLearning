using Microsoft.EntityFrameworkCore.Migrations;

namespace EtaLearning.DataAccess.Migrations
{
    public partial class updateIdColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "SmartDevices",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add logic to revert the changes if necessary
        }
    }
}
