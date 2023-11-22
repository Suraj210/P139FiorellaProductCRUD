using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorellaBackend.Migrations
{
    public partial class AddIdTOArchiveCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "ArchiveCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "ArchiveCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
