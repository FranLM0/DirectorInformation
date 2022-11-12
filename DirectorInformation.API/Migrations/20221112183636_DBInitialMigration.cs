using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectorInformation.API.Migrations
{
    public partial class DBInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    DirectorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Films_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Se lo considera uno de los pioneros de la era del Nuevo Hollywood", "Steven Spielberg" });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "PAPÁ", "Quentin Tarantino" });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "TRILOGO", "Francis Ford Coppola" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "DirectorId", "Name" },
                values: new object[] { 1, "miPelicula favorita", 1, "Empire of the sun" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "DirectorId", "Name" },
                values: new object[] { 2, "La de la niña con el abrigo rojo", 1, "Schindle's List" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "DirectorId", "Name" },
                values: new object[] { 3, "crítica al racismo norteamericano", 2, "Django" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "DirectorId", "Name" },
                values: new object[] { 4, "Mata a Bill", 2, "Kill Bill" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "DirectorId", "Name" },
                values: new object[] { 5, "Clasicazo", 3, "The Godfather I" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "DirectorId", "Name" },
                values: new object[] { 6, "Clasicazox2", 3, "The Godfather II" });

            migrationBuilder.CreateIndex(
                name: "IX_Films_DirectorId",
                table: "Films",
                column: "DirectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
