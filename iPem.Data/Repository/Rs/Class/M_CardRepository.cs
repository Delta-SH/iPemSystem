using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class M_CardRepository : IM_CardRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public M_CardRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public M_Card GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@CardId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            M_Card entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_M_Card_Repository_GetEntity, parms)) {
                if (rdr.Read()) {
                    entity = new M_Card();
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["HexCode"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Password = SqlTypeConverter.DBNullStringHandler(rdr["PWD"]);
                    entity.Type = SqlTypeConverter.DBNullCardTypeHandler(rdr["Type"]);
                    entity.Status = SqlTypeConverter.DBNullCardStatusHandler(rdr["Status"]);
                    entity.StatusTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StatusTime"]);
                    entity.StatusReason = SqlTypeConverter.DBNullStringHandler(rdr["StatusReason"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.Remarks = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<M_Card> GetEntities() {
            var entities = new List<M_Card>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_M_Card_Repository_GetEntities, null)) {
                while (rdr.Read()) {
                    var entity = new M_Card();
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["HexCode"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Password = SqlTypeConverter.DBNullStringHandler(rdr["PWD"]);
                    entity.Type = SqlTypeConverter.DBNullCardTypeHandler(rdr["Type"]);
                    entity.Status = SqlTypeConverter.DBNullCardStatusHandler(rdr["Status"]);
                    entity.StatusTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StatusTime"]);
                    entity.StatusReason = SqlTypeConverter.DBNullStringHandler(rdr["StatusReason"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.Remarks = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
