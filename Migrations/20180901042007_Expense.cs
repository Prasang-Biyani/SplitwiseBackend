using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Splitwise_Backend.Migrations
{
    public partial class Expense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Group",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Friend",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cost = table.Column<int>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Deleted_At = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExpenseId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_ExpenseId",
                table: "User",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ExpenseId",
                table: "Group",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_ExpenseId",
                table: "Friend",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ExpenseId",
                table: "Category",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Expense_ExpenseId",
                table: "Friend",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Expense_ExpenseId",
                table: "Group",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Expense_ExpenseId",
                table: "User",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Expense_ExpenseId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Expense_ExpenseId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Expense_ExpenseId",
                table: "User");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_User_ExpenseId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Group_ExpenseId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Friend_ExpenseId",
                table: "Friend");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Friend");
        }
    }
}
