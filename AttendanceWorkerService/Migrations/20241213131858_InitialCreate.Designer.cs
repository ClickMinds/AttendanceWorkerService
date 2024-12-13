﻿// <auto-generated />
using System;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AttendanceWorkerService.Migrations
{
    [DbContext(typeof(AttendanceDbContext))]
    [Migration("20241213131858_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AttendanceWorkerService.src.Core.Entities.AttendanceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttendanceStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CheckInTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOutTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<double>("TotalHoursWorked")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AttendanceRecords");
                });

            modelBuilder.Entity("AttendanceWorkerService.src.Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ReportingManager")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ShiftScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShiftScheduleId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Himanshu Shekhar",
                            ReportingManager = "Himanshu Shukla",
                            ShiftScheduleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pankaj Bahuguna",
                            ReportingManager = "Himanshu Shukla",
                            ShiftScheduleId = 2
                        });
                });

            modelBuilder.Entity("AttendanceWorkerService.src.Core.Entities.ShiftSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("ShiftSchedules");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            EndTime = new TimeSpan(0, 19, 0, 0, 0),
                            StartTime = new TimeSpan(0, 10, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("AttendanceWorkerService.src.Core.Entities.AttendanceRecord", b =>
                {
                    b.HasOne("AttendanceWorkerService.src.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AttendanceWorkerService.src.Core.Entities.Employee", b =>
                {
                    b.HasOne("AttendanceWorkerService.src.Core.Entities.ShiftSchedule", "ShiftSchedule")
                        .WithMany()
                        .HasForeignKey("ShiftScheduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ShiftSchedule");
                });
#pragma warning restore 612, 618
        }
    }
}