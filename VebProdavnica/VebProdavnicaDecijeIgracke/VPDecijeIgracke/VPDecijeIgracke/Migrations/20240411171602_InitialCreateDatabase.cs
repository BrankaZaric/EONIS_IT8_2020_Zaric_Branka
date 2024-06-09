using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VPDecijeIgracke.Migrations
{
    public partial class InitialCreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeAdmin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrezimeAdmin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailAdmin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KorisnickoImeAdmin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LozinkaAdmin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdresaAdmin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonAdmin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKategorije = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivProizvoda = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    SlikaURL = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    KategorijaID = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.ProizvodID);
                    table.ForeignKey(
                        name: "FK_Proizvod_Administrator_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Administrator",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvod_Kategorija_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorija",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    PorudzbinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Iznos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.PorudzbinaID);
                    table.ForeignKey(
                        name: "FK_Porudzbina_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkaPorudzbine",
                columns: table => new
                {
                    StavkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenaStavka = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KolicinaStavka = table.Column<int>(type: "int", nullable: false),
                    ProizvodID = table.Column<int>(type: "int", nullable: false),
                    PorudzbinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaPorudzbine", x => x.StavkaID);
                    table.ForeignKey(
                        name: "FK_StavkaPorudzbine_Porudzbina_PorudzbinaID",
                        column: x => x.PorudzbinaID,
                        principalTable: "Porudzbina",
                        principalColumn: "PorudzbinaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkaPorudzbine_Proizvod_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_EmailAdmin",
                table: "Administrator",
                column: "EmailAdmin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_KorisnickoImeAdmin",
                table: "Administrator",
                column: "KorisnickoImeAdmin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_LozinkaAdmin",
                table: "Administrator",
                column: "LozinkaAdmin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorija_NazivKategorije",
                table: "Kategorija",
                column: "NazivKategorije",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_Email",
                table: "Korisnik",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_KorisnickoIme",
                table: "Korisnik",
                column: "KorisnickoIme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_Lozinka",
                table: "Korisnik",
                column: "Lozinka",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_KorisnikID",
                table: "Porudzbina",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_AdminID",
                table: "Proizvod",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_KategorijaID",
                table: "Proizvod",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_NazivProizvoda",
                table: "Proizvod",
                column: "NazivProizvoda",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_SlikaURL",
                table: "Proizvod",
                column: "SlikaURL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StavkaPorudzbine_PorudzbinaID",
                table: "StavkaPorudzbine",
                column: "PorudzbinaID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaPorudzbine_ProizvodID",
                table: "StavkaPorudzbine",
                column: "ProizvodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StavkaPorudzbine");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Kategorija");
        }
    }
}
