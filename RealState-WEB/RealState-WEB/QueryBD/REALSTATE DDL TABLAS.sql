-- CREATE DATABASE REALSTATE_Project;	

USE REALSTATE_Project;

----------------------------------------------------------------------- DDL TABLAS -----------------------------------------------------------------------
----------------------------------------------------------------------- PAISES 
-- DROP TABLE PAISES
CREATE TABLE PAISES (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NOMBRE NVARCHAR(50) NOT NULL
);

----------------------------------------------------------------------- PROVINCIAS 
-- DROP TABLE PROVINCIAS
CREATE TABLE PROVINCIAS (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NOMBRE NVARCHAR(50) NOT NULL
);

----------------------------------------------------------------------- USUARIO_DIRECCIONES
-- DROP TABLE USUARIO_DIRECCIONES
CREATE TABLE USUARIO_DIRECCIONES (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    DIRECCION_EXACTA NVARCHAR(255) NOT NULL,
	ID_PAIS_FK BIGINT NOT NULL,
	ID_PROVINCIA_FK BIGINT NOT NULL,
	CANTON NVARCHAR(100) NOT NULL,
	DISTRITO NVARCHAR(100) NOT NULL
	CONSTRAINT FK_DIRECCIONES_USUARIOS_PAIS FOREIGN KEY (ID_PAIS_FK) REFERENCES PAISES (ID),
	CONSTRAINT FK_DIRECCIONES_USUARIOS_PROVINCIA FOREIGN KEY (ID_PROVINCIA_FK) REFERENCES PROVINCIAS (ID),
);

----------------------------------------------------------------------- PROPIEDAD_DIRECCIONES
-- DROP TABLE PROPIEDAD_DIRECCIONES
CREATE TABLE PROPIEDAD_DIRECCIONES (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    DIRECCION_EXACTA NVARCHAR(255) NOT NULL,
	GMAPS_LINK NVARCHAR(MAX) NOT NULL,
	ID_PAIS_FK BIGINT NOT NULL,
	ID_PROVINCIA_FK BIGINT NOT NULL,
	CANTON NVARCHAR(100) NOT NULL,
	DISTRITO NVARCHAR(100) NOT NULL
	CONSTRAINT FK_DIRECCIONES_PROPIEDADES_PAIS FOREIGN KEY (ID_PAIS_FK) REFERENCES PAISES (ID),
	CONSTRAINT FK_DIRECCIONES_PROPIEDADES_PROVINCIA FOREIGN KEY (ID_PROVINCIA_FK) REFERENCES PROVINCIAS (ID),
);

----------------------------------------------------------------------- USUARIO_ROLES 
-- DROP TABLE USUARIO_ROLES
CREATE TABLE USUARIO_ROLES (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NOMBRE NVARCHAR(50) NOT NULL,
	DESCRIPCION NVARCHAR(255) NOT NULL
);

----------------------------------------------------------------------- USUARIOS 
-- DROP TABLE USUARIOS
CREATE TABLE USUARIOS (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NOMBRE NVARCHAR(100) NOT NULL,
	APELLIDOS NVARCHAR(100) NOT NULL,
	EMAIL NVARCHAR(100) NOT NULL,
	TELEFONO NVARCHAR(100) NOT NULL,
	CONTRASENNA NVARCHAR(100) NOT NULL,
	ESTADO BIT DEFAULT 1 NOT NULL,
	ID_ROL_FK BIGINT DEFAULT 2 NOT NULL,
	ID_DIRECCION_FK BIGINT NOT NULL,
	CONSTRAINT FK_USUARIOS_ROLES FOREIGN KEY (ID_ROL_FK) REFERENCES USUARIO_ROLES(ID),
	CONSTRAINT FK_USUARIOS_DIRECCIONES_USUARIOS FOREIGN KEY (ID_DIRECCION_FK) REFERENCES USUARIO_DIRECCIONES(ID)
);

----------------------------------------------------------------------- PROPIEDAD_TIPOS 
-- DROP TABLE PROPIEDAD_TIPOS
CREATE TABLE PROPIEDAD_TIPOS (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NOMBRE NVARCHAR(50) NOT NULL,
	DESCRIPCION NVARCHAR(255) NOT NULL
);

----------------------------------------------------------------------- PROPIEDAD_DETALLES
-- DROP TABLE PROPIEDAD_DETALLES 
CREATE TABLE PROPIEDAD_DETALLES (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    CANTIDAD_BANNOS BIGINT NOT NULL,
	CANTIDAD_CUARTOS BIGINT NOT NULL,
	CANTIDAD_PARQUEO BIGINT NOT NULL,
	CANTIDAD_METROS2 BIGINT NOT NULL
);

----------------------------------------------------------------------- PROPIEDADES
-- DROP TABLE PROPIEDADES 
CREATE TABLE PROPIEDADES (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NOMBRE NVARCHAR(100) NOT NULL,
	DESCRIPCION NVARCHAR(100) NOT NULL,
	PRECIO DECIMAL(12,2) NOT NULL,
	ESTADO BIT DEFAULT 1 NOT NULL,
	ID_TIPO_FK BIGINT NOT NULL,
	ID_USUARIO_FK BIGINT NOT NULL,
	ID_DETALLE_FK BIGINT NOT NULL,
	ID_DIRECCION_FK BIGINT NOT NULL,
	CONSTRAINT FK_PROPIEDADES_TIPOS FOREIGN KEY (ID_TIPO_FK) REFERENCES PROPIEDAD_TIPOS(ID),
	CONSTRAINT FK_PROPIEDADES_USUARIOS FOREIGN KEY (ID_USUARIO_FK) REFERENCES USUARIOS(ID),
	CONSTRAINT FK_PROPIEDADES_DETALLE FOREIGN KEY (ID_DETALLE_FK) REFERENCES PROPIEDAD_DETALLES(ID),
	CONSTRAINT FK_PROPIEDADES_DIRECCION FOREIGN KEY (ID_DIRECCION_FK) REFERENCES PROPIEDAD_DIRECCIONES(ID)
);

----------------------------------------------------------------------- PROPIEDAD_IMAGENES
-- DROP TABLE PROPIEDAD_IMAGENES 
CREATE TABLE PROPIEDAD_IMAGENES (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    IMAGEN NVARCHAR(200),
	ID_PROPIEDAD_FK BIGINT NOT NULL,
	CONSTRAINT FK_IMAGENES_PROPIEDAD FOREIGN KEY (ID_PROPIEDAD_FK) REFERENCES PROPIEDADES(ID)
);

----------------------------------------------------------------------- BITACORAS_USUARIOS 
-- DROP TABLE BITACORAS
CREATE TABLE BITACORAS(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	FECHA_HORA DATETIME DEFAULT GETDATE() NOT NULL,
	ORIGEN NVARCHAR(100) NOT NULL,
	MENSAJE_ERROR NVARCHAR(max) NOT NULL,
	ID_USUARIO_FK BIGINT,
	CONSTRAINT FK_BITACORAS_USUARIOS FOREIGN KEY (ID_USUARIO_FK) REFERENCES USUARIOS(ID)
);

----------------------------------------------------------------------- PROPIEDADES_CITAS
-- DROP TABLE PROPIEDADES_CITAS
CREATE TABLE PROPIEDADES_CITAS(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	FECHA_HORA DATETIME NOT NULL,
	ID_PROPIEDAD BIGINT NOT NULL,
	ID_USUARIO BIGINT NOT NULL,
	CONSTRAINT FK_CITAS_PROPIEDAD FOREIGN KEY (ID_PROPIEDAD) REFERENCES PROPIEDADES(ID),
	CONSTRAINT FK_CITAS_USUARIOS FOREIGN KEY (ID_USUARIO) REFERENCES USUARIOS(ID)
);