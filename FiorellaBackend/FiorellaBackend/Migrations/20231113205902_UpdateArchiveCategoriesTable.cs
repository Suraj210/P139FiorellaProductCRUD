using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorellaBackend.Migrations
{
    public partial class UpdateArchiveCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ArchiveCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ArchiveCategories");
        }
    }
}
