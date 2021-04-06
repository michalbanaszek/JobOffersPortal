using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Updated_JobOffer_Fields_To_List : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Offers",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "JobOffers");

            migrationBuilder.CreateTable(
                name: "JobOfferProposition",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobOfferId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferProposition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOfferProposition_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobOfferRequirement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobOfferId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOfferRequirement_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobOfferSkill",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobOfferId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOfferSkill_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferProposition_JobOfferId",
                table: "JobOfferProposition",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferRequirement_JobOfferId",
                table: "JobOfferRequirement",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferSkill_JobOfferId",
                table: "JobOfferSkill",
                column: "JobOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOfferProposition");

            migrationBuilder.DropTable(
                name: "JobOfferRequirement");

            migrationBuilder.DropTable(
                name: "JobOfferSkill");

            migrationBuilder.AddColumn<string>(
                name: "Offers",
                table: "JobOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "JobOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "JobOffers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
