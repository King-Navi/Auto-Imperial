--USER [AutoImperial]
--GO
BEGIN TRANSACTION;
BEGIN TRY

CREATE TABLE [Administrador] (
  [idAdministrador] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] varchar(40) NOT NULL,
  [apellidoPaterno] varchar(80) NOT NULL,
  [apellidoMaterno] varchar(80) NOT NULL,
  [telefono] varchar(15),
  [correo] varchar(50),
  [calle] varchar(50),
  [numero] int,
  [codigoPostal] varchar(10),
  [ciudad] varchar(50),
  [estadoCuenta] varchar(50) NOT NULL,
  [CURP] varchar(50) NOT NULL,
  [RFC] varchar(15) NOT NULL,
  [puestoAdministrador] varchar(70) NOT NULL,
  [nombreUsuario] VARCHAR(50) NOT NULL UNIQUE,
  [password] VARCHAR(50) NOT NULL,
  [numeroEmpleado] VARCHAR(50) NOT NULL UNIQUE,
  [sucursal] VARCHAR(50)
)

CREATE TABLE [Vendedor] (
  [idVendedor] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] varchar(40) NOT NULL,
  [apellidoPaterno] varchar(80) NOT NULL,
  [apellidoMaterno] varchar(80) NOT NULL,
  [telefono] varchar(15),
  [correo] varchar(50),
  [calle] varchar(50),
  [numero] int,
  [codigoPostal] varchar(10),
  [ciudad] varchar(50),
  [estadoCuenta] varchar(50) NOT NULL,
  [CURP] varchar(50) NOT NULL,
  [RFC] varchar(15) NOT NULL,
  [puestoVendedor] varchar(70) NOT NULL,
  [nombreUsuario] VARCHAR(50) NOT NULL UNIQUE,
  [password] VARCHAR(50) NOT NULL,
  [numeroEmpleado] VARCHAR(50) NOT NULL UNIQUE,
  [sucursal] VARCHAR(50) NOT NULL
)


CREATE TABLE [Reserva] (
  [idReserva] int PRIMARY KEY IDENTITY(1, 1),
  [fechaReserva] datetime NOT NULL,
  [montoEnganche] decimal(38,0),
  [notasAdicionales] varchar(100),
  [idVendedor] int NOT NULL,
  [idCliente] int NOT NULL,
  [idVersion] int NOT NULL
)


CREATE TABLE [Cliente] (
  [idCliente] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] varchar(40) NOT NULL,
  [apellidoPaterno] varchar(80) NOT NULL,
  [apellidoMaterno] varchar(80) NOT NULL,
  [telefono] varchar(15),
  [correo] varchar(50),
  [calle] varchar(50),
  [numero] int,
  [codigoPostal] varchar(10),
  [ciudad] varchar(50),
  [RFC] char(15),
  [CURP] varchar(50) NOT NULL UNIQUE,
  [estado] varchar(40) NOT NULL
)


CREATE TABLE [Venta] (
  [idVenta] int PRIMARY KEY IDENTITY(1, 1),
  [fechaVenta] date NOT NULL,
  [precioVehiculo] decimal(30,0),
  [formaPago] varchar(40),
  [notasAdicionales] varchar(100),
  [idReserva] int NOT NULL,
  [idVehiculo] int NOT NULL,
  [estadoVenta] varchar(20) NOT NULL CONSTRAINT DF_Venta_estadoVenta DEFAULT 'Activa'
);


CREATE TABLE [Vehiculo] (
  [idVehiculo] int PRIMARY KEY IDENTITY(1, 1),
  [tipoVehiculo] varchar(50),
  [estadoVehiculo] varchar(50),
  [precioProveedor] decimal(30,0),
  [precioVehiculo] decimal(30,0),
  [anio] int,
  [color] varchar(50),
  [VIN] varchar(50) NOT NULL,
  [numeroChasis] varchar(50) NOT NULL,
  [numeroMotor] varchar(50) NOT NULL,
  [idCompraProveedor] int NOT NULL,
  [idVersion] int NOT NULL
)


CREATE TABLE [Fotos] (
  [idFotos] int PRIMARY KEY IDENTITY(1, 1),
  [foto] VARBINARY(MAX) NOT NULL,
  [idVehiculo] int NOT NULL
)


CREATE TABLE [Marca] (
  [idMarca] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] varchar(50) NOT NULL
)


CREATE TABLE [Modelo] (
  [idModelo] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] varchar(50) NOT NULL,
  [idMarca] int NOT NULL
)


CREATE TABLE [Version] (
  [idVersion] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] varchar(50) NOT NULL,
  [transmision] varchar(50),
  [puertas] int,
  [motor] varchar(50),
  [idModelo] int NOT NULL
)


CREATE TABLE [DescuentoVehiculo] (
  [idDescuento] int NOT NULL,
  [idVehiculo] int NOT NULL,
  PRIMARY KEY ([idDescuento], [idVehiculo])
)


CREATE TABLE [Descuento] (
  [idDescuento] int PRIMARY KEY IDENTITY(1, 1),
  [fechaInicio] date NOT NULL,
  [fechaFin] date NOT NULL,
  [descuentoPorcentaje] int NOT NULL
)


CREATE TABLE [Proveedor] (
  [idProveedor] int PRIMARY KEY IDENTITY(1, 1),
  [nombreProveedor] varchar(100) NOT NULL,
  [calle] varchar(50),
  [numero] int,
  [codigoPostal] varchar(10),
  [correo] varchar(50),
  [telefono] varchar(15),
  [contactoPrincipal] varchar(50),
  [estado] varchar(50)
)


CREATE TABLE [CompraProveedor] (
  [idCompraProveedor] int PRIMARY KEY IDENTITY(1, 1),
  [montoTotal] decimal(30,0),
  [folio] varchar(50) NOT NULL,
  [fechaCompra] date NOT NULL,
  [idAdministrador] int NOT NULL,
  [idProveedor] int NOT NULL
)


ALTER TABLE [Reserva] ADD FOREIGN KEY ([idVendedor]) REFERENCES [Vendedor] ([idVendedor])
ALTER TABLE [Reserva] ADD FOREIGN KEY ([idCliente]) REFERENCES [Cliente] ([idCliente])
ALTER TABLE [Reserva] ADD FOREIGN KEY ([idVersion]) REFERENCES [Version] ([idVersion])
ALTER TABLE [Venta] ADD FOREIGN KEY ([idReserva]) REFERENCES [Reserva] ([idReserva])
ALTER TABLE [Venta] ADD FOREIGN KEY ([idVehiculo]) REFERENCES [Vehiculo] ([idVehiculo])
ALTER TABLE [Vehiculo] ADD FOREIGN KEY ([idCompraProveedor]) REFERENCES [CompraProveedor] ([idCompraProveedor])
ALTER TABLE [Vehiculo] ADD FOREIGN KEY ([idVersion]) REFERENCES [Version] ([idVersion])
ALTER TABLE [Fotos] ADD FOREIGN KEY ([idVehiculo]) REFERENCES [Vehiculo] ([idVehiculo])
ALTER TABLE [Modelo] ADD FOREIGN KEY ([idMarca]) REFERENCES [Marca] ([idMarca])
ALTER TABLE [Version] ADD FOREIGN KEY ([idModelo]) REFERENCES [Modelo] ([idModelo])
ALTER TABLE [DescuentoVehiculo] ADD FOREIGN KEY ([idDescuento]) REFERENCES [Descuento] ([idDescuento])
ALTER TABLE [DescuentoVehiculo] ADD FOREIGN KEY ([idVehiculo]) REFERENCES [Vehiculo] ([idVehiculo])
ALTER TABLE [CompraProveedor] ADD FOREIGN KEY ([idAdministrador]) REFERENCES [Administrador] ([idAdministrador])
ALTER TABLE [CompraProveedor] ADD FOREIGN KEY ([idProveedor]) REFERENCES [Proveedor] ([idProveedor])

ALTER TABLE [Proveedor] ADD [ciudad] VARCHAR(50);
ALTER TABLE CompraProveedor ADD vehiculosComprados INT NOT NULL DEFAULT(0);

CREATE INDEX idx_idVendedor ON Reserva(idVendedor);
CREATE INDEX idx_idCliente ON Reserva(idCliente);
CREATE INDEX idx_idVersion ON Reserva(idVersion);
CREATE INDEX idx_idCompraProveedor ON Vehiculo(idCompraProveedor);
CREATE INDEX idx_idVersionVehiculo ON Vehiculo(idVersion);
CREATE INDEX idx_idVehiculoFotos ON Fotos(idVehiculo);
CREATE INDEX idx_idDescuentoVehiculo ON DescuentoVehiculo(idDescuento);
CREATE INDEX idx_idVehiculoDescuentoVehiculo ON DescuentoVehiculo(idVehiculo);

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH;