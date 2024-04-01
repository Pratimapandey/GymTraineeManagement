using Gym.Models;
using Gym.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data
{
    public class GymDbContext : IdentityDbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        
        public DbSet<GymTrainee> Trainees { get; set; }
        public DbSet<TrainingLevel> TrainingLevels { get; set; }
        public DbSet<MonthlyFeeVoucher> MonthlyFeeVouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<GymTraineeDetailViewModel>()
                .HasOne(d => d.gymTrainee)
                .WithMany()
              .HasForeignKey(d => d.TraineeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GymTraineeDetailViewModel>()
                .HasOne(d => d.trainingLevel)
                .WithMany()
                .HasForeignKey(d => d.TrainingLevelID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GymTraineeDetailViewModel>()
                .HasOne(d => d.monthlyFeeVoucher)
                .WithMany()
                .HasForeignKey(d => d.MonthlyFeeVoucherID)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}

