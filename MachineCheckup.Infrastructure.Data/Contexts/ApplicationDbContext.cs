using MachineCheckup.Domain.Entities;
using MachineCheckup.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MachineCheckup.Infrastructure.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var utcNow = DateTime.UtcNow;
            // Seed Machines
            modelBuilder.Entity<Machine>().HasData(
                new Machine { Id = 1, Name = "Machine 1" },
                new Machine { Id = 2, Name = "Machine 2" },
                new Machine { Id = 3, Name = "Machine 3" },
                new Machine { Id = 4, Name = "Machine 4" }

            );

            // Seed Issues
            modelBuilder.Entity<Issue>().HasData(
                new Issue
                {
                    Id = 1,
                    Name = "Issue 1",
                    Priority = Priority.High,
                    StartTime = utcNow,
                    Description = "Description for Issue 1",
                    IsResolved = false,
                    MachineId = 1
                },
                new Issue
                {
                    Id = 2,
                    Name = "Issue 2",
                    Priority = Priority.Medium,
                    StartTime = utcNow,
                    Description = "Description for Issue 2",
                    IsResolved = true,
                    MachineId = 2
                },
                new Issue
                {
                    Id = 3,
                    Name = "Issue 3",
                    Priority = Priority.Low,
                    StartTime = utcNow,
                    Description = "Description for Issue 3",
                    IsResolved = false,
                    MachineId = 3
                },
                new Issue
                {
                    Id = 4,
                    Name = "Issue 4",
                    Priority = Priority.Medium,
                    StartTime = utcNow,
                    Description = "Description for Issue 4",
                    IsResolved = true,
                    MachineId = 4
                },
                new Issue
                {
                    Id = 5,
                    Name = "Issue 5",
                    Priority = Priority.High,
                    StartTime = utcNow,
                    Description = "Description for Issue 5",
                    IsResolved = false,
                    MachineId = 1
                },
                new Issue
                {
                    Id = 6,
                    Name = "Issue 6",
                    Priority = Priority.Low,
                    StartTime = utcNow,
                    Description = "Description for Issue 6",
                    IsResolved = true,
                    MachineId = 2
                },
                new Issue
                {
                    Id = 7,
                    Name = "Issue 7",
                    Priority = Priority.High,
                    StartTime = utcNow,
                    Description = "Description for Issue 7",
                    IsResolved = false,
                    MachineId = 3
                },
                new Issue
                {
                    Id = 8,
                    Name = "Issue 8",
                    Priority = Priority.Medium,
                    StartTime = utcNow,
                    Description = "Description for Issue 8",
                    IsResolved = true,
                    MachineId = 4
                },
                new Issue
                {
                    Id = 9,
                    Name = "Issue 9",
                    Priority = Priority.Low,
                    StartTime = utcNow,
                    Description = "Description for Issue 9",
                    IsResolved = false,
                    MachineId = 1
                },
                new Issue
                {
                    Id = 10,
                    Name = "Issue 10",
                    Priority = Priority.Medium,
                    StartTime = utcNow,
                    Description = "Description for Issue 10",
                    IsResolved = true,
                    MachineId = 2
                },
                new Issue
                {
                    Id = 11,
                    Name = "Issue 11",
                    Priority = Priority.High,
                    StartTime = utcNow,
                    Description = "Description for Issue 11",
                    IsResolved = false,
                    MachineId = 3
                },
                new Issue
                {
                    Id = 12,
                    Name = "Issue 12",
                    Priority = Priority.Low,
                    StartTime = utcNow,
                    Description = "Description for Issue 12",
                    IsResolved = true,
                    MachineId = 4
                }
            );
        }
    }
}
