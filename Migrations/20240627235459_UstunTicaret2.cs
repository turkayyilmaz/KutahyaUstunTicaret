using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutahyaUstunTicaret.Migrations
{
    /// <inheritdoc />
    public partial class UstunTicaret2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapsFooter",
                table: "TopLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapsFooter",
                table: "TopLines");
        }
    }
}
