using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CorrectIdCapitalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "id", table: "Repository", newName: "Id");

            migrationBuilder.RenameColumn(name: "id", table: "Provider", newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "NumCommitsOnMain",
                table: "Reports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Id", table: "Repository", newName: "id");

            migrationBuilder.RenameColumn(name: "Id", table: "Provider", newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "NumCommitsOnMain",
                table: "Reports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true
            );
        }
    }
}
