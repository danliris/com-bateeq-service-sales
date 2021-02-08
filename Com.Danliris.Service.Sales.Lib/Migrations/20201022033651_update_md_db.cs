using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class update_md_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentsFileName",
                table: "RO_Garments",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentsPath",
                table: "RO_Garments",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeBreakdownIndex",
                table: "RO_Garment_SizeBreakdowns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeBreakdownDetailIndex",
                table: "RO_Garment_SizeBreakdown_Details",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedKadivMDBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedKadivMDDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedKadivMD",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedROMD",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ValidationMDBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ValidationMDDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ValidationSampleBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ValidationSampleDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "MaterialIndex",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "POMaster",
                table: "CostCalculationGarment_Materials",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PRMasterId",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PRMasterItemId",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "GarmentOmzetTargets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    YearOfPeriod = table.Column<string>(maxLength: 4, nullable: true),
                    MonthOfPeriod = table.Column<string>(maxLength: 10, nullable: true),
                    QuaterCode = table.Column<string>(maxLength: 5, nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    SectionCode = table.Column<string>(maxLength: 5, nullable: true),
                    SectionName = table.Column<string>(maxLength: 50, nullable: true),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentOmzetTargets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentOmzetTargets");

            migrationBuilder.DropColumn(
                name: "DocumentsFileName",
                table: "RO_Garments");

            migrationBuilder.DropColumn(
                name: "DocumentsPath",
                table: "RO_Garments");

            migrationBuilder.DropColumn(
                name: "SizeBreakdownIndex",
                table: "RO_Garment_SizeBreakdowns");

            migrationBuilder.DropColumn(
                name: "SizeBreakdownDetailIndex",
                table: "RO_Garment_SizeBreakdown_Details");

            migrationBuilder.DropColumn(
                name: "ApprovedKadivMDBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedKadivMDDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsApprovedKadivMD",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsValidatedROMD",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationMDBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationMDDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationSampleBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationSampleDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "MaterialIndex",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "POMaster",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "PRMasterId",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "PRMasterItemId",
                table: "CostCalculationGarment_Materials");
        }
    }
}
