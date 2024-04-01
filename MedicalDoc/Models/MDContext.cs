using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MedicalDoc.Models;

public partial class MDContext : DbContext
{
    public MDContext()
    {
    }

    public MDContext(DbContextOptions<MDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Healthinsurance> Healthinsurances { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PrescribedMedication> PrescribedMedications { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("username")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.IdPatient, "ID_Patient");

            entity.Property(e => e.IdAppointment).HasColumnName("ID_Appointment");
            entity.Property(e => e.AdditionalNotes)
                .HasColumnType("text")
                .HasColumnName("Additional_Notes");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IdPatient).HasColumnName("ID_Patient");
            entity.Property(e => e.Status).HasColumnType("enum('Scheduled','Cancelled','Completed')");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdPatient)
                .HasConstraintName("appointments_ibfk_1");
        });

        modelBuilder.Entity<Healthinsurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PRIMARY");

            entity.ToTable("healthinsurance");

            entity.Property(e => e.InsuranceName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PolicyHolderAddress)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.PolicyHolderEmail).HasMaxLength(100);
            entity.Property(e => e.PolicyHolderFirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PolicyHolderGender)
                .IsRequired()
                .HasColumnType("enum('Male','Female','Other')");
            entity.Property(e => e.PolicyHolderLastName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PolicyHolderPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.PolicyNumber)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.HasKey(e => e.IdHistory).HasName("PRIMARY");

            entity.ToTable("medical_history");

            entity.HasIndex(e => e.IdPatient, "ID_Patient");

            entity.Property(e => e.IdHistory).HasColumnName("ID_History");
            entity.Property(e => e.Diagnosis).HasMaxLength(255);
            entity.Property(e => e.DoctorNotes)
                .HasColumnType("text")
                .HasColumnName("Doctor_Notes");
            entity.Property(e => e.IdPatient).HasColumnName("ID_Patient");
            entity.Property(e => e.TestResults)
                .HasColumnType("text")
                .HasColumnName("Test_Results");
            entity.Property(e => e.Treatment).HasColumnType("text");
            entity.Property(e => e.VisitDate).HasColumnName("Visit_Date");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.MedicalHistories)
                .HasForeignKey(d => d.IdPatient)
                .HasConstraintName("medical_history_ibfk_1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.IdPatient).HasName("PRIMARY");

            entity.ToTable("patients");

            entity.Property(e => e.IdPatient).HasColumnName("ID_Patient");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_of_Birth");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.Gender).HasColumnType("enum('Male','Female','Other')");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("Phone_Number");
        });

        modelBuilder.Entity<PrescribedMedication>(entity =>
        {
            entity.HasKey(e => e.IdMedication).HasName("PRIMARY");

            entity.ToTable("prescribed_medications");

            entity.HasIndex(e => e.IdPatient, "ID_Patient");

            entity.Property(e => e.IdMedication).HasColumnName("ID_Medication");
            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.IdPatient).HasColumnName("ID_Patient");
            entity.Property(e => e.MedicationName)
                .HasMaxLength(100)
                .HasColumnName("Medication_Name");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.UsageInstructions)
                .HasColumnType("text")
                .HasColumnName("Usage_Instructions");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.PrescribedMedications)
                .HasForeignKey(d => d.IdPatient)
                .HasConstraintName("prescribed_medications_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
