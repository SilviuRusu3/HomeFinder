using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeFinder.Migrations
{
    public partial class AddGradedFeaturesToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradedFeatures",
                columns: table => new
                {
                    HomeFeaturesId = table.Column<int>(type: "integer", nullable: false),
                    HomeId = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradedFeatures", x => new { x.HomeFeaturesId, x.HomeId });
                    table.ForeignKey(
                        name: "FK_GradedFeatures_Features_HomeFeaturesId",
                        column: x => x.HomeFeaturesId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradedFeatures_Homes_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradedFeatures_HomeId",
                table: "GradedFeatures",
                column: "HomeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradedFeatures");
        }
    }
}
