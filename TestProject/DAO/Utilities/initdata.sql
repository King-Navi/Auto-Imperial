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
INSERT INTO Proveedor 
  (nombreProveedor, calle, numero, codigoPostal, correo, telefono, contactoPrincipal, estado)
VALUES 
  ('Aston Elite Motors', 'Boulevard Prestige', 777, '11223', 'contact@astonelite.com', '5599001122', 'Richard Langston', 'Activo');


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

--15. Insertar marcas:
INSERT INTO Marcas (nombre) VALUES
('Toyota'),
('Honda'),
('Ford'),
('Chevrolet'),
('BMW'),
('Mercedes-Benz'),
('Audi'),
('Volkswagen'),
('Nissan'),
('Hyundai'),
('Kia'),
('Mazda'),
('Subaru'),
('Tesla'),
('Lexus'),
('Jeep'),
('Volvo'),
('Peugeot'),
('Renault'),
('Fiat'),
('Mitsubishi'),
('Acura'),
('Alfa Romeo'),
('Citroën'),
('Mini'),
('Chrysler'),
('Infiniti'),
('Genesis'),
('Land Rover'),
('Jaguar');

INSERT INTO Modelo (nombre, idMarca) VALUES
-- Toyota (1)
('Corolla', 1), ('Camry', 1), ('Yaris', 1), ('RAV4', 1), ('Hilux', 1),

-- Honda (3)
('Civic', 3), ('Accord', 3), ('CR-V', 3), ('Fit', 3), ('HR-V', 3),

-- Ford (4)
('Focus', 4), ('Mustang', 4), ('F-150', 4), ('Escape', 4), ('Explorer', 4),

-- Chevrolet (5)
('Aveo', 5), ('Cruze', 5), ('Spark', 5), ('Trax', 5), ('Malibu', 5),

-- BMW (6)
('Serie 1', 6), ('Serie 3', 6), ('Serie 5', 6), ('X3', 6), ('X5', 6),

-- Mercedes-Benz (7)
('Clase A', 7), ('Clase C', 7), ('GLA', 7), ('GLC', 7), ('Clase E', 7),

-- Audi (8)
('A3', 8), ('A4', 8), ('Q2', 8), ('Q3', 8), ('Q5', 8),

-- Volkswagen (9)
('Jetta', 9), ('Golf', 9), ('Tiguan', 9), ('Polo', 9), ('Passat', 9),

-- Nissan (10)
('Sentra', 10), ('Versa', 10), ('Altima', 10), ('March', 10), ('X-Trail', 10),

-- Hyundai (11)
('Elantra', 11), ('Tucson', 11), ('Accent', 11), ('Santa Fe', 11), ('Kona', 11),

-- Kia (12)
('Rio', 12), ('Forte', 12), ('Sportage', 12), ('Sorento', 12), ('Seltos', 12),

-- Mazda (13)
('Mazda2', 13), ('Mazda3', 13), ('CX-3', 13), ('CX-5', 13), ('Mazda6', 13),

-- Subaru (14)
('Impreza', 14), ('Forester', 14), ('Outback', 14), ('Legacy', 14), ('XV', 14),

-- Tesla (15)
('Model S', 15), ('Model 3', 15), ('Model X', 15), ('Model Y', 15), ('Cybertruck', 15),

-- Lexus (16)
('IS', 16), ('ES', 16), ('RX', 16), ('NX', 16), ('UX', 16),

-- Jeep (17)
('Wrangler', 17), ('Cherokee', 17), ('Compass', 17), ('Renegade', 17), ('Gladiator', 17),

-- Volvo (18)
('XC40', 18), ('XC60', 18), ('XC90', 18), ('S60', 18), ('V60', 18),

-- Peugeot (19)
('208', 19), ('2008', 19), ('3008', 19), ('5008', 19), ('308', 19),

-- Renault (20)
('Clio', 20), ('Koleos', 20), ('Captur', 20), ('Duster', 20), ('Logan', 20),

-- Fiat (21)
('500', 21), ('Punto', 21), ('Tipo', 21), ('Uno', 21), ('Argo', 21),

-- Mitsubishi (22)
('Lancer', 22), ('Outlander', 22), ('ASX', 22), ('Mirage', 22), ('Eclipse Cross', 22),

-- Acura (23)
('ILX', 23), ('TLX', 23), ('RDX', 23), ('MDX', 23), ('Integra', 23),

-- Alfa Romeo (24)
('Giulia', 24), ('Stelvio', 24), ('Mito', 24), ('Tonale', 24), ('4C', 24),

-- Citroën (25)
('C3', 25), ('C4', 25), ('C5', 25), ('Berlingo', 25), ('Spacetourer', 25),

-- Mini (26)
('Cooper', 26), ('Countryman', 26), ('Clubman', 26), ('Paceman', 26), ('Cabrio', 26),

-- Chrysler (27)
('300', 27), ('Pacifica', 27), ('Voyager', 27), ('Aspen', 27), ('Sebring', 27),

-- Infiniti (28)
('Q50', 28), ('Q60', 28), ('QX50', 28), ('QX60', 28), ('QX80', 28),

-- Genesis (29)
('G70', 29), ('G80', 29), ('G90', 29), ('GV70', 29), ('GV80', 29),

-- Land Rover (30)
('Defender', 30), ('Discovery', 30), ('Range Rover', 30), ('Velar', 30), ('Evoque', 30),

-- Jaguar (31)
('XE', 31), ('XF', 31), ('F-Pace', 31), ('E-Pace', 31), ('I-Pace', 31);

-- 16. Insertar en Version
INSERT INTO Version (nombre, transmision, puertas, motor, idModelo) VALUES
('Base', 'Manual', 4, '1.4L I4', 1),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 1),
('Premium', 'CVT', 4, '2.0L I4', 1),
('Base', 'Manual', 4, '1.4L I4', 2),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 2),
('Premium', 'CVT', 4, '2.0L I4', 2),
('Base', 'Manual', 4, '1.4L I4', 3),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 3),
('Premium', 'CVT', 4, '2.0L I4', 3),
('Base', 'Manual', 4, '1.4L I4', 4),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 4),
('Premium', 'CVT', 4, '2.0L I4', 4),
('Base', 'Manual', 4, '1.4L I4', 5),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 5),
('Premium', 'CVT', 4, '2.0L I4', 5),
('Base', 'Manual', 4, '1.4L I4', 6),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 6),
('Premium', 'CVT', 4, '2.0L I4', 6),
('Base', 'Manual', 4, '1.4L I4', 7),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 7),
('Premium', 'CVT', 4, '2.0L I4', 7),
('Base', 'Manual', 4, '1.4L I4', 8),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 8),
('Premium', 'CVT', 4, '2.0L I4', 8),
('Base', 'Manual', 4, '1.4L I4', 9),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 9),
('Premium', 'CVT', 4, '2.0L I4', 9),
('Base', 'Manual', 4, '1.4L I4', 10),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 10),
('Premium', 'CVT', 4, '2.0L I4', 10),
('Base', 'Manual', 4, '1.4L I4', 11),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 11),
('Premium', 'CVT', 4, '2.0L I4', 11),
('Base', 'Manual', 4, '1.4L I4', 12),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 12),
('Premium', 'CVT', 4, '2.0L I4', 12),
('Base', 'Manual', 4, '1.4L I4', 13),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 13),
('Premium', 'CVT', 4, '2.0L I4', 13),
('Base', 'Manual', 4, '1.4L I4', 14),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 14),
('Premium', 'CVT', 4, '2.0L I4', 14),
('Base', 'Manual', 4, '1.4L I4', 15),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 15),
('Premium', 'CVT', 4, '2.0L I4', 15),
('Base', 'Manual', 4, '1.4L I4', 16),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 16),
('Premium', 'CVT', 4, '2.0L I4', 16),
('Base', 'Manual', 4, '1.4L I4', 17),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 17),
('Premium', 'CVT', 4, '2.0L I4', 17),
('Base', 'Manual', 4, '1.4L I4', 18),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 18),
('Premium', 'CVT', 4, '2.0L I4', 18),
('Base', 'Manual', 4, '1.4L I4', 19),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 19),
('Premium', 'CVT', 4, '2.0L I4', 19),
('Base', 'Manual', 4, '1.4L I4', 20),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 20),
('Premium', 'CVT', 4, '2.0L I4', 20),
('Base', 'Manual', 4, '1.4L I4', 21),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 21),
('Premium', 'CVT', 4, '2.0L I4', 21),
('Base', 'Manual', 4, '1.4L I4', 22),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 22),
('Premium', 'CVT', 4, '2.0L I4', 22),
('Base', 'Manual', 4, '1.4L I4', 23),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 23),
('Premium', 'CVT', 4, '2.0L I4', 23),
('Base', 'Manual', 4, '1.4L I4', 24),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 24),
('Premium', 'CVT', 4, '2.0L I4', 24),
('Base', 'Manual', 4, '1.4L I4', 25),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 25),
('Premium', 'CVT', 4, '2.0L I4', 25),
('Base', 'Manual', 4, '1.4L I4', 26),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 26),
('Premium', 'CVT', 4, '2.0L I4', 26),
('Base', 'Manual', 4, '1.4L I4', 27),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 27),
('Premium', 'CVT', 4, '2.0L I4', 27),
('Base', 'Manual', 4, '1.4L I4', 28),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 28),
('Premium', 'CVT', 4, '2.0L I4', 28),
('Base', 'Manual', 4, '1.4L I4', 29),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 29),
('Premium', 'CVT', 4, '2.0L I4', 29),
('Base', 'Manual', 4, '1.4L I4', 30),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 30),
('Premium', 'CVT', 4, '2.0L I4', 30),
('Base', 'Manual', 4, '1.4L I4', 31),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 31),
('Premium', 'CVT', 4, '2.0L I4', 31),
('Base', 'Manual', 4, '1.4L I4', 32),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 32),
('Premium', 'CVT', 4, '2.0L I4', 32),
('Base', 'Manual', 4, '1.4L I4', 33),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 33),
('Premium', 'CVT', 4, '2.0L I4', 33),
('Base', 'Manual', 4, '1.4L I4', 34),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 34),
('Premium', 'CVT', 4, '2.0L I4', 34),
('Base', 'Manual', 4, '1.4L I4', 35),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 35),
('Premium', 'CVT', 4, '2.0L I4', 35),
('Base', 'Manual', 4, '1.4L I4', 36),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 36),
('Premium', 'CVT', 4, '2.0L I4', 36),
('Base', 'Manual', 4, '1.4L I4', 37),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 37),
('Premium', 'CVT', 4, '2.0L I4', 37),
('Base', 'Manual', 4, '1.4L I4', 38),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 38),
('Premium', 'CVT', 4, '2.0L I4', 38),
('Base', 'Manual', 4, '1.4L I4', 39),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 39),
('Premium', 'CVT', 4, '2.0L I4', 39),
('Base', 'Manual', 4, '1.4L I4', 40),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 40),
('Premium', 'CVT', 4, '2.0L I4', 40),
('Base', 'Manual', 4, '1.4L I4', 41),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 41),
('Premium', 'CVT', 4, '2.0L I4', 41),
('Base', 'Manual', 4, '1.4L I4', 42),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 42),
('Premium', 'CVT', 4, '2.0L I4', 42),
('Base', 'Manual', 4, '1.4L I4', 43),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 43),
('Premium', 'CVT', 4, '2.0L I4', 43),
('Base', 'Manual', 4, '1.4L I4', 44),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 44),
('Premium', 'CVT', 4, '2.0L I4', 44),
('Base', 'Manual', 4, '1.4L I4', 45),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 45),
('Premium', 'CVT', 4, '2.0L I4', 45),
('Base', 'Manual', 4, '1.4L I4', 46),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 46),
('Premium', 'CVT', 4, '2.0L I4', 46),
('Base', 'Manual', 4, '1.4L I4', 47),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 47),
('Premium', 'CVT', 4, '2.0L I4', 47),
('Base', 'Manual', 4, '1.4L I4', 48),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 48),
('Premium', 'CVT', 4, '2.0L I4', 48),
('Base', 'Manual', 4, '1.4L I4', 49),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 49),
('Premium', 'CVT', 4, '2.0L I4', 49),
('Base', 'Manual', 4, '1.4L I4', 50),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 50),
('Premium', 'CVT', 4, '2.0L I4', 50),
('Base', 'Manual', 4, '1.4L I4', 51),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 51),
('Premium', 'CVT', 4, '2.0L I4', 51),
('Base', 'Manual', 4, '1.4L I4', 52),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 52),
('Premium', 'CVT', 4, '2.0L I4', 52),
('Base', 'Manual', 4, '1.4L I4', 53),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 53),
('Premium', 'CVT', 4, '2.0L I4', 53),
('Base', 'Manual', 4, '1.4L I4', 54),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 54),
('Premium', 'CVT', 4, '2.0L I4', 54),
('Base', 'Manual', 4, '1.4L I4', 55),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 55),
('Premium', 'CVT', 4, '2.0L I4', 55),
('Base', 'Manual', 4, '1.4L I4', 56),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 56),
('Premium', 'CVT', 4, '2.0L I4', 56),
('Base', 'Manual', 4, '1.4L I4', 57),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 57),
('Premium', 'CVT', 4, '2.0L I4', 57),
('Base', 'Manual', 4, '1.4L I4', 58),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 58),
('Premium', 'CVT', 4, '2.0L I4', 58),
('Base', 'Manual', 4, '1.4L I4', 59),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 59),
('Premium', 'CVT', 4, '2.0L I4', 59),
('Base', 'Manual', 4, '1.4L I4', 60),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 60),
('Premium', 'CVT', 4, '2.0L I4', 60),
('Base', 'Manual', 4, '1.4L I4', 61),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 61),
('Premium', 'CVT', 4, '2.0L I4', 61),
('Base', 'Manual', 4, '1.4L I4', 62),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 62),
('Premium', 'CVT', 4, '2.0L I4', 62),
('Base', 'Manual', 4, '1.4L I4', 63),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 63),
('Premium', 'CVT', 4, '2.0L I4', 63),
('Base', 'Manual', 4, '1.4L I4', 64),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 64),
('Premium', 'CVT', 4, '2.0L I4', 64),
('Base', 'Manual', 4, '1.4L I4', 65),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 65),
('Premium', 'CVT', 4, '2.0L I4', 65),
('Base', 'Manual', 4, '1.4L I4', 66),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 66),
('Premium', 'CVT', 4, '2.0L I4', 66),
('Base', 'Manual', 4, '1.4L I4', 67),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 67),
('Premium', 'CVT', 4, '2.0L I4', 67),
('Base', 'Manual', 4, '1.4L I4', 68),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 68),
('Premium', 'CVT', 4, '2.0L I4', 68),
('Base', 'Manual', 4, '1.4L I4', 69),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 69),
('Premium', 'CVT', 4, '2.0L I4', 69),
('Base', 'Manual', 4, '1.4L I4', 70),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 70),
('Premium', 'CVT', 4, '2.0L I4', 70),
('Base', 'Manual', 4, '1.4L I4', 71),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 71),
('Premium', 'CVT', 4, '2.0L I4', 71),
('Base', 'Manual', 4, '1.4L I4', 72),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 72),
('Premium', 'CVT', 4, '2.0L I4', 72),
('Base', 'Manual', 4, '1.4L I4', 73),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 73),
('Premium', 'CVT', 4, '2.0L I4', 73),
('Base', 'Manual', 4, '1.4L I4', 74),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 74),
('Premium', 'CVT', 4, '2.0L I4', 74),
('Base', 'Manual', 4, '1.4L I4', 75),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 75),
('Premium', 'CVT', 4, '2.0L I4', 75),
('Base', 'Manual', 4, '1.4L I4', 76),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 76),
('Premium', 'CVT', 4, '2.0L I4', 76),
('Base', 'Manual', 4, '1.4L I4', 77),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 77),
('Premium', 'CVT', 4, '2.0L I4', 77),
('Base', 'Manual', 4, '1.4L I4', 78),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 78),
('Premium', 'CVT', 4, '2.0L I4', 78),
('Base', 'Manual', 4, '1.4L I4', 79),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 79),
('Premium', 'CVT', 4, '2.0L I4', 79),
('Base', 'Manual', 4, '1.4L I4', 80),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 80),
('Premium', 'CVT', 4, '2.0L I4', 80),
('Base', 'Manual', 4, '1.4L I4', 81),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 81),
('Premium', 'CVT', 4, '2.0L I4', 81),
('Base', 'Manual', 4, '1.4L I4', 82),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 82),
('Premium', 'CVT', 4, '2.0L I4', 82),
('Base', 'Manual', 4, '1.4L I4', 83),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 83),
('Premium', 'CVT', 4, '2.0L I4', 83),
('Base', 'Manual', 4, '1.4L I4', 84),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 84),
('Premium', 'CVT', 4, '2.0L I4', 84),
('Base', 'Manual', 4, '1.4L I4', 85),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 85),
('Premium', 'CVT', 4, '2.0L I4', 85),
('Base', 'Manual', 4, '1.4L I4', 86),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 86),
('Premium', 'CVT', 4, '2.0L I4', 86),
('Base', 'Manual', 4, '1.4L I4', 87),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 87),
('Premium', 'CVT', 4, '2.0L I4', 87),
('Base', 'Manual', 4, '1.4L I4', 88),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 88),
('Premium', 'CVT', 4, '2.0L I4', 88),
('Base', 'Manual', 4, '1.4L I4', 89),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 89),
('Premium', 'CVT', 4, '2.0L I4', 89),
('Base', 'Manual', 4, '1.4L I4', 90),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 90),
('Premium', 'CVT', 4, '2.0L I4', 90),
('Base', 'Manual', 4, '1.4L I4', 91),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 91),
('Premium', 'CVT', 4, '2.0L I4', 91),
('Base', 'Manual', 4, '1.4L I4', 92),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 92),
('Premium', 'CVT', 4, '2.0L I4', 92),
('Base', 'Manual', 4, '1.4L I4', 93),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 93),
('Premium', 'CVT', 4, '2.0L I4', 93),
('Base', 'Manual', 4, '1.4L I4', 94),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 94),
('Premium', 'CVT', 4, '2.0L I4', 94),
('Base', 'Manual', 4, '1.4L I4', 95),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 95),
('Premium', 'CVT', 4, '2.0L I4', 95),
('Base', 'Manual', 4, '1.4L I4', 96),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 96),
('Premium', 'CVT', 4, '2.0L I4', 96),
('Base', 'Manual', 4, '1.4L I4', 97),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 97),
('Premium', 'CVT', 4, '2.0L I4', 97),
('Base', 'Manual', 4, '1.4L I4', 98),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 98),
('Premium', 'CVT', 4, '2.0L I4', 98),
('Base', 'Manual', 4, '1.4L I4', 99),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 99),
('Premium', 'CVT', 4, '2.0L I4', 99),
('Base', 'Manual', 4, '1.4L I4', 100),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 100),
('Premium', 'CVT', 4, '2.0L I4', 100),
('Base', 'Manual', 4, '1.4L I4', 101),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 101),
('Premium', 'CVT', 4, '2.0L I4', 101),
('Base', 'Manual', 4, '1.4L I4', 102),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 102),
('Premium', 'CVT', 4, '2.0L I4', 102),
('Base', 'Manual', 4, '1.4L I4', 103),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 103),
('Premium', 'CVT', 4, '2.0L I4', 103),
('Base', 'Manual', 4, '1.4L I4', 104),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 104),
('Premium', 'CVT', 4, '2.0L I4', 104),
('Base', 'Manual', 4, '1.4L I4', 105),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 105),
('Premium', 'CVT', 4, '2.0L I4', 105),
('Base', 'Manual', 4, '1.4L I4', 106),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 106),
('Premium', 'CVT', 4, '2.0L I4', 106),
('Base', 'Manual', 4, '1.4L I4', 107),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 107),
('Premium', 'CVT', 4, '2.0L I4', 107),
('Base', 'Manual', 4, '1.4L I4', 108),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 108),
('Premium', 'CVT', 4, '2.0L I4', 108),
('Base', 'Manual', 4, '1.4L I4', 109),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 109),
('Premium', 'CVT', 4, '2.0L I4', 109),
('Base', 'Manual', 4, '1.4L I4', 110),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 110),
('Premium', 'CVT', 4, '2.0L I4', 110),
('Base', 'Manual', 4, '1.4L I4', 111),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 111),
('Premium', 'CVT', 4, '2.0L I4', 111),
('Base', 'Manual', 4, '1.4L I4', 112),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 112),
('Premium', 'CVT', 4, '2.0L I4', 112),
('Base', 'Manual', 4, '1.4L I4', 113),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 113),
('Premium', 'CVT', 4, '2.0L I4', 113),
('Base', 'Manual', 4, '1.4L I4', 114),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 114),
('Premium', 'CVT', 4, '2.0L I4', 114),
('Base', 'Manual', 4, '1.4L I4', 115),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 115),
('Premium', 'CVT', 4, '2.0L I4', 115),
('Base', 'Manual', 4, '1.4L I4', 116),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 116),
('Premium', 'CVT', 4, '2.0L I4', 116),
('Base', 'Manual', 4, '1.4L I4', 117),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 117),
('Premium', 'CVT', 4, '2.0L I4', 117),
('Base', 'Manual', 4, '1.4L I4', 118),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 118),
('Premium', 'CVT', 4, '2.0L I4', 118),
('Base', 'Manual', 4, '1.4L I4', 119),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 119),
('Premium', 'CVT', 4, '2.0L I4', 119),
('Base', 'Manual', 4, '1.4L I4', 120),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 120),
('Premium', 'CVT', 4, '2.0L I4', 120),
('Base', 'Manual', 4, '1.4L I4', 121),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 121),
('Premium', 'CVT', 4, '2.0L I4', 121),
('Base', 'Manual', 4, '1.4L I4', 122),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 122),
('Premium', 'CVT', 4, '2.0L I4', 122),
('Base', 'Manual', 4, '1.4L I4', 123),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 123),
('Premium', 'CVT', 4, '2.0L I4', 123),
('Base', 'Manual', 4, '1.4L I4', 124),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 124),
('Premium', 'CVT', 4, '2.0L I4', 124),
('Base', 'Manual', 4, '1.4L I4', 125),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 125),
('Premium', 'CVT', 4, '2.0L I4', 125),
('Base', 'Manual', 4, '1.4L I4', 126),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 126),
('Premium', 'CVT', 4, '2.0L I4', 126),
('Base', 'Manual', 4, '1.4L I4', 127),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 127),
('Premium', 'CVT', 4, '2.0L I4', 127),
('Base', 'Manual', 4, '1.4L I4', 128),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 128),
('Premium', 'CVT', 4, '2.0L I4', 128),
('Base', 'Manual', 4, '1.4L I4', 129),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 129),
('Premium', 'CVT', 4, '2.0L I4', 129),
('Base', 'Manual', 4, '1.4L I4', 130),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 130),
('Premium', 'CVT', 4, '2.0L I4', 130),
('Base', 'Manual', 4, '1.4L I4', 131),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 131),
('Premium', 'CVT', 4, '2.0L I4', 131),
('Base', 'Manual', 4, '1.4L I4', 132),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 132),
('Premium', 'CVT', 4, '2.0L I4', 132),
('Base', 'Manual', 4, '1.4L I4', 133),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 133),
('Premium', 'CVT', 4, '2.0L I4', 133),
('Base', 'Manual', 4, '1.4L I4', 134),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 134),
('Premium', 'CVT', 4, '2.0L I4', 134),
('Base', 'Manual', 4, '1.4L I4', 135),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 135),
('Premium', 'CVT', 4, '2.0L I4', 135),
('Base', 'Manual', 4, '1.4L I4', 136),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 136),
('Premium', 'CVT', 4, '2.0L I4', 136),
('Base', 'Manual', 4, '1.4L I4', 137),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 137),
('Premium', 'CVT', 4, '2.0L I4', 137),
('Base', 'Manual', 4, '1.4L I4', 138),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 138),
('Premium', 'CVT', 4, '2.0L I4', 138),
('Base', 'Manual', 4, '1.4L I4', 139),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 139),
('Premium', 'CVT', 4, '2.0L I4', 139),
('Base', 'Manual', 4, '1.4L I4', 140),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 140),
('Premium', 'CVT', 4, '2.0L I4', 140),
('Base', 'Manual', 4, '1.4L I4', 141),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 141),
('Premium', 'CVT', 4, '2.0L I4', 141),
('Base', 'Manual', 4, '1.4L I4', 142),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 142),
('Premium', 'CVT', 4, '2.0L I4', 142),
('Base', 'Manual', 4, '1.4L I4', 143),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 143),
('Premium', 'CVT', 4, '2.0L I4', 143),
('Base', 'Manual', 4, '1.4L I4', 144),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 144),
('Premium', 'CVT', 4, '2.0L I4', 144),
('Base', 'Manual', 4, '1.4L I4', 145),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 145),
('Premium', 'CVT', 4, '2.0L I4', 145),
('Base', 'Manual', 4, '1.4L I4', 146),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 146),
('Premium', 'CVT', 4, '2.0L I4', 146),
('Base', 'Manual', 4, '1.4L I4', 147),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 147),
('Premium', 'CVT', 4, '2.0L I4', 147),
('Base', 'Manual', 4, '1.4L I4', 148),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 148),
('Premium', 'CVT', 4, '2.0L I4', 148),
('Base', 'Manual', 4, '1.4L I4', 149),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 149),
('Premium', 'CVT', 4, '2.0L I4', 149),
('Base', 'Manual', 4, '1.4L I4', 150),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 150),
('Premium', 'CVT', 4, '2.0L I4', 150);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (15000.50, 'CP-20250301', '2025-03-01', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (8450.75, 'CP-20250315', '2025-03-15', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (23000.00, 'CP-20250320', '2025-03-20', 1, 2);



  -- Inserciones para pruebas ClientRepositoryTest                                                                                  -- INSERTAR 2 ADMINISTRADORES
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
INSERT INTO Proveedor 
  (nombreProveedor, calle, numero, codigoPostal, correo, telefono, contactoPrincipal, estado)
VALUES 
  ('Aston Elite Motors', 'Boulevard Prestige', 777, '11223', 'contact@astonelite.com', '5599001122', 'Richard Langston', 'Activo');


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

--15. Insertar marcas:
INSERT INTO Marcas (nombre) VALUES
('Toyota'),
('Honda'),
('Ford'),
('Chevrolet'),
('BMW'),
('Mercedes-Benz'),
('Audi'),
('Volkswagen'),
('Nissan'),
('Hyundai'),
('Kia'),
('Mazda'),
('Subaru'),
('Tesla'),
('Lexus'),
('Jeep'),
('Volvo'),
('Peugeot'),
('Renault'),
('Fiat'),
('Mitsubishi'),
('Acura'),
('Alfa Romeo'),
('Citroën'),
('Mini'),
('Chrysler'),
('Infiniti'),
('Genesis'),
('Land Rover'),
('Jaguar');

INSERT INTO Modelo (nombre, idMarca) VALUES
-- Toyota (1)
('Corolla', 1), ('Camry', 1), ('Yaris', 1), ('RAV4', 1), ('Hilux', 1),

-- Honda (3)
('Civic', 3), ('Accord', 3), ('CR-V', 3), ('Fit', 3), ('HR-V', 3),

-- Ford (4)
('Focus', 4), ('Mustang', 4), ('F-150', 4), ('Escape', 4), ('Explorer', 4),

-- Chevrolet (5)
('Aveo', 5), ('Cruze', 5), ('Spark', 5), ('Trax', 5), ('Malibu', 5),

-- BMW (6)
('Serie 1', 6), ('Serie 3', 6), ('Serie 5', 6), ('X3', 6), ('X5', 6),

-- Mercedes-Benz (7)
('Clase A', 7), ('Clase C', 7), ('GLA', 7), ('GLC', 7), ('Clase E', 7),

-- Audi (8)
('A3', 8), ('A4', 8), ('Q2', 8), ('Q3', 8), ('Q5', 8),

-- Volkswagen (9)
('Jetta', 9), ('Golf', 9), ('Tiguan', 9), ('Polo', 9), ('Passat', 9),

-- Nissan (10)
('Sentra', 10), ('Versa', 10), ('Altima', 10), ('March', 10), ('X-Trail', 10),

-- Hyundai (11)
('Elantra', 11), ('Tucson', 11), ('Accent', 11), ('Santa Fe', 11), ('Kona', 11),

-- Kia (12)
('Rio', 12), ('Forte', 12), ('Sportage', 12), ('Sorento', 12), ('Seltos', 12),

-- Mazda (13)
('Mazda2', 13), ('Mazda3', 13), ('CX-3', 13), ('CX-5', 13), ('Mazda6', 13),

-- Subaru (14)
('Impreza', 14), ('Forester', 14), ('Outback', 14), ('Legacy', 14), ('XV', 14),

-- Tesla (15)
('Model S', 15), ('Model 3', 15), ('Model X', 15), ('Model Y', 15), ('Cybertruck', 15),

-- Lexus (16)
('IS', 16), ('ES', 16), ('RX', 16), ('NX', 16), ('UX', 16),

-- Jeep (17)
('Wrangler', 17), ('Cherokee', 17), ('Compass', 17), ('Renegade', 17), ('Gladiator', 17),

-- Volvo (18)
('XC40', 18), ('XC60', 18), ('XC90', 18), ('S60', 18), ('V60', 18),

-- Peugeot (19)
('208', 19), ('2008', 19), ('3008', 19), ('5008', 19), ('308', 19),

-- Renault (20)
('Clio', 20), ('Koleos', 20), ('Captur', 20), ('Duster', 20), ('Logan', 20),

-- Fiat (21)
('500', 21), ('Punto', 21), ('Tipo', 21), ('Uno', 21), ('Argo', 21),

-- Mitsubishi (22)
('Lancer', 22), ('Outlander', 22), ('ASX', 22), ('Mirage', 22), ('Eclipse Cross', 22),

-- Acura (23)
('ILX', 23), ('TLX', 23), ('RDX', 23), ('MDX', 23), ('Integra', 23),

-- Alfa Romeo (24)
('Giulia', 24), ('Stelvio', 24), ('Mito', 24), ('Tonale', 24), ('4C', 24),

-- Citroën (25)
('C3', 25), ('C4', 25), ('C5', 25), ('Berlingo', 25), ('Spacetourer', 25),

-- Mini (26)
('Cooper', 26), ('Countryman', 26), ('Clubman', 26), ('Paceman', 26), ('Cabrio', 26),

-- Chrysler (27)
('300', 27), ('Pacifica', 27), ('Voyager', 27), ('Aspen', 27), ('Sebring', 27),

-- Infiniti (28)
('Q50', 28), ('Q60', 28), ('QX50', 28), ('QX60', 28), ('QX80', 28),

-- Genesis (29)
('G70', 29), ('G80', 29), ('G90', 29), ('GV70', 29), ('GV80', 29),

-- Land Rover (30)
('Defender', 30), ('Discovery', 30), ('Range Rover', 30), ('Velar', 30), ('Evoque', 30),

-- Jaguar (31)
('XE', 31), ('XF', 31), ('F-Pace', 31), ('E-Pace', 31), ('I-Pace', 31);

-- 16. Insertar en Version
INSERT INTO Version (nombre, transmision, puertas, motor, idModelo) VALUES
('Base', 'Manual', 4, '1.4L I4', 1),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 1),
('Premium', 'CVT', 4, '2.0L I4', 1),
('Base', 'Manual', 4, '1.4L I4', 2),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 2),
('Premium', 'CVT', 4, '2.0L I4', 2),
('Base', 'Manual', 4, '1.4L I4', 3),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 3),
('Premium', 'CVT', 4, '2.0L I4', 3),
('Base', 'Manual', 4, '1.4L I4', 4),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 4),
('Premium', 'CVT', 4, '2.0L I4', 4),
('Base', 'Manual', 4, '1.4L I4', 5),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 5),
('Premium', 'CVT', 4, '2.0L I4', 5),
('Base', 'Manual', 4, '1.4L I4', 6),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 6),
('Premium', 'CVT', 4, '2.0L I4', 6),
('Base', 'Manual', 4, '1.4L I4', 7),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 7),
('Premium', 'CVT', 4, '2.0L I4', 7),
('Base', 'Manual', 4, '1.4L I4', 8),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 8),
('Premium', 'CVT', 4, '2.0L I4', 8),
('Base', 'Manual', 4, '1.4L I4', 9),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 9),
('Premium', 'CVT', 4, '2.0L I4', 9),
('Base', 'Manual', 4, '1.4L I4', 10),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 10),
('Premium', 'CVT', 4, '2.0L I4', 10),
('Base', 'Manual', 4, '1.4L I4', 11),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 11),
('Premium', 'CVT', 4, '2.0L I4', 11),
('Base', 'Manual', 4, '1.4L I4', 12),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 12),
('Premium', 'CVT', 4, '2.0L I4', 12),
('Base', 'Manual', 4, '1.4L I4', 13),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 13),
('Premium', 'CVT', 4, '2.0L I4', 13),
('Base', 'Manual', 4, '1.4L I4', 14),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 14),
('Premium', 'CVT', 4, '2.0L I4', 14),
('Base', 'Manual', 4, '1.4L I4', 15),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 15),
('Premium', 'CVT', 4, '2.0L I4', 15),
('Base', 'Manual', 4, '1.4L I4', 16),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 16),
('Premium', 'CVT', 4, '2.0L I4', 16),
('Base', 'Manual', 4, '1.4L I4', 17),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 17),
('Premium', 'CVT', 4, '2.0L I4', 17),
('Base', 'Manual', 4, '1.4L I4', 18),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 18),
('Premium', 'CVT', 4, '2.0L I4', 18),
('Base', 'Manual', 4, '1.4L I4', 19),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 19),
('Premium', 'CVT', 4, '2.0L I4', 19),
('Base', 'Manual', 4, '1.4L I4', 20),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 20),
('Premium', 'CVT', 4, '2.0L I4', 20),
('Base', 'Manual', 4, '1.4L I4', 21),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 21),
('Premium', 'CVT', 4, '2.0L I4', 21),
('Base', 'Manual', 4, '1.4L I4', 22),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 22),
('Premium', 'CVT', 4, '2.0L I4', 22),
('Base', 'Manual', 4, '1.4L I4', 23),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 23),
('Premium', 'CVT', 4, '2.0L I4', 23),
('Base', 'Manual', 4, '1.4L I4', 24),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 24),
('Premium', 'CVT', 4, '2.0L I4', 24),
('Base', 'Manual', 4, '1.4L I4', 25),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 25),
('Premium', 'CVT', 4, '2.0L I4', 25),
('Base', 'Manual', 4, '1.4L I4', 26),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 26),
('Premium', 'CVT', 4, '2.0L I4', 26),
('Base', 'Manual', 4, '1.4L I4', 27),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 27),
('Premium', 'CVT', 4, '2.0L I4', 27),
('Base', 'Manual', 4, '1.4L I4', 28),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 28),
('Premium', 'CVT', 4, '2.0L I4', 28),
('Base', 'Manual', 4, '1.4L I4', 29),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 29),
('Premium', 'CVT', 4, '2.0L I4', 29),
('Base', 'Manual', 4, '1.4L I4', 30),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 30),
('Premium', 'CVT', 4, '2.0L I4', 30),
('Base', 'Manual', 4, '1.4L I4', 31),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 31),
('Premium', 'CVT', 4, '2.0L I4', 31),
('Base', 'Manual', 4, '1.4L I4', 32),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 32),
('Premium', 'CVT', 4, '2.0L I4', 32),
('Base', 'Manual', 4, '1.4L I4', 33),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 33),
('Premium', 'CVT', 4, '2.0L I4', 33),
('Base', 'Manual', 4, '1.4L I4', 34),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 34),
('Premium', 'CVT', 4, '2.0L I4', 34),
('Base', 'Manual', 4, '1.4L I4', 35),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 35),
('Premium', 'CVT', 4, '2.0L I4', 35),
('Base', 'Manual', 4, '1.4L I4', 36),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 36),
('Premium', 'CVT', 4, '2.0L I4', 36),
('Base', 'Manual', 4, '1.4L I4', 37),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 37),
('Premium', 'CVT', 4, '2.0L I4', 37),
('Base', 'Manual', 4, '1.4L I4', 38),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 38),
('Premium', 'CVT', 4, '2.0L I4', 38),
('Base', 'Manual', 4, '1.4L I4', 39),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 39),
('Premium', 'CVT', 4, '2.0L I4', 39),
('Base', 'Manual', 4, '1.4L I4', 40),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 40),
('Premium', 'CVT', 4, '2.0L I4', 40),
('Base', 'Manual', 4, '1.4L I4', 41),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 41),
('Premium', 'CVT', 4, '2.0L I4', 41),
('Base', 'Manual', 4, '1.4L I4', 42),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 42),
('Premium', 'CVT', 4, '2.0L I4', 42),
('Base', 'Manual', 4, '1.4L I4', 43),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 43),
('Premium', 'CVT', 4, '2.0L I4', 43),
('Base', 'Manual', 4, '1.4L I4', 44),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 44),
('Premium', 'CVT', 4, '2.0L I4', 44),
('Base', 'Manual', 4, '1.4L I4', 45),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 45),
('Premium', 'CVT', 4, '2.0L I4', 45),
('Base', 'Manual', 4, '1.4L I4', 46),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 46),
('Premium', 'CVT', 4, '2.0L I4', 46),
('Base', 'Manual', 4, '1.4L I4', 47),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 47),
('Premium', 'CVT', 4, '2.0L I4', 47),
('Base', 'Manual', 4, '1.4L I4', 48),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 48),
('Premium', 'CVT', 4, '2.0L I4', 48),
('Base', 'Manual', 4, '1.4L I4', 49),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 49),
('Premium', 'CVT', 4, '2.0L I4', 49),
('Base', 'Manual', 4, '1.4L I4', 50),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 50),
('Premium', 'CVT', 4, '2.0L I4', 50),
('Base', 'Manual', 4, '1.4L I4', 51),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 51),
('Premium', 'CVT', 4, '2.0L I4', 51),
('Base', 'Manual', 4, '1.4L I4', 52),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 52),
('Premium', 'CVT', 4, '2.0L I4', 52),
('Base', 'Manual', 4, '1.4L I4', 53),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 53),
('Premium', 'CVT', 4, '2.0L I4', 53),
('Base', 'Manual', 4, '1.4L I4', 54),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 54),
('Premium', 'CVT', 4, '2.0L I4', 54),
('Base', 'Manual', 4, '1.4L I4', 55),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 55),
('Premium', 'CVT', 4, '2.0L I4', 55),
('Base', 'Manual', 4, '1.4L I4', 56),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 56),
('Premium', 'CVT', 4, '2.0L I4', 56),
('Base', 'Manual', 4, '1.4L I4', 57),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 57),
('Premium', 'CVT', 4, '2.0L I4', 57),
('Base', 'Manual', 4, '1.4L I4', 58),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 58),
('Premium', 'CVT', 4, '2.0L I4', 58),
('Base', 'Manual', 4, '1.4L I4', 59),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 59),
('Premium', 'CVT', 4, '2.0L I4', 59),
('Base', 'Manual', 4, '1.4L I4', 60),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 60),
('Premium', 'CVT', 4, '2.0L I4', 60),
('Base', 'Manual', 4, '1.4L I4', 61),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 61),
('Premium', 'CVT', 4, '2.0L I4', 61),
('Base', 'Manual', 4, '1.4L I4', 62),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 62),
('Premium', 'CVT', 4, '2.0L I4', 62),
('Base', 'Manual', 4, '1.4L I4', 63),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 63),
('Premium', 'CVT', 4, '2.0L I4', 63),
('Base', 'Manual', 4, '1.4L I4', 64),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 64),
('Premium', 'CVT', 4, '2.0L I4', 64),
('Base', 'Manual', 4, '1.4L I4', 65),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 65),
('Premium', 'CVT', 4, '2.0L I4', 65),
('Base', 'Manual', 4, '1.4L I4', 66),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 66),
('Premium', 'CVT', 4, '2.0L I4', 66),
('Base', 'Manual', 4, '1.4L I4', 67),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 67),
('Premium', 'CVT', 4, '2.0L I4', 67),
('Base', 'Manual', 4, '1.4L I4', 68),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 68),
('Premium', 'CVT', 4, '2.0L I4', 68),
('Base', 'Manual', 4, '1.4L I4', 69),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 69),
('Premium', 'CVT', 4, '2.0L I4', 69),
('Base', 'Manual', 4, '1.4L I4', 70),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 70),
('Premium', 'CVT', 4, '2.0L I4', 70),
('Base', 'Manual', 4, '1.4L I4', 71),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 71),
('Premium', 'CVT', 4, '2.0L I4', 71),
('Base', 'Manual', 4, '1.4L I4', 72),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 72),
('Premium', 'CVT', 4, '2.0L I4', 72),
('Base', 'Manual', 4, '1.4L I4', 73),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 73),
('Premium', 'CVT', 4, '2.0L I4', 73),
('Base', 'Manual', 4, '1.4L I4', 74),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 74),
('Premium', 'CVT', 4, '2.0L I4', 74),
('Base', 'Manual', 4, '1.4L I4', 75),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 75),
('Premium', 'CVT', 4, '2.0L I4', 75),
('Base', 'Manual', 4, '1.4L I4', 76),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 76),
('Premium', 'CVT', 4, '2.0L I4', 76),
('Base', 'Manual', 4, '1.4L I4', 77),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 77),
('Premium', 'CVT', 4, '2.0L I4', 77),
('Base', 'Manual', 4, '1.4L I4', 78),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 78),
('Premium', 'CVT', 4, '2.0L I4', 78),
('Base', 'Manual', 4, '1.4L I4', 79),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 79),
('Premium', 'CVT', 4, '2.0L I4', 79),
('Base', 'Manual', 4, '1.4L I4', 80),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 80),
('Premium', 'CVT', 4, '2.0L I4', 80),
('Base', 'Manual', 4, '1.4L I4', 81),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 81),
('Premium', 'CVT', 4, '2.0L I4', 81),
('Base', 'Manual', 4, '1.4L I4', 82),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 82),
('Premium', 'CVT', 4, '2.0L I4', 82),
('Base', 'Manual', 4, '1.4L I4', 83),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 83),
('Premium', 'CVT', 4, '2.0L I4', 83),
('Base', 'Manual', 4, '1.4L I4', 84),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 84),
('Premium', 'CVT', 4, '2.0L I4', 84),
('Base', 'Manual', 4, '1.4L I4', 85),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 85),
('Premium', 'CVT', 4, '2.0L I4', 85),
('Base', 'Manual', 4, '1.4L I4', 86),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 86),
('Premium', 'CVT', 4, '2.0L I4', 86),
('Base', 'Manual', 4, '1.4L I4', 87),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 87),
('Premium', 'CVT', 4, '2.0L I4', 87),
('Base', 'Manual', 4, '1.4L I4', 88),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 88),
('Premium', 'CVT', 4, '2.0L I4', 88),
('Base', 'Manual', 4, '1.4L I4', 89),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 89),
('Premium', 'CVT', 4, '2.0L I4', 89),
('Base', 'Manual', 4, '1.4L I4', 90),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 90),
('Premium', 'CVT', 4, '2.0L I4', 90),
('Base', 'Manual', 4, '1.4L I4', 91),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 91),
('Premium', 'CVT', 4, '2.0L I4', 91),
('Base', 'Manual', 4, '1.4L I4', 92),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 92),
('Premium', 'CVT', 4, '2.0L I4', 92),
('Base', 'Manual', 4, '1.4L I4', 93),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 93),
('Premium', 'CVT', 4, '2.0L I4', 93),
('Base', 'Manual', 4, '1.4L I4', 94),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 94),
('Premium', 'CVT', 4, '2.0L I4', 94),
('Base', 'Manual', 4, '1.4L I4', 95),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 95),
('Premium', 'CVT', 4, '2.0L I4', 95),
('Base', 'Manual', 4, '1.4L I4', 96),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 96),
('Premium', 'CVT', 4, '2.0L I4', 96),
('Base', 'Manual', 4, '1.4L I4', 97),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 97),
('Premium', 'CVT', 4, '2.0L I4', 97),
('Base', 'Manual', 4, '1.4L I4', 98),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 98),
('Premium', 'CVT', 4, '2.0L I4', 98),
('Base', 'Manual', 4, '1.4L I4', 99),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 99),
('Premium', 'CVT', 4, '2.0L I4', 99),
('Base', 'Manual', 4, '1.4L I4', 100),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 100),
('Premium', 'CVT', 4, '2.0L I4', 100),
('Base', 'Manual', 4, '1.4L I4', 101),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 101),
('Premium', 'CVT', 4, '2.0L I4', 101),
('Base', 'Manual', 4, '1.4L I4', 102),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 102),
('Premium', 'CVT', 4, '2.0L I4', 102),
('Base', 'Manual', 4, '1.4L I4', 103),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 103),
('Premium', 'CVT', 4, '2.0L I4', 103),
('Base', 'Manual', 4, '1.4L I4', 104),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 104),
('Premium', 'CVT', 4, '2.0L I4', 104),
('Base', 'Manual', 4, '1.4L I4', 105),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 105),
('Premium', 'CVT', 4, '2.0L I4', 105),
('Base', 'Manual', 4, '1.4L I4', 106),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 106),
('Premium', 'CVT', 4, '2.0L I4', 106),
('Base', 'Manual', 4, '1.4L I4', 107),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 107),
('Premium', 'CVT', 4, '2.0L I4', 107),
('Base', 'Manual', 4, '1.4L I4', 108),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 108),
('Premium', 'CVT', 4, '2.0L I4', 108),
('Base', 'Manual', 4, '1.4L I4', 109),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 109),
('Premium', 'CVT', 4, '2.0L I4', 109),
('Base', 'Manual', 4, '1.4L I4', 110),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 110),
('Premium', 'CVT', 4, '2.0L I4', 110),
('Base', 'Manual', 4, '1.4L I4', 111),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 111),
('Premium', 'CVT', 4, '2.0L I4', 111),
('Base', 'Manual', 4, '1.4L I4', 112),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 112),
('Premium', 'CVT', 4, '2.0L I4', 112),
('Base', 'Manual', 4, '1.4L I4', 113),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 113),
('Premium', 'CVT', 4, '2.0L I4', 113),
('Base', 'Manual', 4, '1.4L I4', 114),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 114),
('Premium', 'CVT', 4, '2.0L I4', 114),
('Base', 'Manual', 4, '1.4L I4', 115),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 115),
('Premium', 'CVT', 4, '2.0L I4', 115),
('Base', 'Manual', 4, '1.4L I4', 116),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 116),
('Premium', 'CVT', 4, '2.0L I4', 116),
('Base', 'Manual', 4, '1.4L I4', 117),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 117),
('Premium', 'CVT', 4, '2.0L I4', 117),
('Base', 'Manual', 4, '1.4L I4', 118),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 118),
('Premium', 'CVT', 4, '2.0L I4', 118),
('Base', 'Manual', 4, '1.4L I4', 119),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 119),
('Premium', 'CVT', 4, '2.0L I4', 119),
('Base', 'Manual', 4, '1.4L I4', 120),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 120),
('Premium', 'CVT', 4, '2.0L I4', 120),
('Base', 'Manual', 4, '1.4L I4', 121),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 121),
('Premium', 'CVT', 4, '2.0L I4', 121),
('Base', 'Manual', 4, '1.4L I4', 122),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 122),
('Premium', 'CVT', 4, '2.0L I4', 122),
('Base', 'Manual', 4, '1.4L I4', 123),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 123),
('Premium', 'CVT', 4, '2.0L I4', 123),
('Base', 'Manual', 4, '1.4L I4', 124),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 124),
('Premium', 'CVT', 4, '2.0L I4', 124),
('Base', 'Manual', 4, '1.4L I4', 125),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 125),
('Premium', 'CVT', 4, '2.0L I4', 125),
('Base', 'Manual', 4, '1.4L I4', 126),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 126),
('Premium', 'CVT', 4, '2.0L I4', 126),
('Base', 'Manual', 4, '1.4L I4', 127),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 127),
('Premium', 'CVT', 4, '2.0L I4', 127),
('Base', 'Manual', 4, '1.4L I4', 128),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 128),
('Premium', 'CVT', 4, '2.0L I4', 128),
('Base', 'Manual', 4, '1.4L I4', 129),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 129),
('Premium', 'CVT', 4, '2.0L I4', 129),
('Base', 'Manual', 4, '1.4L I4', 130),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 130),
('Premium', 'CVT', 4, '2.0L I4', 130),
('Base', 'Manual', 4, '1.4L I4', 131),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 131),
('Premium', 'CVT', 4, '2.0L I4', 131),
('Base', 'Manual', 4, '1.4L I4', 132),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 132),
('Premium', 'CVT', 4, '2.0L I4', 132),
('Base', 'Manual', 4, '1.4L I4', 133),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 133),
('Premium', 'CVT', 4, '2.0L I4', 133),
('Base', 'Manual', 4, '1.4L I4', 134),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 134),
('Premium', 'CVT', 4, '2.0L I4', 134),
('Base', 'Manual', 4, '1.4L I4', 135),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 135),
('Premium', 'CVT', 4, '2.0L I4', 135),
('Base', 'Manual', 4, '1.4L I4', 136),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 136),
('Premium', 'CVT', 4, '2.0L I4', 136),
('Base', 'Manual', 4, '1.4L I4', 137),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 137),
('Premium', 'CVT', 4, '2.0L I4', 137),
('Base', 'Manual', 4, '1.4L I4', 138),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 138),
('Premium', 'CVT', 4, '2.0L I4', 138),
('Base', 'Manual', 4, '1.4L I4', 139),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 139),
('Premium', 'CVT', 4, '2.0L I4', 139),
('Base', 'Manual', 4, '1.4L I4', 140),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 140),
('Premium', 'CVT', 4, '2.0L I4', 140),
('Base', 'Manual', 4, '1.4L I4', 141),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 141),
('Premium', 'CVT', 4, '2.0L I4', 141),
('Base', 'Manual', 4, '1.4L I4', 142),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 142),
('Premium', 'CVT', 4, '2.0L I4', 142),
('Base', 'Manual', 4, '1.4L I4', 143),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 143),
('Premium', 'CVT', 4, '2.0L I4', 143),
('Base', 'Manual', 4, '1.4L I4', 144),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 144),
('Premium', 'CVT', 4, '2.0L I4', 144),
('Base', 'Manual', 4, '1.4L I4', 145),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 145),
('Premium', 'CVT', 4, '2.0L I4', 145),
('Base', 'Manual', 4, '1.4L I4', 146),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 146),
('Premium', 'CVT', 4, '2.0L I4', 146),
('Base', 'Manual', 4, '1.4L I4', 147),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 147),
('Premium', 'CVT', 4, '2.0L I4', 147),
('Base', 'Manual', 4, '1.4L I4', 148),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 148),
('Premium', 'CVT', 4, '2.0L I4', 148),
('Base', 'Manual', 4, '1.4L I4', 149),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 149),
('Premium', 'CVT', 4, '2.0L I4', 149),
('Base', 'Manual', 4, '1.4L I4', 150),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 150),
('Premium', 'CVT', 4, '2.0L I4', 150);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (15000.50, 'CP-20250301', '2025-03-01', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (8450.75, 'CP-20250315', '2025-03-15', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (23000.00, 'CP-20250320', '2025-03-20', 1, 2);



  -- Inserciones para pruebas ClientRepositoryTest                                                                                  -- INSERTAR 2 ADMINISTRADORES
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
INSERT INTO Proveedor 
  (nombreProveedor, calle, numero, codigoPostal, correo, telefono, contactoPrincipal, estado)
VALUES 
  ('Aston Elite Motors', 'Boulevard Prestige', 777, '11223', 'contact@astonelite.com', '5599001122', 'Richard Langston', 'Activo');


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

--15. Insertar marcas:
INSERT INTO Marcas (nombre) VALUES
('Toyota'),
('Honda'),
('Ford'),
('Chevrolet'),
('BMW'),
('Mercedes-Benz'),
('Audi'),
('Volkswagen'),
('Nissan'),
('Hyundai'),
('Kia'),
('Mazda'),
('Subaru'),
('Tesla'),
('Lexus'),
('Jeep'),
('Volvo'),
('Peugeot'),
('Renault'),
('Fiat'),
('Mitsubishi'),
('Acura'),
('Alfa Romeo'),
('Citroën'),
('Mini'),
('Chrysler'),
('Infiniti'),
('Genesis'),
('Land Rover'),
('Jaguar');

INSERT INTO Modelo (nombre, idMarca) VALUES
-- Toyota (1)
('Corolla', 1), ('Camry', 1), ('Yaris', 1), ('RAV4', 1), ('Hilux', 1),

-- Honda (3)
('Civic', 3), ('Accord', 3), ('CR-V', 3), ('Fit', 3), ('HR-V', 3),

-- Ford (4)
('Focus', 4), ('Mustang', 4), ('F-150', 4), ('Escape', 4), ('Explorer', 4),

-- Chevrolet (5)
('Aveo', 5), ('Cruze', 5), ('Spark', 5), ('Trax', 5), ('Malibu', 5),

-- BMW (6)
('Serie 1', 6), ('Serie 3', 6), ('Serie 5', 6), ('X3', 6), ('X5', 6),

-- Mercedes-Benz (7)
('Clase A', 7), ('Clase C', 7), ('GLA', 7), ('GLC', 7), ('Clase E', 7),

-- Audi (8)
('A3', 8), ('A4', 8), ('Q2', 8), ('Q3', 8), ('Q5', 8),

-- Volkswagen (9)
('Jetta', 9), ('Golf', 9), ('Tiguan', 9), ('Polo', 9), ('Passat', 9),

-- Nissan (10)
('Sentra', 10), ('Versa', 10), ('Altima', 10), ('March', 10), ('X-Trail', 10),

-- Hyundai (11)
('Elantra', 11), ('Tucson', 11), ('Accent', 11), ('Santa Fe', 11), ('Kona', 11),

-- Kia (12)
('Rio', 12), ('Forte', 12), ('Sportage', 12), ('Sorento', 12), ('Seltos', 12),

-- Mazda (13)
('Mazda2', 13), ('Mazda3', 13), ('CX-3', 13), ('CX-5', 13), ('Mazda6', 13),

-- Subaru (14)
('Impreza', 14), ('Forester', 14), ('Outback', 14), ('Legacy', 14), ('XV', 14),

-- Tesla (15)
('Model S', 15), ('Model 3', 15), ('Model X', 15), ('Model Y', 15), ('Cybertruck', 15),

-- Lexus (16)
('IS', 16), ('ES', 16), ('RX', 16), ('NX', 16), ('UX', 16),

-- Jeep (17)
('Wrangler', 17), ('Cherokee', 17), ('Compass', 17), ('Renegade', 17), ('Gladiator', 17),

-- Volvo (18)
('XC40', 18), ('XC60', 18), ('XC90', 18), ('S60', 18), ('V60', 18),

-- Peugeot (19)
('208', 19), ('2008', 19), ('3008', 19), ('5008', 19), ('308', 19),

-- Renault (20)
('Clio', 20), ('Koleos', 20), ('Captur', 20), ('Duster', 20), ('Logan', 20),

-- Fiat (21)
('500', 21), ('Punto', 21), ('Tipo', 21), ('Uno', 21), ('Argo', 21),

-- Mitsubishi (22)
('Lancer', 22), ('Outlander', 22), ('ASX', 22), ('Mirage', 22), ('Eclipse Cross', 22),

-- Acura (23)
('ILX', 23), ('TLX', 23), ('RDX', 23), ('MDX', 23), ('Integra', 23),

-- Alfa Romeo (24)
('Giulia', 24), ('Stelvio', 24), ('Mito', 24), ('Tonale', 24), ('4C', 24),

-- Citroën (25)
('C3', 25), ('C4', 25), ('C5', 25), ('Berlingo', 25), ('Spacetourer', 25),

-- Mini (26)
('Cooper', 26), ('Countryman', 26), ('Clubman', 26), ('Paceman', 26), ('Cabrio', 26),

-- Chrysler (27)
('300', 27), ('Pacifica', 27), ('Voyager', 27), ('Aspen', 27), ('Sebring', 27),

-- Infiniti (28)
('Q50', 28), ('Q60', 28), ('QX50', 28), ('QX60', 28), ('QX80', 28),

-- Genesis (29)
('G70', 29), ('G80', 29), ('G90', 29), ('GV70', 29), ('GV80', 29),

-- Land Rover (30)
('Defender', 30), ('Discovery', 30), ('Range Rover', 30), ('Velar', 30), ('Evoque', 30),

-- Jaguar (31)
('XE', 31), ('XF', 31), ('F-Pace', 31), ('E-Pace', 31), ('I-Pace', 31);

-- 16. Insertar en Version
INSERT INTO Version (nombre, transmision, puertas, motor, idModelo) VALUES
('Base', 'Manual', 4, '1.4L I4', 1),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 1),
('Premium', 'CVT', 4, '2.0L I4', 1),
('Base', 'Manual', 4, '1.4L I4', 2),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 2),
('Premium', 'CVT', 4, '2.0L I4', 2),
('Base', 'Manual', 4, '1.4L I4', 3),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 3),
('Premium', 'CVT', 4, '2.0L I4', 3),
('Base', 'Manual', 4, '1.4L I4', 4),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 4),
('Premium', 'CVT', 4, '2.0L I4', 4),
('Base', 'Manual', 4, '1.4L I4', 5),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 5),
('Premium', 'CVT', 4, '2.0L I4', 5),
('Base', 'Manual', 4, '1.4L I4', 6),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 6),
('Premium', 'CVT', 4, '2.0L I4', 6),
('Base', 'Manual', 4, '1.4L I4', 7),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 7),
('Premium', 'CVT', 4, '2.0L I4', 7),
('Base', 'Manual', 4, '1.4L I4', 8),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 8),
('Premium', 'CVT', 4, '2.0L I4', 8),
('Base', 'Manual', 4, '1.4L I4', 9),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 9),
('Premium', 'CVT', 4, '2.0L I4', 9),
('Base', 'Manual', 4, '1.4L I4', 10),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 10),
('Premium', 'CVT', 4, '2.0L I4', 10),
('Base', 'Manual', 4, '1.4L I4', 11),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 11),
('Premium', 'CVT', 4, '2.0L I4', 11),
('Base', 'Manual', 4, '1.4L I4', 12),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 12),
('Premium', 'CVT', 4, '2.0L I4', 12),
('Base', 'Manual', 4, '1.4L I4', 13),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 13),
('Premium', 'CVT', 4, '2.0L I4', 13),
('Base', 'Manual', 4, '1.4L I4', 14),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 14),
('Premium', 'CVT', 4, '2.0L I4', 14),
('Base', 'Manual', 4, '1.4L I4', 15),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 15),
('Premium', 'CVT', 4, '2.0L I4', 15),
('Base', 'Manual', 4, '1.4L I4', 16),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 16),
('Premium', 'CVT', 4, '2.0L I4', 16),
('Base', 'Manual', 4, '1.4L I4', 17),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 17),
('Premium', 'CVT', 4, '2.0L I4', 17),
('Base', 'Manual', 4, '1.4L I4', 18),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 18),
('Premium', 'CVT', 4, '2.0L I4', 18),
('Base', 'Manual', 4, '1.4L I4', 19),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 19),
('Premium', 'CVT', 4, '2.0L I4', 19),
('Base', 'Manual', 4, '1.4L I4', 20),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 20),
('Premium', 'CVT', 4, '2.0L I4', 20),
('Base', 'Manual', 4, '1.4L I4', 21),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 21),
('Premium', 'CVT', 4, '2.0L I4', 21),
('Base', 'Manual', 4, '1.4L I4', 22),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 22),
('Premium', 'CVT', 4, '2.0L I4', 22),
('Base', 'Manual', 4, '1.4L I4', 23),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 23),
('Premium', 'CVT', 4, '2.0L I4', 23),
('Base', 'Manual', 4, '1.4L I4', 24),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 24),
('Premium', 'CVT', 4, '2.0L I4', 24),
('Base', 'Manual', 4, '1.4L I4', 25),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 25),
('Premium', 'CVT', 4, '2.0L I4', 25),
('Base', 'Manual', 4, '1.4L I4', 26),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 26),
('Premium', 'CVT', 4, '2.0L I4', 26),
('Base', 'Manual', 4, '1.4L I4', 27),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 27),
('Premium', 'CVT', 4, '2.0L I4', 27),
('Base', 'Manual', 4, '1.4L I4', 28),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 28),
('Premium', 'CVT', 4, '2.0L I4', 28),
('Base', 'Manual', 4, '1.4L I4', 29),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 29),
('Premium', 'CVT', 4, '2.0L I4', 29),
('Base', 'Manual', 4, '1.4L I4', 30),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 30),
('Premium', 'CVT', 4, '2.0L I4', 30),
('Base', 'Manual', 4, '1.4L I4', 31),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 31),
('Premium', 'CVT', 4, '2.0L I4', 31),
('Base', 'Manual', 4, '1.4L I4', 32),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 32),
('Premium', 'CVT', 4, '2.0L I4', 32),
('Base', 'Manual', 4, '1.4L I4', 33),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 33),
('Premium', 'CVT', 4, '2.0L I4', 33),
('Base', 'Manual', 4, '1.4L I4', 34),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 34),
('Premium', 'CVT', 4, '2.0L I4', 34),
('Base', 'Manual', 4, '1.4L I4', 35),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 35),
('Premium', 'CVT', 4, '2.0L I4', 35),
('Base', 'Manual', 4, '1.4L I4', 36),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 36),
('Premium', 'CVT', 4, '2.0L I4', 36),
('Base', 'Manual', 4, '1.4L I4', 37),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 37),
('Premium', 'CVT', 4, '2.0L I4', 37),
('Base', 'Manual', 4, '1.4L I4', 38),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 38),
('Premium', 'CVT', 4, '2.0L I4', 38),
('Base', 'Manual', 4, '1.4L I4', 39),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 39),
('Premium', 'CVT', 4, '2.0L I4', 39),
('Base', 'Manual', 4, '1.4L I4', 40),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 40),
('Premium', 'CVT', 4, '2.0L I4', 40),
('Base', 'Manual', 4, '1.4L I4', 41),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 41),
('Premium', 'CVT', 4, '2.0L I4', 41),
('Base', 'Manual', 4, '1.4L I4', 42),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 42),
('Premium', 'CVT', 4, '2.0L I4', 42),
('Base', 'Manual', 4, '1.4L I4', 43),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 43),
('Premium', 'CVT', 4, '2.0L I4', 43),
('Base', 'Manual', 4, '1.4L I4', 44),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 44),
('Premium', 'CVT', 4, '2.0L I4', 44),
('Base', 'Manual', 4, '1.4L I4', 45),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 45),
('Premium', 'CVT', 4, '2.0L I4', 45),
('Base', 'Manual', 4, '1.4L I4', 46),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 46),
('Premium', 'CVT', 4, '2.0L I4', 46),
('Base', 'Manual', 4, '1.4L I4', 47),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 47),
('Premium', 'CVT', 4, '2.0L I4', 47),
('Base', 'Manual', 4, '1.4L I4', 48),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 48),
('Premium', 'CVT', 4, '2.0L I4', 48),
('Base', 'Manual', 4, '1.4L I4', 49),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 49),
('Premium', 'CVT', 4, '2.0L I4', 49),
('Base', 'Manual', 4, '1.4L I4', 50),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 50),
('Premium', 'CVT', 4, '2.0L I4', 50),
('Base', 'Manual', 4, '1.4L I4', 51),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 51),
('Premium', 'CVT', 4, '2.0L I4', 51),
('Base', 'Manual', 4, '1.4L I4', 52),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 52),
('Premium', 'CVT', 4, '2.0L I4', 52),
('Base', 'Manual', 4, '1.4L I4', 53),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 53),
('Premium', 'CVT', 4, '2.0L I4', 53),
('Base', 'Manual', 4, '1.4L I4', 54),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 54),
('Premium', 'CVT', 4, '2.0L I4', 54),
('Base', 'Manual', 4, '1.4L I4', 55),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 55),
('Premium', 'CVT', 4, '2.0L I4', 55),
('Base', 'Manual', 4, '1.4L I4', 56),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 56),
('Premium', 'CVT', 4, '2.0L I4', 56),
('Base', 'Manual', 4, '1.4L I4', 57),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 57),
('Premium', 'CVT', 4, '2.0L I4', 57),
('Base', 'Manual', 4, '1.4L I4', 58),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 58),
('Premium', 'CVT', 4, '2.0L I4', 58),
('Base', 'Manual', 4, '1.4L I4', 59),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 59),
('Premium', 'CVT', 4, '2.0L I4', 59),
('Base', 'Manual', 4, '1.4L I4', 60),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 60),
('Premium', 'CVT', 4, '2.0L I4', 60),
('Base', 'Manual', 4, '1.4L I4', 61),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 61),
('Premium', 'CVT', 4, '2.0L I4', 61),
('Base', 'Manual', 4, '1.4L I4', 62),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 62),
('Premium', 'CVT', 4, '2.0L I4', 62),
('Base', 'Manual', 4, '1.4L I4', 63),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 63),
('Premium', 'CVT', 4, '2.0L I4', 63),
('Base', 'Manual', 4, '1.4L I4', 64),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 64),
('Premium', 'CVT', 4, '2.0L I4', 64),
('Base', 'Manual', 4, '1.4L I4', 65),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 65),
('Premium', 'CVT', 4, '2.0L I4', 65),
('Base', 'Manual', 4, '1.4L I4', 66),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 66),
('Premium', 'CVT', 4, '2.0L I4', 66),
('Base', 'Manual', 4, '1.4L I4', 67),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 67),
('Premium', 'CVT', 4, '2.0L I4', 67),
('Base', 'Manual', 4, '1.4L I4', 68),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 68),
('Premium', 'CVT', 4, '2.0L I4', 68),
('Base', 'Manual', 4, '1.4L I4', 69),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 69),
('Premium', 'CVT', 4, '2.0L I4', 69),
('Base', 'Manual', 4, '1.4L I4', 70),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 70),
('Premium', 'CVT', 4, '2.0L I4', 70),
('Base', 'Manual', 4, '1.4L I4', 71),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 71),
('Premium', 'CVT', 4, '2.0L I4', 71),
('Base', 'Manual', 4, '1.4L I4', 72),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 72),
('Premium', 'CVT', 4, '2.0L I4', 72),
('Base', 'Manual', 4, '1.4L I4', 73),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 73),
('Premium', 'CVT', 4, '2.0L I4', 73),
('Base', 'Manual', 4, '1.4L I4', 74),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 74),
('Premium', 'CVT', 4, '2.0L I4', 74),
('Base', 'Manual', 4, '1.4L I4', 75),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 75),
('Premium', 'CVT', 4, '2.0L I4', 75),
('Base', 'Manual', 4, '1.4L I4', 76),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 76),
('Premium', 'CVT', 4, '2.0L I4', 76),
('Base', 'Manual', 4, '1.4L I4', 77),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 77),
('Premium', 'CVT', 4, '2.0L I4', 77),
('Base', 'Manual', 4, '1.4L I4', 78),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 78),
('Premium', 'CVT', 4, '2.0L I4', 78),
('Base', 'Manual', 4, '1.4L I4', 79),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 79),
('Premium', 'CVT', 4, '2.0L I4', 79),
('Base', 'Manual', 4, '1.4L I4', 80),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 80),
('Premium', 'CVT', 4, '2.0L I4', 80),
('Base', 'Manual', 4, '1.4L I4', 81),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 81),
('Premium', 'CVT', 4, '2.0L I4', 81),
('Base', 'Manual', 4, '1.4L I4', 82),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 82),
('Premium', 'CVT', 4, '2.0L I4', 82),
('Base', 'Manual', 4, '1.4L I4', 83),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 83),
('Premium', 'CVT', 4, '2.0L I4', 83),
('Base', 'Manual', 4, '1.4L I4', 84),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 84),
('Premium', 'CVT', 4, '2.0L I4', 84),
('Base', 'Manual', 4, '1.4L I4', 85),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 85),
('Premium', 'CVT', 4, '2.0L I4', 85),
('Base', 'Manual', 4, '1.4L I4', 86),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 86),
('Premium', 'CVT', 4, '2.0L I4', 86),
('Base', 'Manual', 4, '1.4L I4', 87),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 87),
('Premium', 'CVT', 4, '2.0L I4', 87),
('Base', 'Manual', 4, '1.4L I4', 88),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 88),
('Premium', 'CVT', 4, '2.0L I4', 88),
('Base', 'Manual', 4, '1.4L I4', 89),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 89),
('Premium', 'CVT', 4, '2.0L I4', 89),
('Base', 'Manual', 4, '1.4L I4', 90),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 90),
('Premium', 'CVT', 4, '2.0L I4', 90),
('Base', 'Manual', 4, '1.4L I4', 91),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 91),
('Premium', 'CVT', 4, '2.0L I4', 91),
('Base', 'Manual', 4, '1.4L I4', 92),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 92),
('Premium', 'CVT', 4, '2.0L I4', 92),
('Base', 'Manual', 4, '1.4L I4', 93),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 93),
('Premium', 'CVT', 4, '2.0L I4', 93),
('Base', 'Manual', 4, '1.4L I4', 94),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 94),
('Premium', 'CVT', 4, '2.0L I4', 94),
('Base', 'Manual', 4, '1.4L I4', 95),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 95),
('Premium', 'CVT', 4, '2.0L I4', 95),
('Base', 'Manual', 4, '1.4L I4', 96),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 96),
('Premium', 'CVT', 4, '2.0L I4', 96),
('Base', 'Manual', 4, '1.4L I4', 97),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 97),
('Premium', 'CVT', 4, '2.0L I4', 97),
('Base', 'Manual', 4, '1.4L I4', 98),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 98),
('Premium', 'CVT', 4, '2.0L I4', 98),
('Base', 'Manual', 4, '1.4L I4', 99),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 99),
('Premium', 'CVT', 4, '2.0L I4', 99),
('Base', 'Manual', 4, '1.4L I4', 100),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 100),
('Premium', 'CVT', 4, '2.0L I4', 100),
('Base', 'Manual', 4, '1.4L I4', 101),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 101),
('Premium', 'CVT', 4, '2.0L I4', 101),
('Base', 'Manual', 4, '1.4L I4', 102),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 102),
('Premium', 'CVT', 4, '2.0L I4', 102),
('Base', 'Manual', 4, '1.4L I4', 103),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 103),
('Premium', 'CVT', 4, '2.0L I4', 103),
('Base', 'Manual', 4, '1.4L I4', 104),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 104),
('Premium', 'CVT', 4, '2.0L I4', 104),
('Base', 'Manual', 4, '1.4L I4', 105),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 105),
('Premium', 'CVT', 4, '2.0L I4', 105),
('Base', 'Manual', 4, '1.4L I4', 106),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 106),
('Premium', 'CVT', 4, '2.0L I4', 106),
('Base', 'Manual', 4, '1.4L I4', 107),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 107),
('Premium', 'CVT', 4, '2.0L I4', 107),
('Base', 'Manual', 4, '1.4L I4', 108),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 108),
('Premium', 'CVT', 4, '2.0L I4', 108),
('Base', 'Manual', 4, '1.4L I4', 109),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 109),
('Premium', 'CVT', 4, '2.0L I4', 109),
('Base', 'Manual', 4, '1.4L I4', 110),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 110),
('Premium', 'CVT', 4, '2.0L I4', 110),
('Base', 'Manual', 4, '1.4L I4', 111),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 111),
('Premium', 'CVT', 4, '2.0L I4', 111),
('Base', 'Manual', 4, '1.4L I4', 112),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 112),
('Premium', 'CVT', 4, '2.0L I4', 112),
('Base', 'Manual', 4, '1.4L I4', 113),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 113),
('Premium', 'CVT', 4, '2.0L I4', 113),
('Base', 'Manual', 4, '1.4L I4', 114),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 114),
('Premium', 'CVT', 4, '2.0L I4', 114),
('Base', 'Manual', 4, '1.4L I4', 115),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 115),
('Premium', 'CVT', 4, '2.0L I4', 115),
('Base', 'Manual', 4, '1.4L I4', 116),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 116),
('Premium', 'CVT', 4, '2.0L I4', 116),
('Base', 'Manual', 4, '1.4L I4', 117),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 117),
('Premium', 'CVT', 4, '2.0L I4', 117),
('Base', 'Manual', 4, '1.4L I4', 118),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 118),
('Premium', 'CVT', 4, '2.0L I4', 118),
('Base', 'Manual', 4, '1.4L I4', 119),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 119),
('Premium', 'CVT', 4, '2.0L I4', 119),
('Base', 'Manual', 4, '1.4L I4', 120),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 120),
('Premium', 'CVT', 4, '2.0L I4', 120),
('Base', 'Manual', 4, '1.4L I4', 121),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 121),
('Premium', 'CVT', 4, '2.0L I4', 121),
('Base', 'Manual', 4, '1.4L I4', 122),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 122),
('Premium', 'CVT', 4, '2.0L I4', 122),
('Base', 'Manual', 4, '1.4L I4', 123),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 123),
('Premium', 'CVT', 4, '2.0L I4', 123),
('Base', 'Manual', 4, '1.4L I4', 124),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 124),
('Premium', 'CVT', 4, '2.0L I4', 124),
('Base', 'Manual', 4, '1.4L I4', 125),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 125),
('Premium', 'CVT', 4, '2.0L I4', 125),
('Base', 'Manual', 4, '1.4L I4', 126),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 126),
('Premium', 'CVT', 4, '2.0L I4', 126),
('Base', 'Manual', 4, '1.4L I4', 127),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 127),
('Premium', 'CVT', 4, '2.0L I4', 127),
('Base', 'Manual', 4, '1.4L I4', 128),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 128),
('Premium', 'CVT', 4, '2.0L I4', 128),
('Base', 'Manual', 4, '1.4L I4', 129),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 129),
('Premium', 'CVT', 4, '2.0L I4', 129),
('Base', 'Manual', 4, '1.4L I4', 130),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 130),
('Premium', 'CVT', 4, '2.0L I4', 130),
('Base', 'Manual', 4, '1.4L I4', 131),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 131),
('Premium', 'CVT', 4, '2.0L I4', 131),
('Base', 'Manual', 4, '1.4L I4', 132),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 132),
('Premium', 'CVT', 4, '2.0L I4', 132),
('Base', 'Manual', 4, '1.4L I4', 133),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 133),
('Premium', 'CVT', 4, '2.0L I4', 133),
('Base', 'Manual', 4, '1.4L I4', 134),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 134),
('Premium', 'CVT', 4, '2.0L I4', 134),
('Base', 'Manual', 4, '1.4L I4', 135),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 135),
('Premium', 'CVT', 4, '2.0L I4', 135),
('Base', 'Manual', 4, '1.4L I4', 136),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 136),
('Premium', 'CVT', 4, '2.0L I4', 136),
('Base', 'Manual', 4, '1.4L I4', 137),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 137),
('Premium', 'CVT', 4, '2.0L I4', 137),
('Base', 'Manual', 4, '1.4L I4', 138),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 138),
('Premium', 'CVT', 4, '2.0L I4', 138),
('Base', 'Manual', 4, '1.4L I4', 139),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 139),
('Premium', 'CVT', 4, '2.0L I4', 139),
('Base', 'Manual', 4, '1.4L I4', 140),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 140),
('Premium', 'CVT', 4, '2.0L I4', 140),
('Base', 'Manual', 4, '1.4L I4', 141),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 141),
('Premium', 'CVT', 4, '2.0L I4', 141),
('Base', 'Manual', 4, '1.4L I4', 142),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 142),
('Premium', 'CVT', 4, '2.0L I4', 142),
('Base', 'Manual', 4, '1.4L I4', 143),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 143),
('Premium', 'CVT', 4, '2.0L I4', 143),
('Base', 'Manual', 4, '1.4L I4', 144),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 144),
('Premium', 'CVT', 4, '2.0L I4', 144),
('Base', 'Manual', 4, '1.4L I4', 145),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 145),
('Premium', 'CVT', 4, '2.0L I4', 145),
('Base', 'Manual', 4, '1.4L I4', 146),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 146),
('Premium', 'CVT', 4, '2.0L I4', 146),
('Base', 'Manual', 4, '1.4L I4', 147),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 147),
('Premium', 'CVT', 4, '2.0L I4', 147),
('Base', 'Manual', 4, '1.4L I4', 148),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 148),
('Premium', 'CVT', 4, '2.0L I4', 148),
('Base', 'Manual', 4, '1.4L I4', 149),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 149),
('Premium', 'CVT', 4, '2.0L I4', 149),
('Base', 'Manual', 4, '1.4L I4', 150),
('Sport', 'Automática', 4, '1.6L I4 Turbo', 150),
('Premium', 'CVT', 4, '2.0L I4', 150);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (15000.50, 'CP-20250301', '2025-03-01', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (8450.75, 'CP-20250315', '2025-03-15', 1, 2);

INSERT INTO AutoImperial.dbo.CompraProveedor (montoTotal, folio, fechaCompra, idAdministrador, idProveedor)
VALUES (23000.00, 'CP-20250320', '2025-03-20', 1, 2);



  -- Inserciones para pruebas ClientRepositoryTest