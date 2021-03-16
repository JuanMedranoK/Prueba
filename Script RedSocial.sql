Create database RedSocial
go
USE [RedSocial]
GO

CREATE TABLE [dbo].[Tbl_Comments](
	[Id] [int] IDENTITY(1,1) primary key NOT NULL,
	[PostId] [int] NULL,
	[Comment] [varchar](max) NULL,
	[UserName] [varchar](100) NULL,
	[FriendId] [varchar](100) NULL,
)

CREATE TABLE [dbo].[Tbl_Friends](
	[Id] [int] IDENTITY(1,1) primary key NOT NULL,
	[UserId] [varchar](100) NULL,
	[FriendId] [varchar](100) NULL,
)

CREATE TABLE [dbo].[Tbl_Post](
	[Id] [int] IDENTITY(1,1) primary key NOT NULL,
	[Content] [varchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[UserName] [varchar](100) NULL,
)

CREATE TABLE [dbo].[Tbl_Users](
	[Id] [int] IDENTITY(1,1) primary key NOT NULL,
	[Name] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Photo] [varchar](max) NULL,
	[Mail] [varchar](150) NULL,
	[Phone] [varchar](20) NULL,
)

CREATE TABLE [dbo].[Tbl_Events](
	[Id] [int] IDENTITY(1,1) primary key NOT NULL,
	[Name] [varchar](100) NULL,
	[Place] [varchar](100) NULL,
	[CreationDate] [Date] NULL,
	[State] [int] NULL,
	[PeopleQuantity] [int] NULL,
	[UserName] [varchar](100) NULL,
)

CREATE TABLE [dbo].[Tbl_Events_Invitation](
	[Id] [int] IDENTITY(1,1) primary key NOT NULL,
	[EventId] [int] NULL,
	[UserName] [varchar](100) NULL,
	[Accept] [int] NULL,
	CONSTRAINT fk_EventId FOREIGN KEY(EventId) 
REFERENCES Tbl_Events(Id) ON DELETE CASCADE ON UPDATE CASCADE
)

CREATE TABLE [dbo].[Sesion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Tbl_PostImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NULL,
	[UserName] [varchar](100) NULL,
	[PhotoPublish] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



