using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AddPlateNumberToView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[VehicleLatestLocations] AS
                select tbl.* from (
                    SELECT l.*,v.IsActive,v.PlateNumber,
                    ROW_NUMBER() OVER ( PARTITION BY VehicleId ORDER BY CONVERT(datetime,CreateDate, 101) DESC) AS Recency
                    FROM Locations l 
	                JOIN (select Id,IsActive,PlateNumber from Vehicles) v on l.VehicleId = v.Id) tbl where Recency = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
