using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CandH_API.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Strip",
                columns: table => new
                {
                    ComicStripId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Image = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OriginalPrintDate = table.Column<DateTime>(nullable: true),
                    Transcript = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strip", x => x.ComicStripId);
                });

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    ComicUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.ComicUserId);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ComicStripCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComicStripId = table.Column<int>(nullable: false),
                    ComicUserId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ComicStripCommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Strip_ComicStripId",
                        column: x => x.ComicStripId,
                        principalTable: "Strip",
                        principalColumn: "ComicStripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Reader_ComicUserId",
                        column: x => x.ComicUserId,
                        principalTable: "Reader",
                        principalColumn: "ComicUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emotion",
                columns: table => new
                {
                    ComicStripEmotionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComicStripId = table.Column<int>(nullable: false),
                    ComicUserId = table.Column<int>(nullable: false),
                    Emotion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotion", x => x.ComicStripEmotionId);
                    table.ForeignKey(
                        name: "FK_Emotion_Strip_ComicStripId",
                        column: x => x.ComicStripId,
                        principalTable: "Strip",
                        principalColumn: "ComicStripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emotion_Reader_ComicUserId",
                        column: x => x.ComicUserId,
                        principalTable: "Reader",
                        principalColumn: "ComicUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    FavoriteComicStripId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComicStripId = table.Column<int>(nullable: false),
                    ComicUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.FavoriteComicStripId);
                    table.ForeignKey(
                        name: "FK_Favorite_Strip_ComicStripId",
                        column: x => x.ComicStripId,
                        principalTable: "Strip",
                        principalColumn: "ComicStripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorite_Reader_ComicUserId",
                        column: x => x.ComicUserId,
                        principalTable: "Reader",
                        principalColumn: "ComicUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ComicStripId",
                table: "Comment",
                column: "ComicStripId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ComicUserId",
                table: "Comment",
                column: "ComicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emotion_ComicStripId",
                table: "Emotion",
                column: "ComicStripId");

            migrationBuilder.CreateIndex(
                name: "IX_Emotion_ComicUserId",
                table: "Emotion",
                column: "ComicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ComicStripId",
                table: "Favorite",
                column: "ComicStripId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ComicUserId",
                table: "Favorite",
                column: "ComicUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Emotion");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "Strip");

            migrationBuilder.DropTable(
                name: "Reader");
        }
    }
}
