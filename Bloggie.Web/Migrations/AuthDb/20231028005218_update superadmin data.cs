using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations.AuthDb
{
    public partial class updatesuperadmindata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb8659f3-9b60-4fe1-81e8-bec77c28eb2f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aaf998da-da58-4372-86b6-8cac526e1516", "AQAAAAEAACcQAAAAEBydXy+LcqvxKU3uRwNL9WvFiGiAjLs+nxOf4Yp8C0QdgUx7PPR/6gTlRmYk9lm3Tg==", "37a3c96e-8bec-475b-a513-079075b0c675" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb8659f3-9b60-4fe1-81e8-bec77c28eb2f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67de74c9-2913-42e0-93fb-9e0a35c9f15d", "AQAAAAEAACcQAAAAEHv5j+h6/ogM5DQrS4HHsHSV5wuG4Mepsolf/fxtOAku5AwfK8nhWuGmEbvbze69SA==", "b9796d6f-be7d-4c33-92b5-7c6ba07fe390" });
        }
    }
}
