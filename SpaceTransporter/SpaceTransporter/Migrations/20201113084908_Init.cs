using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceTransporter.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanetsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DockingCapacityLimit = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxWarpSpeed = table.Column<double>(type: "float", nullable: false),
                    IsDocked = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipsTable_PlanetsTable_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "PlanetsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipsTable_PlanetId",
                table: "ShipsTable",
                column: "PlanetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipsTable");

            migrationBuilder.DropTable(
                name: "PlanetsTable");
        }
    }
}
