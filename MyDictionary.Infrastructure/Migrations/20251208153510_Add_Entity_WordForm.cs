using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Entity_WordForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DictionaryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Infinitive = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PastSimple = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PastParticiple = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordForm_DictionaryItems_DictionaryItemId",
                        column: x => x.DictionaryItemId,
                        principalTable: "DictionaryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordForm_DictionaryItemId",
                table: "WordForm",
                column: "DictionaryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordForm");
        }
    }
}
