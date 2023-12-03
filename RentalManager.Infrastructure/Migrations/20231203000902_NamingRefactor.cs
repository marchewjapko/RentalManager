using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NamingRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_RentalAgreements_RentalAgreementId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalAgreements_Clients_ClientId",
                table: "RentalAgreements");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalAgreements_Employees_EmployeeId",
                table: "RentalAgreements");

            migrationBuilder.DropTable(
                name: "RentalAgreementRentalEquipment");

            migrationBuilder.DropTable(
                name: "RentalEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalAgreements",
                table: "RentalAgreements");

            migrationBuilder.RenameTable(
                name: "RentalAgreements",
                newName: "Agreements");

            migrationBuilder.RenameColumn(
                name: "RentalAgreementId",
                table: "Payments",
                newName: "AgreementId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_RentalAgreementId",
                table: "Payments",
                newName: "IX_Payments_AgreementId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalAgreements_EmployeeId",
                table: "Agreements",
                newName: "IX_Agreements_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalAgreements_ClientId",
                table: "Agreements",
                newName: "IX_Agreements_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agreements",
                table: "Agreements",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AgreementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Agreements_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_AgreementId",
                table: "Equipment",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Id",
                table: "Equipment",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Clients_ClientId",
                table: "Agreements",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Employees_EmployeeId",
                table: "Agreements",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Agreements_AgreementId",
                table: "Payments",
                column: "AgreementId",
                principalTable: "Agreements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Clients_ClientId",
                table: "Agreements");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Employees_EmployeeId",
                table: "Agreements");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Agreements_AgreementId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agreements",
                table: "Agreements");

            migrationBuilder.RenameTable(
                name: "Agreements",
                newName: "RentalAgreements");

            migrationBuilder.RenameColumn(
                name: "AgreementId",
                table: "Payments",
                newName: "RentalAgreementId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_AgreementId",
                table: "Payments",
                newName: "IX_Payments_RentalAgreementId");

            migrationBuilder.RenameIndex(
                name: "IX_Agreements_EmployeeId",
                table: "RentalAgreements",
                newName: "IX_RentalAgreements_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Agreements_ClientId",
                table: "RentalAgreements",
                newName: "IX_RentalAgreements_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalAgreements",
                table: "RentalAgreements",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RentalEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalEquipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalAgreementRentalEquipment",
                columns: table => new
                {
                    RentalAgreementsId = table.Column<int>(type: "int", nullable: false),
                    RentalEquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalAgreementRentalEquipment", x => new { x.RentalAgreementsId, x.RentalEquipmentId });
                    table.ForeignKey(
                        name: "FK_RentalAgreementRentalEquipment_RentalAgreements_RentalAgreementsId",
                        column: x => x.RentalAgreementsId,
                        principalTable: "RentalAgreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalAgreementRentalEquipment_RentalEquipment_RentalEquipmentId",
                        column: x => x.RentalEquipmentId,
                        principalTable: "RentalEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalAgreementRentalEquipment_RentalEquipmentId",
                table: "RentalAgreementRentalEquipment",
                column: "RentalEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalEquipment_Id",
                table: "RentalEquipment",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_RentalAgreements_RentalAgreementId",
                table: "Payments",
                column: "RentalAgreementId",
                principalTable: "RentalAgreements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalAgreements_Clients_ClientId",
                table: "RentalAgreements",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalAgreements_Employees_EmployeeId",
                table: "RentalAgreements",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
