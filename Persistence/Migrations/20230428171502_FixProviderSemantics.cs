using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixProviderSemantics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Repository_RepositoryId",
                table: "Reports"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Provider_ProviderId",
                table: "Repository"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Users_RequestorId",
                table: "Repository"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Repository", table: "Repository");

            migrationBuilder.DropPrimaryKey(name: "PK_Provider", table: "Provider");

            migrationBuilder.RenameTable(name: "Repository", newName: "Repositories");

            migrationBuilder.RenameTable(name: "Provider", newName: "Providers");

            migrationBuilder.RenameIndex(
                name: "IX_Repository_RequestorId",
                table: "Repositories",
                newName: "IX_Repositories_RequestorId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Repository_ProviderId",
                table: "Repositories",
                newName: "IX_Repositories_ProviderId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repositories",
                table: "Repositories",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_Providers", table: "Providers", column: "Id");

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[]
                {
                    new Guid("240095d2-8c4b-48f9-a5e1-276c07bd7678"),
                    "Github",
                    "https://github.com"
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a71a55d6-99d7-4123-b4e0-1218ecb90e3e"),
                column: "CreatedAt",
                value: new DateTime(2023, 4, 28, 17, 15, 2, 10, DateTimeKind.Utc).AddTicks(8594)
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c309fa92-2123-47be-b397-a1c77adb502c"),
                column: "CreatedAt",
                value: new DateTime(2023, 4, 28, 17, 15, 2, 10, DateTimeKind.Utc).AddTicks(8602)
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Repositories_RepositoryId",
                table: "Reports",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_Providers_ProviderId",
                table: "Repositories",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_Users_RequestorId",
                table: "Repositories",
                column: "RequestorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Repositories_RepositoryId",
                table: "Reports"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_Providers_ProviderId",
                table: "Repositories"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_Users_RequestorId",
                table: "Repositories"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Repositories", table: "Repositories");

            migrationBuilder.DropPrimaryKey(name: "PK_Providers", table: "Providers");

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: new Guid("240095d2-8c4b-48f9-a5e1-276c07bd7678")
            );

            migrationBuilder.RenameTable(name: "Repositories", newName: "Repository");

            migrationBuilder.RenameTable(name: "Providers", newName: "Provider");

            migrationBuilder.RenameIndex(
                name: "IX_Repositories_RequestorId",
                table: "Repository",
                newName: "IX_Repository_RequestorId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Repositories_ProviderId",
                table: "Repository",
                newName: "IX_Repository_ProviderId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repository",
                table: "Repository",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_Provider", table: "Provider", column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a71a55d6-99d7-4123-b4e0-1218ecb90e3e"),
                column: "CreatedAt",
                value: new DateTime(2023, 4, 18, 16, 55, 39, 613, DateTimeKind.Utc).AddTicks(4894)
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c309fa92-2123-47be-b397-a1c77adb502c"),
                column: "CreatedAt",
                value: new DateTime(2023, 4, 18, 16, 55, 39, 613, DateTimeKind.Utc).AddTicks(4902)
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Repository_RepositoryId",
                table: "Reports",
                column: "RepositoryId",
                principalTable: "Repository",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Provider_ProviderId",
                table: "Repository",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Users_RequestorId",
                table: "Repository",
                column: "RequestorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
