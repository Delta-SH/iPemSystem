using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace iPem.Core.Data {
    public partial class SqliteDataProvider : IDataProvider {

        #region Fields

        private const string filename = "Registry.db";
        private static string dataConnectionString;

        #endregion

        #region Ctor

        public SqliteDataProvider(string filePath = null) {
            if (String.IsNullOrWhiteSpace(filePath)) {
                filePath = "~/";
            }

            filePath = CommonHelper.MapPath(filePath);
            if (!Directory.Exists(filePath)) {
                Directory.CreateDirectory(filePath);
            }

            filePath = Path.Combine(filePath, filename);
            dataConnectionString = String.Format(@"Data Source={0};Version=3;Password=1qaz2wsx3edc;Pooling=True;Max Pool Size=100;FailIfMissing=False;", filePath);
            CreateRegistry();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a setting file.
        /// </summary>
        public virtual void CreateRegistry() {
            using(var conn = new SQLiteConnection(dataConnectionString)) {
                conn.Open();
                using (var command = new SQLiteCommand()) {
                    command.Connection = conn;
                    command.CommandText = SQLiteText.Registry_Create_Tables;
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets database entities.
        /// </summary>
        /// <returns>all the database entities.</returns>
        public virtual IList<DbEntity> GetEntites() {
            var entities = new List<DbEntity>();
            using(var conn = new SQLiteConnection(dataConnectionString)) {
                conn.Open();
                using (var command = new SQLiteCommand(SQLiteText.Registry_Get_Entities, conn)) {
                    using (var rdr = command.ExecuteReader(CommandBehavior.CloseConnection)) {
                        while (rdr.Read()) {
                            entities.Add(new DbEntity() {
                                Id = DbTypeConverter.DBNullGuidHandler(rdr["Id"]),
                                Provider = DbTypeConverter.DBNullDataProviderHandler(rdr["Provider"]),
                                Type = DbTypeConverter.DBNullDatabaseTypeHandler(rdr["Type"]),
                                IP = DbTypeConverter.DBNullStringHandler(rdr["IP"]),
                                Port = DbTypeConverter.DBNullInt32Handler(rdr["Port"]),
                                UId = DbTypeConverter.DBNullStringHandler(rdr["UId"]),
                                Pwd = DbTypeConverter.DBNullStringHandler(rdr["Pwd"]),
                                Name = DbTypeConverter.DBNullStringHandler(rdr["Name"])
                            });
                        }
                    }
                }
            }
            return entities;
        }

        /// <summary>
        /// Save database entities.
        /// </summary>
        public virtual void SaveEntites(IList<DbEntity> entities) {
            SQLiteParameter[] parms = { new SQLiteParameter("@Id", DbType.Guid),
                                        new SQLiteParameter("@Provider", DbType.Int32),
                                        new SQLiteParameter("@Type", DbType.Int32),
                                        new SQLiteParameter("@IP", DbType.String,128),
                                        new SQLiteParameter("@Port", DbType.Int32),
                                        new SQLiteParameter("@UId", DbType.String,50),
                                        new SQLiteParameter("@Pwd", DbType.String,50),
                                        new SQLiteParameter("@Name", DbType.String,128) };

            using(var conn = new SQLiteConnection(dataConnectionString)) {
                conn.Open();
                using (var command = new SQLiteCommand(SQLiteText.Registry_Save_Entities, conn)) {
                    foreach (var entity in entities) {
                        parms[0].Value = entity.Id;
                        parms[1].Value = (Int32)entity.Provider;
                        parms[2].Value = (Int32)entity.Type;
                        parms[3].Value = DbTypeConverter.DBNullStringChecker(entity.IP);
                        parms[4].Value = DbTypeConverter.DBNullInt32Checker(entity.Port);
                        parms[5].Value = DbTypeConverter.DBNullStringChecker(entity.UId);
                        parms[6].Value = DbTypeConverter.DBNullStringChecker(entity.Pwd);
                        parms[7].Value = DbTypeConverter.DBNullStringChecker(entity.Name);

                        command.Parameters.Clear();
                        command.Parameters.AddRange(parms);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Delete database entities.
        /// </summary>
        public virtual void DelEntites(IList<DbEntity> entities) {
            SQLiteParameter[] parms = { new SQLiteParameter("@Id", DbType.Guid),
                                        new SQLiteParameter("@Provider", DbType.Int32),
                                        new SQLiteParameter("@Type", DbType.Int32),
                                        new SQLiteParameter("@IP", DbType.String,128),
                                        new SQLiteParameter("@Port", DbType.Int32),
                                        new SQLiteParameter("@UId", DbType.String,50),
                                        new SQLiteParameter("@Pwd", DbType.String,50),
                                        new SQLiteParameter("@Name", DbType.String,128) };

            using(var conn = new SQLiteConnection(dataConnectionString)) {
                conn.Open();
                using (var command = new SQLiteCommand(SQLiteText.Registry_Del_Entities, conn)) {
                    foreach (var entity in entities) {
                        parms[0].Value = entity.Id;
                        parms[1].Value = (Int32)entity.Provider;
                        parms[2].Value = (Int32)entity.Type;
                        parms[3].Value = DbTypeConverter.DBNullStringChecker(entity.IP);
                        parms[4].Value = DbTypeConverter.DBNullInt32Checker(entity.Port);
                        parms[5].Value = DbTypeConverter.DBNullStringChecker(entity.UId);
                        parms[6].Value = DbTypeConverter.DBNullStringChecker(entity.Pwd);
                        parms[7].Value = DbTypeConverter.DBNullStringChecker(entity.Name);

                        command.Parameters.Clear();
                        command.Parameters.AddRange(parms);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// The SQLiteText class is intended to store sqlite statements.
    /// </summary>
    public static class SQLiteText {
        public const string Registry_Create_Tables = @"
        CREATE TABLE IF NOT EXISTS [DbEntities] (
            [Id] guid PRIMARY KEY NOT NULL,
            [Provider] int,
            [Type] int,
            [IP] nvarchar(128),
            [Port] int,
            [UId] nvarchar(50),
            [Pwd] nvarchar(50),
            [Name] nvarchar(128)
        );";

        public const string Registry_Get_Entities = @"
        SELECT [Id],[Provider],[Type],[IP],[Port],[UId],[Pwd],[Name] FROM [DbEntities];";

        public const string Registry_Save_Entities = @"
        DELETE FROM [DbEntities] WHERE [Type] = @Type;
        INSERT INTO [DbEntities]([Id],[Provider],[Type],[IP],[Port],[UId],[Pwd],[Name]) 
        VALUES(@Id,@Provider,@Type,@IP,@Port,@UId,@Pwd,@Name);";

        public const string Registry_Del_Entities = @"
        DELETE FROM [DbEntities] WHERE [Type] = @Type;";
    }
}
