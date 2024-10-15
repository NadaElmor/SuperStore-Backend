using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperStore.Repositery.Data.Migrations
{
    public partial class editProuctPicUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_ProductUrl",
                table: "OrderItems",
                newName: "Product_PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_PictureUrl",
                table: "OrderItems",
                newName: "Product_ProductUrl");
        }
    }
}
