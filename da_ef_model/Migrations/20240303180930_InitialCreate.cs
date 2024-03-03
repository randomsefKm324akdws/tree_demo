using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace da_ef_model.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Data = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    EventTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Log_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => new { x.TreeName, x.Id });
                    table.ForeignKey(
                        name: "Nodes_Nodes_TreeName_Id_fk",
                        columns: x => new { x.TreeName, x.ParentId },
                        principalTable: "Nodes",
                        principalColumns: new[] { "TreeName", "Id" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_TreeName_ParentId",
                table: "Nodes",
                columns: new[] { "TreeName", "ParentId" });

            migrationBuilder.CreateIndex(
                name: "Nodes_ParentId_Null_Index",
                table: "Nodes",
                columns: new[] { "TreeName", "ParentId" },
                unique: true,
                filter: "([ParentId] IS NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Nodes");
        }
    }
}
