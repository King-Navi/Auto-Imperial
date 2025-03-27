                                                                                  -- INSERTAR 2 ADMINISTRADORES
INSERT INTO [Administrador]
    ([nombre], [apellidoPaterno], [apellidoMaterno], [telefono], [correo], [calle], [numero], [codigoPostal], [ciudad],
     [estadoCuenta], [CURP], [RFC], [puestoAdministrador], [nombreUsuario], [password], [numeroEmpleado], [sucursal])
VALUES
('Juan', 'Perez', 'Lopez', '5551234', 'juan.perez@example.com', 'Av. Principal', 101, '12345', 'CDMX',
 'Activo', 'CURPJP1111', 'RFCJP1111', 'Gerente General', 'AdminJuan', 'admin123', 'AD001', 'Matriz'),
('Maria', 'Garcia', 'Hernandez', '5555678', 'maria.garcia@example.com', 'Calle Secundaria', 202, '67890', 'Monterrey',
 'Activo', 'CURPMG1111', 'RFCMG1111', 'Subgerente', 'AdminMaria', 'admin456', 'AD002', 'SucursalNorte');

-- INSERTAR 2 VENDEDORES
INSERT INTO [Vendedor]
    ([nombre], [apellidoPaterno], [apellidoMaterno], [telefono], [correo], [calle], [numero], [codigoPostal], [ciudad],
     [estadoCuenta], [CURP], [RFC], [puestoVendedor], [nombreUsuario], [password], [numeroEmpleado], [sucursal])
VALUES
('Carlos', 'Ramirez', 'Sanchez', '5559876', 'carlos.ramirez@example.com', 'Av. Ventas', 11, '34567', 'CDMX',
 'Activo', 'CURPCR1111', 'RFCCR1111', 'Vendedor Senior', 'VendedorCarlos', 'vendedor123', 'VD001', 'SucursalCentro'),
('Ana', 'Lopez', 'Martinez', '5556543', 'ana.lopez@example.com', 'Calle Ventas', 22, '98765', 'Puebla',
 'Activo', 'CURPAL1111', 'RFCLL1111', 'Vendedor Junior', 'VendedorAna', 'vendedor456', 'VD002', 'SucursalSur');
 -- 1. Insertar en Administrador
INSERT INTO Administrador 
  (nombre, apellidoPaterno, apellidoMaterno, telefono, correo, calle, numero, codigoPostal, ciudad, estadoCuenta, CURP, RFC, puestoAdministrador, nombreUsuario, password, numeroEmpleado, sucursal)
VALUES 
  ('Juan', 'Pérez', 'Gómez', '1234567890', 'juan.perez@example.com', 'Calle Falsa', 123, '12345', 'CiudadX', 'Activo', 'CURP123456', 'RFC1234567890', 'Gerente',  'a1', 'a1', 'EMP001', 'Sucursal1');
-- 2. Insertar en Vendedor
INSERT INTO Vendedor 
  (nombre, apellidoPaterno, apellidoMaterno, telefono, correo, calle, numero, codigoPostal, ciudad, estadoCuenta, CURP, RFC, puestoVendedor, nombreUsuario, password, numeroEmpleado, sucursal)
VALUES 
  ('Carlos', 'Martínez', 'Lopez', '0987654321', 'carlos.martinez@example.com', 'Av Siempre Viva', 456, '54321', 'CiudadX', 'Activo', 'CURP654321', 'RFC6543217890', 'Vendedor', 'v1', 'v1', 'EMP002', 'Sucursal1');

-- 3. Insertar en Cliente
INSERT INTO Cliente 
  (nombre, apellidoPaterno, apellidoMaterno, telefono, correo, calle, numero, codigoPostal, ciudad, RFC, CURP, estado)
VALUES 
  ('Ana', 'Ramirez', 'Sanchez', '1122334455', 'ana.ramirez@example.com', 'Calle Luna', 789, '67890', 'CiudadX', 'RFC0000000000', 'CURPANA123456', 'Activo');

-- 4. Insertar en Marca
INSERT INTO Marca (nombre)
VALUES ('Toyota');

-- 5. Insertar en Descuento
INSERT INTO Descuento 
  (fechaInicio, fechaFin, descuentoPorcentaje)
VALUES 
  ('2025-01-01', '2025-12-31', 10);

-- 6. Insertar en Proveedor
INSERT INTO Proveedor 
  (nombreProveedor, calle, numero, codigoPostal, correo, telefono, contactoPrincipal, estado)
VALUES 
  ('Proveedor Ejemplo', 'Av Principal', 100, '11111', 'proveedor@example.com', '5566778899', 'Contacto Principal', 'Activo');

-- 7. Insertar en CompraProveedor (requiere idAdministrador = 1 y idProveedor = 1)
INSERT INTO CompraProveedor 
  (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES 
  (50000, 'FOLIO001', '2025-03-13', 1, 1);

-- 8. Insertar en Modelo (requiere idMarca = 1)
INSERT INTO Modelo 
  (nombre, idMarca)
VALUES 
  ('Corolla', 1);

-- 9. Insertar en Version (requiere idModelo = 1)
INSERT INTO Version 
  (nombre, transmision, puertas, motor, idModelo)
VALUES 
  ('Corolla 2025', 'Automática', 4, '1.8L', 1);

-- 10. Insertar en Reserva (requiere idVendedor = 1, idCliente = 1, idVersion = 1)
INSERT INTO Reserva 
  (fechaReserva, montoEnganche, notasAdicionales, idVendedor, idCliente, idVersion)
VALUES 
  ('2025-03-13', 1000, 'Reserva inicial', 1, 1, 1);

-- 11. Insertar en Vehiculo (requiere idCompraProveedor = 1 y idVersion = 1)
INSERT INTO Vehiculo 
  (tipoVehiculo, estadoVehiculo, precioProveedor, precioVehiculo, anio, color, VIN, numeroChasis, numeroMotor, idCompraProveedor, idVersion)
VALUES 
  ('Sedan', 'Disponible', 30000, 35000, 2025, 'Rojo', 'VIN1234567890', 'CHASIS123456', 'MOTOR123456', 1, 1);

-- 12. Insertar en Venta (requiere idReserva = 1 y idVehiculo = 1)
INSERT INTO Venta 
  (fechaVenta, precioVehiculo, formaPago, notasAdicionales, idReserva, idVehiculo)
VALUES 
  ('2025-03-14', 35000, 'Contado', 'Venta exitosa', 1, 1);

-- 13. Insertar en Fotos (requiere idVehiculo = 1)
INSERT INTO Fotos 
  (foto, idVehiculo)
VALUES 
  (0xFFD8FFE0, 1);

-- 14. Insertar en DescuentoVehiculo (requiere idDescuento = 1 y idVehiculo = 1)
INSERT INTO DescuentoVehiculo 
  (idDescuento, idVehiculo)
VALUES 
  (1, 1);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (15000.50, 'CP-20250301', '2025-03-01', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (8450.75, 'CP-20250315', '2025-03-15', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (23000.00, 'CP-20250320', '2025-03-20', 1, 2);



  -- Inserciones para pruebas ClientRepositoryTest