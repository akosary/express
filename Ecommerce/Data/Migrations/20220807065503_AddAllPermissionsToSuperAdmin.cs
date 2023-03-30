using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    public partial class AddAllPermissionsToSuperAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Users.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Users.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Users.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Users.Delete')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Roles.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Roles.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Roles.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Roles.Delete')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Brands.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Brands.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Brands.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Brands.Delete')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Categories.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Categories.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Categories.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Categories.Delete')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Departments.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Departments.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Departments.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Departments.Delete')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Orders.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Orders.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Orders.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Orders.Delete')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Products.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Products.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Products.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Products.Delete')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Supplier.View')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Supplier.Create')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Supplier.Edit')");
            migrationBuilder.Sql("INSERT INTO[dbo].[RoleClaims] ([RoleId], [ClaimType], [ClaimValue]) VALUES(N'7d46cbd3-e7b8-440c-a8fb-6f03a3c30719', N'Permission', N'Permissions.Supplier.Delete')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[RoleClaims] WHERE RoleId = '7d46cbd3-e7b8-440c-a8fb-6f03a3c30719'");
        }
    }
}
