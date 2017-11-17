using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class H_NoteRepository : IH_NoteRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_NoteRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_Note> GetEntities() {
            var entities = new List<H_Note>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_H_Note_Repository_GetEntities, null)) {
                while (rdr.Read()) {
                    var entity = new H_Note();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.SysType = SqlTypeConverter.DBNullInt32Handler(rdr["SysType"]);
                    entity.GroupID = SqlTypeConverter.DBNullStringHandler(rdr["GroupID"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DtType = SqlTypeConverter.DBNullInt32Handler(rdr["DtType"]);
                    entity.OpType = SqlTypeConverter.DBNullInt32Handler(rdr["OpType"]);
                    entity.Time = SqlTypeConverter.DBNullDateTimeHandler(rdr["Time"]);
                    entity.Desc = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<H_Note> entities) {
            SqlParameter[] parms = { new SqlParameter("@SysType", SqlDbType.Int),
                                     new SqlParameter("@GroupID", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar),
                                     new SqlParameter("@DtType", SqlDbType.Int),
                                     new SqlParameter("@OpType", SqlDbType.Int),
                                     new SqlParameter("@Time", SqlDbType.DateTime),
                                     new SqlParameter("@Desc", SqlDbType.Text) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullInt32Checker(entity.SysType);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.GroupID);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[3].Value = SqlTypeConverter.DBNullInt32Checker(entity.DtType);
                        parms[4].Value = SqlTypeConverter.DBNullInt32Checker(entity.OpType);
                        parms[5].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.Time);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Desc);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_H_Note_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<H_Note> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.Int) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullInt32Checker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_H_Note_Repository_Delete, parms);
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
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_H_Note_Repository_Clear, null);
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
