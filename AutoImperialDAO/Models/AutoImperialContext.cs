using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoImperialDAO.Models;

public partial class AutoImperialContext : DbContext
{
    public AutoImperialContext()
    {
    }
    public AutoImperialContext(DbContextOptions<AutoImperialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CompraProveedor> CompraProveedors { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<Foto> Fotos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<Vendedor> Vendedores { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.idAdministrador).HasName("PK__Administ__EBE80EA130587AB7");

            entity.ToTable("Administrador");

            entity.HasIndex(e => e.nombreUsuario, "UQ_Administrador_nombreUsuario").IsUnique();

            entity.Property(e => e.CURP)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RFC)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.apellidoMaterno)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.apellidoPaterno)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ciudad)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.codigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.estadoCuenta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.nombre)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.nombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("usuario_default");
            entity.Property(e => e.password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("pass_default");
            entity.Property(e => e.puestoAdministrador)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.idCliente).HasName("PK__Cliente__885457EE81741C78");

            entity.ToTable("Cliente");

            entity.Property(e => e.CURP)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RFC)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.apellidoMaterno)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.apellidoPaterno)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ciudad)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.codigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.estado)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.nombre)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CompraProveedor>(entity =>
        {
            entity.HasKey(e => e.idCompraProveedor).HasName("PK__CompraPr__98B4D02F3B7CF3DA");

            entity.ToTable("CompraProveedor");

            entity.Property(e => e.folio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.montoTotal).HasColumnType("decimal(30, 0)");

            entity.HasOne(d => d.idAdministradorNavigation).WithMany(p => p.CompraProveedors)
                .HasForeignKey(d => d.idAdministrador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraPro__idAdm__5BE2A6F2");

            entity.HasOne(d => d.idProveedorNavigation).WithMany(p => p.CompraProveedors)
                .HasForeignKey(d => d.idProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraPro__idPro__5CD6CB2B");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.idDescuento).HasName("PK__Descuent__33F69846CC049BDD");

            entity.ToTable("Descuento");

            entity.HasMany(d => d.idVehiculos).WithMany(p => p.idDescuentos)
                .UsingEntity<Dictionary<string, object>>(
                    "DescuentoVehiculo",
                    r => r.HasOne<Vehiculo>().WithMany()
                        .HasForeignKey("idVehiculo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descuento__idVeh__5AEE82B9"),
                    l => l.HasOne<Descuento>().WithMany()
                        .HasForeignKey("idDescuento")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descuento__idDes__59FA5E80"),
                    j =>
                    {
                        j.HasKey("idDescuento", "idVehiculo").HasName("PK__Descuent__27701AD1B63993EC");
                        j.ToTable("DescuentoVehiculo");
                        j.HasIndex(new[] { "idDescuento" }, "idx_idDescuentoVehiculo");
                        j.HasIndex(new[] { "idVehiculo" }, "idx_idVehiculoDescuentoVehiculo");
                    });
        });

        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(e => e.idFotos).HasName("PK__Fotos__6E121FACE9C88111");

            entity.HasIndex(e => e.idVehiculo, "idx_idVehiculoFotos");

            entity.HasOne(d => d.idVehiculoNavigation).WithMany(p => p.Fotos)
                .HasForeignKey(d => d.idVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fotos__idVehicul__571DF1D5");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.idMarca).HasName("PK__Marca__70331812FACD57DC");

            entity.ToTable("Marca");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.idModelo).HasName("PK__Modelo__13A52CD1EC973467");

            entity.ToTable("Modelo");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.idMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Modelo__idMarca__5812160E");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.idProveedor).HasName("PK__Proveedo__A3FA8E6BB66B6A8D");

            entity.ToTable("Proveedor");

            entity.Property(e => e.calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.codigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.contactoPrincipal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.nombreProveedor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.idReserva).HasName("PK__Reserva__94D104C85C5222F1");

            entity.ToTable("Reserva");

            entity.HasIndex(e => e.idCliente, "idx_idCliente");

            entity.HasIndex(e => e.idVendedor, "idx_idVendedor");

            entity.HasIndex(e => e.idVersion, "idx_idVersion");

            entity.Property(e => e.fechaReserva).HasColumnType("datetime");
            entity.Property(e => e.montoEnganche).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.notasAdicionales)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.idCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idClien__5165187F");

            entity.HasOne(d => d.idVendedorNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.idVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idVende__5070F446");

            entity.HasOne(d => d.idVersionNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.idVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idVersi__52593CB8");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.idVehiculo).HasName("PK__Vehiculo__48682970D2F424B7");

            entity.ToTable("Vehiculo");

            entity.HasIndex(e => e.idCompraProveedor, "idx_idCompraProveedor");

            entity.HasIndex(e => e.idVersion, "idx_idVersionVehiculo");

            entity.Property(e => e.VIN)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.estadoVehiculo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.numeroChasis)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.numeroMotor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.precioProveedor).HasColumnType("decimal(30, 0)");
            entity.Property(e => e.precioVehiculo).HasColumnType("decimal(30, 0)");
            entity.Property(e => e.tipoVehiculo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idCompraProveedorNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.idCompraProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculo__idComp__5535A963");

            entity.HasOne(d => d.idVersionNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.idVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculo__idVers__5629CD9C");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.idVendedor).HasName("PK__Vendedor__A6693F93388C3282");

            entity.ToTable("Vendedor");

            entity.HasIndex(e => e.nombreUsuario, "UQ_Vendedor_nombreUsuario").IsUnique();

            entity.Property(e => e.CURP)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RFC)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.apellidoMaterno)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.apellidoPaterno)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ciudad)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.codigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.estadoCuenta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.nombre)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.nombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("usuario_default");
            entity.Property(e => e.password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("pass_default");
            entity.Property(e => e.puestoVendedor)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.idVenta).HasName("PK__Venta__077D5614C258B6A8");

            entity.Property(e => e.formaPago)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.notasAdicionales)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.precioVehiculo).HasColumnType("decimal(30, 0)");

            entity.HasOne(d => d.idReservaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.idReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__idReserva__534D60F1");

            entity.HasOne(d => d.idVehiculoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.idVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__idVehicul__5441852A");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.idVersion).HasName("PK__Version__BBD5F8B25B9F8045");

            entity.ToTable("Version");

            entity.Property(e => e.motor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.transmision)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idModeloNavigation).WithMany(p => p.Versions)
                .HasForeignKey(d => d.idModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Version__idModel__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
