using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPI_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET ANSI_NULLS ON;");
            migrationBuilder.Sql("SET QUOTED_IDENTIFIER ON;");

            var sp = @"
                CREATE PROCEDURE [dbo].[SPI_Address]         
                 @CustomerId int,
                 @Place varchar(150)
                AS        
                BEGIN
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    INSERT INTO Address(Place, CustomerId, CreateDate)
                    VALUES(@Place, @CustomerId, GETDATE())
                END;
              ";

            migrationBuilder.Sql(sp);
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE [dbo].[SPI_Address];";

            migrationBuilder.Sql(sp);
        }
    }
}