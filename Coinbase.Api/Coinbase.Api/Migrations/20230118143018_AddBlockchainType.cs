using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coinbase.Api.Migrations
{
    public partial class AddBlockchainType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockchainType",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WalletType",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockchainType",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "WalletType",
                table: "Wallet");
        }
    }
}
