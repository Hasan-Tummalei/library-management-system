using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingLoansRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Books_BookId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Borrowers_BorrowerId",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loan",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Loan");

            migrationBuilder.RenameTable(
                name: "Loan",
                newName: "Loans");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_BorrowerId",
                table: "Loans",
                newName: "IX_Loans_BorrowerId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_BookId",
                table: "Loans",
                newName: "IX_Loans_BookId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReturnDate",
                table: "Loans",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Borrowers_BorrowerId",
                table: "Loans",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Borrowers_BorrowerId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "Loan");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BorrowerId",
                table: "Loan",
                newName: "IX_Loan_BorrowerId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BookId",
                table: "Loan",
                newName: "IX_Loan_BookId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReturnDate",
                table: "Loan",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DueDate",
                table: "Loan",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loan",
                table: "Loan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Books_BookId",
                table: "Loan",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Borrowers_BorrowerId",
                table: "Loan",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
