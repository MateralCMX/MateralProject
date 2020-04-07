using Authority.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Authority.SqliteEFRepository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Account = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
            const string adminUserID = "B4C91CC7-D68C-448B-9440-0893C1D54E90";
            string defaultPassword = PasswordHelper.GetEncodePassword("123456");
            migrationBuilder.Sql($@"
INSERT INTO [User] VALUES('{adminUserID}', '0001-01-01 00:00:00', '0001-01-01 00:00:00', 'Admin', '{defaultPassword}', '管理员')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
