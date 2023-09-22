﻿// <auto-generated />
using System;
using MachineCheckup.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachineCheckup.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MachineCheckup.Domain.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("boolean");

                    b.Property<int>("MachineId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MachineId");

                    b.ToTable("Issues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description for Issue 1",
                            IsResolved = false,
                            MachineId = 1,
                            Name = "Issue 1",
                            Priority = 2,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description for Issue 2",
                            IsResolved = true,
                            MachineId = 2,
                            Name = "Issue 2",
                            Priority = 1,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 3,
                            Description = "Description for Issue 3",
                            IsResolved = false,
                            MachineId = 3,
                            Name = "Issue 3",
                            Priority = 0,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 4,
                            Description = "Description for Issue 4",
                            IsResolved = true,
                            MachineId = 4,
                            Name = "Issue 4",
                            Priority = 1,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 5,
                            Description = "Description for Issue 5",
                            IsResolved = false,
                            MachineId = 1,
                            Name = "Issue 5",
                            Priority = 2,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 6,
                            Description = "Description for Issue 6",
                            IsResolved = true,
                            MachineId = 2,
                            Name = "Issue 6",
                            Priority = 0,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 7,
                            Description = "Description for Issue 7",
                            IsResolved = false,
                            MachineId = 3,
                            Name = "Issue 7",
                            Priority = 2,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 8,
                            Description = "Description for Issue 8",
                            IsResolved = true,
                            MachineId = 4,
                            Name = "Issue 8",
                            Priority = 1,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 9,
                            Description = "Description for Issue 9",
                            IsResolved = false,
                            MachineId = 1,
                            Name = "Issue 9",
                            Priority = 0,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 10,
                            Description = "Description for Issue 10",
                            IsResolved = true,
                            MachineId = 2,
                            Name = "Issue 10",
                            Priority = 1,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 11,
                            Description = "Description for Issue 11",
                            IsResolved = false,
                            MachineId = 3,
                            Name = "Issue 11",
                            Priority = 2,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        },
                        new
                        {
                            Id = 12,
                            Description = "Description for Issue 12",
                            IsResolved = true,
                            MachineId = 4,
                            Name = "Issue 12",
                            Priority = 0,
                            StartTime = new DateTime(2023, 9, 22, 15, 34, 52, 575, DateTimeKind.Utc).AddTicks(3656)
                        });
                });

            modelBuilder.Entity("MachineCheckup.Domain.Entities.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Machines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Machine 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Machine 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Machine 3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Machine 4"
                        });
                });

            modelBuilder.Entity("MachineCheckup.Domain.Entities.Issue", b =>
                {
                    b.HasOne("MachineCheckup.Domain.Entities.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });
#pragma warning restore 612, 618
        }
    }
}