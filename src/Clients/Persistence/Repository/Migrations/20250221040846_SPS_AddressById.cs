using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPS_AddressById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET ANSI_NULLS ON;");
            migrationBuilder.Sql("SET QUOTED_IDENTIFIER ON;");

            var spGetById = @"
                CREATE PROCEDURE [dbo].[SPS_AddressById]         
                 @AddressId int
                AS        
                BEGIN
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    SELECT a.*, c.*, u.*
                    FROM Address a
                    INNER JOIN Customers c ON a.CustomerId = c.Id
                    LEFT JOIN Users u ON c.Id = u.CustomerId
                    WHERE a.Id = @AddressId;
                END;
              ";

            migrationBuilder.Sql(spGetById);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var spDropGetById = "DROP PROCEDURE [dbo].[SPS_AddressById];";
            migrationBuilder.Sql(spDropGetById);
        }
    }
}
