using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CorrectNameAndModifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderUrl",
                table: "Provider",
                newName: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Provider",
                newName: "ProviderUrl");
        }
    }
}
