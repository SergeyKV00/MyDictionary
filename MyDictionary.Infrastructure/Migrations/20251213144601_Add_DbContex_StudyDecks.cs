using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_DbContex_StudyDecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeck_Users_UserId",
                table: "StudyDeck");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckDictionary_StudyDeck_StudyDeckId",
                table: "StudyDeckDictionary");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckDictionary_UserDictionaries_DictionaryId",
                table: "StudyDeckDictionary");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckWord_DictionaryItems_DictionaryItemId",
                table: "StudyDeckWord");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckWord_StudyDeck_StudyDeckId",
                table: "StudyDeckWord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyDeckWord",
                table: "StudyDeckWord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyDeckDictionary",
                table: "StudyDeckDictionary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyDeck",
                table: "StudyDeck");

            migrationBuilder.RenameTable(
                name: "StudyDeckWord",
                newName: "StudyDeckWords");

            migrationBuilder.RenameTable(
                name: "StudyDeckDictionary",
                newName: "StudyDeckDictionaries");

            migrationBuilder.RenameTable(
                name: "StudyDeck",
                newName: "StudyDecks");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckWord_StudyDeckId",
                table: "StudyDeckWords",
                newName: "IX_StudyDeckWords_StudyDeckId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckWord_DictionaryItemId",
                table: "StudyDeckWords",
                newName: "IX_StudyDeckWords_DictionaryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckDictionary_StudyDeckId",
                table: "StudyDeckDictionaries",
                newName: "IX_StudyDeckDictionaries_StudyDeckId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckDictionary_DictionaryId",
                table: "StudyDeckDictionaries",
                newName: "IX_StudyDeckDictionaries_DictionaryId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeck_UserId",
                table: "StudyDecks",
                newName: "IX_StudyDecks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyDeckWords",
                table: "StudyDeckWords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyDeckDictionaries",
                table: "StudyDeckDictionaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyDecks",
                table: "StudyDecks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckDictionaries_StudyDecks_StudyDeckId",
                table: "StudyDeckDictionaries",
                column: "StudyDeckId",
                principalTable: "StudyDecks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckDictionaries_UserDictionaries_DictionaryId",
                table: "StudyDeckDictionaries",
                column: "DictionaryId",
                principalTable: "UserDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDecks_Users_UserId",
                table: "StudyDecks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckWords_DictionaryItems_DictionaryItemId",
                table: "StudyDeckWords",
                column: "DictionaryItemId",
                principalTable: "DictionaryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckWords_StudyDecks_StudyDeckId",
                table: "StudyDeckWords",
                column: "StudyDeckId",
                principalTable: "StudyDecks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckDictionaries_StudyDecks_StudyDeckId",
                table: "StudyDeckDictionaries");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckDictionaries_UserDictionaries_DictionaryId",
                table: "StudyDeckDictionaries");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDecks_Users_UserId",
                table: "StudyDecks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckWords_DictionaryItems_DictionaryItemId",
                table: "StudyDeckWords");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyDeckWords_StudyDecks_StudyDeckId",
                table: "StudyDeckWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyDeckWords",
                table: "StudyDeckWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyDecks",
                table: "StudyDecks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyDeckDictionaries",
                table: "StudyDeckDictionaries");

            migrationBuilder.RenameTable(
                name: "StudyDeckWords",
                newName: "StudyDeckWord");

            migrationBuilder.RenameTable(
                name: "StudyDecks",
                newName: "StudyDeck");

            migrationBuilder.RenameTable(
                name: "StudyDeckDictionaries",
                newName: "StudyDeckDictionary");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckWords_StudyDeckId",
                table: "StudyDeckWord",
                newName: "IX_StudyDeckWord_StudyDeckId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckWords_DictionaryItemId",
                table: "StudyDeckWord",
                newName: "IX_StudyDeckWord_DictionaryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDecks_UserId",
                table: "StudyDeck",
                newName: "IX_StudyDeck_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckDictionaries_StudyDeckId",
                table: "StudyDeckDictionary",
                newName: "IX_StudyDeckDictionary_StudyDeckId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyDeckDictionaries_DictionaryId",
                table: "StudyDeckDictionary",
                newName: "IX_StudyDeckDictionary_DictionaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyDeckWord",
                table: "StudyDeckWord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyDeck",
                table: "StudyDeck",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyDeckDictionary",
                table: "StudyDeckDictionary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeck_Users_UserId",
                table: "StudyDeck",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckDictionary_StudyDeck_StudyDeckId",
                table: "StudyDeckDictionary",
                column: "StudyDeckId",
                principalTable: "StudyDeck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckDictionary_UserDictionaries_DictionaryId",
                table: "StudyDeckDictionary",
                column: "DictionaryId",
                principalTable: "UserDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckWord_DictionaryItems_DictionaryItemId",
                table: "StudyDeckWord",
                column: "DictionaryItemId",
                principalTable: "DictionaryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyDeckWord_StudyDeck_StudyDeckId",
                table: "StudyDeckWord",
                column: "StudyDeckId",
                principalTable: "StudyDeck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
