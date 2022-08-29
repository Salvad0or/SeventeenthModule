using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SeventeenthModule.EntityObjects
{
    public partial class Context : DbContext
    {
        public Context()
        {
            Database.Migrate();
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<EntityClient> Clients { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = (LocalDB)\\MSSQLLocalDB; DataBase = ITVDN2db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_CI_AS");

            modelBuilder.Entity<EntityClient>(entity =>
            {
                entity.HasIndex(e => e.Phone, "UQ__Clients__5C7E359EC95EA147")
                    .IsUnique();

                entity.Property(e => e.Emai).HasMaxLength(50);

                entity.Property(e => e.Fname).HasMaxLength(50);

                entity.Property(e => e.Lname).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(11);

                entity.Property(e => e.Pname).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Clientid)
                    .HasConstraintName("FK__Orders__Clientid__09746778");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Orders__ProductI__0880433F");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Article, "UQ__Products__4943444A3E31A87D")
                    .IsUnique();

                entity.Property(e => e.Article)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Firm).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 1)");

                entity.Property(e => e.ProductCategory).HasMaxLength(50);

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((0))");
            });
        }

         
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
