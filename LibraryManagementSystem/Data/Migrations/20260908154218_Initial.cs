using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 42, 17, 766, DateTimeKind.Local).AddTicks(7034));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 42, 17, 768, DateTimeKind.Local).AddTicks(3691));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 32, 53, 245, DateTimeKind.Local).AddTicks(5963));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 32, 53, 247, DateTimeKind.Local).AddTicks(2177));
        }
    }
}
