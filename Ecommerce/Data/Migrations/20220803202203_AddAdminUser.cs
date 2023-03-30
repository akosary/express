using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'079201cf-1595-4197-8032-f428f52f3eab', N'Mahmoud_gamal2022', N'MAHMOUD_GAMAL2022', N'Mahmoud_gamal2022@yahoo.com', N'MAHMOUD_GAMAL2022@YAHOO.COM', 1, N'AQAAAAEAACcQAAAAECFqS/gDvO2yghqi1mxSqUM8OXTxWfRZQ90Fii1Nh5CYxbom3Nru9WlXBZI2Y/dqQQ==', N'CDN5LDHSOJG3GCCTNXZK4PY7EOL7D7TE', N'7d9e4bfa-ca3f-4d00-9ec9-ab3ac632f6d0', NULL, 0, 0, NULL, 1, 0, N'Mahmoud', N'Gamal', NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users WHERE Id = '079201cf-1595-4197-8032-f428f52f3eab'");
        }
    }
}
