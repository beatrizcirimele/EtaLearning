using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EtaLearning.DataAccess.Migrations
{
    public partial class UpdateSmartDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (!migrationBuilder.IsSqlServer())
            {
                return;
            }

            // SQL query to check if the column exists
            var sql = @"
                SELECT COUNT(*)
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_NAME = 'SmartDevices' 
                AND COLUMN_NAME = 'Description';";

            // Execute the SQL query
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No action needed in the Down method
        }
    }
}
