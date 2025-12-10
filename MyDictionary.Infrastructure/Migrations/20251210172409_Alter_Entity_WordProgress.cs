using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDictionary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Entity_WordProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EaseFactor",
                table: "WordProgresses");

            migrationBuilder.DropColumn(
                name: "IntervalDays",
                table: "WordProgresses");

            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "WordProgresses",
                newName: "State");

            migrationBuilder.AddColumn<double>(
                name: "Difficulty",
                table: "WordProgresses",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastReview",
                table: "WordProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Stability",
                table: "WordProgresses",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "WordProgresses");

            migrationBuilder.DropColumn(
                name: "LastReview",
                table: "WordProgresses");

            migrationBuilder.DropColumn(
                name: "Stability",
                table: "WordProgresses");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "WordProgresses",
                newName: "Stage");

            migrationBuilder.AddColumn<double>(
                name: "EaseFactor",
                table: "WordProgresses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "IntervalDays",
                table: "WordProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
