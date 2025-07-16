using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("f3654fe0-f092-410a-bf9d-56ac13edee39"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("591e5f9e-66d6-46f9-9219-92394bc66d15"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("5d002f3f-21ac-4911-9c63-15f0cf695c29"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("33e285ba-d1e3-40d8-9f18-a2200a22d8c5"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("ea123e52-997c-4afa-9955-215613c6e9a9"));

            migrationBuilder.AddColumn<float>(
                name: "Limit",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CreatedAt", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("1de5e49d-cd70-401e-a43a-fc1d24aac653"), new DateTimeOffset(new DateTime(2025, 7, 8, 18, 53, 44, 892, DateTimeKind.Unspecified).AddTicks(6052), new TimeSpan(0, 0, 0, 0, 0)), "cliente2@email.com", "Cliente 2" },
                    { new Guid("555fd1cb-f046-4406-bfb9-df8ba563bb1d"), new DateTimeOffset(new DateTime(2025, 7, 8, 18, 53, 44, 892, DateTimeKind.Unspecified).AddTicks(6050), new TimeSpan(0, 0, 0, 0, 0)), "cliente1@email.com", "Cliente 1" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "ClientId", "CreatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 1000f, new Guid("555fd1cb-f046-4406-bfb9-df8ba563bb1d"), new DateTimeOffset(new DateTime(2025, 7, 8, 18, 53, 44, 892, DateTimeKind.Unspecified).AddTicks(6142), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 500f, new Guid("1de5e49d-cd70-401e-a43a-fc1d24aac653"), new DateTimeOffset(new DateTime(2025, 7, 8, 18, 53, 44, 892, DateTimeKind.Unspecified).AddTicks(6144), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountFromId", "AccountToId", "Amount", "CreatedAt" },
                values: new object[] { new Guid("ac94da39-8f6b-49ba-a3c7-61cc14b906ed"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), 100f, new DateTimeOffset(new DateTime(2025, 7, 8, 18, 53, 44, 892, DateTimeKind.Unspecified).AddTicks(6271), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("ac94da39-8f6b-49ba-a3c7-61cc14b906ed"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("1de5e49d-cd70-401e-a43a-fc1d24aac653"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("555fd1cb-f046-4406-bfb9-df8ba563bb1d"));

            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CreatedAt", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("33e285ba-d1e3-40d8-9f18-a2200a22d8c5"), new DateTimeOffset(new DateTime(2025, 7, 4, 19, 51, 28, 702, DateTimeKind.Unspecified).AddTicks(9491), new TimeSpan(0, 0, 0, 0, 0)), "cliente2@email.com", "Cliente 2" },
                    { new Guid("ea123e52-997c-4afa-9955-215613c6e9a9"), new DateTimeOffset(new DateTime(2025, 7, 4, 19, 51, 28, 702, DateTimeKind.Unspecified).AddTicks(9488), new TimeSpan(0, 0, 0, 0, 0)), "cliente1@email.com", "Cliente 1" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "ClientId", "CreatedAt" },
                values: new object[,]
                {
                    { new Guid("591e5f9e-66d6-46f9-9219-92394bc66d15"), 1000f, new Guid("ea123e52-997c-4afa-9955-215613c6e9a9"), new DateTimeOffset(new DateTime(2025, 7, 4, 19, 51, 28, 702, DateTimeKind.Unspecified).AddTicks(9547), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5d002f3f-21ac-4911-9c63-15f0cf695c29"), 500f, new Guid("33e285ba-d1e3-40d8-9f18-a2200a22d8c5"), new DateTimeOffset(new DateTime(2025, 7, 4, 19, 51, 28, 702, DateTimeKind.Unspecified).AddTicks(9549), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountFromId", "AccountToId", "Amount", "CreatedAt" },
                values: new object[] { new Guid("f3654fe0-f092-410a-bf9d-56ac13edee39"), new Guid("591e5f9e-66d6-46f9-9219-92394bc66d15"), new Guid("5d002f3f-21ac-4911-9c63-15f0cf695c29"), 100f, new DateTimeOffset(new DateTime(2025, 7, 4, 19, 51, 28, 703, DateTimeKind.Unspecified).AddTicks(1069), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
