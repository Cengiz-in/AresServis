using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class InsertOnlineRegisterData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Institutions",
                columns: new[] { "Name", "CreateDate" },
                values: new object[,]
                {
                    {"Online Register",DateTime.Now}
                });
            migrationBuilder.InsertData(
                table: "Enterprises",
                columns: new[] { "Name", "InstitutionId", "CreateDate" },
                values: new object[,]
                {
                    {"Test", "2" ,DateTime.Now}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
