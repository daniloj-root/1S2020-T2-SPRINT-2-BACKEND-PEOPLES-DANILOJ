CREATE DATABASE T_Peoples
GO
USE T_Peoples
GO

CREATE TABLE TiposUsuario (
IdTipoUsuario INT PRIMARY KEY IDENTITY,
Descricao VARCHAR (100)
)

CREATE TABLE Usuarios (
	ID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100),
	Sobrenome VARCHAR(100),
	Senha VARCHAR(4000),
	IdTipoUsuario INT FOREIGN KEY REFERENCES TiposUsuario(IdTipoUsuario)
)



