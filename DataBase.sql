USE [master]
GO

CREATE DATABASE [KN_DB]
GO
USE [KN_DB]
GO
CREATE TABLE [dbo].[tUsuario](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Contrasenna] [varchar](15) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[CorreoElectronico] [varchar](100) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_tUsuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tUsuario] ON 
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado]) VALUES (7, N'800600076', N'123', N'TORRES FLORES ANA ARGELIA', N'ana@gmail.com', 1)
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado]) VALUES (8, N'116700557', N'00557', N'FAJARDO TORRES MARIA FERNANDA', N'mfajardo00557@ufife.ac.cr', 1)
GO
SET IDENTITY_INSERT [dbo].[tUsuario] OFF
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_tUsuario_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

GO
CREATE PROCEDURE [dbo].[IniciarSesion] 
	@CorreoElectronico varchar(100),
	@Contrasenna varchar(15)

AS
BEGIN
	SELECT Consecutivo
		   Identificacion,
		   Nombre
	FROM tUsuario
	WHERE CorreoElectronico = @CorreoElectronico
	AND Contrasenna = @Contrasenna
	AND Estado = 1
END
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario] 
	@Identificacion varchar(15),
	@Contrasenna varchar(15),
	@Nombre varchar(200),
	@CorreoElectronico varchar(100)

AS
BEGIN
	INSERT INTO dbo.tUsuario (Identificacion, Contrasenna, Nombre, CorreoElectronico, Estado)
	VALUES (@Identificacion, @Contrasenna, @Nombre, @CorreoElectronico, 1)
END
GO