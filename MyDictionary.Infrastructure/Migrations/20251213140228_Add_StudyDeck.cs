using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_StudyDeck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyDeck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyDeck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyDeck_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyDeckDictionary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudyDeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DictionaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyDeckDictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyDeckDictionary_StudyDeck_StudyDeckId",
                        column: x => x.StudyDeckId,
                        principalTable: "StudyDeck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyDeckDictionary_UserDictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "UserDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyDeckWord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudyDeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DictionaryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyDeckWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyDeckWord_DictionaryItems_DictionaryItemId",
                        column: x => x.DictionaryItemId,
                        principalTable: "DictionaryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyDeckWord_StudyDeck_StudyDeckId",
                        column: x => x.StudyDeckId,
                        principalTable: "StudyDeck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyDeck_UserId",
                table: "StudyDeck",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyDeckDictionary_DictionaryId",
                table: "StudyDeckDictionary",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyDeckDictionary_StudyDeckId",
                table: "StudyDeckDictionary",
                column: "StudyDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyDeckWord_DictionaryItemId",
                table: "StudyDeckWord",
                column: "DictionaryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyDeckWord_StudyDeckId",
                table: "StudyDeckWord",
                column: "StudyDeckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyDeckDictionary");

            migrationBuilder.DropTable(
                name: "StudyDeckWord");

            migrationBuilder.DropTable(
                name: "StudyDeck");
        }
    }
}
