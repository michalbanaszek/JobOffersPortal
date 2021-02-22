using Microsoft.EntityFrameworkCore.Migrations;

namespace JobOffersPortal.WebUI.Migrations
{
    public partial class Add_UserId_Field_To_JobOffer_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JobOffers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_UserId",
                table: "JobOffers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_AspNetUsers_UserId",
                table: "JobOffers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_AspNetUsers_UserId",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_JobOffers_UserId",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobOffers");
        }
    }
}
