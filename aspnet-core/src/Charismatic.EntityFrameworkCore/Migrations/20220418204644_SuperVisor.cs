using Microsoft.EntityFrameworkCore.Migrations;

namespace Charismatic.Migrations
{
    public partial class SuperVisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuperVisorId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SuperVisorId",
                table: "Departments",
                column: "SuperVisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Admins_SuperVisorId",
                table: "Departments",
                column: "SuperVisorId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Admins_SuperVisorId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SuperVisorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SuperVisorId",
                table: "Departments");
        }
    }
}
