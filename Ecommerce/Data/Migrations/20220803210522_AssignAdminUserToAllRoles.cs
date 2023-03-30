using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    public partial class AssignAdminUserToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[UserRoles] ([UserId], [RoleId]) SELECT N'079201cf-1595-4197-8032-f428f52f3eab', Id FROM [dbo].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[UserRoles] WHERE UserId = '079201cf-1595-4197-8032-f428f52f3eab'");
        }
    }
}
