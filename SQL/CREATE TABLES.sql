CREATE TABLE [File] (
	FileId int IDENTITY(1,1) PRIMARY KEY,
	FileName varchar(50) NOT NULL,
	FileType varchar(20) NOT NULL,
	FilePath varchar(100) NOT NULL
);

CREATE TABLE [User] (
	UserId int IDENTITY(1,1) PRIMARY KEY,
	Login varchar(50) NOT NULL UNIQUE,
	Password varchar(100) NOT NULL,
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

CREATE TABLE [VisibilityStatus] (
	Id int PRIMARY KEY,
	Status varchar(30) NOT NULL UNIQUE
)

INSERT INTO [VisibilityStatus] ([Id], [Status])
	VALUES (0, 'ForFriends'), (1, 'Private'), (2, 'Public')

CREATE TABLE [wishlist].[WishStatus] (
	Id int PRIMARY KEY,
	Status varchar(30) NOT NULL UNIQUE
)

INSERT INTO [wishlist].[WishStatus] ([Id], [Status])
	VALUES (0, 'Open'), (1, 'InProgress'), (2, 'Filled'), (3, 'Done'), (4, 'Rejected')

CREATE TABLE [wishlist].[Wish] (
	WishId bigint IDENTITY(1,1) PRIMARY KEY,
	Name varchar(100) NOT NULL,
	UserId int NOT NULL FOREIGN KEY REFERENCES [User](UserId),
	Price decimal(10,4),
	Description varchar(1000) NOT NULL,
	Image int FOREIGN KEY REFERENCES [File](FileId),
	CreateDate datetime,
	Deadline datetime,
	Quantity int NOT NULL,
	IsMaxOne bit NOT NULL,
	StatusId int NOT NULL FOREIGN KEY REFERENCES [wishlist].[WishStatus](Id),
	VisibilityId int NOT NULL FOREIGN KEY REFERENCES [VisibilityStatus](Id),
)

CREATE TABLE [wishlist].[WishContribute] (
	WishContributeId int IDENTITY(1,1) PRIMARY KEY,
	WishId bigint NOT NULL FOREIGN KEY REFERENCES [wishlist].[Wish](WishId),
	UserId int NOT NULL FOREIGN KEY REFERENCES [User](UserId),
	Contribution decimal(10,4) NOT NULL
)