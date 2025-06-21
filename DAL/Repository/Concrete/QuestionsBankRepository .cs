using DAL.Entity.SubjectHandling;
using DAL.Repository.Interfaces;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace DAL.Repository.Concrete
{
    public class QuestionsBankRepository : IRepository<QuestionsBank>
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public QuestionsBankRepository()
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
                    CREATE INDEX IF NOT EXISTS idx_questionsbanks_active ON QuestionsBanks(IsActive);";
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region CRUD Operations
        public IEnumerable<QuestionsBank> GetAll()
        {
            var questionsBanks = new List<QuestionsBank>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM QuestionsBanks", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questionsBanks.Add(MapReaderToQuestionsBank(reader));
                    }
                }
            }
            return questionsBanks;
        }

        public QuestionsBank GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM QuestionsBanks WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToQuestionsBank(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Add(QuestionsBank entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    INSERT INTO QuestionsBanks 
                    (SubjectID, Title, Description, IsActive, QuestionsIDs, CreatedAt, UpdatedAt)
                    VALUES 
                    (@SubjectID, @Title, @Description, @IsActive, @QuestionsIDs, @CreatedAt, @UpdatedAt)",
                    connection))
                {
                    AddQuestionsBankParameters(command, entity);
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";
                    entity.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Update(QuestionsBank entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    UPDATE QuestionsBanks 
                    SET SubjectID = @SubjectID, 
                        Title = @Title, 
                        Description = @Description, 
                        IsActive = @IsActive, 
                        QuestionsIDs = @QuestionsIDs, 
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @Id", connection))
                {
                    AddQuestionsBankParameters(command, entity);
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
                using (var command = new SQLiteCommand("DELETE FROM QuestionsBanks WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Helper Methods
        private QuestionsBank MapReaderToQuestionsBank(SQLiteDataReader reader)
        {
            var questionsIDsJson = reader.GetString(5);
            var questionsIDs = JsonConvert.DeserializeObject<List<int>>(questionsIDsJson);

            return new QuestionsBank(
                reader.GetInt32(0), // ID
                reader.GetInt32(1), // SubjectID
                reader.GetString(2), // Title
                reader.IsDBNull(3) ? string.Empty : reader.GetString(3), // Description
                reader.GetBoolean(4), // IsActive
                questionsIDs // QuestionsIDs
            )
            {
                CreatedAt = reader.GetString(6),
                UpdatedAt = reader.GetString(7)
            };
        }

        private void AddQuestionsBankParameters(SQLiteCommand command, QuestionsBank questionsBank)
        {
            command.Parameters.AddWithValue("@SubjectID", questionsBank.SubjectID);
            command.Parameters.AddWithValue("@Title", questionsBank.Title);
            command.Parameters.AddWithValue("@Description",
                string.IsNullOrEmpty(questionsBank.Description) ? DBNull.Value : (object)questionsBank.Description);
            command.Parameters.AddWithValue("@IsActive", questionsBank.IsActive);
            command.Parameters.AddWithValue("@QuestionsIDs", JsonConvert.SerializeObject(questionsBank.QuestionsIDs));
            command.Parameters.AddWithValue("@CreatedAt", questionsBank.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", questionsBank.UpdatedAt);
        }
        #endregion

        #region Additional Methods
        public IEnumerable<QuestionsBank> GetBySubjectId(int subjectId)
        {
            var questionsBanks = new List<QuestionsBank>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM QuestionsBanks WHERE SubjectID = @SubjectId", connection))
                {
                    command.Parameters.AddWithValue("@SubjectId", subjectId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questionsBanks.Add(MapReaderToQuestionsBank(reader));
                        }
                    }
                }
            }
            return questionsBanks;
        }

        public IEnumerable<QuestionsBank> GetActiveBanks()
        {
            var questionsBanks = new List<QuestionsBank>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM QuestionsBanks WHERE IsActive = 1", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questionsBanks.Add(MapReaderToQuestionsBank(reader));
                    }
                }
            }
            return questionsBanks;
        }
        #endregion
    }
}