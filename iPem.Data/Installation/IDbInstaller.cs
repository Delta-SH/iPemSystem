using System;

namespace iPem.Data.Installation {
    public partial interface IDbInstaller {
        string CheckPermissions();

        void InstallDatabase(string connectionString, string databaseName, string filePath);

        void InstallData(string connectionString, string filePath);
    }
}
