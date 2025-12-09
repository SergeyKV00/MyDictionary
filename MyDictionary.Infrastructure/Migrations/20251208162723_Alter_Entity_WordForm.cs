using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Entity_WordForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordForm_DictionaryItems_DictionaryItemId",
                table: "WordForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordForm",
                table: "WordForm");

            migrationBuilder.DropIndex(
                name: "IX_WordForm_DictionaryItemId",
                table: "WordForm");

            migrationBuilder.RenameTable(
                name: "WordForm",
                newName: "WordForms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordForms",
                table: "WordForms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WordForms_DictionaryItemId",
                table: "WordForms",
                column: "DictionaryItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WordForms_DictionaryItems_DictionaryItemId",
                table: "WordForms",
                column: "DictionaryItemId",
                principalTable: "DictionaryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordForms_DictionaryItems_DictionaryItemId",
                table: "WordForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordForms",
                table: "WordForms");

            migrationBuilder.DropIndex(
                name: "IX_WordForms_DictionaryItemId",
                table: "WordForms");

            migrationBuilder.RenameTable(
                name: "WordForms",
                newName: "WordForm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordForm",
                table: "WordForm",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WordForm_DictionaryItemId",
                table: "WordForm",
                column: "DictionaryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WordForm_DictionaryItems_DictionaryItemId",
                table: "WordForm",
                column: "DictionaryItemId",
                principalTable: "DictionaryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
