using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class LatestLocationView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW [dbo].[VehicleLatestLocations] AS
                select tbl.* from (
                    SELECT l.*,v.IsActive,
                    ROW_NUMBER() OVER ( PARTITION BY VehicleId ORDER BY CONVERT(datetime,CreateDate, 101) DESC) AS Recency
                    FROM Locations l 
	                JOIN (select Id,IsActive from Vehicles) v on l.VehicleId = v.Id) tbl where Recency = 1
            GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view VehicleLatestLocations;");
        }
    }
}
