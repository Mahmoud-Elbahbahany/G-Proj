using System;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace Testing
{
    public class DatabaseInitializer
    {
        private readonly string _databaseDir;
        private readonly bool _enableLogging;

        public DatabaseInitializer(string databaseDirectory = null, bool enableLogging = true)
        {
            // Use provided directory or default to executable directory if not specified
            _databaseDir = databaseDirectory ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
            _enableLogging = enableLogging;
        }

        public void InitializeDatabases()
        {
            try
            {
                Log("Starting database initialization...");

                // Create directory if it doesn't exist
                EnsureDatabaseDirectoryExists();

                InitializeSecurityDatabase();
                InitializeSubjectHandlingDatabase();

                Log("Database initialization completed successfully.");
            }
            catch (Exception ex)
            {
                Log($"Error initializing databases: {ex.Message}");
                throw; // Re-throw to allow calling code to handle the error
            }
        }

        private void EnsureDatabaseDirectoryExists()
        {
            try
            {
                if (!Directory.Exists(_databaseDir))
                {
                    Log($"Creating database directory at: {_databaseDir}");
                    Directory.CreateDirectory(_databaseDir);
                }
            }
            catch (Exception ex)
            {
                Log($"Error creating database directory: {ex.Message}");
                throw;
            }
        }

        private void InitializeSecurityDatabase()
        {
            string dbPath = Path.Combine(_databaseDir, "Security.db");
            Log($"Initializing Security database at: {dbPath}");

            ExecuteDatabaseScript(dbPath, SecurityDbScript());
        }

        private void InitializeSubjectHandlingDatabase()
        {
            string dbPath = Path.Combine(_databaseDir, "SubjectHandling.db");
            Log($"Initializing SubjectHandling database at: {dbPath}");

            ExecuteDatabaseScript(dbPath, SubjectHandlingDbScript());
        }

        private void ExecuteDatabaseScript(string dbPath, string script)
        {
            SQLiteConnection connection = null;
            SQLiteTransaction transaction = null;

            try
            {
                var connectionString = $"Data Source={dbPath};Version=3;FailIfMissing=False;";

                connection = new SQLiteConnection(connectionString);
                connection.Open();

                // Begin transaction
                transaction = connection.BeginTransaction();

                // Execute the script
                ExecuteSqlScript(connection, script);

                // Commit transaction if everything succeeded
                transaction.Commit();
                Log($"Database script executed successfully for: {Path.GetFileName(dbPath)}");
            }
            catch (Exception ex)
            {
                // Rollback transaction if there was an error
                try { transaction?.Rollback(); } catch { /* Ignore rollback errors */ }

                Log($"Error executing database script for {Path.GetFileName(dbPath)}: {ex.Message}");
                throw;
            }
            finally
            {
                transaction?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        private void ExecuteSqlScript(SQLiteConnection connection, string script)
        {
            // Remove all transaction commands from the script since we're handling transactions at a higher level
            var cleanedScript = script.Replace("BEGIN TRANSACTION;", "")
                                    .Replace("COMMIT;", "");

            var commands = cleanedScript.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var commandText in commands)
            {
                var trimmedCommand = commandText.Trim();
                if (string.IsNullOrWhiteSpace(trimmedCommand))
                    continue;

                try
                {
                    using (var command = new SQLiteCommand(trimmedCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error executing command: {trimmedCommand}");
                    Log($"Error details: {ex.Message}");
                    throw; // Re-throw to ensure transaction is rolled back
                }
            }
        }

        private void Log(string message)
        {
            if (_enableLogging)
            {
                Debug.WriteLine($"[DatabaseInitializer] {DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}");
            }
        }

        private string SecurityDbScript()
        {
            return @"
CREATE TABLE IF NOT EXISTS Users (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    HashedPassword TEXT NOT NULL,
    Role INTEGER NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);

INSERT OR IGNORE INTO Users (Name, Email, HashedPassword, Role, CreatedAt, UpdatedAt) VALUES 
('Admin User', 'admin@example.com', '$2a$12$xyz123', Admin, '2023-01-01T00:00:00', '2023-01-01T00:00:00'),
('Instructor One', 'instructor1@example.com', '$2a$12$abc456', Dr , '2023-01-02T00:00:00', '2023-01-02T00:00:00'),
('Instructor Two', 'instructor2@example.com', '$2a$12$def789', Dr , '2023-01-03T00:00:00', '2023-01-03T00:00:00'),
('Student One', 'student1@example.com', '$2a$12$ghi012', Guest, '2023-01-04T00:00:00', '2023-01-04T00:00:00'),
('Student Two', 'student2@example.com', '$2a$12$jkl345', Guest, '2023-01-05T00:00:00', '2023-01-05T00:00:00');";
        }

        private string SubjectHandlingDbScript()
        {
            return @"
-- Create tables
CREATE TABLE IF NOT EXISTS Subjects (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    InstructorID INTEGER NOT NULL,
    CodeID TEXT NOT NULL UNIQUE,
    Name TEXT NOT NULL,
    Description TEXT,
    QuestionsBankIDs TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_subjects_instructor ON Subjects(InstructorID);
CREATE INDEX IF NOT EXISTS idx_subjects_code ON Subjects(CodeID);

CREATE TABLE IF NOT EXISTS Questions (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    SubjectID INTEGER NOT NULL,
    Type INTEGER NOT NULL,
    Difficulty_lvl INTEGER NOT NULL,
    Text TEXT NOT NULL,
    Answers TEXT NOT NULL,
    CorrectAnswer TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL,
    FOREIGN KEY (SubjectID) REFERENCES Subjects(ID)
);

CREATE INDEX IF NOT EXISTS idx_questions_subject ON Questions(SubjectID);
CREATE INDEX IF NOT EXISTS idx_questions_type ON Questions(Type);
CREATE INDEX IF NOT EXISTS idx_questions_difficulty ON Questions(Difficulty_lvl);

CREATE TABLE IF NOT EXISTS QuestionsBanks (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    SubjectID INTEGER NOT NULL,
    Title TEXT NOT NULL,
    Description TEXT,
    IsActive INTEGER NOT NULL,
    QuestionsIDs TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL,
    FOREIGN KEY (SubjectID) REFERENCES Subjects(ID)
);

CREATE INDEX IF NOT EXISTS idx_questionsbanks_subject ON QuestionsBanks(SubjectID);
CREATE INDEX IF NOT EXISTS idx_questionsbanks_active ON QuestionsBanks(IsActive);

CREATE TABLE IF NOT EXISTS TestsBanks (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    InstructorID INTEGER NOT NULL,
    Title TEXT NOT NULL,
    Description TEXT,
    IsActive INTEGER NOT NULL,
    TestsIDs TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_testsbanks_instructor ON TestsBanks(InstructorID);
CREATE INDEX IF NOT EXISTS idx_testsbanks_active ON TestsBanks(IsActive);

CREATE TABLE IF NOT EXISTS Tests (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    TestBankID INTEGER NOT NULL,
    Title TEXT NOT NULL,
    Duration TEXT NOT NULL,
    IsPublished INTEGER NOT NULL,
    QuestionsIDs TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL,
    FOREIGN KEY (TestBankID) REFERENCES TestsBanks(ID)
);

CREATE INDEX IF NOT EXISTS idx_tests_testbank ON Tests(TestBankID);
CREATE INDEX IF NOT EXISTS idx_tests_published ON Tests(IsPublished);

-- Insert Data Structures subject
INSERT OR IGNORE INTO Subjects (InstructorID, CodeID, Name, Description, QuestionsBankIDs, CreatedAt, UpdatedAt) VALUES 
(2, 'CS101', 'Data Structures', 'Fundamental data structures and algorithms', '[1,2,3,4]', '2023-01-10T00:00:00', '2023-01-10T00:00:00');

-- Insert Algorithms subject
INSERT OR IGNORE INTO Subjects (InstructorID, CodeID, Name, Description, QuestionsBankIDs, CreatedAt, UpdatedAt) VALUES 
(3, 'CS102', 'Algorithms', 'Algorithm design and analysis', '[5,6]', '2023-01-11T00:00:00', '2023-01-11T00:00:00');

-- Insert all Data Structures questions (1-37)
INSERT OR IGNORE INTO Questions (SubjectID, Type, Difficulty_lvl, Text, Answers, CorrectAnswer, CreatedAt, UpdatedAt) VALUES
(1, 0, 1, 'What is a data structure?', '[\""A programming language\"", \""A collection of algorithms\"", \""A way to store and organize data\"", \""A type of computer hardware\""]', 'A way to store and organize data', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'What are the disadvantages of arrays?', '[\""Index value of an array can be negative\"", \""Elements are sequentially accessed\"", \""Data structure like queue or stack cannot be implemented\"", \""There are chances of wastage of memory space if elements inserted in an array are lesser than the allocated size\""]', 'There are chances of wastage of memory space if elements inserted in an array are lesser than the allocated size', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'Which data structure is used for implementing recursion?', '[\""Stack\"", \""Queue\"", \""List\"", \""Array\""]', 'Stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'The data structure required to check whether an expression contains a balanced parenthesis is?', '[\""Queue\"", \""Stack\"", \""Tree\"", \""Array\""]', 'Stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'Which of the following is not the application of stack?', '[\""Data Transfer between two asynchronous process\"", \""Compiler Syntax Analyzer\"", \""Tracking of local variables at run time\"", \""A parentheses balancing program\""]', 'Data Transfer between two asynchronous process', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'Which data structure is needed to convert infix notation to postfix notation?', '[\""Tree\"", \""Branch\"", \""Stack\"", \""Queue\""]', 'Stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'What is the value of the postfix expression 6 3 2 4 + – *?', '[\""74\"", \""-18\"", \""22\"", \""40\""]', '-18', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'What data structure would you mostly likely see in non recursive implementation of a recursive algorithm?', '[\""Stack\"", \""Linked List\"", \""Tree\"", \""Queue\""]', 'Stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'Which of the following statement(s) about stack data structure is/are NOT correct?', '[\""Top of the Stack always contain the new node\"", \""Stack is the FIFO data structure\"", \""Null link is present in the last node at the bottom of the stack\"", \""Linked List are used for implementing Stacks\""]', 'Stack is the FIFO data structure', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'The data structure required for Breadth First Traversal on a graph is?', '[\""Array\"", \""Stack\"", \""Tree\"", \""Queue\""]', 'Queue', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'The prefix form of A-B/ (C * D ^ E) is?', '[\""-A/B*C^DE\"", \""-A/BC*^DE\"", \""-ABCD*^DE\"", \""-/*^ACBDE\""]', '-A/B*C^DE', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'Which of the following points is/are not true about Linked List data structure when it is compared with an array?', '[\""Random access is not allowed in a typical implementation of Linked Lists\"", \""Access of elements in linked list takes less time than compared to arrays\"", \""Arrays have better cache locality that can make them better in terms of performance\"", \""It is easy to insert and delete elements in Linked List\""]', 'Access of elements in linked list takes less time than compared to arrays', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'Which data structure is based on the Last In First Out (LIFO) principle?', '[\""Tree\"", \""Linked List\"", \""Stack\"", \""Queue\""]', 'Stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'Which of the following application makes use of a circular linked list?', '[\""Recursive function calls\"", \""Undo operation in a text editor\"", \""Implement Hash Tables\"", \""Allocating CPU to resources\""]', 'Allocating CPU to resources', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'What is a bit array?', '[\""Data structure that compactly stores bits\"", \""Data structure for representing arrays of records\"", \""Array in which elements are not present in continuous locations\"", \""An array in which most of the elements have the same value\""]', 'Data structure that compactly stores bits', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'Which of the following tree data structures is not a balanced binary tree?', '[\""Splay tree\"", \""B-tree\"", \""AVL tree\"", \""Red-black tree\""]', 'B-tree', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'Which of the following is not the type of queue?', '[\""Priority queue\"", \""Circular queue\"", \""Single ended queue\"", \""Ordinary queue\""]', 'Single ended queue', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'Which of the following data structures can be used for parentheses matching?', '[\""n-ary tree\"", \""queue\"", \""priority queue\"", \""stack\""]', 'stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'Which algorithm is used in the top tree data structure?', '[\""Backtracking\"", \""Divide and Conquer\"", \""Branch\"", \""Greedy\""]', 'Divide and Conquer', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'What is the need for a circular queue?', '[\""easier computations\"", \""implement LIFO principle in queues\"", \""effective usage of memory\"", \""to delete elements based on priority\""]', 'effective usage of memory', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'Which of the following is the most widely used external memory data structure?', '[\""B-tree\"", \""Red-black tree\"", \""AVL tree\"", \""Both AVL tree and Red-black tree\""]', 'B-tree', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'Which of the following is also known as Rope data structure?', '[\""Linked List\"", \""Array\"", \""String\"", \""Cord\""]', 'Cord', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'What will be the output of the following program?\n\nmain()\n{\n   char str[]=\""san foundry\"";\n   int len = strlen(str);\n   int i;\n\n   for(i=0;i<len;i++)\n        push(str[i]);\n\n   for(i=0;i<len;i++)\n      pop();\n}', '[\""yrdnuof nas\"", \""foundry nas\"", \""sanfoundry\"", \""san foundry\""]', 'yrdnuof nas', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'Which of the following data structure can provide efficient searching of the elements?', '[\""binary search tree\"", \""unordered lists\"", \""2-3 tree\"", \""treap\""]', '2-3 tree', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'What is an AVL tree?', '[\""a tree which is unbalanced and is a height balanced tree\"", \""a tree which is balanced and is a height balanced tree\"", \""a tree with atmost 3 children\"", \""a tree with three children\""]', 'a tree which is balanced and is a height balanced tree', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'What is the time complexity for searching a key or integer in Van Emde Boas data structure?', '[\""O (M!)\"", \""O (log M!)\"", \""O (log (log M))\"", \""O (M2)\""]', 'O (log (log M))', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'The optimal data structure used to solve Tower of Hanoi is _________', '[\""Tree\"", \""Heap\"", \""Priority queue\"", \""Stack\""]', 'Stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'What is the use of the bin data structure?', '[\""to have efficient traversal\"", \""to have efficient region query\"", \""to have efficient deletion\"", \""to have efficient insertion\""]', 'to have efficient region query', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'Which is the most appropriate data structure for reversing a word?', '[\""stack\"", \""queue\"", \""graph\"", \""tree\""]', 'stack', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'What is the functionality of the following piece of code?\n\npublic void display() \n{\n\tif(size == 0)\n\t\tSystem.out.println(\""underflow\"");\n\telse\n\t{\n\t\tNode current = first;\n\t\twhile(current != null)\n\t\t{\n\t\t\tSystem.out.println(current.getEle());\n\t\t\tcurrent = current.getNext();\n\t\t}\n\t}\n}', '[\""display the list\"", \""reverse the list\"", \""reverse the list excluding top-of-the-stack-element\"", \""display the list excluding top-of-the-stack-element\""]', 'display the list', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 3, 'Which of the following is the simplest data structure that supports range searching?', '[\""AA-trees\"", \""K-d trees\"", \""Heaps\"", \""binary search trees\""]', 'K-d trees', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'What is the advantage of a hash table as a data structure?', '[\""easy to implement\"", \""faster access of data\"", \""exhibit good locality of reference\"", \""very efficient for less number of entries\""]', 'faster access of data', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'Which type of data structure is a ternary heap?', '[\""Hash\"", \""Array\"", \""Priority Stack\"", \""Priority Queue\""]', 'Priority Queue', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'What is a dequeue?', '[\""A queue implemented with both singly and doubly linked lists\"", \""A queue with insert/delete defined for front side of the queue\"", \""A queue with insert/delete defined for both front and rear ends of the queue\"", \""A queue implemented with a doubly linked list\""]', 'A queue with insert/delete defined for both front and rear ends of the queue', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'A data structure in which elements can be inserted or deleted at/from both ends but not in the middle is?', '[\""Priority queue\"", \""Dequeue\"", \""Circular queue\"", \""Queue\""]', 'Dequeue', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 1, 'What is the output of the following Java code?\n\npublic class array\n{\n\tpublic static void main(String args[])\n\t{\n\t\tint []arr = {1,2,3,4,5};\n\t\tSystem.out.println(arr[2]);\n\t\tSystem.out.println(arr[4]);\n\t}\n}', '[\""4 and 2\"", \""2 and 4\"", \""5 and 3\"", \""3 and 5\""]', '3 and 5', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),
(1, 0, 2, 'In simple chaining, what data structure is appropriate?', '[\""Doubly linked list\"", \""Circular linked list\"", \""Singly linked list\"", \""Binary trees\""]', 'Doubly linked list', '2023-01-12T00:00:00', '2023-01-12T00:00:00'),

-- Insert all Programming/Algorithm questions (38-47)
(2, 0, 1, 'Programming based on stepwise refinement process.', '[\""Structural\"", \""C programming\"", \""Procedural\"", \""Fine\""]', 'Structural', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'Top-down approach is followed in structural programming.', '[\""True\"", \""False\""]', 'True', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'A ________ is a directed graph that describes the flow of execution control of the program.', '[\""Flowchart\"", \""Flow graph\"", \""Complexity curve\"", \""Algorithm\""]', 'Flowchart', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'A program should be ________', '[\""Secure\"", \""Sequential\"", \""Ordered\"", \""Simple\""]', 'Sequential', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'The following is the syntax for: ____(condition) action', '[\""Else\"", \""Elif\"", \""If\"", \""Switch\""]', 'If', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'Which of the following is a loop statement?', '[\""IF\"", \""ELSE\"", \""WHILE\"", \""DO\""]', 'WHILE', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'What is the correct syntax of for statement?', '[\""for(initialization;condition;update)\"", \""for(initialization,condition,update)\"", \""for(condition;initialization;update)\"", \""for(initialization;condition;)\""]', 'for(initialization;condition;update)', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'Semicolon is used after :', '[\""Function definition\"", \""Function call\"", \""for loop\"", \""while loop\""]', 'Function call', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'The number of values a function can return at a time?', '[\""1\"", \""0\"", \""2\"", \""more than 2\""]', '1', '2023-01-13T00:00:00', '2023-01-13T00:00:00'),
(2, 0, 1, 'Which of the following isn't a loop statement?', '[\""for\"", \""elif\"", \""while\"", \""do-while\""]', 'elif', '2023-01-13T00:00:00', '2023-01-13T00:00:00');

-- Create Question Banks
INSERT OR IGNORE INTO QuestionsBanks (SubjectID, Title, Description, IsActive, QuestionsIDs, CreatedAt, UpdatedAt) VALUES
(1, 'Basic Data Structures', 'Fundamental data structure concepts', 1, '[1,2,3,4,5,6,7,8,9,10]', '2023-01-14T00:00:00', '2023-01-14T00:00:00'),
(1, 'Intermediate Data Structures', 'Intermediate level data structure questions', 1, '[11,12,13,14,15,16,17,18,19,20]', '2023-01-15T00:00:00', '2023-01-15T00:00:00'),
(1, 'Advanced Data Structures', 'Advanced data structure concepts', 1, '[21,22,23,24,25,26,27,28,29,30]', '2023-01-16T00:00:00', '2023-01-16T00:00:00'),
(1, 'Data Structures Applications', 'Practical applications of data structures', 1, '[31,32,33,34,35,36,37]', '2023-01-17T00:00:00', '2023-01-17T00:00:00'),
(2, 'Programming Fundamentals', 'Basic programming concepts', 1, '[38,39,40,41,42]', '2023-01-18T00:00:00', '2023-01-18T00:00:00'),
(2, 'Control Structures', 'Questions about programming control flow', 1, '[43,44,45,46,47]', '2023-01-19T00:00:00', '2023-01-19T00:00:00');

-- Create Tests Banks
INSERT OR IGNORE INTO TestsBanks (InstructorID, Title, Description, IsActive, TestsIDs, CreatedAt, UpdatedAt) VALUES
(2, 'Data Structures Assessments', 'All data structures tests', 1, '[1,2,3,4]', '2023-01-20T00:00:00', '2023-01-20T00:00:00'),
(3, 'Programming Assessments', 'Programming fundamentals tests', 1, '[5,6]', '2023-01-21T00:00:00', '2023-01-21T00:00:00');

-- Create Tests
INSERT OR IGNORE INTO Tests (TestBankID, Title, Duration, IsPublished, QuestionsIDs, CreatedAt, UpdatedAt) VALUES
(1, 'Data Structures Quiz 1', '00:30:00', 1, '[1,2,3,4,5,6,7,8,9,10]', '2023-01-22T00:00:00', '2023-01-22T00:00:00'),
(1, 'Data Structures Quiz 2', '00:30:00', 1, '[11,12,13,14,15,16,17,18,19,20]', '2023-01-23T00:00:00', '2023-01-23T00:00:00'),
(1, 'Data Structures Midterm', '01:30:00', 1, '[21,22,23,24,25,26,27,28,29,30]', '2023-01-24T00:00:00', '2023-01-24T00:00:00'),
(1, 'Data Structures Final', '02:00:00', 0, '[31,32,33,34,35,36,37]', '2023-01-25T00:00:00', '2023-01-25T00:00:00'),
(2, 'Programming Basics Test', '00:45:00', 1, '[38,39,40,41,42]', '2023-01-26T00:00:00', '2023-01-26T00:00:00'),
(2, 'Control Structures Test', '00:45:00', 1, '[43,44,45,46,47]', '2023-01-27T00:00:00', '2023-01-27T00:00:00');";
        }
    }
}