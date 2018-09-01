using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Splitwise_Backend.Migrations
{
    public partial class Friend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "Picture",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Member",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Member",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "Group",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    FriendId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    RegistrationStatus = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.FriendId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_FriendId",
                table: "Picture",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_FriendId",
                table: "Group",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Friend_FriendId",
                table: "Group",
                column: "FriendId",
                principalTable: "Friend",
                principalColumn: "FriendId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Friend_FriendId",
                table: "Picture",
                column: "FriendId",
                principalTable: "Friend",
                principalColumn: "FriendId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Friend_FriendId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Friend_FriendId",
                table: "Picture");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropIndex(
                name: "IX_Picture_FriendId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Group_FriendId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Group");

            migrationBuilder.AlterColumn<int>(
                name: "LastName",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstName",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
