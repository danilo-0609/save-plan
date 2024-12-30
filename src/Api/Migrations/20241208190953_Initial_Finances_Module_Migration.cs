using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavePlan.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Finances_Module_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "finances");

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                schema: "finances",
                columns: table => new
                {
                    ExpenseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.ExpenseCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "IncomeSources",
                schema: "finances",
                columns: table => new
                {
                    IncomeSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeSources", x => x.IncomeSourceId);
                });

            migrationBuilder.CreateTable(
                name: "SavingGoals",
                schema: "finances",
                columns: table => new
                {
                    SavingGoalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SavingGoalReason = table.Column<string>(type: "text", nullable: false),
                    SavingGoalCycle = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingGoals", x => x.SavingGoalId);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                schema: "finances",
                columns: table => new
                {
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Date = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ExpenseCycle = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalSchema: "finances",
                        principalTable: "ExpenseCategories",
                        principalColumn: "ExpenseCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                schema: "finances",
                columns: table => new
                {
                    IncomeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IncomeSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IncomeCycle = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Date = table.Column<TimeSpan>(type: "interval", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IncomeId);
                    table.ForeignKey(
                        name: "FK_Incomes_IncomeSources_IncomeSourceId",
                        column: x => x.IncomeSourceId,
                        principalSchema: "finances",
                        principalTable: "IncomeSources",
                        principalColumn: "IncomeSourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                schema: "finances",
                table: "ExpenseCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_UserId",
                schema: "finances",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseCategoryId",
                schema: "finances",
                table: "Expenses",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IncomeSourceId",
                schema: "finances",
                table: "Incomes",
                column: "IncomeSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                schema: "finances",
                table: "Incomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeSources_UserId",
                schema: "finances",
                table: "IncomeSources",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingGoals_UserId",
                schema: "finances",
                table: "SavingGoals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Incomes",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "SavingGoals",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "ExpenseCategories",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "IncomeSources",
                schema: "finances");
        }
    }
}
