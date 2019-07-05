using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAPI.DataContext.Migrations
{
    public partial class FeaturedSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FeaturedSections",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Aside" },
                    { 2, "Main" },
                    { 3, "Opinion" },
                    { 4, "Travel" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FeaturedSections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FeaturedSections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FeaturedSections",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FeaturedSections",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
