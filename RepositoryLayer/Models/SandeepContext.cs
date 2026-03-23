using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Models;

public partial class SandeepContext : DbContext
{
    public SandeepContext()
    {
    }

    public SandeepContext(DbContextOptions<SandeepContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SANDEEP\\SQLEXPRESS;Database=Sandeep;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FF39A7401");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Quantity).HasDefaultValue(0);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    


    
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Username)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(x => x.Password)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(x => x.Role)
                  .IsRequired()
                  .HasMaxLength(50);
        });
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
