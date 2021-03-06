﻿// <auto-generated />
using FeatureFlag.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FeatureFlag.Infrastructure.Migrations
{
    [DbContext(typeof(FeatureFlagContext))]
    [Migration("20201120142150_Initial schema")]
    partial class Initialschema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FeatureFlag.Infrastructure.Models.Environment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.ToTable("Environments");
                });

            modelBuilder.Entity("FeatureFlag.Infrastructure.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("FeatureFlag.Infrastructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EnvironmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FeatureFlag.Infrastructure.Models.Environment", b =>
                {
                    b.HasOne("FeatureFlag.Infrastructure.Models.Feature", "Feature")
                        .WithMany("Environments")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FeatureFlag.Infrastructure.Models.User", b =>
                {
                    b.HasOne("FeatureFlag.Infrastructure.Models.Environment", null)
                        .WithMany("UsersEnabled")
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
