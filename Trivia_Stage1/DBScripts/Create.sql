use master
drop database triviadb
go

CREATE Database TriviaDB
Go

use TriviaDB 
Go

CREATE TABLE SubjectTab
(
	SubID INT PRIMARY KEY NOT NULL,
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
	QuestionText NVARCHAR (256) NOT NULL,
	RightAnswer NVARCHAR (256) NOT NULL,
	BadAnswer1 NVARCHAR (256) NOT NULL,
	BadAnswer2 NVARCHAR (256) NOT NULL,
	BadAnswer3 NVARCHAR (256) NOT NULL,
);

INSERT INTO SubjectTab (SubID, SubName) VALUES (1, 'Sports');
INSERT INTO SubjectTab (SubID, SubName) VALUES (2, 'Movies');
INSERT INTO SubjectTab (SubID, SubName) VALUES (3, 'Countrys');
INSERT INTO SubjectTab (SubID, SubName) VALUES (4, 'Other');

INSERT INTO QuestionsStatusTab (StatusId, StatusName) VALUES (1,'Pending');
INSERT INTO QuestionsStatusTab (StatusId, StatusName) VALUES (2,'Approved');
INSERT INTO QuestionsStatusTab (StatusId, StatusName) VALUES (3,'Declined');

INSERT INTO LevelTab (LevelID, LEVELSName) VALUES (3,'Manager');
INSERT INTO LevelTab (LevelID, LEVELSName) VALUES (2,'Master');
INSERT INTO LevelTab (LevelID, LEVELSName) VALUES (1,'Rookie');

INSERT INTO PlayersTab (mail, [name], [password], score, [IDlevel]) VALUES ('amit_marom.co.il', 'mamit', '16012008', 200, 1);
INSERT INTO PlayersTab (mail, [name], [password], score, [IDlevel]) VALUES ('Master@gmail.com', 'Master', '111', 230, 2);
INSERT INTO PlayersTab (mail, [name], [password], score, [IDlevel]) VALUES ('m@m', 'manager', '111', 1300, 3);

INSERT INTO QuestionTab (PlayerID, [SubjectID], [StatusId], QuestionText, RightAnswer, BadAnswer1, BadAnswer2, BadAnswer3) VALUES (2, 1, 1, 'Which country has the shape of a boot?' , 'Italy', 'Mexico', 'Israel', 'Cyprus');
INSERT INTO QuestionTab (PlayerID, [SubjectID], [StatusId], QuestionText, RightAnswer, BadAnswer1, BadAnswer2, BadAnswer3) VALUES (3, 4, 2, 'How many days are there in a year?' , '365', '355', '3129', '7');
INSERT INTO QuestionTab (PlayerID, [SubjectID], [StatusId], QuestionText, RightAnswer, BadAnswer1, BadAnswer2, BadAnswer3) VALUES (3, 2, 2, 'What is unique about Peter Pan?' , 'Ability to fly', 'He can dance', 'Talented with his voice', 'He can cook');

INSERT INTO QuestionTab (PlayerID, [SubjectID], [StatusId], QuestionText, RightAnswer, BadAnswer1, BadAnswer2, BadAnswer3) VALUES (2, 3, 1, 'What colors are in the Israeli flag?' , 'Blue and white', 'white and yellow', 'pink and blue', 'white and brown');
INSERT INTO QuestionTab (PlayerID, [SubjectID], [StatusId], QuestionText, RightAnswer, BadAnswer1, BadAnswer2, BadAnswer3) VALUES (3, 3, 1, 'How many states are there in the US?' , '50', '33', '247', '7');
INSERT INTO QuestionTab (PlayerID, [SubjectID], [StatusId], QuestionText, RightAnswer, BadAnswer1, BadAnswer2, BadAnswer3) VALUES (3, 3, 1, 'How many countries are there in the world?' , '247', '60', '444', '248');

select * from PlayersTab
select * from QuestionsStatusTab
select * from QuestionTab
select * from SubjectTab

