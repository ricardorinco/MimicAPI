using Microsoft.EntityFrameworkCore.Migrations;

namespace Mimic.WebApi.Migrations
{
    public partial class UpdateWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mimic",
                table: "Words",
                newName: "Description"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Words",
                newName: "Mimic"
            );
        }
    }
}
