using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobOffersPortal.WebUI.Migrations
{
    public partial class Updated_JobOffer_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "JobOffers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Offers",
                table: "JobOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "JobOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "JobOffers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "JobOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "JobOffers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Offers",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "JobOffers");
        }
    }
}
