using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDb.Migrations
{
    /// <inheritdoc />
    public partial class CustomerCaseInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerCases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CustomerCases",
                type: "int",
                nullable: true);
        }
    }
}
