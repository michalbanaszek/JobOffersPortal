using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Added_Job_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferProposition_JobOffers_JobOfferId",
                table: "JobOfferProposition");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferRequirement_JobOffers_JobOfferId",
                table: "JobOfferRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferSkill_JobOffers_JobOfferId",
                table: "JobOfferSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOfferSkill",
                table: "JobOfferSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOfferRequirement",
                table: "JobOfferRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOfferProposition",
                table: "JobOfferProposition");

            migrationBuilder.RenameTable(
                name: "JobOfferSkill",
                newName: "JobOfferSkills");

            migrationBuilder.RenameTable(
                name: "JobOfferRequirement",
                newName: "JobOfferRequirements");

            migrationBuilder.RenameTable(
                name: "JobOfferProposition",
                newName: "JobOfferPropositions");

            migrationBuilder.RenameIndex(
                name: "IX_JobOfferSkill_JobOfferId",
                table: "JobOfferSkills",
                newName: "IX_JobOfferSkills_JobOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOfferRequirement_JobOfferId",
                table: "JobOfferRequirements",
                newName: "IX_JobOfferRequirements_JobOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOfferProposition_JobOfferId",
                table: "JobOfferPropositions",
                newName: "IX_JobOfferPropositions_JobOfferId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOfferSkills",
                table: "JobOfferSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOfferRequirements",
                table: "JobOfferRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOfferPropositions",
                table: "JobOfferPropositions",
                column: "Id");

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
                name: "FK_JobOfferSkills_JobOffers_JobOfferId",
                table: "JobOfferSkills",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                name: "FK_JobOfferSkills_JobOffers_JobOfferId",
                table: "JobOfferSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOfferSkills",
                table: "JobOfferSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOfferRequirements",
                table: "JobOfferRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOfferPropositions",
                table: "JobOfferPropositions");

            migrationBuilder.RenameTable(
                name: "JobOfferSkills",
                newName: "JobOfferSkill");

            migrationBuilder.RenameTable(
                name: "JobOfferRequirements",
                newName: "JobOfferRequirement");

            migrationBuilder.RenameTable(
                name: "JobOfferPropositions",
                newName: "JobOfferProposition");

            migrationBuilder.RenameIndex(
                name: "IX_JobOfferSkills_JobOfferId",
                table: "JobOfferSkill",
                newName: "IX_JobOfferSkill_JobOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOfferRequirements_JobOfferId",
                table: "JobOfferRequirement",
                newName: "IX_JobOfferRequirement_JobOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOfferPropositions_JobOfferId",
                table: "JobOfferProposition",
                newName: "IX_JobOfferProposition_JobOfferId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOfferSkill",
                table: "JobOfferSkill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOfferRequirement",
                table: "JobOfferRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOfferProposition",
                table: "JobOfferProposition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferProposition_JobOffers_JobOfferId",
                table: "JobOfferProposition",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferRequirement_JobOffers_JobOfferId",
                table: "JobOfferRequirement",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferSkill_JobOffers_JobOfferId",
                table: "JobOfferSkill",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
