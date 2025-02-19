using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPU_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET ANSI_NULLS ON;");
            migrationBuilder.Sql("SET QUOTED_IDENTIFIER ON;");

            var sp = @"
                CREATE PROCEDURE [dbo].[SPU_Address]         
                    @AddressId int,
                    @Place varchar(150)
                AS        
                BEGIN
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    
                    UPDATE Address 
                    SET Place = @Place, 
                        UpdateDate = GETDATE() 
                    WHERE Id = @AddressId;
                END;
            ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[SPU_Address];");
        }
    }
}
