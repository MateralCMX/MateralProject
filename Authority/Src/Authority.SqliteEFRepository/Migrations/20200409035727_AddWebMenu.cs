using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authority.SqliteEFRepository.Migrations
{
    public partial class AddWebMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ParentID = table.Column<Guid>(nullable: true),
                    SubSystemID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleWebMenuMap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    WebMenuID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWebMenuMap", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubSystem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    Style = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Display = table.Column<bool>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSystem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMap",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMap", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WebMenu",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Style = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    ParentID = table.Column<Guid>(nullable: true),
                    SubSystemID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebMenu", x => x.ID);
                });
            migrationBuilder.Sql(@"
create view [UserOwnedSubSystemView]
as
select Distinct
	last_insert_rowid() as [ID],
	[UserRoleMap].[UserID],
	[SubSystem].[ID] as [SubSystemID],
	[SubSystem].[Name],
	[SubSystem].[Code],
	[SubSystem].[Style],
	[SubSystem].[Index],
	[SubSystem].[Display],
	[SubSystem].[Data]
from [SubSystem]
	inner join [Role] on [SubSystem].[ID] = [Role].[SubSystemID]
	inner join [UserRoleMap] on [Role].[ID] = [UserRoleMap].[RoleID]
");
            migrationBuilder.Sql(@"
create view [UserOwnedWebMenuView]
as
select Distinct 
	last_insert_rowid() as [ID],
	[UserRoleMap].[UserID],
	[Role].[SubSystemID],
	[WebMenu].[ID] as [WebMenuID],
	[WebMenu].[Name],
	[WebMenu].[Style],
	[WebMenu].[Data],
	[WebMenu].[Index],
	[WebMenu].[ParentID],
	[SubSystem].[Code] as [SystemCode]
from 
	[WebMenu]
	inner join [RoleWebMenuMap] on [WebMenu].[ID] = [RoleWebMenuMap].[WebMenuID]
	inner join [UserRoleMap] on [RoleWebMenuMap].[RoleID] = [UserRoleMap].[RoleID]
	inner join [Role] on [UserRoleMap].[RoleID] = [Role].[ID]
	inner join [SubSystem] on [Role].[SubSystemID] = [SubSystem].[ID]
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleWebMenuMap");

            migrationBuilder.DropTable(
                name: "SubSystem");

            migrationBuilder.DropTable(
                name: "UserRoleMap");

            migrationBuilder.DropTable(
                name: "WebMenu");
            migrationBuilder.Sql(@"drop view [UserOwnedSubSystemView]");
            migrationBuilder.Sql(@"drop view [UserOwnedWebMenuView]");
        }
    }
}
