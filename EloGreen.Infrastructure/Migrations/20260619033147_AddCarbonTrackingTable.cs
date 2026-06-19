using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EloGreen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarbonTrackingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_EG_CARBON_TRACKING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TX_ACTIVITY_DESC = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    NR_CARBON_EMITTED = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    DT_TRACKING = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ProductId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EG_CARBON_TRACKING", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRACKING_PRODUCT",
                        column: x => x.ProductId,
                        principalTable: "TB_EG_PRODUCT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_EG_CARBON_TRACKING_ProductId",
                table: "TB_EG_CARBON_TRACKING",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_EG_CARBON_TRACKING");
        }
    }
}
