namespace AttendanceWorkerService.src.Infrastructure.Data
{
    using global::AttendanceWorkerService.src.Core.Entities;
    using Microsoft.EntityFrameworkCore;

    namespace AttendanceWorkerService.Infrastructure.Data
    {
        public class AttendanceDbContext : DbContext
        {
            public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options)
            {
            }

            // DbSets for the entities
            public DbSet<Employee> Employees { get; set; }
            public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
            public DbSet<ShiftSchedule> ShiftSchedules { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Employee entity configuration
                modelBuilder.Entity<Employee>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.ReportingManager).HasMaxLength(100);

                    entity.HasOne(e => e.ShiftSchedule)
                          .WithMany()
                          .HasForeignKey(e => e.ShiftScheduleId)
                          .OnDelete(DeleteBehavior.Restrict);
                });

                // AttendanceRecord entity configuration
                modelBuilder.Entity<AttendanceRecord>(entity =>
                {
                    entity.HasKey(a => a.Id);
                    entity.Property(a => a.CheckInTime);
                    entity.Property(a => a.CheckOutTime);
                    entity.Property(a => a.TotalHoursWorked);//.HasColumnType("decimal(5, 2)");

                    entity.HasOne(a => a.Employee)
                          .WithMany()
                          .HasForeignKey(a => a.EmployeeId)
                          .OnDelete(DeleteBehavior.Cascade);

                    entity.Property(a => a.Status).IsRequired();
                });

                // ShiftSchedule entity configuration
                modelBuilder.Entity<ShiftSchedule>(entity =>
                {
                    entity.HasKey(s => s.Id);
                    entity.Property(s => s.StartTime).IsRequired();
                    entity.Property(s => s.EndTime).IsRequired();
                });

                // Seed data (optional, for development/testing purposes)
                modelBuilder.Entity<ShiftSchedule>().HasData(
                    new ShiftSchedule { Id = 1, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(18, 0, 0) },
                    new ShiftSchedule { Id = 2, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(19, 0, 0) }
                );

                modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "John Doe", ReportingManager = "Jane Smith", ShiftScheduleId = 1 },
                    new Employee { Id = 2, Name = "Alice Brown", ReportingManager = "Jane Smith", ShiftScheduleId = 2 }
                );
            }
        }
    }
}
