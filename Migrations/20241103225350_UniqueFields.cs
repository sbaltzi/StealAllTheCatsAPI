using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StealAllTheCatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class UniqueFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CatId",
                table: "CatEntity",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_Name",
                table: "TagEntity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatEntity_CatId",
                table: "CatEntity",
                column: "CatId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TagEntity_Name",
                table: "TagEntity");

            migrationBuilder.DropIndex(
                name: "IX_CatEntity_CatId",
                table: "CatEntity");

            migrationBuilder.AlterColumn<string>(
                name: "CatId",
                table: "CatEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
