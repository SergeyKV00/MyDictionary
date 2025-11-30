using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DictionaryItems_DictionaryId",
                table: "DictionaryItems");

            migrationBuilder.AlterColumn<string>(
                name: "Meaning",
                table: "DictionaryItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItems_DictionaryId_Deleted",
                table: "DictionaryItems",
                columns: new[] { "DictionaryId", "Deleted" })
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DictionaryItems_DictionaryId_Deleted",
                table: "DictionaryItems");

            migrationBuilder.AlterColumn<string>(
                name: "Meaning",
                table: "DictionaryItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItems_DictionaryId",
                table: "DictionaryItems",
                column: "DictionaryId");
        }
    }
}
