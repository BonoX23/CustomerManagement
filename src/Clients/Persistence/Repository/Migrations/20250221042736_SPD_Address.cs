using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPD_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET ANSI_NULLS ON;");
            migrationBuilder.Sql("SET QUOTED_IDENTIFIER ON;");

            var spDelete = @"
                CREATE PROCEDURE [dbo].[SPD_Address]         
                 @AddressId int
                AS        
                BEGIN
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    DELETE FROM Address WHERE Id = @AddressId;
                END;
              ";

            migrationBuilder.Sql(spDelete);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var spDropDelete = "DROP PROCEDURE [dbo].[SPD_Address];";
            migrationBuilder.Sql(spDropDelete);
        }
    }
}
