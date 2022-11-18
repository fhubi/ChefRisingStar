PRAGMA foreign_keys = ON;

CREATE TABLE SocialMediaSites (
ID INTEGER PRIMARY KEY,
Name TEXT UNIQUE NOT NULL,
Url TEXT  NOT NULL
);

CREATE TABLE Achievements (
ID INTEGER PRIMARY KEY,
Name TEXT NOT NULL,
Description TEXT  NOT NULL
);

CREATE TABLE Metrics (
ID INTEGER PRIMARY KEY,
Name TEXT UNIQUE NOT NULL,
Description TEXT
);

CREATE TABLE Users (
ID INTEGER PRIMARY KEY,
Email TEXT UNIQUE NOT NULL,
FirstName TEXT NOT NULL,
LastName TEXT NOT NULL,
UserName TEXT NOT NULL,
SchoolId INTEGER,
TeamID INTEGER,
JoinDate TEXT,
LastLoginDate TEXT,
IsAdministrator INTEGER,
IsLocked INTEGER,
PasswordHash TEXT,
Salt TEXT,
FOREIGN KEY(SchoolID) REFERENCES Schools(ID),
FOREIGN KEY(TeamID) REFERENCES Teams(ID)
);

CREATE TABLE Schools (
ID INTEGER PRIMARY KEY,
Name TEXT NOT NULL,
Address TEXT,
Phone TEXT,
City TEXT,
ContactId INTEGER
);

CREATE TABLE Teams (
ID INTEGER PRIMARY KEY,
Name TEXT NOT NULL,
CaptainId INTEGER,
Active INTEGER
);


CREATE TABLE UserSocialMediaSites (
UserID INTEGER,
SocialMediaSiteId INTEGER,
AccountName TEXT NOT NULL,
FOREIGN KEY(UserID) REFERENCES Users(ID),
FOREIGN KEY(SocialMediaSiteID) REFERENCES SocialMediaSites(ID),
PRIMARY KEY(UserId,SocialMediaSiteID)
);

CREATE TABLE UserAchievements (
AchievementID INTEGER NOT NULL,
UserID INTEGER NOT NULL,
DateEarned TEXT NOT NULL,
FOREIGN KEY(AchievementId) REFERENCES Achievements(ID),
FOREIGN KEY(UserID) REFERENCES Users(ID),
PRIMARY KEY(UserID,AchievementID)
);

CREATE TABLE AchievementSteps (
StepID INTEGER PRIMARY KEY,
AchievementID INTEGER NOT NULL,
StepOrder INTEGER,
Name TEXT NOT NULL,
Description TEXT,
FOREIGN KEY(AchievementID) REFERENCES Achievements(ID)
);

CREATE TABLE UserAchievementSteps (
StepID INTEGER NOT NULL,
UserID INTEGER NOT NULL,
DateEarned TEXT NOT NULL,
FOREIGN KEY(StepID) REFERENCES AchievementSteps(ID),
FOREIGN KEY(UserID) REFERENCES Users(ID),
PRIMARY KEY(UserID,StepID)
);

CREATE TABLE UserRecipes (
Id INTEGER PRIMARY KEY,
UserId INTEGER NOT NULL,
SpoonacularId TEXT NOT NULL,
FOREIGN KEY(UserId) REFERENCES Users(ID)
);

CREATE TABLE UserMetrics (
ID INTEGER PRIMARY KEY,
UserID INTEGER NOT NULL,
MetricID INTEGER NOT NULL,
ActivityTime TEXT NOT NULL,
FOREIGN KEY(MetricID) REFERENCES Metrics(ID),
FOREIGN KEY(UserID) REFERENCES Users(ID)
);

CREATE TABLE FavoriteTypes (
Id INTEGER PRIMARY KEY,
Name TEXT NOT NULL
);

CREATE TABLE UserFavortites (
UserId INTEGER,
RefId TEXT NOT NULL,
FavoriteTypeID INTEGER NOT NULL,
ActivityTime TEXT NOT NULL,
FOREIGN KEY(UserID) REFERENCES Users(ID),
FOREIGN KEY(FavoriteTypeID) REFERENCES FavoriteTypes(ID),
PRIMARY KEY(UserID,RefId)
);

INSERT INTO FavoriteTypes VALUES (1, "Recipe");
INSERT INTO FavoriteTypes VALUES (2, "Ingredient");
INSERT INTO FavoriteTypes VALUES (3, "Bookmark");
INSERT INTO FavoriteTypes VALUES (4, "Cuisine");
INSERT INTO FavoriteTypes VALUES (5, "Person");

INSERT INTO Metrics VALUES (1, "NewUserCreated", "New User Created in application");
INSERT INTO Metrics VALUES (2, "UserLoggedIn", "User Logged into application");
INSERT INTO Metrics VALUES (3, "UserLinkedSocialMediaAccount", "User Linked Social Media Account");
INSERT INTO Metrics VALUES (4, "UserCompletedAchievement", "User Completed Achievement");
INSERT INTO Metrics VALUES (5, "UserSearchedRecipe", "User Searched Recipe");
INSERT INTO Metrics VALUES (6, "UserLikedRecipe", "User Liked Recipe");
INSERT INTO Metrics VALUES (7, "UserLikedIngredient", "User Liked Ingredient");
INSERT INTO Metrics VALUES (8, "UserSharedPhoto", "User Shared Photo");
INSERT INTO Metrics VALUES (9, "UserSharedUpdate", "User Shared Update");
INSERT INTO Metrics VALUES (10, "UserSharedRecipe", "User Shared Update");
INSERT INTO Metrics VALUES (11, "UserJoinedBrigade", "User Joined Brigade");
INSERT INTO Metrics VALUES (12, "UserSubmittedRecipe", "User Submitted Recipe");
INSERT INTO Metrics VALUES (13, "UserSentMessage", "User Sent Message");
INSERT INTO Metrics VALUES (14, "AppError", "Application Error");

INSERT INTO Users VALUES (1, "jonathan.brunette@gmail.com", "Jonathan", "Brunette", "Jonathan", null, null, null, null, 1, 0, null, null);

INSERT INTO SocialMediaSites VALUES (1, "Instagram", "Instagram.com");
INSERT INTO SocialMediaSites VALUES (2, "Facebook", "Facebook.com");
INSERT INTO SocialMediaSites VALUES (3, "Twitter", "Twitrer.com");
INSERT INTO SocialMediaSites VALUES (4, "TicTok", "TicTok.com");
INSERT INTO SocialMediaSites VALUES (5, "SnapChat", "Snapchat.com");

INSERT INTO Schools VALUES (1, "Morgan Institute", "700 Wellington, Montreal, H4C", "514 555-3252", "Montreal", 1);

INSERT INTO Teams VALUES (1, "Dragons", 1, 1);