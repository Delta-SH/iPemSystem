using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class V_ParamDiffRepository : IV_ParamDiffRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public V_ParamDiffRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<V_ParamDiff> GetDiffs(DateTime date) {
            SqlParameter[] parms = { new SqlParameter("@Date", SqlDbType.DateTime) };
            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(date);

            var entities = new List<V_ParamDiff>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_ParamDiff_Repository_GetDiffs, parms)) {
                while (rdr.Read()) {
                    var entity = new V_ParamDiff();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Threshold = SqlTypeConverter.DBNullStringHandler(rdr["Threshold"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullStringHandler(rdr["AlarmLevel"]);
                    entity.NMAlarmID = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmID"]);
                    entity.AbsoluteVal = SqlTypeConverter.DBNullStringHandler(rdr["AbsoluteVal"]);
                    entity.RelativeVal = SqlTypeConverter.DBNullStringHandler(rdr["RelativeVal"]);
                    entity.StorageInterval = SqlTypeConverter.DBNullStringHandler(rdr["StorageInterval"]);
                    entity.StorageRefTime = SqlTypeConverter.DBNullStringHandler(rdr["StorageRefTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
