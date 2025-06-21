using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Sys.Core;

namespace DbL.DatabaseHandler
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;

    namespace DbL.DatabaseHandler
    {
        public static class DatabaseConfiger
        {
            #region Begin Enums
            private enum DatabaseType
            {
                Security,
                AcademicContent,
                Results,
                Processing,
                Utilities,
                Sync,
                Analytics
            }
            #endregion

            #region Begin Properties
            private static readonly string BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            private static Dictionary<string, string> Main_Pathes;
            private static Dictionary<string, string> Files_Pathes;
            private static Dictionary<string, string> Connection_Strings;
            #endregion

            #region Public Access Methods
            public static string GetSecurityDatabaseConnection()
            {
                InitializeIfNeeded();
                return Connection_Strings[DatabaseType.Security.ToString()];
            }
            #endregion

            #region Initialization Methods
            private static void InitializeIfNeeded()
            {
                if (Connection_Strings == null)
                {
                    Invoke_DirectoriesBuilders();
                    Invoke_DirectoriesCreators();
                }
            }

            public static void Invoke_DirectoriesBuilders()
            {
                Build_Main_Paths();
                Build_Files_Pathes();
                Build_Connection_String();
            }

            private static void Build_Main_Paths()
            {
                try
                {
                    Main_Pathes = new Dictionary<string, string>
                {
                    { "Main", Path.GetFullPath(Path.Combine(BaseDirectoryPath, @"..\..\..\..\Database\_Main\")) },
                    { "Backup", Path.GetFullPath(Path.Combine(BaseDirectoryPath, @"..\..\..\..\Database\_BackUp\")) },
                    { "Save", Path.GetFullPath(Path.Combine(BaseDirectoryPath, @"..\..\..\..\Database\_Save\")) }
                };
                }
                catch (Exception e)
                {
                    // Log error
                    Console.WriteLine(e.ToString());
                }
            }

            private static void Build_Files_Pathes()
            {
                try
                {
                    Files_Pathes = new Dictionary<string, string>
                {
                    { DatabaseType.Security.ToString(), Path.Combine(Main_Pathes["Main"], "Security.db") },
                    { DatabaseType.AcademicContent.ToString(), Path.Combine(Main_Pathes["Main"], "AcademicContent.db") },
                    // Add other database types as needed
                };
                }
                catch (Exception e)
                {
                    // Log error
                    Console.WriteLine(e.ToString());
                }
            }

            private static void Build_Connection_String()
            {
                try
                {
                    Connection_Strings = new Dictionary<string, string>
                {
                    { DatabaseType.Security.ToString(), $"Data Source={Files_Pathes[DatabaseType.Security.ToString()]};Version=3;" },
                    { DatabaseType.AcademicContent.ToString(), $"Data Source={Files_Pathes[DatabaseType.AcademicContent.ToString()]};Version=3;" },
                    // Add other connection strings as needed
                };
                }
                catch (Exception e)
                {
                    // Log error
                    Console.WriteLine(e.ToString());
                }
            }
            #endregion

            #region Directory Creation Methods
            public static void Invoke_DirectoriesCreators()
            {
                Create_Main_Pathes();
                Create_Files_Pathes();
            }

            private static void Create_Main_Pathes()
            {
                foreach (var path in Main_Pathes.Values)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }

            private static void Create_Files_Pathes()
            {
                foreach (var filePath in Files_Pathes.Values)
                {
                    if (!File.Exists(filePath))
                    {
                        SQLiteConnection.CreateFile(filePath);
                    }
                }
            }
            #endregion

            #region Cleanup Methods
            public static void Cleanup()
            {
                Connection_Strings?.Clear();
                Files_Pathes?.Clear();
                Main_Pathes?.Clear();
            }
            #endregion
        }
    }
}
