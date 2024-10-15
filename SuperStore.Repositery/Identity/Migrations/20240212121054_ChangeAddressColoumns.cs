using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperStore.Repositery.Identity.Migrations
{
    public partial class ChangeAddressColoumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LName",
                table: "Address",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Fname",
                table: "Address",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Address",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Address",
                newName: "Fname");
        }
    }
}
