using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class KoiPondConstructionManagementContext : DbContext
{
    public KoiPondConstructionManagementContext()
    {
    }

    public KoiPondConstructionManagementContext(DbContextOptions<KoiPondConstructionManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConstructionProcess> ConstructionProcesses { get; set; }

    public virtual DbSet<ConstructionRequest> ConstructionRequests { get; set; }

    public virtual DbSet<CustomerOrderHistory> CustomerOrderHistories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<KoiDesign> KoiDesigns { get; set; }

    public virtual DbSet<MaintenanceService> MaintenanceServices { get; set; }

    public virtual DbSet<PaymentPolicy> PaymentPolicies { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=localhost;Database=KoiPondConstructionManagement;User Id=sa;Password=12345678;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConstructionProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__Construc__1B39A976DC28FE0E");

            entity.ToTable("ConstructionProcess");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.AssignedStaffId).HasColumnName("AssignedStaffID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Step);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AssignedStaff).WithMany(p => p.ConstructionProcesses)
                .HasForeignKey(d => d.AssignedStaffId)
                .HasConstraintName("FK__Construct__Assig__45F365D3");

            entity.HasOne(d => d.Request).WithMany(p => p.ConstructionProcesses)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__Construct__Reque__44FF419A");
        });

        modelBuilder.Entity<ConstructionRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Construc__33A8519A9A183ECA");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomDesignDescription);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DesignId).HasColumnName("DesignID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.ConstructionRequests)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Construct__Custo__3F466844");

            entity.HasOne(d => d.Design).WithMany(p => p.ConstructionRequests)
                .HasForeignKey(d => d.DesignId)
                .HasConstraintName("FK__Construct__Desig__403A8C7D");
        });

        modelBuilder.Entity<CustomerOrderHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Customer__4D7B4ADD623FA230");

            entity.ToTable("CustomerOrderHistory");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.ActualCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerOrderHistories)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CustomerO__Custo__5CD6CB2B");

            entity.HasOne(d => d.Request).WithMany(p => p.CustomerOrderHistories)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__CustomerO__Reque__5DCAEF64");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF672535547");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Feedback__Custom__5441852A");

            entity.HasOne(d => d.Request).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__Feedback__Reques__5535A963");
        });

        modelBuilder.Entity<KoiDesign>(entity =>
        {
            entity.HasKey(e => e.DesignId).HasName("PK__KoiDesig__32B8E17F671C8093");

            entity.Property(e => e.DesignId).HasColumnName("DesignID");
            entity.Property(e => e.CostEstimate).HasMaxLength(100);
            entity.Property(e => e.Description);
            entity.Property(e => e.DesignImage).HasMaxLength(2048);
            entity.Property(e => e.DesignName).HasMaxLength(100);
        });

        modelBuilder.Entity<MaintenanceService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Maintena__C51BB0EAF9178D2D");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Description);
            entity.Property(e => e.ServiceImage).HasMaxLength(2048);
            entity.Property(e => e.Price).HasMaxLength(100);
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        modelBuilder.Entity<PaymentPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__PaymentP__2E13394406BD6B45");

            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            entity.Property(e => e.Description);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42F2F325EA4D0");

            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.Description);
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PromotionName).HasMaxLength(100);
            entity.Property(e => e.PromotionImage).HasMaxLength(2048);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3AC9B5849C");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC0619AF3F");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105349EE3CA92").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Avatar).HasMaxLength(2048);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
