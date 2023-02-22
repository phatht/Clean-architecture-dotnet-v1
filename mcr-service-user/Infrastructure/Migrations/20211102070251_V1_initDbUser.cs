using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mcr_service_user.Migrations
{
    public partial class V1_initDbUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Mail = table.Column<string>(type: "TEXT", nullable: true),
                    OtherData = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Mail", "Name", "OtherData", "Status" },
                values: new object[] { new Guid("fb678c25-184a-4f88-aca6-750405b0dfc3"), "phatht@vietinfo.tech", "Huỳnh Tấn Phát", null, 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Mail", "Name", "OtherData", "Status" },
                values: new object[] { new Guid("f59bcb26-dbbe-4b2f-b699-4d13d90baf27"), "phat@gmail.com", "Phát", null, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
