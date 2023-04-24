using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mineshard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Users_Role_RoleId", table: "Users");

            migrationBuilder.DropPrimaryKey(name: "PK_Role", table: "Role");

            migrationBuilder.RenameTable(name: "Role", newName: "Roles");

            migrationBuilder.AddPrimaryKey(name: "PK_Roles", table: "Roles", column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    {
                        new Guid("a71a55d6-99d7-4123-b4e0-1218ecb90e3e"),
                        new DateTime(2023, 4, 18, 16, 55, 39, 613, DateTimeKind.Utc).AddTicks(4894),
                        "Administrator role",
                        "Admin",
                        null
                    },
                    {
                        new Guid("c309fa92-2123-47be-b397-a1c77adb502c"),
                        new DateTime(2023, 4, 18, 16, 55, 39, 613, DateTimeKind.Utc).AddTicks(4902),
                        "Collaborator role",
                        "Collaborator",
                        null
                    }
                }
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Users_Roles_RoleId", table: "Users");

            migrationBuilder.DropPrimaryKey(name: "PK_Roles", table: "Roles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a71a55d6-99d7-4123-b4e0-1218ecb90e3e")
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c309fa92-2123-47be-b397-a1c77adb502c")
            );

            migrationBuilder.RenameTable(name: "Roles", newName: "Role");

            migrationBuilder.AddPrimaryKey(name: "PK_Role", table: "Role", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
