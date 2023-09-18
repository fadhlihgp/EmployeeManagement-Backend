using EmployeeManagement_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_Backend.Context;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<AttendanceCode> AttendanceCodes => Set<AttendanceCode>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<LoginHistory> LoginHistories => Set<LoginHistory>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectStatus> ProjectsStatus => Set<ProjectStatus>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<TR_EmployeeProject> TrEmployeeProjects => Set<TR_EmployeeProject>();

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasIndex(a => a.Email).IsUnique();
            builder.HasIndex(a => a.UserName).IsUnique();
        });

        modelBuilder.Entity<Company>(builder =>
        {
            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => c.PhoneNumber).IsUnique();
        });

        modelBuilder.Entity<Employee>(builder =>
        {
            builder.HasIndex(e => e.PhoneNumber).IsUnique();
        });

        modelBuilder.Entity<AttendanceCode>().HasData(
            new AttendanceCode{ Id = "A1", Name = "Sakit" },
            new AttendanceCode{ Id = "A2", Name = "Izin"},
            new AttendanceCode{ Id = "A3", Name = "Cuti"},
            new AttendanceCode{ Id = "A4", Name = "Alpa"},
            new AttendanceCode{ Id = "A5", Name = "Lainya"});

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = "1", Name = "SuperAdmin" },
            new Role { Id = "2", Name = "Owner" },
            new Role { Id = "3", Name = "Admin" });

        modelBuilder.Entity<Account>().HasData(
            new Account
            {
                Id = Guid.NewGuid().ToString(), FullName = "Super Admin", Email = "superadmin@email.com",
                UserName = "SuperAdmin", Address = "Bekasi", Password = "$2a$11$vRuGfcDB.4zEp.rpYGE3TumF3SUs3mzIXAlDRYwyEYyQ/yqzCCczm", RoleId = "1" 
            });

        modelBuilder.Entity<ProjectStatus>().HasData(
            new ProjectStatus { Id = "P1", Name = "Perencanaan" },
            new ProjectStatus { Id = "P2", Name = "Analisis" },
            new ProjectStatus { Id = "P3", Name = "Sedang Berlangsung" },
            new ProjectStatus { Id = "P4", Name = "Selesai" });
    }
}