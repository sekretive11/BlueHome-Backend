using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueHome.Server.Infrastructure.Migrations
{
    public partial class Baseline_LocationSpaceRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpaceId",
                table: "locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_locations_SpaceId",
                table: "locations",
                column: "SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_locations_iot_space_SpaceId",
                table: "locations",
                column: "SpaceId",
                principalTable: "iot_space",
                principalColumn: "space_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locations_iot_space_SpaceId",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "IX_locations_SpaceId",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "SpaceId",
                table: "locations");
        }
    }
}
