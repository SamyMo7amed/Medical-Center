﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Medical_CenterAPI.Models;
using System.Reflection.Emit;

namespace Medical_CenterAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser,IdentityRole<Guid>,Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {




        }

        //  Table definition

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentConfirmation> AppointmentConfirmations { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // relationships  

            // Doctor --- Appointment
            builder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            // Patiant ---- Appointment
            builder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patiant)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.SetNull);
            // Assistant --- AppointmentConfirmations
            builder.Entity<Assistant>()
                .HasMany(a => a.AppointmentConfirmations)
                .WithOne(c => c.Assistant)
                .HasForeignKey(c => c.AssistantId)
                .OnDelete(DeleteBehavior.SetNull);
            // Appointment --- AppointmentConfirmations
            builder.Entity<AppointmentConfirmation>()

                .HasOne(ac => ac.Appointment)
                .WithOne(a => a.AppointmentConfirmation)
                .HasForeignKey<AppointmentConfirmation>(ac => ac.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);


        }



    }

}
