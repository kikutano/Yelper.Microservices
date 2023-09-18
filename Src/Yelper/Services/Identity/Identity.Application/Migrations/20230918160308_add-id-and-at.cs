using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Application.Migrations
{
    /// <inheritdoc />
    public partial class addidandat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Identifier",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FollowingIdentifier",
                table: "Followings");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Followings");

            migrationBuilder.DropColumn(
                name: "FollowedByIdentifier",
                table: "Followers");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Followers");

            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "Users",
                newName: "At");

            migrationBuilder.AddColumn<Guid>(
                name: "FollowingUserId",
                table: "Followings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FollowedByUserId",
                table: "Followers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_At",
                table: "Users",
                column: "At")
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_At",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FollowingUserId",
                table: "Followings");

            migrationBuilder.DropColumn(
                name: "FollowedByUserId",
                table: "Followers");

            migrationBuilder.RenameColumn(
                name: "At",
                table: "Users",
                newName: "Identifier");

            migrationBuilder.AddColumn<string>(
                name: "FollowingIdentifier",
                table: "Followings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Followings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FollowedByIdentifier",
                table: "Followers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Followers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Identifier",
                table: "Users",
                column: "Identifier",
                unique: true);
        }
    }
}
