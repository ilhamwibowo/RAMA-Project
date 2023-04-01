﻿// <auto-generated />
using System;
using System.Collections.Generic;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230401070333_add_new_column_race")]
    partial class addnewcolumnrace
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.Account", b =>
                {
                    b.Property<Guid>("AccId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

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

                    b.Property<Guid?>("ProfilePhotoPhotoId")
                        .HasColumnType("uuid");

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
                    b.Property<Guid>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

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

            modelBuilder.Entity("API.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<string>("Latitude")
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("RaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid?>("AlbumId")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("BibTags")
                        .HasColumnType("text[]");

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
                    b.Property<Guid>("RaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<int>("Distance")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("EndDateRegistration")
                        .HasColumnType("date");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("RaceAlbumAlbumId")
                        .HasColumnType("uuid");

                    b.Property<string>("RaceDesc")
                        .HasColumnType("text");

                    b.Property<string>("RaceName")
                        .HasColumnType("text");

                    b.Property<Guid?>("RaceThumbnailPhotoId")
                        .HasColumnType("uuid");

                    b.Property<int>("RegistrationFee")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("StartDateRegistration")
                        .HasColumnType("date");

                    b.Property<Guid?>("StartLocationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RaceId");

                    b.HasIndex("RaceAlbumAlbumId");

                    b.HasIndex("RaceThumbnailPhotoId");

                    b.HasIndex("StartLocationId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("API.Entities.RaceAttendance", b =>
                {
                    b.Property<Guid>("RaceId")
                        .HasColumnType("uuid");

                    b.Property<string>("BibNumber")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTime>("FinishTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RunnerAccId")
                        .HasColumnType("uuid");

                    b.HasKey("RaceId", "BibNumber");

                    b.HasIndex("RunnerAccId");

                    b.ToTable("RaceAttendance");
                });

            modelBuilder.Entity("API.Entities.RaceRegistration", b =>
                {
                    b.Property<Guid>("RaceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RegistedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("AccountAccId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PaidAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RegistrationFee")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TakenKitAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RaceId", "AccId", "RegistedAt");

                    b.HasIndex("AccountAccId");

                    b.ToTable("RaceRegistrations");
                });

            modelBuilder.Entity("API.Entities.Account", b =>
                {
                    b.HasOne("API.Entities.Photo", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoPhotoId");

                    b.Navigation("ProfilePhoto");
                });

            modelBuilder.Entity("API.Entities.Location", b =>
                {
                    b.HasOne("API.Entities.Race", null)
                        .WithMany("Points")
                        .HasForeignKey("RaceId");
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

                    b.HasOne("API.Entities.Photo", "RaceThumbnail")
                        .WithMany()
                        .HasForeignKey("RaceThumbnailPhotoId");

                    b.HasOne("API.Entities.Location", "StartLocation")
                        .WithMany()
                        .HasForeignKey("StartLocationId");

                    b.Navigation("RaceAlbum");

                    b.Navigation("RaceThumbnail");

                    b.Navigation("StartLocation");
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

            modelBuilder.Entity("API.Entities.RaceRegistration", b =>
                {
                    b.HasOne("API.Entities.Account", null)
                        .WithMany("RaceHistory")
                        .HasForeignKey("AccountAccId");
                });

            modelBuilder.Entity("API.Entities.Account", b =>
                {
                    b.Navigation("RaceHistory");
                });

            modelBuilder.Entity("API.Entities.Album", b =>
                {
                    b.Navigation("AlbumPhotos");
                });

            modelBuilder.Entity("API.Entities.Race", b =>
                {
                    b.Navigation("Points");

                    b.Navigation("RaceAttendee");
                });
#pragma warning restore 612, 618
        }
    }
}
