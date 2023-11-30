using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Heading = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    PageTitle = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    ShortDescription = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    UrlHandle = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Author = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
