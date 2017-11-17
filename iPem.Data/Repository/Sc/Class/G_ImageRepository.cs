using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class G_ImageRepository : IG_ImageRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public G_ImageRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public G_Image GetEntity(string name) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);

            G_Image entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_GetEntity, parms)) {
                if (rdr.Read()) {
                    entity = new G_Image();
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                    entity.Content = SqlTypeConverter.DBNullBytesHandler(rdr["Content"]);
                    entity.Thumbnail = SqlTypeConverter.DBNullBytesHandler(rdr["Thumbnail"]);
                    entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                }
            }
            return entity;
        }

        public Boolean ExistEntity(string name) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);

            var result = 0;
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                var cnt = SqlHelper.ExecuteScalar(conn, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_ExistEntity, parms);
                if (cnt != null && cnt != DBNull.Value) { result = Convert.ToInt32(cnt); }
            }

            return result > 0;
        }

        public List<G_Image> GetEntities() {
            var entities = new List<G_Image>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new G_Image();
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                    entity.Content = SqlTypeConverter.DBNullBytesHandler(rdr["Content"]);
                    entity.Thumbnail = SqlTypeConverter.DBNullBytesHandler(rdr["Thumbnail"]);
                    entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<G_Image> GetEntities(IList<string> names) {
            var entities = new List<G_Image>();
            if (names.Count > 0) {
                var cmds = new string[names.Count];
                for (var i = 0; i < cmds.Length; i++) {
                    cmds[i] = string.Format("SELECT N'{0}' AS [Name]", names[i]);
                }

                var sql = String.Format(@"
                ;WITH T AS (
	                {0}
                )
                SELECT I.[Name],I.[Type],I.[Content],I.[Thumbnail],I.[UpdateMark],I.[Remark] FROM [dbo].[G_Images] I INNER JOIN T ON I.[Name] = T.[Name];", string.Join(" UNION ALL ", cmds));

                using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, sql, null)) {
                    while (rdr.Read()) {
                        var entity = new G_Image();
                        entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                        entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                        entity.Content = SqlTypeConverter.DBNullBytesHandler(rdr["Content"]);
                        entity.Thumbnail = SqlTypeConverter.DBNullBytesHandler(rdr["Thumbnail"]);
                        entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                        entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public List<G_Image> GetContents() {
            var entities = new List<G_Image>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_GetContents, null)) {
                while (rdr.Read()) {
                    var entity = new G_Image();
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                    entity.Content = SqlTypeConverter.DBNullBytesHandler(rdr["Content"]);
                    entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<G_Image> GetContents(IList<string> names) {
            var entities = new List<G_Image>();
            if (names.Count > 0) {
                var cmds = new string[names.Count];
                for (var i = 0; i < cmds.Length; i++) {
                    cmds[i] = string.Format("SELECT N'{0}' AS [Name]", names[i]);
                }

                var sql = String.Format(@"
                ;WITH T AS (
	                {0}
                )
                SELECT I.[Name],I.[Type],I.[Content],I.[UpdateMark],I.[Remark] FROM [dbo].[G_Images] I INNER JOIN T ON I.[Name] = T.[Name];", string.Join(" UNION ALL ", cmds));

                using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, sql, null)) {
                    while (rdr.Read()) {
                        var entity = new G_Image();
                        entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                        entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                        entity.Content = SqlTypeConverter.DBNullBytesHandler(rdr["Content"]);
                        entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                        entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public List<G_Image> GetThumbnails() {
            var entities = new List<G_Image>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_GetThumbnails, null)) {
                while (rdr.Read()) {
                    var entity = new G_Image();
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                    entity.Thumbnail = SqlTypeConverter.DBNullBytesHandler(rdr["Thumbnail"]);
                    entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<G_Image> GetThumbnails(IList<string> names) {
            var entities = new List<G_Image>();
            if (names.Count > 0) {
                var cmds = new string[names.Count];
                for (var i = 0; i < cmds.Length; i++) {
                    cmds[i] = string.Format("SELECT N'{0}' AS [Name]", names[i]);
                }

                var sql = String.Format(@"
                ;WITH T AS (
	                {0}
                )
                SELECT I.[Name],I.[Type],I.[Thumbnail],I.[UpdateMark],I.[Remark] FROM [dbo].[G_Images] I INNER JOIN T ON I.[Name] = T.[Name];", string.Join(" UNION ALL ", cmds));

                using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, sql, null)) {
                    while (rdr.Read()) {
                        var entity = new G_Image();
                        entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                        entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                        entity.Thumbnail = SqlTypeConverter.DBNullBytesHandler(rdr["Thumbnail"]);
                        entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                        entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public List<G_Image> GetNames() {
            var entities = new List<G_Image>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_GetNames, null)) {
                while (rdr.Read()) {
                    var entity = new G_Image();
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                    entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<G_Image> GetNames(IList<string> names) {
            var entities = new List<G_Image>();
            if (names.Count > 0) {
                var cmds = new string[names.Count];
                for (var i = 0; i < cmds.Length; i++) {
                    cmds[i] = string.Format("SELECT N'{0}' AS [Name]", names[i]);
                }

                var sql = String.Format(@"
                ;WITH T AS (
	                {0}
                )
                SELECT I.[Name],I.[Type],I.[UpdateMark],I.[Remark] FROM [dbo].[G_Images] I INNER JOIN T ON I.[Name] = T.[Name];", string.Join(" UNION ALL ", cmds));

                using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, sql, null)) {
                    while (rdr.Read()) {
                        var entity = new G_Image();
                        entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                        entity.Type = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                        entity.UpdateMark = SqlTypeConverter.DBNullStringHandler(rdr["UpdateMark"]);
                        entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public void Insert(IList<G_Image> entities) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@Content", SqlDbType.Image),
                                     new SqlParameter("@Thumbnail", SqlDbType.Image),
                                     new SqlParameter("@UpdateMark", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Remark", SqlDbType.VarChar, 512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[1].Value = SqlTypeConverter.DBNullInt32Checker(entity.Type);
                        parms[2].Value = SqlTypeConverter.DBNullBytesChecker(entity.Content);
                        parms[3].Value = SqlTypeConverter.DBNullBytesChecker(entity.Thumbnail);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.UpdateMark);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<G_Image> entities) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@Content", SqlDbType.Image),
                                     new SqlParameter("@Thumbnail", SqlDbType.Image),
                                     new SqlParameter("@UpdateMark", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Remark", SqlDbType.VarChar, 512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[1].Value = SqlTypeConverter.DBNullInt32Checker(entity.Type);
                        parms[2].Value = SqlTypeConverter.DBNullBytesChecker(entity.Content);
                        parms[3].Value = SqlTypeConverter.DBNullBytesChecker(entity.Thumbnail);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.UpdateMark);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<string> names) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var name in names) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_Delete, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Clear() {
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Image_Repository_Clear, null);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion

    }
}
