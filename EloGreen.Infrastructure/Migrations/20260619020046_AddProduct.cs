using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EloGreen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_EG_PRODUCT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TX_NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TX_DESCRIPTION = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    NR_CARBON_FOOTPRINT = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    DT_CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EG_PRODUCT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUCT_SUPPLIER",
                        column: x => x.SupplierId,
                        principalTable: "TB_EG_SUPPLIER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_EG_PRODUCT_SupplierId",
                table: "TB_EG_PRODUCT",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_EG_PRODUCT");
        }
    }
}
