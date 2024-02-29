using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtaLearning.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddClientIdToClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                           name: "ClientId",
                           table: "Clients",
                           type: "uniqueidentifier",
                           nullable: false,
                           defaultValueSql: "NEWID()"); // Use defaultValueSql to set the default value to NEWID()
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "ClientId",
               table: "Clients");
        }
    }
}
