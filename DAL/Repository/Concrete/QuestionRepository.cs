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
    public class QuestionRepository : IRepository<Question>
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public QuestionRepository()
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
                    CREATE INDEX IF NOT EXISTS idx_questions_difficulty ON Questions(Difficulty_lvl);";
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region CRUD Operations
        public IEnumerable<Question> GetAll()
        {
            var questions = new List<Question>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Questions", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(MapReaderToQuestion(reader));
                    }
                }
            }
            return questions;
        }

        public Question GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Questions WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToQuestion(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Question entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    INSERT INTO Questions 
                    (SubjectID, Type, Difficulty_lvl, Text, Answers, CorrectAnswer, CreatedAt, UpdatedAt)
                    VALUES 
                    (@SubjectID, @Type, @Difficulty_lvl, @Text, @Answers, @CorrectAnswer, @CreatedAt, @UpdatedAt)",
                    connection))
                {
                    AddQuestionParameters(command, entity);
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";
                    entity.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Update(Question entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    UPDATE Questions 
                    SET SubjectID = @SubjectID, 
                        Type = @Type, 
                        Difficulty_lvl = @Difficulty_lvl, 
                        Text = @Text, 
                        Answers = @Answers, 
                        CorrectAnswer = @CorrectAnswer,
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @Id", connection))
                {
                    AddQuestionParameters(command, entity);
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
                using (var command = new SQLiteCommand("DELETE FROM Questions WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Helper Methods
        private Question MapReaderToQuestion(SQLiteDataReader reader)
        {
            var answersJson = reader.GetString(5);
            var answers = JsonConvert.DeserializeObject<List<string>>(answersJson);

            return new Question(
                reader.GetInt32(0), // ID
                reader.GetInt32(1), // SubjectID
                (QuestionType)reader.GetInt32(2), // Type
                (QuestionDifficultyLevel)reader.GetInt32(3), // Difficulty_lvl
                reader.GetString(4), // Text
                answers, // Answers
                reader.GetString(6) // CorrectAnswer
            )
            {
                CreatedAt = reader.GetString(7),
                UpdatedAt = reader.GetString(8)
            };
        }

        private void AddQuestionParameters(SQLiteCommand command, Question question)
        {
            command.Parameters.AddWithValue("@SubjectID", question.SubjectID);
            command.Parameters.AddWithValue("@Type", (int)question.Type);
            command.Parameters.AddWithValue("@Difficulty_lvl", (int)question.Difficulty_lvl);
            command.Parameters.AddWithValue("@Text", question.Text);
            command.Parameters.AddWithValue("@Answers", JsonConvert.SerializeObject(question.Answers));
            command.Parameters.AddWithValue("@CorrectAnswer", question.CorrectAnswer);
            command.Parameters.AddWithValue("@CreatedAt", question.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", question.UpdatedAt);
        }
        #endregion

        #region Additional Methods
        public IEnumerable<Question> GetBySubjectId(int subjectId)
        {
            var questions = new List<Question>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Questions WHERE SubjectID = @SubjectId", connection))
                {
                    command.Parameters.AddWithValue("@SubjectId", subjectId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(MapReaderToQuestion(reader));
                        }
                    }
                }
            }
            return questions;
        }

        public IEnumerable<Question> GetByType(QuestionType type)
        {
            var questions = new List<Question>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Questions WHERE Type = @Type", connection))
                {
                    command.Parameters.AddWithValue("@Type", (int)type);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(MapReaderToQuestion(reader));
                        }
                    }
                }
            }
            return questions;
        }

        public IEnumerable<Question> GetByDifficulty(QuestionDifficultyLevel difficulty)
        {
            var questions = new List<Question>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Questions WHERE Difficulty_lvl = @Difficulty", connection))
                {
                    command.Parameters.AddWithValue("@Difficulty", (int)difficulty);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(MapReaderToQuestion(reader));
                        }
                    }
                }
            }
            return questions;
        }
        #endregion
    }
}