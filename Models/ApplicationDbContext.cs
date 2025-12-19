using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ABC_Company.Models;

namespace ABC_Company.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblApplicant> TblApplicants { get; set; }

    public virtual DbSet<TblJob> TblJobs { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<StaffModel> TblStaff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblApplicant>(entity =>
        {
            entity.HasKey(e => e.ApplicantId).HasName("PK__tblAppli__39AE91A83DE253FA");

            entity.Property(e => e.ApplicantCreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Job).WithMany(p => p.TblApplicants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblApplic__JobId__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.TblApplicants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblApplic__UserI__440B1D61");
        });

        modelBuilder.Entity<TblJob>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__tblJobs__056690C267965D5E");

            entity.Property(e => e.JobCreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.TblJobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblJobs__RoleId__3B75D760");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblRoles__8AFACE1A01B5F767");

            entity.Property(e => e.RoleCreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblUsers__1788CC4CED076670");

            entity.Property(e => e.UserCreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
