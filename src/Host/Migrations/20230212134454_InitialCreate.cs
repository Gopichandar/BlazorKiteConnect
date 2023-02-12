using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorKiteConnect.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentToken = table.Column<int>(type: "INTEGER", nullable: false),
                    ExchangeToken = table.Column<int>(type: "INTEGER", nullable: false),
                    TradingSymbol = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Expiry = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Strike = table.Column<decimal>(type: "TEXT", nullable: false),
                    TickSize = table.Column<decimal>(type: "TEXT", nullable: false),
                    LotSize = table.Column<int>(type: "INTEGER", nullable: false),
                    InstrumentType = table.Column<string>(type: "TEXT", nullable: false),
                    Segment = table.Column<string>(type: "TEXT", nullable: false),
                    Exchange = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instruments");
        }
    }
}
