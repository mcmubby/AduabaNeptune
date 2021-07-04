using Microsoft.EntityFrameworkCore.Migrations;

namespace AduabaNeptune.Migrations
{
    public partial class ColumnAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendorName",
                table: "Vendors",
                newName: "ShopLogoUrl");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ShopLogoUrl",
                table: "Vendors",
                newName: "VendorName");
        }
    }
}
