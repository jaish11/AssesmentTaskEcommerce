using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateTable(
        name: "Products",
        columns: table => new
        {
            ProductID = table.Column<int>(nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            ProductName = table.Column<string>(nullable: false),
            Price = table.Column<decimal>(nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Products", x => x.ProductID);
        });
}



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Products Table
            migrationBuilder.DropTable(
                name: "Products");

            // Drop other tables if necessary
        }

    }
}
