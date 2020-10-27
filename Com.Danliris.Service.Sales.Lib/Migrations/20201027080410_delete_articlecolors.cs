using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class delete_articlecolors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleColors",
                table: "ArticleColors");

            migrationBuilder.RenameTable(
                name: "ArticleColors",
                newName: "ArticleColor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleColor",
                table: "ArticleColor",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleColor",
                table: "ArticleColor");

            migrationBuilder.RenameTable(
                name: "ArticleColor",
                newName: "ArticleColors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleColors",
                table: "ArticleColors",
                column: "Id");
        }
    }
}
