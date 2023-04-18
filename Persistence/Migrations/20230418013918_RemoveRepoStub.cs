using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRepoStub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Reports_Repo_RepoId", table: "Reports");

            migrationBuilder.DropTable(name: "Repo");

            migrationBuilder.RenameColumn(
                name: "RepoId",
                table: "Reports",
                newName: "RepositoryId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Reports_RepoId",
                table: "Reports",
                newName: "IX_Reports_RepositoryId"
            );

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        ProviderUrl = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Description = table.Column<string>(type: "text", nullable: false),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "User",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Username = table.Column<string>(type: "text", nullable: false),
                        Email = table.Column<string>(type: "text", nullable: false),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true
                        ),
                        RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Repository",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        ProviderUsername = table.Column<string>(type: "text", nullable: false),
                        ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                        RequestorId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repository", x => x.id);
                    table.ForeignKey(
                        name: "FK_Repository_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Repository_User_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Repository_ProviderId",
                table: "Repository",
                column: "ProviderId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Repository_RequestorId",
                table: "Repository",
                column: "RequestorId"
            );

            migrationBuilder.CreateIndex(name: "IX_User_RoleId", table: "User", column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Repository_RepositoryId",
                table: "Reports",
                column: "RepositoryId",
                principalTable: "Repository",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Repository_RepositoryId",
                table: "Reports"
            );

            migrationBuilder.DropTable(name: "Repository");

            migrationBuilder.DropTable(name: "Provider");

            migrationBuilder.DropTable(name: "User");

            migrationBuilder.DropTable(name: "Role");

            migrationBuilder.RenameColumn(
                name: "RepositoryId",
                table: "Reports",
                newName: "RepoId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Reports_RepositoryId",
                table: "Reports",
                newName: "IX_Reports_RepoId"
            );

            migrationBuilder.CreateTable(
                name: "Repo",
                columns: table => new { Id = table.Column<Guid>(type: "uuid", nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repo", x => x.Id);
                }
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Repo_RepoId",
                table: "Reports",
                column: "RepoId",
                principalTable: "Repo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
