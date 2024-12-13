namespace AttendanceWorkerService.src.Infrastructure.Data
{
    using global::AttendanceWorkerService.src.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;

    namespace AttendanceWorkerService.Infrastructure.Data
    {
        /// <summary>
        /// Represents the database context for the attendance management system.
        /// Manages entities and their relationships, providing access to the database.
        /// </summary>
        public class AttendanceDbContext : DbContext
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AttendanceDbContext"/> class
            /// with the specified options.
            /// </summary>
            /// <param name="options">Options for configuring the DbContext.</param>
            public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options)
            {
            }

            // DbSets representing tables in the database

            /// <summary>
            /// Gets or sets the Employees table.
            /// </summary>
            public DbSet<Employee> Employees { get; set; }

            /// <summary>
            /// Gets or sets the AttendanceRecords table.
            /// </summary>
            public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

            /// <summary>
            /// Gets or sets the ShiftSchedules table.
            /// </summary>
            public DbSet<ShiftSchedule> ShiftSchedules { get; set; }

            /// <summary>
            /// Configures the entity models and their relationships using the Fluent API.
            /// </summary>
            /// <param name="modelBuilder">The builder used to configure the models.</param>
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Employee entity configuration
                modelBuilder.Entity<Employee>(entity =>
                {
                    entity.HasKey(e => e.Id); // Primary key configuration
                    entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // Name is required
                    entity.Property(e => e.ReportingManager).HasMaxLength(100); // Optional reporting manager

                    // Relationship: Employee has one ShiftSchedule
                    entity.HasOne(e => e.ShiftSchedule)
                          .WithMany()
                          .HasForeignKey(e => e.ShiftScheduleId)
                          .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of related shifts
                });

                // AttendanceStatus entity configuration
                modelBuilder.Entity<Core.Entities.AttendanceStatus>(entity =>
                {
                    entity.HasKey(e => e.Id); // Primary key configuration
                    entity.Property(e => e.StatusName)
                          .IsRequired()
                          .HasMaxLength(10); // Enforce string length for StatusName
                });

                // AttendanceRecord entity configuration
                modelBuilder.Entity<AttendanceRecord>(entity =>
                {
                    entity.HasKey(a => a.Id); // Primary key configuration

                    // Nullable fields for check-in and check-out times
                    entity.Property(a => a.CheckInTime);
                    entity.Property(a => a.CheckOutTime);

                    // Total hours worked with decimal precision and default value
                    entity.Property(a => a.TotalHoursWorked)
                          .HasColumnType("decimal(5, 2)")
                          .HasDefaultValue(0);

                    // Relationship: AttendanceRecord belongs to an Employee
                    entity.HasOne(a => a.Employee)
                          .WithMany()
                          .HasForeignKey(a => a.EmployeeId)
                          .OnDelete(DeleteBehavior.Cascade); // Cascade deletion of records

                    // Relationship: AttendanceRecord has one AttendanceStatus
                    entity.HasOne(a => a.AttendanceStatus)
                          .WithMany()
                          .HasForeignKey(a => a.AttendanceStatusId)
                          .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of statuses in use
                });

                // ShiftSchedule entity configuration
                modelBuilder.Entity<ShiftSchedule>(entity =>
                {
                    entity.HasKey(s => s.Id); // Primary key configuration
                    entity.Property(s => s.StartTime).IsRequired(); // Start time is required
                    entity.Property(s => s.EndTime).IsRequired(); // End time is required
                });

                // Seed data for ShiftSchedule table
                modelBuilder.Entity<ShiftSchedule>().HasData(
                    new ShiftSchedule { Id = 1, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(18, 0, 0) },
                    new ShiftSchedule { Id = 2, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(19, 0, 0) }
                );

                // Seed data for Employee table
                modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "Himanshu Shekhar", ReportingManager = "Himanshu Shukla", ShiftScheduleId = 1 },
                    new Employee { Id = 2, Name = "Pankaj Bahuguna", ReportingManager = "Himanshu Shukla", ShiftScheduleId = 2 }
                );
            }
        }
    }
}
