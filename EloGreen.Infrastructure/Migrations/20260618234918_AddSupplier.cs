using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EloGreen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_EG_SUPPLIER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TX_NAME = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    NR_DOCUMENT = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    ST_ESG_CERTIFIED = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DT_CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EG_SUPPLIER", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_SUP_NAME",
                table: "TB_EG_SUPPLIER",
                column: "TX_NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_EG_SUPPLIER");
        }
    }
}
