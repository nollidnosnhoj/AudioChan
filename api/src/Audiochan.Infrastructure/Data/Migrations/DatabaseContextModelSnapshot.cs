﻿// <auto-generated />
using System;
using Audiochan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Audiochan.Infrastructure.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("AudioTag", b =>
                {
                    b.Property<string>("AudiosId")
                        .HasColumnType("TEXT")
                        .HasColumnName("audios_id");

                    b.Property<string>("TagsId")
                        .HasColumnType("TEXT")
                        .HasColumnName("tags_id");

                    b.HasKey("AudiosId", "TagsId")
                        .HasName("pk_audio_tags");

                    b.HasIndex("TagsId")
                        .HasDatabaseName("ix_audio_tags_tags_id");

                    b.ToTable("audio_tags");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER")
                        .HasColumnName("duration");

                    b.Property<string>("FileExt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT")
                        .HasColumnName("file_ext");

                    b.Property<long>("FileSize")
                        .HasColumnType("INTEGER")
                        .HasColumnName("file_size");

                    b.Property<long>("GenreId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("genre_id");

                    b.Property<bool>("IsLoop")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_loop");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_public");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT")
                        .HasColumnName("url");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_audios");

                    b.HasIndex("GenreId")
                        .HasDatabaseName("ix_audios_genre_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_audios_user_id");

                    b.ToTable("audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FavoriteAudio", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.Property<string>("AudioId")
                        .HasColumnType("TEXT")
                        .HasColumnName("audio_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified");

                    b.HasKey("UserId", "AudioId")
                        .HasName("pk_favorite_audios");

                    b.HasIndex("AudioId")
                        .HasDatabaseName("ix_favorite_audios_audio_id");

                    b.ToTable("favorite_audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FollowedUser", b =>
                {
                    b.Property<long>("ObserverId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("observer_id");

                    b.Property<long>("TargetId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("target_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified");

                    b.HasKey("ObserverId", "TargetId")
                        .HasName("pk_followed_users");

                    b.HasIndex("TargetId")
                        .HasDatabaseName("ix_followed_users_target_id");

                    b.ToTable("followed_users");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("slug");

                    b.HasKey("Id")
                        .HasName("pk_genres");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("role_name_index");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("About")
                        .HasColumnType("TEXT")
                        .HasColumnName("about");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("display_name");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER")
                        .HasColumnName("email_confirmed");

                    b.Property<DateTime>("Joined")
                        .HasColumnType("TEXT")
                        .HasColumnName("joined");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("user_name");

                    b.Property<string>("Website")
                        .HasColumnType("TEXT")
                        .HasColumnName("website");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("email_index");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("user_name_index");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT")
                        .HasColumnName("claim_value");

                    b.Property<long>("RoleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_claims_role_id");

                    b.ToTable("role_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT")
                        .HasColumnName("claim_value");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_claims_user_id");

                    b.ToTable("user_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT")
                        .HasColumnName("provider_display_name");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_logins_user_id");

                    b.ToTable("user_logins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_roles_role_id");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_user_tokens");

                    b.ToTable("user_tokens");
                });

            modelBuilder.Entity("AudioTag", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Audio", null)
                        .WithMany()
                        .HasForeignKey("AudiosId")
                        .HasConstraintName("fk_audio_tags_audios_audios_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .HasConstraintName("fk_audio_tags_tags_tags_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Genre", "Genre")
                        .WithMany("Audios")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("fk_audios_genres_genre_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("Audios")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_audios_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FavoriteAudio", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Audio", "Audio")
                        .WithMany("Favorited")
                        .HasForeignKey("AudioId")
                        .HasConstraintName("fk_favorite_audios_audios_audio_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("FavoriteAudios")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_favorite_audios_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FollowedUser", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", "Observer")
                        .WithMany("Followers")
                        .HasForeignKey("ObserverId")
                        .HasConstraintName("fk_followed_users_users_observer_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", "Target")
                        .WithMany("Followings")
                        .HasForeignKey("TargetId")
                        .HasConstraintName("fk_followed_users_users_target_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Observer");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
                {
                    b.OwnsMany("Audiochan.Core.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER")
                                .HasColumnName("id");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("TEXT")
                                .HasColumnName("created");

                            b1.Property<DateTime>("Expiry")
                                .HasColumnType("TEXT")
                                .HasColumnName("expiry");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("TEXT")
                                .HasColumnName("replaced_by_token");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("TEXT")
                                .HasColumnName("revoked");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("token");

                            b1.Property<long>("UserId")
                                .HasColumnType("INTEGER")
                                .HasColumnName("user_id");

                            b1.HasKey("Id")
                                .HasName("pk_refresh_tokens");

                            b1.HasIndex("UserId")
                                .HasDatabaseName("ix_refresh_tokens_user_id");

                            b1.ToTable("refresh_tokens");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_refresh_tokens_users_user_id");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_role_claims_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_claims_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_logins_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_user_roles_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_roles_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_tokens_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.Navigation("Favorited");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Genre", b =>
                {
                    b.Navigation("Audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
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
