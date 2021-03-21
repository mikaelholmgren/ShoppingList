using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingList.Migrations
{
    public partial class Familymembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMember_Family_FamilyId",
                table: "FamilyMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyMember",
                table: "FamilyMember");

            migrationBuilder.RenameTable(
                name: "FamilyMember",
                newName: "FamilyMembers");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyMember_FamilyId",
                table: "FamilyMembers",
                newName: "IX_FamilyMembers_FamilyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyMembers",
                table: "FamilyMembers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Family_FamilyId",
                table: "FamilyMembers",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Family_FamilyId",
                table: "FamilyMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyMembers",
                table: "FamilyMembers");

            migrationBuilder.RenameTable(
                name: "FamilyMembers",
                newName: "FamilyMember");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyMembers_FamilyId",
                table: "FamilyMember",
                newName: "IX_FamilyMember_FamilyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyMember",
                table: "FamilyMember",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMember_Family_FamilyId",
                table: "FamilyMember",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
