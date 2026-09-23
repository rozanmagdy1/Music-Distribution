using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicDistribution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dsps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dsps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    ISRC = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackDistributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    DspId = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackDistributions_Dsps_DspId",
                        column: x => x.DspId,
                        principalTable: "Dsps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackDistributions_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Country", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "Egypt", "nour.eldin@example.com", "Nour El Din" },
                    { 2, "Lebanon", "maya.hassan@example.com", "Maya Hassan" },
                    { 3, "Jordan", "omar.farouk@example.com", "Omar Farouk" }
                });

            migrationBuilder.InsertData(
                table: "Dsps",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Spotify" },
                    { 2, "Apple Music" },
                    { 3, "YouTube" }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "ArtistId", "Genre", "ISRC", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Arabic Pop", "EGABC2600001", new DateOnly(2024, 2, 16), 2, "Cairo at Dawn" },
                    { 2, 1, "Indie Pop", "EGABC2600002", new DateOnly(2023, 9, 8), 1, "Nile Lights" },
                    { 3, 2, "Alternative", "LBDEF2600001", new DateOnly(2022, 6, 3), 2, "Beirut Blue" },
                    { 4, 2, "Acoustic", "LBDEF2600002", new DateOnly(2024, 11, 22), 0, "Paper Moon" },
                    { 5, 3, "Electronic", "JOXYZ2600001", new DateOnly(2021, 4, 14), 2, "Desert Radio" },
                    { 6, 3, "R&B", "JOXYZ2600002", new DateOnly(2025, 1, 10), 1, "Amman Nights" },
                    { 7, 2, "Folk", "LBDEF2600003", new DateOnly(2020, 12, 1), 0, "Olive Branches" },
                    { 8, 1, "Jazz", "EGABC2600003", new DateOnly(2023, 3, 27), 2, "Open Skies" }
                });

            migrationBuilder.InsertData(
                table: "TrackDistributions",
                columns: new[] { "Id", "DspId", "Status", "SubmittedAt", "TrackId" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTimeOffset(new DateTime(2024, 1, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 2, 1, new DateTimeOffset(new DateTime(2024, 1, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 3, 3, 0, new DateTimeOffset(new DateTime(2024, 1, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, 1, 0, new DateTimeOffset(new DateTime(2023, 9, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 5, 2, 1, new DateTimeOffset(new DateTime(2022, 5, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 6, 1, 2, new DateTimeOffset(new DateTime(2024, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 7, 3, 1, new DateTimeOffset(new DateTime(2021, 4, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 8, 2, 0, new DateTimeOffset(new DateTime(2025, 1, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 6 },
                    { 9, 1, 1, new DateTimeOffset(new DateTime(2023, 3, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dsps_Name",
                table: "Dsps",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackDistributions_DspId",
                table: "TrackDistributions",
                column: "DspId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackDistributions_TrackId_DspId",
                table: "TrackDistributions",
                columns: new[] { "TrackId", "DspId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ISRC",
                table: "Tracks",
                column: "ISRC",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackDistributions");

            migrationBuilder.DropTable(
                name: "Dsps");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
