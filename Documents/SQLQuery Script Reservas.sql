use Reservas;

/* Crear tabla usuario*/
DROP TABLE IF EXISTS [dbo].[Usuario];

CREATE TABLE [dbo].[Usuario](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](200) NULL,
	IsAdmin bit NOT NULL DEFAULT 0,
	[Estado] [bit] NOT NULL DEFAULT 1,
	CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED (Id)
) ON [PRIMARY]
GO

INSERT INTO Usuario(Nombre, Password, IsAdmin, Estado)
    VALUES ('admin', '123456', 1, 1);
INSERT INTO Usuario(Nombre, Password, IsAdmin, Estado)
    VALUES ('cliente', '123456', 0, 1);
--select * from Usuario;


/* Crear tabla cliente*/
DROP TABLE IF EXISTS [dbo].[Cliente];

CREATE TABLE [Cliente] (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(250) NOT NULL,
    Email NVARCHAR(250) NULL,
    Telefono NVARCHAR(20) NULL,
    FechaCreacion DATETIME NOT NULL,
    Usuario INT NOT NULL,
	Estado bit NOT NULL DEFAULT 1,
	CONSTRAINT PK_Cliente PRIMARY KEY CLUSTERED (Id),
    FOREIGN KEY (Usuario) REFERENCES Usuario(Id)
);

-- 2024-06-27 16:52:37.563
INSERT INTO Cliente (Nombre, Email, FechaCreacion, Usuario, Estado)
    VALUES ('Ricardo', 'ricardo@gmail.com', '2024-07-10 06:52:37.563', 1, 1);
INSERT INTO Cliente (Nombre, Email, FechaCreacion, Usuario, Estado)
    VALUES ('Juan', 'juan@gmail.com', '2024-07-10 06:52:37.563', 2, 1);

/* Crear tabla [TipoServicio]*/
DROP TABLE IF EXISTS [dbo].[TipoServicio];

CREATE TABLE [TipoServicio] (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(250) NOT NULL,
	IsHabitacion bit NOT NULL DEFAULT 0,
	IsRestaurante bit NOT NULL DEFAULT 0,
    FechaCreacion DATETIME NOT NULL,
	Estado bit NOT NULL DEFAULT 1,
	CONSTRAINT PK_TipoServicio PRIMARY KEY CLUSTERED (Id)
);


INSERT INTO TipoServicio (Nombre, IsHabitacion, IsRestaurante, FechaCreacion, Estado)
    VALUES ('Habitación', 1, 0, '2024-07-10 06:52:37.563', 1);

INSERT INTO TipoServicio (Nombre, IsHabitacion, IsRestaurante, FechaCreacion, Estado)
    VALUES ('Comidas', 0, 0, '2024-07-10 06:52:37.563', 1);

INSERT INTO TipoServicio (Nombre, IsHabitacion, IsRestaurante, FechaCreacion, Estado)
    VALUES ('Bebidas', 0, 0, '2024-07-10 06:52:37.563', 1);

INSERT INTO TipoServicio (Nombre, IsHabitacion, IsRestaurante, FechaCreacion, Estado)
    VALUES ('Restaurante', 0, 1, '2024-07-10 06:52:37.563', 1);



/* Crear tabla Servicio*/
DROP TABLE IF EXISTS [dbo].[Servicio];

CREATE TABLE [Servicio] (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(250) NOT NULL,
    Descripcion NVARCHAR(250) NULL,
    Precio DECIMAL(10,2) NULL,
    FechaCreacion DATETIME NOT NULL,
    TipoServicio INT NOT NULL,
	Estado bit NOT NULL DEFAULT 1,
	CONSTRAINT PK_Servicio PRIMARY KEY CLUSTERED (Id),
    FOREIGN KEY (TipoServicio) REFERENCES TipoServicio(Id)
);

INSERT INTO Servicio(Nombre, Descripcion, Precio, FechaCreacion, TipoServicio, Estado)
    VALUES ('Habitacion', 'habitacion', 0, '2024-07-10 06:52:37.563', 1, 1);

INSERT INTO Servicio(Nombre, Descripcion, Precio, FechaCreacion, TipoServicio, Estado)
    VALUES ('Desayuno basic', 'Desayuno basico', 10000, '2024-07-10 06:52:37.563', 2, 1);

INSERT INTO Servicio(Nombre, Descripcion, Precio, FechaCreacion, TipoServicio, Estado)
    VALUES ('Cena basic', 'Cena basico', 15000, '2024-07-10 06:52:37.563', 2, 1);

INSERT INTO Servicio(Nombre, Descripcion, Precio, FechaCreacion, TipoServicio, Estado)
    VALUES ('Coca-Cola', 'Coca-Cola personal', 2000, '2024-07-10 06:52:37.563', 3, 1);

INSERT INTO Servicio(Nombre, Descripcion, Precio, FechaCreacion, TipoServicio, Estado)
    VALUES ('Cerveza Aguila', 'Cerveza personal', 3000, '2024-07-10 06:52:37.563', 3, 1);

/* Crear tabla Reserva*/
DROP TABLE IF EXISTS Reserva;
CREATE TABLE Reserva (
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NULL,
    Cliente INT,
	FechaCreacion DATETIME NOT NULL,
    Estado bit NOT NULL DEFAULT 1,
    FOREIGN KEY (Cliente) REFERENCES Cliente(Id)
);



/* Crear tabla Habitacion*/
DROP TABLE IF EXISTS Habitacion;
CREATE TABLE Habitacion (
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NumeroCuarto VARCHAR(10) NOT NULL,
    NumeroCamas INT NOT NULL,
    NumeroPersonas INT NOT NULL,
    NumeroBanos INT  NULL,
	Precio DECIMAL(10,2) NOT NULL,
	FechaCreacion DATETIME NOT NULL,
    Estado bit NOT NULL DEFAULT 1
);

INSERT INTO Habitacion(NumeroCuarto, NumeroCamas, NumeroPersonas, Precio, FechaCreacion, Estado)
    VALUES ('101', 3, 4, 140000,'2024-07-10 06:52:37.563', 1);
INSERT INTO Habitacion(NumeroCuarto, NumeroCamas, NumeroPersonas, Precio, FechaCreacion, Estado)
    VALUES ('102', 2, 3, 130000,'2024-07-10 06:52:37.563', 1);
INSERT INTO Habitacion(NumeroCuarto, NumeroCamas, NumeroPersonas, Precio, FechaCreacion, Estado)
    VALUES ('103', 1, 2, 120000,'2024-07-10 06:52:37.563', 1);

/* Crear tabla Mesa*/
DROP TABLE IF EXISTS Mesa;
CREATE TABLE Mesa (
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NumeroMesa VARCHAR(10) NOT NULL,
    NumeroSillas INT NOT NULL,
	FechaCreacion DATETIME NOT NULL,
    Estado bit NOT NULL DEFAULT 1
);
INSERT INTO Mesa(NumeroMesa, NumeroSillas, FechaCreacion, Estado)
    VALUES ('01', 4,'2024-07-10 06:52:37.563', 1);
INSERT INTO Mesa(NumeroMesa, NumeroSillas, FechaCreacion, Estado)
    VALUES ('02', 4,'2024-07-10 06:52:37.563', 1);

/* Crear tabla ServicioReservado*/
DROP TABLE IF EXISTS ServicioReservado;
CREATE TABLE ServicioReservado (
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Reserva INT,
    Servicio INT,
    Habitacion INT,
    Mesa INT,
    PrecioReal DECIMAL(10,2) NULL,
	Estado bit NOT NULL DEFAULT 1
    FOREIGN KEY (Reserva) REFERENCES Reserva(Id),
    FOREIGN KEY (Servicio) REFERENCES Servicio(Id),
    FOREIGN KEY (Habitacion) REFERENCES Habitacion(Id),
    FOREIGN KEY (Mesa) REFERENCES Mesa(Id)
);


