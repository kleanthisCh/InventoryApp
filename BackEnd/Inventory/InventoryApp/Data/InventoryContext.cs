using System;
using System.Collections.Generic;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Data;

public partial class InventoryContext : DbContext
{
    public InventoryContext()
    {
    }

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barcode> Barcodes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Models.Type> Types { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Barcode>(entity =>
        {
            entity.HasKey(e => e.BarcodeId).HasName("PK__BARCODES__ABAF82C2968D1F6D");

            entity.ToTable("BARCODES");
           // [MODEL] NVARCHAR(100) NOT NULL,
            entity.HasIndex(e => e.ProductId, "IX_BARCODES_PRODUCT");
            entity.HasIndex(e => e.Model, "IX_BARCODES_MODEL");

            entity.Property(e => e.BarcodeId)
                .HasMaxLength(50)
                .HasColumnName("BARCODE");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .HasColumnName("SIZE");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("MODEL");

            entity.HasOne(d => d.Product).WithMany(p => p.Barcodes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTS_TO_BARCODES");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__GENDERS__E0F0815E2A9157C1");

            entity.ToTable("GENDERS");

            entity.Property(e => e.GenderId).HasColumnName("GENDER_ID");
            entity.Property(e => e.GenderDescription)
                .HasMaxLength(100)
                .HasColumnName("GENDER_DESCRIPTION");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__MANUFACT__401906DE640A11A6");

            entity.ToTable("MANUFACTURERS");

            entity.HasIndex(e => e.ManufacturerName, "IX_MANUFACTURERS_MANUFACTURER_NAME");

            entity.Property(e => e.ManufacturerId).HasColumnName("MANUFACTURER_ID");
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(100)
                .HasColumnName("MANUFACTURER_NAME");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__PRODUCTS__52B4176351834F3B");

            entity.ToTable("PRODUCTS");

            entity.HasIndex(e => e.GenderId, "IX_PRODUCTS_GENDER");

            entity.HasIndex(e => e.ManufacturerId, "IX_PRODUCTS_MANUFACTURER");

            entity.HasIndex(e => e.Model, "IX_PRODUCTS_MODEL");

            entity.HasIndex(e => e.TypeId, "IX_PRODUCTS_TYPE");

            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.GenderId).HasColumnName("GENDER_ID");
            entity.Property(e => e.ManufacturerId).HasColumnName("MANUFACTURER_ID");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("MODEL");
            entity.Property(e => e.TypeId).HasColumnName("TYPE_ID");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

            entity.HasOne(d => d.Gender).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GENDERS_TO_PRODUCTS");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MANUFACTURERS_TO_PRODUCTS");

            entity.HasOne(d => d.Type).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TYPES_TO_PRODUCTS");
        });

        modelBuilder.Entity<Models.Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__TYPES__41F99A528CACBAA5");

            entity.ToTable("TYPES");

            entity.Property(e => e.TypeId).HasColumnName("TYPE_ID");
            entity.Property(e => e.TypeDescription)
                .HasMaxLength(20)
                .HasColumnName("TYPE_DESCRIPTION");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
