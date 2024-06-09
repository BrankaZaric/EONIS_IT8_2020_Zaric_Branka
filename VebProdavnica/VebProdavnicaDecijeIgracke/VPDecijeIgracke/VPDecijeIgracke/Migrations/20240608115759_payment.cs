using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VPDecijeIgracke.Migrations
{
    public partial class payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "Porudzbina",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Porudzbina",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "Porudzbina");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Porudzbina");
        }
    }
}
