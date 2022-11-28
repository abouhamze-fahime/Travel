using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cmn");

            migrationBuilder.CreateTable(
                name: "tblPermission",
                schema: "cmn",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPermission", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_tblPermission_tblPermission_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "cmn",
                        principalTable: "tblPermission",
                        principalColumn: "PermissionId");
                });

            migrationBuilder.CreateTable(
                name: "tblRole",
                schema: "cmn",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                schema: "cmn",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserAvatar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "tblRolePermission",
                schema: "cmn",
                columns: table => new
                {
                    RP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRolePermission", x => x.RP_Id);
                    table.ForeignKey(
                        name: "FK_tblRolePermission_tblPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "cmn",
                        principalTable: "tblPermission",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRolePermission_tblRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "cmn",
                        principalTable: "tblRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserRole",
                schema: "cmn",
                columns: table => new
                {
                    Ur_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserRole", x => x.Ur_Id);
                    table.ForeignKey(
                        name: "FK_tblUserRole_tblRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "cmn",
                        principalTable: "tblRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserRole_tblUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "cmn",
                        principalTable: "tblUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPermission_ParentId",
                schema: "cmn",
                table: "tblPermission",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRolePermission_PermissionId",
                schema: "cmn",
                table: "tblRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRolePermission_RoleId",
                schema: "cmn",
                table: "tblRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRole_RoleId",
                schema: "cmn",
                table: "tblUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRole_UserId",
                schema: "cmn",
                table: "tblUserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRolePermission",
                schema: "cmn");

            migrationBuilder.DropTable(
                name: "tblUserRole",
                schema: "cmn");

            migrationBuilder.DropTable(
                name: "tblPermission",
                schema: "cmn");

            migrationBuilder.DropTable(
                name: "tblRole",
                schema: "cmn");

            migrationBuilder.DropTable(
                name: "tblUser",
                schema: "cmn");
        }
    }
}
