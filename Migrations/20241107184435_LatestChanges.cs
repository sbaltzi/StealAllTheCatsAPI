using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StealAllTheCatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class LatestChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatEntityTagEntity_CatEntity_CatsId",
                table: "CatEntityTagEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CatEntityTagEntity_TagEntity_TagsId",
                table: "CatEntityTagEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagEntity",
                table: "TagEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatEntity",
                table: "CatEntity");

            migrationBuilder.RenameTable(
                name: "TagEntity",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "CatEntity",
                newName: "Cats");

            migrationBuilder.RenameIndex(
                name: "IX_TagEntity_Name",
                table: "Tags",
                newName: "IX_Tags_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CatEntity_CatId",
                table: "Cats",
                newName: "IX_Cats_CatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cats",
                table: "Cats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatEntityTagEntity_Cats_CatsId",
                table: "CatEntityTagEntity",
                column: "CatsId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatEntityTagEntity_Tags_TagsId",
                table: "CatEntityTagEntity",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatEntityTagEntity_Cats_CatsId",
                table: "CatEntityTagEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CatEntityTagEntity_Tags_TagsId",
                table: "CatEntityTagEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cats",
                table: "Cats");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "TagEntity");

            migrationBuilder.RenameTable(
                name: "Cats",
                newName: "CatEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_Name",
                table: "TagEntity",
                newName: "IX_TagEntity_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Cats_CatId",
                table: "CatEntity",
                newName: "IX_CatEntity_CatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagEntity",
                table: "TagEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatEntity",
                table: "CatEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatEntityTagEntity_CatEntity_CatsId",
                table: "CatEntityTagEntity",
                column: "CatsId",
                principalTable: "CatEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatEntityTagEntity_TagEntity_TagsId",
                table: "CatEntityTagEntity",
                column: "TagsId",
                principalTable: "TagEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
