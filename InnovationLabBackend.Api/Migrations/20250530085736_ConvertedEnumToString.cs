using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnovationLabBackend.Api.Migrations
{
    /// <inheritdoc />
    public partial class ConvertedEnumToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convert Banners.Type from integer to text (string)
            migrationBuilder.Sql(
                @"ALTER TABLE ""Banners"" 
                  ALTER COLUMN ""Type"" TYPE text 
                  USING ""Type""::text;");

            // Add unique index on AspNetUsers.Email (your original migration logic)
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert Banners.Type from text back to integer
            migrationBuilder.Sql(
                @"ALTER TABLE ""Banners"" 
                  ALTER COLUMN ""Type"" TYPE integer 
                  USING ""Type""::integer;");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");
        }
    }
}