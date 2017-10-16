using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class M_AuthorizationRepository : IM_AuthorizationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public M_AuthorizationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<M_Authorization> GetEntities() {
            var entities = new List<M_Authorization>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_M_Authorization_Repository_GetEntities, null)) {
                while (rdr.Read()) {
                    var entity = new M_Authorization();
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["HexCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LimitTime"]);
                    entity.Password = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<M_Authorization> GetEntitiesInType(EnmEmpType type) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int) };
            parms[0].Value = (int)type;

            var entities = new List<M_Authorization>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_M_Authorization_Repository_GetEntitiesInType, parms)) {
                while (rdr.Read()) {
                    var entity = new M_Authorization();
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["HexCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LimitTime"]);
                    entity.Password = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<M_Authorization> GetEntitiesInCard(string card) {
            SqlParameter[] parms = { new SqlParameter("@CardId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(card);

            var entities = new List<M_Authorization>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_M_Authorization_Repository_GetEntitiesInCard, parms)) {
                while (rdr.Read()) {
                    var entity = new M_Authorization();
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["HexCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LimitTime"]);
                    entity.Password = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
