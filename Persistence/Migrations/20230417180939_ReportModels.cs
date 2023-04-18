using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mineshard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReportModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repo",
                columns: table => new { Id = table.Column<Guid>(type: "uuid", nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repo", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        RepoId = table.Column<Guid>(type: "uuid", nullable: false),
                        NumCommitsOnMain = table.Column<int>(type: "integer", nullable: false),
                        status = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Repo_RepoId",
                        column: x => x.RepoId,
                        principalTable: "Repo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: true),
                        ReportId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Commiters",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: true),
                        NumCommits = table.Column<int>(type: "integer", nullable: false),
                        ReportId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commiters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commiters_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "MonthlyLoads",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uuid", nullable: false),
                        Month = table.Column<int>(type: "integer", nullable: false),
                        NumCommits = table.Column<int>(type: "integer", nullable: false),
                        ReportId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyLoads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyLoads_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ReportId",
                table: "Branches",
                column: "ReportId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Commiters_ReportId",
                table: "Commiters",
                column: "ReportId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyLoads_ReportId",
                table: "MonthlyLoads",
                column: "ReportId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reports_RepoId",
                table: "Reports",
                column: "RepoId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Branches");

            migrationBuilder.DropTable(name: "Commiters");

            migrationBuilder.DropTable(name: "MonthlyLoads");

            migrationBuilder.DropTable(name: "Reports");

            migrationBuilder.DropTable(name: "Repo");
        }
    }
}
