using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;
using DAL.Repository.Interfaces;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace DAL.Repository.Concrete
{
    public class TestRepository : IRepository<Test>
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public TestRepository()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string databaseDir = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\Database\_Main\"));
            _databasePath = Path.Combine(databaseDir, "SubjectHandling.db");
            _connectionString = $"Data Source={_databasePath};Version=3;";

            InitializeDatabaseStructure();
            InitializeDatabase();
        }

        #region Database Initialization
        private void InitializeDatabaseStructure()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_databasePath));
            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
            }
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
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
                    CREATE INDEX IF NOT EXISTS idx_tests_published ON Tests(IsPublished);";
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region CRUD Operations
        public IEnumerable<Test> GetAll()
        {
            var tests = new List<Test>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Tests", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tests.Add(MapReaderToTest(reader));
                    }
                }
            }
            return tests;
        }

        public Test GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Tests WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToTest(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Test entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    INSERT INTO Tests 
                    (TestBankID, Title, Duration, IsPublished, QuestionsIDs, CreatedAt, UpdatedAt)
                    VALUES 
                    (@TestBankID, @Title, @Duration, @IsPublished, @QuestionsIDs, @CreatedAt, @UpdatedAt)",
                    connection))
                {
                    AddTestParameters(command, entity);
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";
                    entity.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Update(Test entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    UPDATE Tests 
                    SET TestBankID = @TestBankID, 
                        Title = @Title, 
                        Duration = @Duration, 
                        IsPublished = @IsPublished, 
                        QuestionsIDs = @QuestionsIDs, 
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @Id", connection))
                {
                    AddTestParameters(command, entity);
                    command.Parameters.AddWithValue("@Id", entity.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Tests WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Helper Methods
        private Test MapReaderToTest(SQLiteDataReader reader)
        {
            var questionsIDsJson = reader.GetString(5);
            var questionsIDs = JsonConvert.DeserializeObject<List<int>>(questionsIDsJson);

            return new Test(
                reader.GetInt32(0), // ID
                reader.GetInt32(1), // TestBankID
                reader.GetString(2), // Title
                reader.GetString(3), // Duration
                reader.GetBoolean(4), // IsPublished
                questionsIDs // QuestionsIDs
            )
            {
                CreatedAt = reader.GetString(6),
                UpdatedAt = reader.GetString(7)
            };
        }

        private void AddTestParameters(SQLiteCommand command, Test test)
        {
            command.Parameters.AddWithValue("@TestBankID", test.TestBankID);
            command.Parameters.AddWithValue("@Title", test.Title);
            command.Parameters.AddWithValue("@Duration", test.Duration);
            command.Parameters.AddWithValue("@IsPublished", test.IsPublished);
            command.Parameters.AddWithValue("@QuestionsIDs", JsonConvert.SerializeObject(test.QuestionsIDs));
            command.Parameters.AddWithValue("@CreatedAt", test.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", test.UpdatedAt);
        }
        #endregion

        #region Additional Methods
        public IEnumerable<Test> GetByTestBankId(int testBankId)
        {
            var tests = new List<Test>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Tests WHERE TestBankID = @TestBankId", connection))
                {
                    command.Parameters.AddWithValue("@TestBankId", testBankId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tests.Add(MapReaderToTest(reader));
                        }
                    }
                }
            }
            return tests;
        }

        public IEnumerable<Test> GetPublishedTests()
        {
            var tests = new List<Test>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Tests WHERE IsPublished = 1", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tests.Add(MapReaderToTest(reader));
                    }
                }
            }
            return tests;
        }
        #endregion
    }
}