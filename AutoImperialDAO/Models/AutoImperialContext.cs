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
            entity.HasKey(e => e.idAdministrador).HasName("PK__Administ__EBE80EA1402C5AD4");

            entity.HasIndex(e => e.numeroEmpleado, "UQ__Administ__49C3142CED0DC607").IsUnique();

            entity.HasIndex(e => e.nombreUsuario, "UQ__Administ__A0436BD75B50FC90").IsUnique();

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
            entity.HasKey(e => e.idCliente).HasName("PK__Cliente__885457EE4A44299C");

            entity.HasIndex(e => e.CURP, "UQ__Cliente__F46C4CBFAE9123DE").IsUnique();

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
            entity.HasKey(e => e.idCompraProveedor).HasName("PK__CompraPr__98B4D02F78717E84");

            entity.Property(e => e.folio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.montoTotal).HasColumnType("decimal(30, 0)");

            entity.HasOne(d => d.idAdministradorNavigation).WithMany(p => p.CompraProveedor)
                .HasForeignKey(d => d.idAdministrador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraPro__idAdm__60A75C0F");

            entity.HasOne(d => d.idProveedorNavigation).WithMany(p => p.CompraProveedor)
                .HasForeignKey(d => d.idProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraPro__idPro__619B8048");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.idDescuento).HasName("PK__Descuent__33F698462F17FC93");

            entity.HasMany(d => d.idVehiculo).WithMany(p => p.idDescuento)
                .UsingEntity<Dictionary<string, object>>(
                    "DescuentoVehiculo",
                    r => r.HasOne<Vehiculo>().WithMany()
                        .HasForeignKey("idVehiculo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descuento__idVeh__5FB337D6"),
                    l => l.HasOne<Descuento>().WithMany()
                        .HasForeignKey("idDescuento")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Descuento__idDes__5EBF139D"),
                    j =>
                    {
                        j.HasKey("idDescuento", "idVehiculo").HasName("PK__Descuent__27701AD10F791F56");
                        j.HasIndex(new[] { "idDescuento" }, "idx_idDescuentoVehiculo");
                        j.HasIndex(new[] { "idVehiculo" }, "idx_idVehiculoDescuentoVehiculo");
                    });
        });

        modelBuilder.Entity<Fotos>(entity =>
        {
            entity.HasKey(e => e.idFotos).HasName("PK__Fotos__6E121FACEB8DCFAF");

            entity.HasIndex(e => e.idVehiculo, "idx_idVehiculoFotos");

            entity.HasOne(d => d.idVehiculoNavigation).WithMany(p => p.Fotos)
                .HasForeignKey(d => d.idVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fotos__idVehicul__5BE2A6F2");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.idMarca).HasName("PK__Marca__703318121D6171D2");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.idModelo).HasName("PK__Modelo__13A52CD198499B19");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idMarcaNavigation).WithMany(p => p.Modelo)
                .HasForeignKey(d => d.idMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Modelo__idMarca__5CD6CB2B");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.idProveedor).HasName("PK__Proveedo__A3FA8E6B007B2A97");

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
            entity.HasKey(e => e.idReserva).HasName("PK__Reserva__94D104C8E6E81C89");

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
                .HasConstraintName("FK__Reserva__idClien__5629CD9C");

            entity.HasOne(d => d.idVendedorNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.idVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idVende__5535A963");

            entity.HasOne(d => d.idVersionNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.idVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idVersi__571DF1D5");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.idVehiculo).HasName("PK__Vehiculo__48682970E4D2D424");

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
                .HasConstraintName("FK__Vehiculo__idComp__59FA5E80");

            entity.HasOne(d => d.idVersionNavigation).WithMany(p => p.Vehiculo)
                .HasForeignKey(d => d.idVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculo__idVers__5AEE82B9");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.idVendedor).HasName("PK__Vendedor__A6693F935694E687");

            entity.HasIndex(e => e.numeroEmpleado, "UQ__Vendedor__49C3142C1FF4770C").IsUnique();

            entity.HasIndex(e => e.nombreUsuario, "UQ__Vendedor__A0436BD723D596C5").IsUnique();

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
            entity.HasKey(e => e.idVenta).HasName("PK__Venta__077D5614A44EF779");

            entity.Property(e => e.estadoVenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activa");
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
                .HasConstraintName("FK__Venta__idReserva__5812160E");

            entity.HasOne(d => d.idVehiculoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.idVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__idVehicul__59063A47");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.idVersion).HasName("PK__Version__BBD5F8B26C508228");

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
                .HasConstraintName("FK__Version__idModel__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
