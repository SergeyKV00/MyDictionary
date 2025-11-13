using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Enity_DictionaryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictionaryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DictionaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Meaning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryItems_UserDictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "UserDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItems_DictionaryId",
                table: "DictionaryItems",
                column: "DictionaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictionaryItems");
        }
    }
}
