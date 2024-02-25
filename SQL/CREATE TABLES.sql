CREATE TABLE [File] (
	FileId int IDENTITY(1,1) PRIMARY KEY,
	[FileName] varchar(50) NOT NULL,
	FileType varchar(20) NOT NULL,
	FilePath varchar(100) NOT NULL
);

CREATE TABLE [User] (
	UserId int IDENTITY(1,1) PRIMARY KEY,
	[Login] varchar(50) NOT NULL UNIQUE,
	[Password] varchar(100) NOT NULL
	Email varchar(100) NOT NULL UNIQUE,
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	RegisterDate datetime NOT NULL,
	IsActive bit NOT NULL,
	AvatarImage int FOREIGN KEY REFERENCES [File](FileId)
);

CREATE TABLE [Friend] (
	FriendId int IDENTITY(1,1) PRIMARY KEY,
	UserId1 int NOT NULL FOREIGN KEY REFERENCES [User](UserId),
	UserId2 int NOT NULL FOREIGN KEY REFERENCES [User](UserId),
	AcceptDate datetime,
	IsConfirmed bit,
	UNIQUE (UserId1, UserId2)
)