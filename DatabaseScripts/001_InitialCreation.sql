USE [fdevscom_api]
GO
/****** Object:  Table [fdevscom_api].[APIKeys]    Script Date: 1/4/2017 10:28:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [fdevscom_api].[APIKeys](
	[APIKeyId] [int] IDENTITY(1,1) NOT NULL,
	[APIKey] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_APIKey] PRIMARY KEY CLUSTERED 
(
	[APIKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [fdevscom_api].[ToDoItemDetails]    Script Date: 1/4/2017 10:28:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [fdevscom_api].[ToDoItemDetails](
	[ToDoItemDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ToDoItemId] [int] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateDue] [smalldatetime] NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_ToDoItemDetails] PRIMARY KEY CLUSTERED 
(
	[ToDoItemDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [fdevscom_api].[ToDoItems]    Script Date: 1/4/2017 10:28:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [fdevscom_api].[ToDoItems](
	[ToDoItemId] [int] IDENTITY(1,1) NOT NULL,
	[ToDoItem] [nvarchar](50) NOT NULL,
	[IsCompleted] [bit] NULL,
	[IsArchived] [bit] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ToDoItems] PRIMARY KEY CLUSTERED 
(
	[ToDoItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [fdevscom_api].[Users]    Script Date: 1/4/2017 10:28:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [fdevscom_api].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
