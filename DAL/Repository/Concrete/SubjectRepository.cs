using DAL.Entity.SubjectHandling;
using DAL.Repository.Interfaces;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace DAL.Repository.Concrete
{
    public class SubjectRepository : IRepository<Subject>
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public SubjectRepository()
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
                    CREATE INDEX IF NOT EXISTS idx_subjects_code ON Subjects(CodeID);";
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region CRUD Operations
        public IEnumerable<Subject> GetAll()
        {
            var subjects = new List<Subject>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Subjects", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(MapReaderToSubject(reader));
                    }
                }
            }
            return subjects;
        }

        public Subject GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Subjects WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToSubject(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Subject entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    INSERT INTO Subjects 
                    (InstructorID, CodeID, Name, Description, QuestionsBankIDs, CreatedAt, UpdatedAt)
                    VALUES 
                    (@InstructorID, @CodeID, @Name, @Description, @QuestionsBankIDs, @CreatedAt, @UpdatedAt)",
                    connection))
                {
                    AddSubjectParameters(command, entity);
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";
                    entity.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Update(Subject entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    UPDATE Subjects 
                    SET InstructorID = @InstructorID, 
                        CodeID = @CodeID, 
                        Name = @Name, 
                        Description = @Description, 
                        QuestionsBankIDs = @QuestionsBankIDs, 
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @Id", connection))
                {
                    AddSubjectParameters(command, entity);
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
                using (var command = new SQLiteCommand("DELETE FROM Subjects WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Helper Methods
        private Subject MapReaderToSubject(SQLiteDataReader reader)
        {
            var questionsBankIDsJson = reader.GetString(5);
            var questionsBankIDs = JsonConvert.DeserializeObject<List<int>>(questionsBankIDsJson);

            return new Subject(
                reader.GetInt32(0), // ID
                reader.GetInt32(1), // InstructorID
                reader.GetString(2), // CodeID
                reader.GetString(3), // Name
                reader.IsDBNull(4) ? string.Empty : reader.GetString(4), // Description
                questionsBankIDs // QuestionsBankIDs
            )
            {
                CreatedAt = reader.GetString(6),
                UpdatedAt = reader.GetString(7)
            };
        }

        private void AddSubjectParameters(SQLiteCommand command, Subject subject)
        {
            command.Parameters.AddWithValue("@InstructorID", subject.InstructorID);
            command.Parameters.AddWithValue("@CodeID", subject.CodeID);
            command.Parameters.AddWithValue("@Name", subject.Name);
            command.Parameters.AddWithValue("@Description",
                string.IsNullOrEmpty(subject.Description) ? DBNull.Value : (object)subject.Description);
            command.Parameters.AddWithValue("@QuestionsBankIDs", JsonConvert.SerializeObject(subject.QuestionsBankIDs));
            command.Parameters.AddWithValue("@CreatedAt", subject.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", subject.UpdatedAt);
        }
        #endregion

        #region Additional Methods
        public IEnumerable<Subject> GetByInstructorId(int instructorId)
        {
            var subjects = new List<Subject>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Subjects WHERE InstructorID = @InstructorId", connection))
                {
                    command.Parameters.AddWithValue("@InstructorId", instructorId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(MapReaderToSubject(reader));
                        }
                    }
                }
            }
            return subjects;
        }

        public Subject GetByCode(string code)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Subjects WHERE CodeID = @Code", connection))
                {
                    command.Parameters.AddWithValue("@Code", code);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToSubject(reader);
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}