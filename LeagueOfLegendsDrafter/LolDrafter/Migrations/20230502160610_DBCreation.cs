using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolDrafter.Migrations
{
    /// <inheritdoc />
    public partial class DBCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Champions",
                columns: table => new
                {
                    ChampionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChampName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abilities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadMatchup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoodMatchup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Build = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Positions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.ChampionID);
                });

            migrationBuilder.InsertData(
                table: "Champions",
                columns: new[] { "ChampionID", "Abilities", "BadMatchup", "Build", "ChampName", "GoodMatchup", "Positions" },
                values: new object[] { 1, "q, w, e, r", "Kled, Jax", "item1, item2, item3", "Aatrox", "Ramus, Nausus", "Top, Mid" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Champions");
        }
    }
}
