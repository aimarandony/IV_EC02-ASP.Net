CREATE DATABASE SEMANA09;
USE SEMANA09;

CREATE TABLE DISTRITO(
 iddis int primary key,
 nombredis varchar(255) UNIQUE
);

CREATE TABLE CONTRIBUYENTE(
 dnicont char(8) primary key,
 nomcont varchar(255) not null,
 apecont varchar(255) not null,
 dircont varchar(255) not null,
 iddis int references DISTRITO
);


INSERT INTO DISTRITO VALUES
(1,'DISTRITO01'),(2,'DISTRITO02'),(3,'DISTRITO03'),
(4,'DISTRITO04'),(5,'DISTRITO05'),(6,'DISTRITO06'),
(7,'DISTRITO07'),(8,'DISTRITO08'),(9,'DISTRITO09');

GO

CREATE PROC usp_guardar_contribuyente
@dni char(8),
@nombre varchar(255),
@ape varchar(255),
@dir varchar(255),
@dis int
AS
MERGE INTO CONTRIBUYENTE AS TARGET
USING (SELECT @dni,@nombre,@ape,@dir,@dis) AS SOURCE(dni,nombre,ape,dir,dis)
ON TARGET.dnicont = SOURCE.dni
WHEN MATCHED THEN
	UPDATE SET target.nomcont = source.nombre, target.apecont = source.ape,
			   target.dircont = source.dir, target.iddis = source.dis
WHEN NOT MATCHED THEN
	INSERT VALUES(source.dni,source.nombre,source.ape,source.dir,source.dis);
GO

usp_guardar_contribuyente '12345671','NOMBRE01','APELLIDO01','DIRECCION01',2;
GO

CREATE PROC usp_contribuyentes
AS
SELECT * FROM CONTRIBUYENTE;
GO

usp_contribuyentes;
