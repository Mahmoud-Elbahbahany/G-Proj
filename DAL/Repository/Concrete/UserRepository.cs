using DAL.Entity.Sys;
using DAL.Enum.Sys;
using DAL.Repository.Interfaces;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace DAL.Repository.Concrete
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public UserRepository()
        {
            // Configure database paths directly
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string databaseDir = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\Database\_Main\"));
            _databasePath = Path.Combine(databaseDir, "Security.db");
            _connectionString = $"Data Source={_databasePath};Version=3;";

            // Ensure directories and database exist
            InitializeDatabaseStructure();
            InitializeDatabase();
        }

        #region Database Initialization
        private void InitializeDatabaseStructure()
        {
            // Create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(_databasePath));

            // Create database file if it doesn't exist
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
                    CREATE TABLE IF NOT EXISTS Users (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Email TEXT NOT NULL UNIQUE,
                        HashedPassword TEXT NOT NULL,
                        Role INTEGER NOT NULL,
                        CreatedAt TEXT NOT NULL,
                        UpdatedAt TEXT NOT NULL
                    );
                    CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);";
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region CRUD Operations
        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Users", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(MapReaderToUser(reader));
                    }
                }
            }
            return users;
        }

        public User GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Users WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToUser(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Add(User entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    INSERT INTO Users (Name, Email, HashedPassword, Role, CreatedAt, UpdatedAt)
                    VALUES (@Name, @Email, @HashedPassword, @Role, @CreatedAt, @UpdatedAt)", connection))
                {
                    AddUserParameters(command, entity);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(User entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"
                    UPDATE Users 
                    SET Name = @Name, 
                        Email = @Email, 
                        HashedPassword = @HashedPassword, 
                        Role = @Role, 
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @Id", connection))
                {
                    AddUserParameters(command, entity);
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
                using (var command = new SQLiteCommand("DELETE FROM Users WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Helper Methods
        private User MapReaderToUser(SQLiteDataReader reader)
        {
            return new User(
                reader.GetInt32(0), // ID
                reader.GetString(1), // Name
                reader.GetString(2), // Email
                reader.GetString(3), // HashedPassword
                (UserRole)reader.GetInt32(4)) // Role
            {
                CreatedAt = reader.GetString(5),
                UpdatedAt = reader.GetString(6)
            };
        }

        private void AddUserParameters(SQLiteCommand command, User user)
        {
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@HashedPassword", user.HashedPassword);
            command.Parameters.AddWithValue("@Role", (int)user.Role);
            command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", user.UpdatedAt);
        }
        #endregion

        #region Additional User-specific Methods
        public User GetByEmail(string email)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Users WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToUser(reader);
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}