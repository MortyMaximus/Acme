using Microsoft.EntityFrameworkCore;

namespace Acme.Repository.Models;

public partial class AcmeContext : DbContext
{
    public AcmeContext(DbContextOptions<AcmeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<SerialNumbers> SerialNumbers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "IX_Customer_Email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SerialNumbers>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_SerialNumbers_SerialNumber").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Customer1).HasColumnName("Customer_1");
            entity.Property(e => e.Customer2).HasColumnName("Customer_2");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SerialNumber");

            entity.HasOne(d => d.Customer1Navigation).WithMany(p => p.SerialNumberCustomer1Navigations)
                .HasForeignKey(d => d.Customer1)
                .HasConstraintName("FK_SerialNumbers_Customer_1");

            entity.HasOne(d => d.Customer2Navigation).WithMany(p => p.SerialNumberCustomer2Navigations)
                .HasForeignKey(d => d.Customer2)
                .HasConstraintName("FK_SerialNumbers_Customer_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "IX_User_Email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
