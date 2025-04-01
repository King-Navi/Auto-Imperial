using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Administrador> Administrador { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<CompraProveedor> CompraProveedor { get; set; }

    public virtual DbSet<Descuento> Descuento { get; set; }

    public virtual DbSet<Fotos> Fotos { get; set; }

    public virtual DbSet<Marca> Marca { get; set; }

    public virtual DbSet<Modelo> Modelo { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Reserva> Reserva { get; set; }

    public virtual DbSet<Vehiculo> Vehiculo { get; set; }

    public virtual DbSet<Vendedor> Vendedor { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    public virtual DbSet<Version> Version { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.idAdministrador).HasName("PK__Administ__EBE80EA1AA437AAA");

            entity.HasIndex(e => e.numeroEmpleado, "UQ__Administ__49C3142CA0083521").IsUnique();

            entity.HasIndex(e => e.nombreUsuario, "UQ__Administ__A0436BD7A59DB777").IsUnique();

            entity.Property(e => e.CURP)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RFC)
                .HasMaxLength(15)
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
                .HasMaxLength(50)
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
                .IsUnicode(false);
            entity.Property(e => e.numeroEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.puestoAdministrador)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.sucursal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.idCliente).HasName("PK__Cliente__885457EE6F718F5A");

            entity.HasIndex(e => e.CURP, "UQ__Cliente__F46C4CBF32CC3BA2").IsUnique();

            entity.Property(e => e.CURP)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RFC)
                .HasMaxLength(15)
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
                .HasMaxLength(50)
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
            entity.HasKey(e => e.idCompraProveedor).HasName("PK__CompraPr__98B4D02FA6AAAC0F");

            entity.Property(e => e.folio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.montoTotal).HasColumnType("decimal(30, 0)");

            entity.HasOne(d => d.idAdministradorNavigation).WithMany(p => p.CompraProveedor)
                .HasForeignKey(d => d.idAdministrador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraPro__idAdm__5224328E");

            entity.HasOne(d => d.idProveedorNavigation).WithMany(p => p.CompraProveedor)
                .HasForeignKey(d => d.idProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraPro__idPro__531856C7");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.idDescuento).HasName("PK__Descuent__33F6984681F5B1D2");

            entity.HasMany(d => d.idVehiculo).WithMany(p => p.idDescuento)
                .UsingEntity<Dictionary<string, object>>(
                    "DescuentoVehiculo",
                    r => r.HasOne<Vehiculo>().WithMany()
                        .HasForeignKey("idVehiculo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descuento__idVeh__51300E55"),
                    l => l.HasOne<Descuento>().WithMany()
                        .HasForeignKey("idDescuento")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descuento__idDes__503BEA1C"),
                    j =>
                    {
                        j.HasKey("idDescuento", "idVehiculo").HasName("PK__Descuent__27701AD114B79F40");
                        j.HasIndex(new[] { "idDescuento" }, "idx_idDescuentoVehiculo");
                        j.HasIndex(new[] { "idVehiculo" }, "idx_idVehiculoDescuentoVehiculo");
                    });
        });

        modelBuilder.Entity<Fotos>(entity =>
        {
            entity.HasKey(e => e.idFotos).HasName("PK__Fotos__6E121FAC88311BA0");

            entity.HasIndex(e => e.idVehiculo, "idx_idVehiculoFotos");

            entity.HasOne(d => d.idVehiculoNavigation).WithMany(p => p.Fotos)
                .HasForeignKey(d => d.idVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fotos__idVehicul__4D5F7D71");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.idMarca).HasName("PK__Marca__7033181281574284");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.idModelo).HasName("PK__Modelo__13A52CD1E59BFCED");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idMarcaNavigation).WithMany(p => p.Modelo)
                .HasForeignKey(d => d.idMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Modelo__idMarca__4E53A1AA");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.idProveedor).HasName("PK__Proveedo__A3FA8E6BCD79E162");

            entity.Property(e => e.calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ciudad)
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
            entity.HasKey(e => e.idReserva).HasName("PK__Reserva__94D104C84C564BEF");

            entity.HasIndex(e => e.idCliente, "idx_idCliente");

            entity.HasIndex(e => e.idVendedor, "idx_idVendedor");

            entity.HasIndex(e => e.idVersion, "idx_idVersion");

            entity.Property(e => e.estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Interesado");
            entity.Property(e => e.fechaReserva).HasColumnType("datetime");
            entity.Property(e => e.montoEnganche).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.notasAdicionales)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.idClienteNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.idCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idClien__47A6A41B");

            entity.HasOne(d => d.idVendedorNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.idVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idVende__46B27FE2");

            entity.HasOne(d => d.idVersionNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.idVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idVersi__489AC854");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.idVehiculo).HasName("PK__Vehiculo__48682970000F4F08");

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

            entity.HasOne(d => d.idCompraProveedorNavigation).WithMany(p => p.Vehiculo)
                .HasForeignKey(d => d.idCompraProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculo__idComp__4B7734FF");

            entity.HasOne(d => d.idVersionNavigation).WithMany(p => p.Vehiculo)
                .HasForeignKey(d => d.idVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculo__idVers__4C6B5938");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.idVendedor).HasName("PK__Vendedor__A6693F9335E76A3B");

            entity.HasIndex(e => e.numeroEmpleado, "UQ__Vendedor__49C3142C2F28F930").IsUnique();

            entity.HasIndex(e => e.nombreUsuario, "UQ__Vendedor__A0436BD714DDD595").IsUnique();

            entity.Property(e => e.CURP)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RFC)
                .HasMaxLength(15)
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
                .HasMaxLength(50)
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
                .IsUnicode(false);
            entity.Property(e => e.numeroEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.puestoVendedor)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.sucursal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.idVenta).HasName("PK__Venta__077D56147663407C");

            entity.Property(e => e.formaPago)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.notasAdicionales)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.precioVehiculo).HasColumnType("decimal(30, 0)");

            entity.HasOne(d => d.idReservaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.idReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__idReserva__498EEC8D");

            entity.HasOne(d => d.idVehiculoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.idVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__idVehicul__4A8310C6");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.idVersion).HasName("PK__Version__BBD5F8B29319AEDF");

            entity.Property(e => e.motor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.transmision)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idModeloNavigation).WithMany(p => p.Version)
                .HasForeignKey(d => d.idModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Version__idModel__4F47C5E3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}