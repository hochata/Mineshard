using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updateuserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repository_User_RequestorId",
                table: "Repository"
            );

            migrationBuilder.DropForeignKey(name: "FK_User_Role_RoleId", table: "User");

            migrationBuilder.DropPrimaryKey(name: "PK_User", table: "User");

            migrationBuilder.RenameTable(name: "User", newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_Users", table: "Users", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Users_RequestorId",
                table: "Repository",
                column: "RequestorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Users_RequestorId",
                table: "Repository"
            );

            migrationBuilder.DropForeignKey(name: "FK_Users_Role_RoleId", table: "Users");

            migrationBuilder.DropPrimaryKey(name: "PK_Users", table: "Users");

            migrationBuilder.RenameTable(name: "Users", newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_User", table: "User", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_User_RequestorId",
                table: "Repository",
                column: "RequestorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
