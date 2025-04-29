using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDatos.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaEspecial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CEspecial_categoriaEspecialid",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "categoriaEspecialid",
                table: "Productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CEspecial_categoriaEspecialid",
                table: "Productos",
                column: "categoriaEspecialid",
                principalTable: "CEspecial",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CEspecial_categoriaEspecialid",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "categoriaEspecialid",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CEspecial_categoriaEspecialid",
                table: "Productos",
                column: "categoriaEspecialid",
                principalTable: "CEspecial",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
