using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPS_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET ANSI_NULLS ON;");
            migrationBuilder.Sql("SET QUOTED_IDENTIFIER ON;");

            var sp = @"
                CREATE PROCEDURE [dbo].[SPS_Address]         
                 @CustomerId int
                AS        
                BEGIN
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    SELECT * FROM Address WHERE CustomerId = @CustomerId;
                END;
            ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[SPS_Address];");
        }
    }
}
