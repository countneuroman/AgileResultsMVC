using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileResultsMVC.Migrations
{
    public partial class UpdateUserModelsToManyTaskForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AllTask_userId",
                table: "AllTask");

            migrationBuilder.CreateIndex(
                name: "IX_AllTask_userId",
                table: "AllTask",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AllTask_userId",
                table: "AllTask");

            migrationBuilder.CreateIndex(
                name: "IX_AllTask_userId",
                table: "AllTask",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");
        }
    }
}
