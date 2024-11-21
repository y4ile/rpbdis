using Microsoft.EntityFrameworkCore;

namespace lab4.Models
{
    public partial class PlansContext : DbContext
    {
        public PlansContext(DbContextOptions<PlansContext> options) : base(options) { }

        public virtual DbSet<DevelopmentDirection> DevelopmentDirections { get; set; }

        public virtual DbSet<PlanStage> PlanStages { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<StudyPlan> StudyPlans { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevelopmentDirection>(entity =>
            {
                entity.HasKey(e => e.DirectionID).HasName("PK__Developm__876846264ED3BABD");

                entity.Property(e => e.DirectionID).HasColumnName("DirectionID");
                entity.Property(e => e.DirectionName).HasMaxLength(50);
            });

            modelBuilder.Entity<PlanStage>(entity =>
            {
                entity.HasKey(e => e.StageID).HasName("PK__PlanStag__03EB7AF86433D57F");

                entity.Property(e => e.StageID).HasColumnName("StageID");
                entity.Property(e => e.PlanId).HasColumnName("PlanID");
                entity.Property(e => e.StageName).HasMaxLength(50);
                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Plan).WithMany(p => p.PlanStages)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlanStages_To_StudyPlans");

                entity.HasOne(d => d.Status).WithMany(p => p.PlanStages)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlanStages_To_Statuses");
            });

            modelBuilder.Entity<PlanStagesWithInfo>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("PlanStagesWithInfo");

                entity.Property(e => e.PlanName).HasMaxLength(100);
                entity.Property(e => e.StageID).HasColumnName("StageID");
                entity.Property(e => e.StageName).HasMaxLength(50);
                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StatusId).HasName("PK__Statuses__C8EE20431F0BE42F");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<StudyPlan>(entity =>
            {
                entity.HasKey(e => e.PlanID).HasName("PK__StudyPla__755C22D7C9CFBA5E");

                entity.Property(e => e.PlanID).HasColumnName("PlanID");
                entity.Property(e => e.DirectionId).HasColumnName("DirectionID");
                entity.Property(e => e.PlanName).HasMaxLength(100);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Direction).WithMany(p => p.StudyPlans)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_StudyPlans_To_DevelopmentDirections");

                entity.HasOne(d => d.User).WithMany(p => p.StudyPlans)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_StudyPlans_To_Users");
            });

            modelBuilder.Entity<StudyPlanWithUserInfo>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("StudyPlanWithUserInfo");

                entity.Property(e => e.DirectionName).HasMaxLength(50);
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PlanID).HasColumnName("PlanID");
                entity.Property(e => e.PlanName).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID).HasName("PK__Users__1788CCAC1D507487");

                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersWithStudyPlan>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("UsersWithStudyPlans");

                entity.Property(e => e.DirectionName).HasMaxLength(50);
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PlanID).HasColumnName("PlanID");
                entity.Property(e => e.PlanName).HasMaxLength(100);
                entity.Property(e => e.UserID).HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
