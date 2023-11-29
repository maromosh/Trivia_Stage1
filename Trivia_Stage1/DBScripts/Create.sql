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
	LevelID INT PRIMARY KEY,
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

INSERT INTO SubjectTab (SubName) VALUES ('Sports');
INSERT INTO SubjectTab (SubName) VALUES ('Science');

INSERT INTO QuestionsStatusTab (StatusId, StatusName) VALUES (1,'Pending');
INSERT INTO QuestionsStatusTab (StatusId, StatusName) VALUES (2,'Approved');
INSERT INTO QuestionsStatusTab (StatusId, StatusName) VALUES (3,'Declined');

INSERT INTO LevelTab (LevelID, LEVELSName) VALUES (1,'Manager');
INSERT INTO LevelTab (LevelID, LEVELSName) VALUES (2,'Master');
INSERT INTO LevelTab (LevelID, LEVELSName) VALUES (3,'Rookie');

INSERT INTO PlayersTab (mail, [name], [password], score, [IDlevel]) VALUES ('amit_marom.co.il', 'mamit', '16012008', 12000, 1);


 