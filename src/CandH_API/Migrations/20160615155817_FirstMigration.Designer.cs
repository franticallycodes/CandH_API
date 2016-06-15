using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CandH_API.Models;

namespace CandH_API.Migrations
{
    [DbContext(typeof(CandH_Context))]
    [Migration("20160615155817_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CandH_API.Models.ComicStrip", b =>
                {
                    b.Property<int>("ComicStripId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name");

                    b.Property<DateTime>("OriginalPrintDate");

                    b.Property<string>("Transcript");

                    b.HasKey("ComicStripId");

                    b.ToTable("Strip");
                });

            modelBuilder.Entity("CandH_API.Models.ComicStripComment", b =>
                {
                    b.Property<int>("ComicStripCommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComicStripId");

                    b.Property<int>("ComicUserId");

                    b.Property<string>("Comment");

                    b.HasKey("ComicStripCommentId");

                    b.HasIndex("ComicStripId");

                    b.HasIndex("ComicUserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CandH_API.Models.ComicStripEmotion", b =>
                {
                    b.Property<int>("ComicStripEmotionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComicStripId");

                    b.Property<int>("ComicUserId");

                    b.Property<string>("Emotion");

                    b.HasKey("ComicStripEmotionId");

                    b.HasIndex("ComicStripId");

                    b.HasIndex("ComicUserId");

                    b.ToTable("Emotion");
                });

            modelBuilder.Entity("CandH_API.Models.ComicUser", b =>
                {
                    b.Property<int>("ComicUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Username");

                    b.HasKey("ComicUserId");

                    b.ToTable("Reader");
                });

            modelBuilder.Entity("CandH_API.Models.FavoriteComicStrip", b =>
                {
                    b.Property<int>("FavoriteComicStripId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComicStripId");

                    b.Property<int>("ComicUserId");

                    b.HasKey("FavoriteComicStripId");

                    b.HasIndex("ComicStripId");

                    b.HasIndex("ComicUserId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("CandH_API.Models.ComicStripComment", b =>
                {
                    b.HasOne("CandH_API.Models.ComicStrip")
                        .WithMany()
                        .HasForeignKey("ComicStripId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CandH_API.Models.ComicUser")
                        .WithMany()
                        .HasForeignKey("ComicUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CandH_API.Models.ComicStripEmotion", b =>
                {
                    b.HasOne("CandH_API.Models.ComicStrip")
                        .WithMany()
                        .HasForeignKey("ComicStripId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CandH_API.Models.ComicUser")
                        .WithMany()
                        .HasForeignKey("ComicUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CandH_API.Models.FavoriteComicStrip", b =>
                {
                    b.HasOne("CandH_API.Models.ComicStrip")
                        .WithMany()
                        .HasForeignKey("ComicStripId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CandH_API.Models.ComicUser")
                        .WithMany()
                        .HasForeignKey("ComicUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
