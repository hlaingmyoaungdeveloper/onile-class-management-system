using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OCMS.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblEnrollment> TblEnrollments { get; set; }

    public virtual DbSet<TblSubClass> TblSubClasses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ASPIERLITE16;Database=OCMSDb;User Id = sa;Password = sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);

            entity.ToTable("Tbl_Enrollment", tb => tb.HasTrigger("trg_UpdateEnrollmentModifiedTime"));

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FatherName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentInfo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentContact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.SubClass).WithMany(p => p.TblEnrollments)
                .HasForeignKey(d => d.SubClassId)
                .HasConstraintName("FK_Tbl_Enrollment_Tbl_SubClass");
        });

        modelBuilder.Entity<TblSubClass>(entity =>
        {
            entity.HasKey(e => e.SubClassId);

            entity.ToTable("Tbl_SubClass", tb => tb.HasTrigger("trg_UpdateSubClassModifiedTime"));

            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
