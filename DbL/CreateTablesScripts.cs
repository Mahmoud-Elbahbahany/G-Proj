using System.Data;
using System.Xml.Linq;
using System.Xml;
using System;

namespace DbL
{
    public class CreateTablesScripts
    {
        #region SubjectHandling_Module--Tables:
        const string Create_SubjectsTable = @"
                CREATE TABLE Subjects (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                InstructorID INTEGER NOT NULL,
                QuestionsBankID INTEGER,  -- Added to match QuestionsBankID property (nullable)
                Code TEXT NOT NULL CHECK(LENGTH(Code) <= 10),
                Name TEXT NOT NULL CHECK(LENGTH(Name) <= 100),
                Description TEXT CHECK(LENGTH(Description) <= 500),
                CreatedAt TEXT NOT NULL CHECK(LENGTH(CreatedAt) <= 19),
                UpdatedAt TEXT NOT NULL CHECK(LENGTH(UpdatedAt) <= 19),
                FOREIGN KEY (InstructorID) REFERENCES Users(ID),    -- Assuming Users table exists
                FOREIGN KEY (QuestionsBankID) REFERENCES QuestionsBank(ID)  -- Assuming QuestionsBank table exists
                                            )";

        const string Create_QuestionsTable = @"
                CREATE TABLE Questions (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectID INTEGER NOT NULL,
                Type TEXT NOT NULL CHECK(Type IN ('UnDefined', 'MCQ', 'TrueFalse', 'Essay')),
                Text TEXT NOT NULL CHECK(LENGTH(Text) <= 500),
                CorrectAnswer TEXT NOT NULL,
                CreatedAt TEXT NOT NULL CHECK(LENGTH(CreatedAt) <= 19),
                UpdatedAt TEXT NOT NULL CHECK(LENGTH(UpdatedAt) <= 19),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(ID) ON DELETE CASCADE
                                             )";

        const string Create_QuestionsBanksTable = @"
                CREATE TABLE QuestionsBank (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL CHECK(LENGTH(Name) <= 100),
                SubjectID INTEGER,  -- Nullable since it's int? in C#
                CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(ID)
                                               )";

        const string Create_TestsTable = @"
                CREATE TABLE Tests (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL CHECK(LENGTH(Title) <= 25),
                SubjectID INTEGER NOT NULL,
                QuestionsIDsJson TEXT NOT NULL DEFAULT '',
                Duration TEXT NOT NULL CHECK(LENGTH(Duration) <= 19),
                IsPublished INTEGER NOT NULL DEFAULT 0,
                CreatedAt TEXT NOT NULL CHECK(LENGTH(CreatedAt) <= 19),
                UpdatedAt TEXT NOT NULL CHECK(LENGTH(UpdatedAt) <= 19),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(ID)
                                                )";
        #endregion

        #region Sys_Core--Tables:
        const string Create_UsersTable = @"
                CREATE TABLE Users(
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Email TEXT UNIQUE NOT NULL,
                HashedPassword TEXT NOT NULL,
                Role TEXT CHECK(Role IN ('Dr', 'Admin', 'Student')) NOT NULL,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                                       )";
        #endregion



    }
}

/*
 #region SubjectHandling_Module--Tables:
        const string Create_SubjectsTable = @"
                CREATE TABLE Subjects (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                InstructorID INTEGER NOT NULL,
                QuestionsBankID INTEGER,  -- Added to match QuestionsBankID property (nullable)
                Code TEXT NOT NULL CHECK(LENGTH(Code) <= 10),
                Name TEXT NOT NULL CHECK(LENGTH(Name) <= 100),
                Description TEXT CHECK(LENGTH(Description) <= 500),
                CreatedAt TEXT NOT NULL CHECK(LENGTH(CreatedAt) <= 19),
                UpdatedAt TEXT NOT NULL CHECK(LENGTH(UpdatedAt) <= 19),
                FOREIGN KEY (InstructorID) REFERENCES Users(ID),    -- Assuming Users table exists
                FOREIGN KEY (QuestionsBankID) REFERENCES QuestionsBank(ID)  -- Assuming QuestionsBank table exists
                                            )";

        const string Create_QuestionsTable = @"
                CREATE TABLE Questions (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectID INTEGER NOT NULL,
                Type TEXT NOT NULL CHECK(Type IN ('UnDefined', 'MCQ', 'TrueFalse', 'Essay')),
                Text TEXT NOT NULL CHECK(LENGTH(Text) <= 500),
                CorrectAnswer TEXT NOT NULL,
                CreatedAt TEXT NOT NULL CHECK(LENGTH(CreatedAt) <= 19),
                UpdatedAt TEXT NOT NULL CHECK(LENGTH(UpdatedAt) <= 19),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(ID) ON DELETE CASCADE
                                             )";

        const string Create_QuestionsBanksTable = @"
                CREATE TABLE QuestionsBank (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL CHECK(LENGTH(Name) <= 100),
                SubjectID INTEGER,  -- Nullable since it's int? in C#
                CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(ID)
                                               )";
        
        const string Create_TestsTable = @"
                CREATE TABLE Tests (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL CHECK(LENGTH(Title) <= 25),
                SubjectID INTEGER NOT NULL,
                QuestionsIDsJson TEXT NOT NULL DEFAULT '',
                QuestionsJson TEXT NOT NULL DEFAULT '',
                Duration TEXT NOT NULL CHECK(LENGTH(Duration) <= 19),
                IsPublished INTEGER NOT NULL DEFAULT 0,
                CreatedAt TEXT NOT NULL CHECK(LENGTH(CreatedAt) <= 19),
                UpdatedAt TEXT NOT NULL CHECK(LENGTH(UpdatedAt) <= 19),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(ID)
                                                )";
        #endregion
)
 */

