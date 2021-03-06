USE [UsersAndRewards]
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_AddReward]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Добавить награду
CREATE PROCEDURE [dbo].[spUsersAndRewards_AddReward](
	@Title nvarchar(50),
	@Description nvarchar(150))
AS
	INSERT INTO Rewards(Title, Description)
	VALUES(@Title, @Description)

	SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_AddUser]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Добавить пользователя
CREATE PROCEDURE [dbo].[spUsersAndRewards_AddUser](
	@FirstName nvarchar(max),
	@LastName nvarchar(max),
	@BirthDate datetime)
AS
	INSERT INTO Users(FirstName, LastName, BirthDate)
	VALUES(@FirstName, @LastName, @BirthDate)
	
	SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_AwardUser]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Наградить пользователя наградой
CREATE PROCEDURE [dbo].[spUsersAndRewards_AwardUser](@UserID int, @RewardID int)
AS
BEGIN
	INSERT INTO UsersAndRewards(UserID, RewardID)
	VALUES(@UserID, @RewardID)
END
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_DeleteReward]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Удалить пользователя
CREATE PROCEDURE [dbo].[spUsersAndRewards_DeleteReward](@RewardID int)
AS
BEGIN
	-- Удаляем всех награждённых пользователей
	DELETE FROM UsersAndRewards
	WHERE RewardID = @RewardID
	-- Удаляем саму награду
	DELETE FROM Rewards
	WHERE RewardID = @RewardID
END
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_DeleteUser]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Удалить пользователя
CREATE PROCEDURE [dbo].[spUsersAndRewards_DeleteUser](@UserID int)
AS
BEGIN
	-- Удаляем все награды пользователя
	DELETE FROM UsersAndRewards
	WHERE UserID = @UserID
	-- Удаляем самого пользователя
	DELETE FROM Users
	WHERE UserID = @UserID
END
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_EditReward]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Редактировать награду
CREATE PROCEDURE [dbo].[spUsersAndRewards_EditReward](
	@RewardID int,
	@Title nvarchar(50),
	@Description nvarchar(150))
AS
BEGIN
	UPDATE Rewards
	SET Title = @Title, Description = @Description
	WHERE RewardID = @RewardID
END
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_EditUser]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Редактировать пользователя
CREATE PROCEDURE [dbo].[spUsersAndRewards_EditUser](
	@UserID int,
	@FirstName nvarchar(max),
	@LastName nvarchar(max),
	@BirthDate datetime)
AS
BEGIN
	UPDATE Users
	SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate
	WHERE UserID = @UserID
END
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_GetAllAwardedUsers]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Вывести всех награждённых пользователей
CREATE PROCEDURE [dbo].[spUsersAndRewards_GetAllAwardedUsers](@RewardID int)
AS
BEGIN
	SELECT Users.UserID, FirstName, LastName, BirthDate
	FROM UsersAndRewards JOIN Users ON UsersAndRewards.UserID = Users.UserID
	WHERE RewardID = @RewardID
END
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_GetAllUserRewards]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Вывести все награды пользователя
CREATE PROCEDURE [dbo].[spUsersAndRewards_GetAllUserRewards](@UserID int)
AS
BEGIN
	SELECT Rewards.RewardID, Title, Description
	FROM UsersAndRewards JOIN Rewards ON UsersAndRewards.RewardID = Rewards.RewardID
	WHERE UserID = @UserID
END
GO
/****** Object:  StoredProcedure [dbo].[spUsersAndRewards_TakeAwayRewardFromUser]    Script Date: 23.04.2022 11:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Забрать награду у пользователя
CREATE PROCEDURE [dbo].[spUsersAndRewards_TakeAwayRewardFromUser](@UserID int, @RewardID int)
AS
BEGIN
	DELETE FROM UsersAndRewards
	WHERE UserID = @UserID AND RewardID = @RewardID
END
GO
