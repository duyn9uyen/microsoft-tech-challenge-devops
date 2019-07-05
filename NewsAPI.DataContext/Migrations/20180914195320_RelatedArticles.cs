using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAPI.DataContext.Migrations
{
    public partial class RelatedArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RelatedArticles",
                columns: new[] { "ArticleID", "RelatedArticleID" },
                values: new object[,]
                {
                    { 4, 10 },
                    { 10, 4 },
                    { 6, 7 },
                    { 7, 6 },
                    { 8, 9 },
                    { 9, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RelatedArticles",
                keyColumns: new[] { "ArticleID", "RelatedArticleID" },
                keyValues: new object[] { 4, 10 });

            migrationBuilder.DeleteData(
                table: "RelatedArticles",
                keyColumns: new[] { "ArticleID", "RelatedArticleID" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "RelatedArticles",
                keyColumns: new[] { "ArticleID", "RelatedArticleID" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "RelatedArticles",
                keyColumns: new[] { "ArticleID", "RelatedArticleID" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "RelatedArticles",
                keyColumns: new[] { "ArticleID", "RelatedArticleID" },
                keyValues: new object[] { 9, 8 });

            migrationBuilder.DeleteData(
                table: "RelatedArticles",
                keyColumns: new[] { "ArticleID", "RelatedArticleID" },
                keyValues: new object[] { 10, 4 });
        }
    }
}
