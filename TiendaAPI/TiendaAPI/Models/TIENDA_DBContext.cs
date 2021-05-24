using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TiendaAPI.Models
{
    public partial class TIENDA_DBContext : DbContext
    {
        public TIENDA_DBContext()
        {
        }

        public TIENDA_DBContext(DbContextOptions<TIENDA_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TestCliente> TestClientes { get; set; }
        public virtual DbSet<TestFactura> TestFacturas { get; set; }
        public virtual DbSet<TestFacturaDetalle> TestFacturaDetalles { get; set; }
        public virtual DbSet<TestProducto> TestProductos { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-2EKIUBS\\SQLEXPRESS;Database=TIENDA_DB;user id=sa;password=123");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<TestCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__TEST_CLI__D59466422069F7B6");

                entity.ToTable("TEST_CLIENTE");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Identifiacion).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(50);
            });

            modelBuilder.Entity<TestFactura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK__TEST_FAC__50E7BAF18FC8BB15");

                entity.ToTable("TEST_FACTURA");

                entity.Property(e => e.FechaVenta).HasColumnType("datetime");

                entity.Property(e => e.ValorTotal).HasColumnType("numeric(18, 3)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TestFacturas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TEST_FACT__Valor__286302EC");
            });

            modelBuilder.Entity<TestFacturaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdFacturaDetalle)
                    .HasName("PK__TEST_FAC__3D8E1AB8223E99A6");

                entity.ToTable("TEST_FACTURA_DETALLE");

                entity.Property(e => e.IdFacturaDetalle)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ValorTotal).HasColumnType("numeric(18, 3)");

                entity.Property(e => e.ValorUnidad).HasColumnType("numeric(18, 3)");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.TestFacturaDetalles)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TEST_FACT__IdFac__2B3F6F97");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TestFacturaDetalles)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TEST_FACT__IdPro__2C3393D0");
            });

            modelBuilder.Entity<TestProducto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__TEST_PRO__09889210679F10C1");

                entity.ToTable("TEST_PRODUCTO");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ValorUnidad).HasColumnType("numeric(18, 3)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
