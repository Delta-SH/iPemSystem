using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class DictionaryRepository : IDictionaryRepository {
        
        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DictionaryRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public M_Dictionary GetEntity(int id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.Int) };
            parms[0].Value = id;

            M_Dictionary entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Dictionary_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new M_Dictionary();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]);
                    entity.ValuesBinary = SqlTypeConverter.DBNullBytesHandler(rdr["ValuesBinary"]);
                    entity.LastUpdatedDate = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastUpdatedDate"]);
                }
            }
            return entity;
        }

        public List<M_Dictionary> GetEntities() {
            var entities = new List<M_Dictionary>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Dictionary_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new M_Dictionary();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]);
                    entity.ValuesBinary = SqlTypeConverter.DBNullBytesHandler(rdr["ValuesBinary"]);
                    entity.LastUpdatedDate = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastUpdatedDate"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Update(M_Dictionary entity) {
            Update(new List<M_Dictionary>() { entity });
        }

        public void Update(List<M_Dictionary> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.Int),
                                     new SqlParameter("@Name", SqlDbType.VarChar,100),
                                     new SqlParameter("@ValuesJson", SqlDbType.NText),
                                     new SqlParameter("@ValuesBinary", SqlDbType.Image),
                                     new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime)};

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullInt32Checker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.ValuesJson);
                        parms[3].Value = SqlTypeConverter.DBNullBytesChecker(entity.ValuesBinary);
                        parms[4].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastUpdatedDate);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Dictionary_Repository_UpdateEntities, parms);
                    }
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