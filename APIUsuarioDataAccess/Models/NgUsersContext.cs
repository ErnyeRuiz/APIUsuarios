using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIUsuariosDataAccess.Models;

public partial class NgUsersContext : DbContext
{
    public NgUsersContext()
    {
    }

    public NgUsersContext(DbContextOptions<NgUsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__users__3717C98259912D3A");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616484043679").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
