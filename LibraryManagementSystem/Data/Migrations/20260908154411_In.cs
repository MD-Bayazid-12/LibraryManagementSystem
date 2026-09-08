using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class In : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 44, 10, 392, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 44, 10, 393, DateTimeKind.Local).AddTicks(5107));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 43, 33, 376, DateTimeKind.Local).AddTicks(3133));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "AddedDate",
                value: new DateTime(2026, 9, 8, 21, 43, 33, 378, DateTimeKind.Local).AddTicks(599));
        }
    }
}
