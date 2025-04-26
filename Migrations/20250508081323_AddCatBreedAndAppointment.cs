using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatStore.Migrations
{
    /// <inheritdoc />
    public partial class AddCatBreedAndAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CatBreed",
                table: "CatInfo",
                newName: "CatBreedId");

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatBreed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BreedName = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatBreed", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatInfo_CatBreedId",
                table: "CatInfo",
                column: "CatBreedId");

            migrationBuilder.CreateIndex(
                name: "IX_CatBreed_BreedName",
                table: "CatBreed",
                column: "BreedName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CatInfo_CatBreed_CatBreedId",
                table: "CatInfo",
                column: "CatBreedId",
                principalTable: "CatBreed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatInfo_CatBreed_CatBreedId",
                table: "CatInfo");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "CatBreed");

            migrationBuilder.DropIndex(
                name: "IX_CatInfo_CatBreedId",
                table: "CatInfo");

            migrationBuilder.RenameColumn(
                name: "CatBreedId",
                table: "CatInfo",
                newName: "CatBreed");
        }
    }
}
