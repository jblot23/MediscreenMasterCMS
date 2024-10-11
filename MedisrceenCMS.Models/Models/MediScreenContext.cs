using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MedisrceenCMS.Models.Models;

public partial class MediScreenContext : DbContext
{
    public MediScreenContext()
    {
    }

    public MediScreenContext(DbContextOptions<MediScreenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyCheckupReport> DailyCheckupReports { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorShift> DoctorShifts { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientIllness> PatientIllnesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MediScreen;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyCheckupReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__DailyChe__D5BD4805E427BB1E");

            entity.ToTable("DailyCheckupReport");

            entity.Property(e => e.CheckupDetails).HasMaxLength(1000);

            entity.HasOne(d => d.Doctor).WithMany(p => p.DailyCheckupReports)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyChec__Docto__4222D4EF");

            entity.HasOne(d => d.Illness).WithMany(p => p.DailyCheckupReports)
                .HasForeignKey(d => d.IllnessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyChec__Illne__4316F928");

            entity.HasOne(d => d.Note).WithMany(p => p.DailyCheckupReports)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyChec__NoteI__440B1D61");

            entity.HasOne(d => d.Patient).WithMany(p => p.DailyCheckupReports)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyChec__Patie__412EB0B6");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EBFF70EDC68");

            entity.ToTable("Doctor");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Specialty).HasMaxLength(50);
        });

        modelBuilder.Entity<DoctorShift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__DoctorSh__C0A83881B623023A");

            entity.ToTable("DoctorShift");

            entity.Property(e => e.ShiftTime).HasMaxLength(50);

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorShifts)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorShi__Docto__46E78A0C");

            entity.HasOne(d => d.Patient).WithMany(p => p.DoctorShifts)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorShi__Patie__47DBAE45");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Note__EACE355F9D6999D1");

            entity.ToTable("Note");

            entity.Property(e => e.NoteText).HasMaxLength(1000);

            entity.HasOne(d => d.Patient).WithMany(p => p.Notes)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Note__PatientId__3E52440B");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC366D03D68E8");

            entity.ToTable("Patient");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<PatientIllness>(entity =>
        {
            entity.HasKey(e => e.IllnessId).HasName("PK__PatientI__2BA575BB7005FB2D");

            entity.ToTable("PatientIllness");

            entity.Property(e => e.IllnessName).HasMaxLength(100);
            entity.Property(e => e.Treatment).HasMaxLength(1000);

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientIllnesses)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientIl__Patie__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
