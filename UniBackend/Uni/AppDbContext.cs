using Microsoft.EntityFrameworkCore;
using UniBackend.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ComputerModel> Computer => Set<ComputerModel>();
    public DbSet<UserModel> User => Set<UserModel>();
    public DbSet<OrderModel> Order => Set<OrderModel>();
    public DbSet<OrderItemModel> OrderItem => Set<OrderItemModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.ToTable("User");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Username)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.HasIndex(e => e.Username)
                  .IsUnique();

            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.Property(e => e.PasswordHash)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Role)
                  .IsRequired()
                  .HasMaxLength(20)
                  .HasDefaultValue("User");

            entity.Property(e => e.RegisteredOn)
                  .IsRequired()
                  .HasDefaultValueSql("NOW()");

            entity.Property(e => e.IsActive)
                  .IsRequired()
                  .HasDefaultValue(true);
        });

        modelBuilder.Entity<ComputerModel>(entity =>
        {
            entity.ToTable("Computer");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Description)
                  .HasMaxLength(500);

            entity.Property(e => e.ImageUrl)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(e => e.Price)
                  .IsRequired()
                  .HasColumnType("decimal(10,2)");

            entity.Property(e => e.Stock)
                  .IsRequired();

            entity.Property(e => e.Type)
                  .HasMaxLength(50);

            entity.Property(e => e.Processor)
                  .HasMaxLength(100);

            entity.Property(e => e.RAM)
                  .HasMaxLength(50);

            entity.Property(e => e.Storage)
                  .HasMaxLength(100);

            entity.Property(e => e.ScreenSize)
                  .HasColumnType("decimal(4,2)");

            entity.Property(e => e.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<OrderModel>(entity =>
        {
            entity.ToTable("Order");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("NOW()");

            entity.Property(e => e.Total)
                  .IsRequired()
                  .HasColumnType("decimal(10,2)");

            entity.Property(e => e.Status)
                  .IsRequired()
                  .HasMaxLength(20)
                  .HasDefaultValue("Pending");

            entity.Property(e => e.OrderAddress)
                  .IsRequired()
                  .HasMaxLength(200);

        });

        modelBuilder.Entity<OrderItemModel>(entity =>
        {
            entity.ToTable("OrderItem");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Quantity)
                  .IsRequired();

            entity.Property(e => e.Price)
                  .IsRequired()
                  .HasColumnType("decimal(10,2)");

            entity.HasOne<OrderModel>()
                  .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId);
        });
    }
}
