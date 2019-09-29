using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mtpd.Models
{
    public partial class mtpdContext : DbContext
    {
        public mtpdContext()
        {
        }

        public mtpdContext(DbContextOptions<mtpdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<UnitCode> UnitCode { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=mtpd;Username=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.Id)
                    .HasName("product_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.PricePerUnit)
                    .HasColumnName("price_per_unit")
                    .HasColumnType("money");

                entity.Property(e => e.QuantityInStock)
                    .HasColumnName("quantity_in_stock")
                    .HasColumnType("numeric(8,2)");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("sale");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(8,2)");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.QuantitySold)
                    .HasColumnName("quantity_sold")
                    .HasColumnType("numeric(8,2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("sale_product_id_fk");
            });

            modelBuilder.Entity<UnitCode>(entity =>
            {
                entity.ToTable("unit_code");

                entity.HasIndex(e => e.Id)
                    .HasName("unit_code_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");
            });
        }
    }
}
