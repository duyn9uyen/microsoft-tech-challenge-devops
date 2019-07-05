using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAPI.DataContext.Migrations
{
    public partial class FeaturedSectionArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FeaturedSectionArticles",
                columns: new[] { "ArticleID", "FeaturedSectionID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 3 },
                    { 7, 3 },
                    { 8, 4 },
                    { 9, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "FeaturedSectionArticles",
                keyColumns: new[] { "ArticleID", "FeaturedSectionID" },
                keyValues: new object[] { 9, 4 });
        }
    }
}
