using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDictionaryItemExample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictionaryItemExamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DictionaryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Example = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryItemExamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryItemExamples_DictionaryItems_DictionaryItemId",
                        column: x => x.DictionaryItemId,
                        principalTable: "DictionaryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItemExamples_DictionaryItemId",
                table: "DictionaryItemExamples",
                column: "DictionaryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictionaryItemExamples");
        }
    }
}
