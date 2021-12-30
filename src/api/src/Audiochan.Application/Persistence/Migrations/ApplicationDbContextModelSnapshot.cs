﻿// <auto-generated />
using System;
using Audiochan.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Audiochan.Application.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Audiochan.Domain.Entities.Audio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal>("Duration")
                        .HasColumnType("numeric")
                        .HasColumnName("duration");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("file");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("Picture")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("picture");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<string[]>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("tags");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_audios");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_audios_created");

                    b.HasIndex("Tags")
                        .HasDatabaseName("ix_audios_tags");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("Tags"), "GIN");

                    b.HasIndex("Title")
                        .HasDatabaseName("ix_audios_title");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_audios_user_id");

                    b.ToTable("audios", (string)null);
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.FavoriteAudio", b =>
                {
                    b.Property<long>("AudioId")
                        .HasColumnType("bigint")
                        .HasColumnName("audio_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("Favorited")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("favorited");

                    b.HasKey("AudioId", "UserId")
                        .HasName("pk_favorite_audios");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_favorite_audios_user_id");

                    b.ToTable("favorite_audios", (string)null);
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.FollowedUser", b =>
                {
                    b.Property<long>("ObserverId")
                        .HasColumnType("bigint")
                        .HasColumnName("observer_id");

                    b.Property<long>("TargetId")
                        .HasColumnType("bigint")
                        .HasColumnName("target_id");

                    b.Property<DateTime>("FollowedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("followed_date");

                    b.Property<DateTime?>("UnfollowedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("unfollowed_date");

                    b.HasKey("ObserverId", "TargetId")
                        .HasName("pk_followed_users");

                    b.HasIndex("FollowedDate")
                        .HasDatabaseName("ix_followed_users_followed_date");

                    b.HasIndex("TargetId")
                        .HasDatabaseName("ix_followed_users_target_id");

                    b.ToTable("followed_users", (string)null);
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("Picture")
                        .HasColumnType("text")
                        .HasColumnName("picture");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasDatabaseName("ix_users_user_name");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.Audio", b =>
                {
                    b.HasOne("Audiochan.Domain.Entities.User", "User")
                        .WithMany("Audios")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_audios_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.FavoriteAudio", b =>
                {
                    b.HasOne("Audiochan.Domain.Entities.Audio", "Audio")
                        .WithMany("FavoriteAudios")
                        .HasForeignKey("AudioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_favorite_audios_audios_audio_id");

                    b.HasOne("Audiochan.Domain.Entities.User", "User")
                        .WithMany("FavoriteAudios")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_favorite_audios_users_user_id");

                    b.Navigation("Audio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.FollowedUser", b =>
                {
                    b.HasOne("Audiochan.Domain.Entities.User", "Observer")
                        .WithMany("Followings")
                        .HasForeignKey("ObserverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_followed_users_users_observer_id");

                    b.HasOne("Audiochan.Domain.Entities.User", "Target")
                        .WithMany("Followers")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_followed_users_users_target_id");

                    b.Navigation("Observer");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.Audio", b =>
                {
                    b.Navigation("FavoriteAudios");
                });

            modelBuilder.Entity("Audiochan.Domain.Entities.User", b =>
                {
                    b.Navigation("Audios");

                    b.Navigation("FavoriteAudios");

                    b.Navigation("Followers");

                    b.Navigation("Followings");
                });
#pragma warning restore 612, 618
        }
    }
}
