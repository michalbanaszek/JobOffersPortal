using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Added_Delete_Cascade_Behavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferPropositions_JobOffers_JobOfferId",
                table: "JobOfferPropositions");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferRequirements_JobOffers_JobOfferId",
                table: "JobOfferRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferSkills_JobOffers_JobOfferId",
                table: "JobOfferSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferPropositions_JobOffers_JobOfferId",
                table: "JobOfferPropositions",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferRequirements_JobOffers_JobOfferId",
                table: "JobOfferRequirements",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferSkills_JobOffers_JobOfferId",
                table: "JobOfferSkills",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferPropositions_JobOffers_JobOfferId",
                table: "JobOfferPropositions");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferRequirements_JobOffers_JobOfferId",
                table: "JobOfferRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferSkills_JobOffers_JobOfferId",
                table: "JobOfferSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferPropositions_JobOffers_JobOfferId",
                table: "JobOfferPropositions",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferRequirements_JobOffers_JobOfferId",
                table: "JobOfferRequirements",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferSkills_JobOffers_JobOfferId",
                table: "JobOfferSkills",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
