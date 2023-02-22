using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mcr_service_post.Migrations
{
    public partial class V1_initDbPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tile = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Name", "Tile", "UserId" },
                values: new object[] { new Guid("e3283297-5c6c-4562-b1f6-d4ffeb3c5d2b"), "Content 1", "Huỳnh Tấn Phát", "Tile 1", new Guid("637f0a75-0185-44ee-a550-e01c6effec75") });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Name", "Tile", "UserId" },
                values: new object[] { new Guid("ea43574b-66f3-4963-bea8-c128d480ac21"), "Content 2", "Huỳnh Tấn Phát", "Tile 2", new Guid("e63a1343-ec97-4d91-b0bf-690df5eaafc9") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
