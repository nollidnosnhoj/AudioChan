﻿// <auto-generated />
using System;
using Audiochan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Audiochan.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AudiochanContext))]
    [Migration("20210103075827_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration");

                    b.Property<string>("FileExt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("file_ext");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint")
                        .HasColumnName("file_size");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean")
                        .HasColumnName("is_public");

                    b.Property<bool>("IsUploaded")
                        .HasColumnType("boolean")
                        .HasColumnName("is_uploaded");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_modified");

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

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_audios_user_id");

                    b.ToTable("audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.AudioTag", b =>
                {
                    b.Property<string>("AudioId")
                        .HasColumnType("text")
                        .HasColumnName("audio_id");

                    b.Property<string>("TagId")
                        .HasColumnType("text")
                        .HasColumnName("tag_id");

                    b.HasKey("AudioId", "TagId")
                        .HasName("pk_audio_tags");

                    b.HasIndex("TagId")
                        .HasDatabaseName("ix_audio_tags_tag_id");

                    b.ToTable("audio_tags");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FavoriteAudio", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("AudioId")
                        .HasColumnType("text")
                        .HasColumnName("audio_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone")
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
                        .HasColumnType("bigint")
                        .HasColumnName("observer_id");

                    b.Property<long>("TargetId")
                        .HasColumnType("bigint")
                        .HasColumnName("target_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_modified");

                    b.HasKey("ObserverId", "TargetId")
                        .HasName("pk_followed_users");

                    b.HasIndex("TargetId")
                        .HasDatabaseName("ix_followed_users_target_id");

                    b.ToTable("followed_users");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
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
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("display_name");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_modified");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("email_index");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("user_name_index");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_roles_role_id");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
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
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
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
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_logins_user_id");

                    b.ToTable("user_logins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_user_tokens");

                    b.ToTable("user_tokens");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("Audios")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_audios_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.AudioTag", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Audio", "Audio")
                        .WithMany("Tags")
                        .HasForeignKey("AudioId")
                        .HasConstraintName("fk_audio_tags_audios_audio_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.Tag", "Tag")
                        .WithMany("Audios")
                        .HasForeignKey("TagId")
                        .HasConstraintName("fk_audio_tags_tags_tag_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("Tag");
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
                    b.OwnsOne("Audiochan.Core.Entities.Profile", "Profile", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasColumnName("id")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("About")
                                .HasColumnType("text")
                                .HasColumnName("about");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("created");

                            b1.Property<DateTime?>("LastModified")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("last_modified");

                            b1.Property<long>("UserId")
                                .HasColumnType("bigint")
                                .HasColumnName("user_id");

                            b1.Property<string>("Website")
                                .HasColumnType("text")
                                .HasColumnName("website");

                            b1.HasKey("Id")
                                .HasName("pk_profiles");

                            b1.HasIndex("UserId")
                                .IsUnique()
                                .HasDatabaseName("ix_users_user_id");

                            b1.ToTable("profiles");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_profile_users_user_id");
                        });

                    b.OwnsMany("Audiochan.Core.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasColumnName("id")
                                .UseIdentityByDefaultColumn();

                            b1.Property<DateTime>("Created")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("created");

                            b1.Property<DateTime>("Expiry")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("expiry");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("text")
                                .HasColumnName("replaced_by_token");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("revoked");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("token");

                            b1.Property<long>("UserId")
                                .HasColumnType("bigint")
                                .HasColumnName("user_id");

                            b1.HasKey("Id")
                                .HasName("pk_refresh_tokens");

                            b1.HasIndex("UserId")
                                .HasDatabaseName("ix_refresh_token_user_id");

                            b1.ToTable("refresh_tokens");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_refresh_token_users_user_id");
                        });

                    b.Navigation("Profile")
                        .IsRequired();

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.UserRole", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_user_roles_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_roles_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_role_claims_asp_net_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_claims_asp_net_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_logins_asp_net_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_tokens_asp_net_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.Navigation("Favorited");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Tag", b =>
                {
                    b.Navigation("Audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
                {
                    b.Navigation("Audios");

                    b.Navigation("FavoriteAudios");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
