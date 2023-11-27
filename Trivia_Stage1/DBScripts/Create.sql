use master
drop database triviadb
go

CREATE Database TriviaDB
Go

use TriviaDB 
Go

CREATE TABLE SubjectTab
(
	SubID INT IDENTITY (1,1) PRIMARY KEY,
	SubName NVARCHAR (20) NOT NULL
);
Go

CREATE TABLE QuestionsStatusTab
(
	 StatusId INT PRIMARY KEY,
	 StatusName NVARCHAR(15)
);
Go
CREATE TABLE LevelTab
(
	LevelID INT PRIMARY KEY identity (1,1),
	LEVELSName NVARCHAR (20) NOT NULL,
);
Go
CREATE TABLE PlayersTab
(
	ID INT PRIMARY KEY identity(1,1),
	mail NVARCHAR (20) NOT NULL unique,
	[name] NVARCHAR (20) NOT NULL,
	[password] NVARCHAR (20) NOT NULL,
	score INT,
	[IDlevel] INT FOREIGN KEY REFERENCES LevelTab(LevelID),
);
Go

CREATE TABLE QuestionTab
(
	QuestionID INT IDENTITY (1,1) PRIMARY KEY,
	PlayerID INT FOREIGN KEY REFERENCES PlayersTab(ID),
	[SubjectID] INT FOREIGN KEY REFERENCES SubjectTab(SubID),
	[StatusId] INT FOREIGN KEY REFERENCES QuestionsStatusTab(StatusId),
	RightQuestions NVARCHAR (100) NOT NULL,
	Questions1 NVARCHAR (100) NOT NULL,
	Questions2 NVARCHAR (100) NOT NULL,
	Questions3 NVARCHAR (100) NOT NULL,
);




