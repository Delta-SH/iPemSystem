using System;
using System.IO;

namespace iPem.Data.Installation {
    public partial interface IDbInstaller {
        string CheckPermissions();

        void InstallDatabase(string connectionString, string databaseName, string filePath);

        void ExecuteScript(string connectionString, string filePath);

        void ExecuteScript(string connectionString, Stream stream);
    }
}
