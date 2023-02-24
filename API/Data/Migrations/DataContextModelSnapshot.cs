﻿// <auto-generated />
using System;
using System.Collections.Generic;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.Account", b =>
                {
                    b.Property<int>("AccId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AccId"));

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KTP")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("No_HP")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<int?>("ProfilePhotoPhotoId")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("AccId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProfilePhotoPhotoId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("API.Entities.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AlbumId"));

                    b.Property<string>("AlbumName")
                        .HasColumnType("text");

                    b.HasKey("AlbumId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("API.Entities.ForgotPasswordHistory", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("CheckCount")
                        .HasColumnType("integer");

                    b.Property<string>("Otp")
                        .HasColumnType("text");

                    b.Property<int>("RequestCount")
                        .HasColumnType("integer");

                    b.HasKey("Date", "Email");

                    b.ToTable("ForgotPasswordHistories");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PhotoId"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("BigTags")
                        .HasColumnType("integer[]");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("PhotoId");

                    b.HasIndex("AlbumId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("API.Entities.Race", b =>
                {
                    b.Property<int>("RaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RaceId"));

                    b.Property<int?>("RaceAlbumAlbumId")
                        .HasColumnType("integer");

                    b.Property<string>("RaceName")
                        .HasColumnType("text");

                    b.HasKey("RaceId");

                    b.HasIndex("RaceAlbumAlbumId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("API.Entities.RaceAttendance", b =>
                {
                    b.Property<int>("RaceId")
                        .HasColumnType("integer");

                    b.Property<int>("BibNumber")
                        .HasColumnType("integer");

                    b.Property<int?>("RunnerAccId")
                        .HasColumnType("integer");

                    b.HasKey("RaceId", "BibNumber");

                    b.HasIndex("RunnerAccId");

                    b.ToTable("RaceAttendance");
                });

            modelBuilder.Entity("API.Entities.Account", b =>
                {
                    b.HasOne("API.Entities.Photo", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoPhotoId");

                    b.Navigation("ProfilePhoto");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.HasOne("API.Entities.Album", null)
                        .WithMany("AlbumPhotos")
                        .HasForeignKey("AlbumId");
                });

            modelBuilder.Entity("API.Entities.Race", b =>
                {
                    b.HasOne("API.Entities.Album", "RaceAlbum")
                        .WithMany()
                        .HasForeignKey("RaceAlbumAlbumId");

                    b.Navigation("RaceAlbum");
                });

            modelBuilder.Entity("API.Entities.RaceAttendance", b =>
                {
                    b.HasOne("API.Entities.Race", null)
                        .WithMany("RaceAttendee")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Account", "Runner")
                        .WithMany()
                        .HasForeignKey("RunnerAccId");

                    b.Navigation("Runner");
                });

            modelBuilder.Entity("API.Entities.Album", b =>
                {
                    b.Navigation("AlbumPhotos");
                });

            modelBuilder.Entity("API.Entities.Race", b =>
                {
                    b.Navigation("RaceAttendee");
                });
#pragma warning restore 612, 618
        }
    }
}
