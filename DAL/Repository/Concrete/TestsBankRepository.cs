using DAL.Entity.SubjectHandling;
using DAL.Repository.Interfaces;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace DAL.Repository.Concrete
{
    public class TestsBankRepository : IRepository<TestsBank>
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public TestsBankRepository()
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
                    CREATE INDEX IF NOT EXISTS idx_testsbanks_active ON TestsBanks(IsActive);";
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region CRUD Operations
        public IEnumerable<TestsBank> GetAll()
        {
            var testsBanks = new List<TestsBank>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM TestsBanks", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        testsBanks.Add(MapReaderToTestsBank(reader));
                    }
                }
            }
            return testsBanks;
        }

        public TestsBank GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM TestsBanks WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToTestsBank(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Add(TestsBank entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    INSERT INTO TestsBanks 
                    (InstructorID, Title, Description, IsActive, TestsIDs, CreatedAt, UpdatedAt)
                    VALUES 
                    (@InstructorID, @Title, @Description, @IsActive, @TestsIDs, @CreatedAt, @UpdatedAt)",
                    connection))
                {
                    AddTestsBankParameters(command, entity);
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";
                    entity.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Update(TestsBank entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    UPDATE TestsBanks 
                    SET InstructorID = @InstructorID, 
                        Title = @Title, 
                        Description = @Description, 
                        IsActive = @IsActive, 
                        TestsIDs = @TestsIDs, 
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @Id", connection))
                {
                    AddTestsBankParameters(command, entity);
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
                using (var command = new SQLiteCommand("DELETE FROM TestsBanks WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Helper Methods
        private TestsBank MapReaderToTestsBank(SQLiteDataReader reader)
        {
            var testsIDsJson = reader.GetString(5);
            var testsIDs = JsonConvert.DeserializeObject<List<int>>(testsIDsJson);

            return new TestsBank(
                reader.GetInt32(0), // ID
                reader.GetInt32(1), // InstructorID
                reader.GetString(2), // Title
                reader.IsDBNull(3) ? string.Empty : reader.GetString(3), // Description
                reader.GetBoolean(4), // IsActive
                testsIDs // TestsIDs
            )
            {
                CreatedAt = reader.GetString(6),
                UpdatedAt = reader.GetString(7)
            };
        }

        private void AddTestsBankParameters(SQLiteCommand command, TestsBank testsBank)
        {
            command.Parameters.AddWithValue("@InstructorID", testsBank.InstructorID);
            command.Parameters.AddWithValue("@Title", testsBank.Title);
            command.Parameters.AddWithValue("@Description",
                string.IsNullOrEmpty(testsBank.Description) ? DBNull.Value : (object)testsBank.Description);
            command.Parameters.AddWithValue("@IsActive", testsBank.IsActive);
            command.Parameters.AddWithValue("@TestsIDs", JsonConvert.SerializeObject(testsBank.TestsIDs));
            command.Parameters.AddWithValue("@CreatedAt", testsBank.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", testsBank.UpdatedAt);
        }
        #endregion

        #region Additional Methods
        public IEnumerable<TestsBank> GetByInstructorId(int instructorId)
        {
            var testsBanks = new List<TestsBank>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM TestsBanks WHERE InstructorID = @InstructorId", connection))
                {
                    command.Parameters.AddWithValue("@InstructorId", instructorId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            testsBanks.Add(MapReaderToTestsBank(reader));
                        }
                    }
                }
            }
            return testsBanks;
        }

        public IEnumerable<TestsBank> GetActiveTestsBanks()
        {
            var testsBanks = new List<TestsBank>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM TestsBanks WHERE IsActive = 1", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        testsBanks.Add(MapReaderToTestsBank(reader));
                    }
                }
            }
            return testsBanks;
        }
        #endregion
    }
}