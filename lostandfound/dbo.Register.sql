CREATE TABLE [dbo].[Register] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Fname]    VARCHAR (50) NOT NULL,
    [Regno]    VARCHAR (50) NOT NULL,
    [Email]    VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Regno])
);

