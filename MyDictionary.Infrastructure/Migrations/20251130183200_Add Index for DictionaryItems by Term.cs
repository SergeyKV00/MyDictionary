using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexforDictionaryItemsbyTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItems_Term",
                table: "DictionaryItems",
                column: "Term")
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DictionaryItems_Term",
                table: "DictionaryItems");
        }
    }
}
