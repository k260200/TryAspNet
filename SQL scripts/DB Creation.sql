USE [UsersAndRewards]
GO
/****** Object:  Table [dbo].[Rewards]    Script Date: 23.04.2022 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rewards](
	[RewardID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK__Rewards__82501599BB7DEA62] PRIMARY KEY CLUSTERED 
(
	[RewardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.04.2022 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[BirthDate] [date] NOT NULL,
 CONSTRAINT [PK__Users__1788CCAC7CD15001] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersAndRewards]    Script Date: 23.04.2022 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersAndRewards](
	[UserID] [int] NOT NULL,
	[RewardID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RewardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Rewards] ON 

INSERT [dbo].[Rewards] ([RewardID], [Title], [Description]) VALUES (5, N'Хы!', N'только для пользователей, написанных кириллицей')
INSERT [dbo].[Rewards] ([RewardID], [Title], [Description]) VALUES (6, N'jgvjh', N'')
INSERT [dbo].[Rewards] ([RewardID], [Title], [Description]) VALUES (8, N'dfvbdbdb dfvdsfv!', N'Here is so many text djvndsfvdsvfslvdfsvfjvndsfvndsfdsf')
INSERT [dbo].[Rewards] ([RewardID], [Title], [Description]) VALUES (10, N'new cool reward', N'dfvdsfbdbndslbfnbsnbgbflsjnsdkv')
SET IDENTITY_INSERT [dbo].[Rewards] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [BirthDate]) VALUES (5, N'dbfbgf', N'аdfvdv-------------', CAST(N'2020-12-31' AS Date))
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [BirthDate]) VALUES (6, N'sdfvdsjf', N'dfvdsfvdabv', CAST(N'2017-12-06' AS Date))
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [BirthDate]) VALUES (9, N'Русский я теперь', N'ыы', CAST(N'2020-12-08' AS Date))
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [BirthDate]) VALUES (11, N'New', N'Юзверь dfvsdv', CAST(N'2018-12-08' AS Date))
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [BirthDate]) VALUES (12, N'Кто', N'Это', CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [BirthDate]) VALUES (15, N'ddfvd', N'fvdsfv', CAST(N'2020-12-09' AS Date))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (5, 5)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (5, 6)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (9, 8)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (9, 10)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (11, 5)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (11, 8)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (12, 5)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (12, 6)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (12, 8)
INSERT [dbo].[UsersAndRewards] ([UserID], [RewardID]) VALUES (15, 10)
GO
ALTER TABLE [dbo].[UsersAndRewards]  WITH CHECK ADD  CONSTRAINT [FK__UsersAndR__Rewar__412EB0B6] FOREIGN KEY([RewardID])
REFERENCES [dbo].[Rewards] ([RewardID])
GO
ALTER TABLE [dbo].[UsersAndRewards] CHECK CONSTRAINT [FK__UsersAndR__Rewar__412EB0B6]
GO
ALTER TABLE [dbo].[UsersAndRewards]  WITH CHECK ADD  CONSTRAINT [FK__UsersAndR__UserI__403A8C7D] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UsersAndRewards] CHECK CONSTRAINT [FK__UsersAndR__UserI__403A8C7D]
GO
