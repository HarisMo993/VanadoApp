using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MachineCheckup.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialAndSeedDataToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsResolved = table.Column<bool>(type: "boolean", nullable: false),
                    MachineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Machine 1" },
                    { 2, "Machine 2" },
                    { 3, "Machine 3" },
                    { 4, "Machine 4" }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "Description", "EndTime", "IsResolved", "MachineId", "Name", "Priority", "StartTime" },
                values: new object[,]
                {
                    { 1, "Description for Issue 1", null, false, 1, "Issue 1", 2, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 2, "Description for Issue 2", null, true, 2, "Issue 2", 1, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 3, "Description for Issue 3", null, false, 3, "Issue 3", 0, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 4, "Description for Issue 4", null, true, 4, "Issue 4", 1, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 5, "Description for Issue 5", null, false, 1, "Issue 5", 2, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 6, "Description for Issue 6", null, true, 2, "Issue 6", 0, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 7, "Description for Issue 7", null, false, 3, "Issue 7", 2, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 8, "Description for Issue 8", null, true, 4, "Issue 8", 1, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 9, "Description for Issue 9", null, false, 1, "Issue 9", 0, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 10, "Description for Issue 10", null, true, 2, "Issue 10", 1, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 11, "Description for Issue 11", null, false, 3, "Issue 11", 2, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) },
                    { 12, "Description for Issue 12", null, true, 4, "Issue 12", 0, new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_MachineId",
                table: "Issues",
                column: "MachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
