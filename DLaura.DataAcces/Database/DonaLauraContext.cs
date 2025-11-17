using System;
using System.Collections.Generic;
using DLaura.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLaura.DataAcces;

public partial class DonaLauraContext : DbContext
{
    public DonaLauraContext()
    {
    }

    public DonaLauraContext(DbContextOptions<DonaLauraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=ACOSTA; Database=DonaLaura; User Id=sa; Password=1245; Encrypt=False; TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC070E730DA1");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC071EF3A5E0");

            entity.Property(e => e.CoordinateX).HasMaxLength(100);
            entity.Property(e => e.CoordinateY).HasMaxLength(100);

            entity.HasOne(d => d.TableNumberNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.TableNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Locations__Table__3F466844");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC074F2D7863");

            entity.Property(e => e.Comment).HasMaxLength(250);
            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Category).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__Categ__48CFD27E");

            entity.HasOne(d => d.Rol).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__RolId__47DBAE45");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC07CD985793");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ReservateShift)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.TableNumberNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TableNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Table__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__UserI__4222D4EF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0722589862");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07603D0BA1");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343C896D4F").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RolId__3A81B327");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.TableNumber).HasName("PK__UserTabl__E8E0DB5310D90559");

            entity.Property(e => e.TableNumber).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
