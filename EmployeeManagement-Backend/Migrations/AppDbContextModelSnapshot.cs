﻿// <auto-generated />
using System;
using EmployeeManagement_Backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmployeeManagement_Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Varchar(50)");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("CompanyId")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            Id = "0d559e48-d19c-405d-b0dd-fdd8a46d0a8a",
                            Address = "Bekasi",
                            Email = "superadmin@email.com",
                            FullName = "Super Admin",
                            IsActive = true,
                            Password = "$2a$11$vRuGfcDB.4zEp.rpYGE3TumF3SUs3mzIXAlDRYwyEYyQ/yqzCCczm",
                            RoleId = "1",
                            UserName = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Attendance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Varchar(50)");

                    b.Property<string>("AttendanceCodeId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AttendanceCodeId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.AttendanceCode", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AttendanceCode");

                    b.HasData(
                        new
                        {
                            Id = "A1",
                            Name = "Sakit"
                        },
                        new
                        {
                            Id = "A2",
                            Name = "Izin"
                        },
                        new
                        {
                            Id = "A3",
                            Name = "Cuti"
                        },
                        new
                        {
                            Id = "A4",
                            Name = "Alpa"
                        },
                        new
                        {
                            Id = "A5",
                            Name = "Lainya"
                        });
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Varchar(50)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Company");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Varchar(50)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("JoinDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.LoginHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("LoginHistory");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClientName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("LongDay")
                        .HasColumnType("integer");

                    b.Property<string>("ProjectCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProjectStatusId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProjectStatusId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.ProjectStatus", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("projectStatus");

                    b.HasData(
                        new
                        {
                            Id = "P1",
                            Name = "Perencanaan"
                        },
                        new
                        {
                            Id = "P2",
                            Name = "Analisis"
                        },
                        new
                        {
                            Id = "P3",
                            Name = "Sedang Berlangsung"
                        },
                        new
                        {
                            Id = "P4",
                            Name = "Selesai"
                        });
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "SuperAdmin"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Owner"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.TR_EmployeeProject", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("TR_Employee_Project");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Account", b =>
                {
                    b.HasOne("EmployeeManagement_Backend.Entities.Company", "Company")
                        .WithMany("Accounts")
                        .HasForeignKey("CompanyId");

                    b.HasOne("EmployeeManagement_Backend.Entities.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Attendance", b =>
                {
                    b.HasOne("EmployeeManagement_Backend.Entities.AttendanceCode", "EmployeeCodeId")
                        .WithMany()
                        .HasForeignKey("AttendanceCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagement_Backend.Entities.Company", "Company")
                        .WithMany("Attendances")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagement_Backend.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Employee");

                    b.Navigation("EmployeeCodeId");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Employee", b =>
                {
                    b.HasOne("EmployeeManagement_Backend.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.LoginHistory", b =>
                {
                    b.HasOne("EmployeeManagement_Backend.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Project", b =>
                {
                    b.HasOne("EmployeeManagement_Backend.Entities.Company", "Company")
                        .WithMany("Projects")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagement_Backend.Entities.ProjectStatus", "ProjectStatus")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("ProjectStatus");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.TR_EmployeeProject", b =>
                {
                    b.HasOne("EmployeeManagement_Backend.Entities.Employee", "Employee")
                        .WithMany("TrEmployeeProjects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagement_Backend.Entities.Project", "Project")
                        .WithMany("TrEmployeeProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Company", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Attendances");

                    b.Navigation("Employees");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Employee", b =>
                {
                    b.Navigation("TrEmployeeProjects");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Project", b =>
                {
                    b.Navigation("TrEmployeeProjects");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.ProjectStatus", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("EmployeeManagement_Backend.Entities.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
