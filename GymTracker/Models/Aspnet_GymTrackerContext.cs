using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GymTracker.Models
{
    public partial class Aspnet_GymTrackerContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<DailyProgress> DailyProgress { get; set; }
        public virtual DbSet<DailyRoutine> DailyRoutine { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Exercise> Exercise { get; set; }
        public virtual DbSet<Gym> Gym { get; set; }
        public virtual DbSet<Trainee> Trainee { get; set; }
        public virtual DbSet<TraineeGoals> TraineeGoals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-GymTracker;Trusted_Connection=True;");
            }
        }

        public Aspnet_GymTrackerContext(DbContextOptions<Aspnet_GymTrackerContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ApplicationUserConfiguration(modelBuilder.Entity<ApplicationUser>());
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Picture).HasMaxLength(250);

                entity.Property(e => e.Surname).HasMaxLength(150);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Gym)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.GymId)
                    .HasConstraintName("FK_AspNetUsers_Gym");
            });
            
            modelBuilder.Entity<DailyProgress>(entity =>
            {
                entity.HasKey(e => e.ProgressId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TraineeId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.DailyProgress)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyProgress_Exercise");

                entity.HasOne(d => d.Trainee)
                    .WithMany(p => p.DailyProgress)
                    .HasForeignKey(d => d.TraineeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyProgress_AspNetUsers");
            });

            modelBuilder.Entity<DailyRoutine>(entity =>
            {
                entity.HasKey(e => e.RoutineId);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TraineeId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.DailyRoutine)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyRoutine_Exercise");

                entity.HasOne(d => d.Trainee)
                    .WithMany(p => p.DailyRoutine)
                    .HasForeignKey(d => d.TraineeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyRoutine_AspNetUsers");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TrainerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_AspNetUsers");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.GifPicture)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Gym>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Trainee>(entity =>
            {
                entity.Property(e => e.TraineeId).ValueGeneratedNever();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.TrainerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.TraineeNavigation)
                    .WithOne(p => p.TraineeTraineeNavigation)
                    .HasForeignKey<Trainee>(d => d.TraineeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trainee_AspNetUsers");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.TraineeTrainer)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trainee_AspNetUsers1");
            });

            modelBuilder.Entity<TraineeGoals>(entity =>
            {
                entity.HasKey(e => e.GoalId);

                entity.Property(e => e.ByDate).HasColumnType("date");

                entity.Property(e => e.TraineeId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Trainee)
                    .WithMany(p => p.TraineeGoals)
                    .HasForeignKey(d => d.TraineeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TraineeGoals_AspNetUsers");
            });
        }
    }
}
