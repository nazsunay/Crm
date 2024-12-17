using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDb.Migrations
{
    /// <inheritdoc />
    public partial class AuthorizepersonCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizedPersons_Customers_CustomerId1",
                table: "AuthorizedPersons");

            migrationBuilder.DropIndex(
                name: "IX_AuthorizedPersons_CustomerId1",
                table: "AuthorizedPersons");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "AuthorizedPersons");

            migrationBuilder.AddColumn<int>(
                name: "AuthorizePersonId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizePersonId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "AuthorizedPersons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedPersons_CustomerId1",
                table: "AuthorizedPersons",
                column: "CustomerId1",
                unique: true,
                filter: "[CustomerId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizedPersons_Customers_CustomerId1",
                table: "AuthorizedPersons",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
