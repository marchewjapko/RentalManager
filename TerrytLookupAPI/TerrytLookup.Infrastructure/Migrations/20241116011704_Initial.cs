using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerrytLookup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voivodeships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ValidFromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voivodeships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    VoivodeshipId = table.Column<int>(type: "integer", nullable: false),
                    CountyId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ValidFromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => new { x.VoivodeshipId, x.CountyId });
                    table.ForeignKey(
                        name: "FK_Counties_Voivodeships_VoivodeshipId",
                        column: x => x.VoivodeshipId,
                        principalTable: "Voivodeships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentTownId = table.Column<int>(type: "integer", nullable: true),
                    CountyVoivodeshipId = table.Column<int>(type: "integer", nullable: false),
                    CountyId = table.Column<int>(type: "integer", nullable: false),
                    ValidFromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Counties_CountyVoivodeshipId_CountyId",
                        columns: x => new { x.CountyVoivodeshipId, x.CountyId },
                        principalTable: "Counties",
                        principalColumns: new[] { "VoivodeshipId", "CountyId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Towns_Towns_ParentTownId",
                        column: x => x.ParentTownId,
                        principalTable: "Towns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    NameId = table.Column<int>(type: "integer", nullable: false),
                    TownId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ValidFromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => new { x.TownId, x.NameId });
                    table.ForeignKey(
                        name: "FK_Streets_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counties_Name",
                table: "Counties",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Counties_VoivodeshipId_CountyId",
                table: "Counties",
                columns: new[] { "VoivodeshipId", "CountyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_Name",
                table: "Streets",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CountyVoivodeshipId_CountyId",
                table: "Towns",
                columns: new[] { "CountyVoivodeshipId", "CountyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Towns_Name",
                table: "Towns",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_ParentTownId",
                table: "Towns",
                column: "ParentTownId");

            migrationBuilder.CreateIndex(
                name: "IX_Voivodeships_Name",
                table: "Voivodeships",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "Voivodeships");
        }
    }
}
