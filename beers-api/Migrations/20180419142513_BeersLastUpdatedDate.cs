using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace beersapi.Migrations
{
    public partial class BeersLastUpdatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Beers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Beers");
        }
    }
}
