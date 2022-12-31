using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SimplifiedGroupRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_GroupRoles_GroupRoleId",
                table: "GroupMembers");

            migrationBuilder.DropTable(
                name: "GroupRoles");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupRoleId",
                table: "GroupMembers");

            migrationBuilder.RenameColumn(
                name: "GroupRoleId",
                table: "GroupMembers",
                newName: "GroupRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupRole",
                table: "GroupMembers",
                newName: "GroupRoleId");

            migrationBuilder.CreateTable(
                name: "GroupRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupRoleId",
                table: "GroupMembers",
                column: "GroupRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_GroupRoles_GroupRoleId",
                table: "GroupMembers",
                column: "GroupRoleId",
                principalTable: "GroupRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
