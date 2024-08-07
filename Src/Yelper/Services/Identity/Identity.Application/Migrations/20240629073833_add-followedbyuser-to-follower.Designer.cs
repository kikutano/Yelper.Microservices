﻿// <auto-generated />
using System;
using Identity.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Identity.Application.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20240629073833_add-followedbyuser-to-follower")]
    partial class addfollowedbyusertofollower
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Identity.Domain.AggregatesModel.FollowerAggregate.Follower", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FollowedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Following")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FollowedByUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("Identity.Domain.AggregatesModel.FollowingAggregate.Following", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FollowingUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FollowingUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Followings");
                });

            modelBuilder.Entity("Identity.Domain.AggregatesModel.SecurityAggregate.Security", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Securities");
                });

            modelBuilder.Entity("Identity.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("At")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubscriptionAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("At");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Identity.Domain.AggregatesModel.FollowerAggregate.Follower", b =>
                {
                    b.HasOne("Identity.Domain.AggregatesModel.UserAggregate.User", "FollowedByUser")
                        .WithMany()
                        .HasForeignKey("FollowedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.Domain.AggregatesModel.UserAggregate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FollowedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Identity.Domain.AggregatesModel.FollowingAggregate.Following", b =>
                {
                    b.HasOne("Identity.Domain.AggregatesModel.UserAggregate.User", "FollowingUser")
                        .WithMany()
                        .HasForeignKey("FollowingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.Domain.AggregatesModel.UserAggregate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FollowingUser");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
