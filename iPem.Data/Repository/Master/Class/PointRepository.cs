using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class PointRepository : IPointRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public PointRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<Point> GetEntities(EnmNode nodeType) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int) };
            parms[0].Value = (int)nodeType;

            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Point_Repository_GetEntitiesByType, parms)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmNodeHandler(rdr["Type"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["StaTypeId"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["DeviceTypeId"]);
                    entity.LogicTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["LogicTypeId"]);
                    entity.Unit = SqlTypeConverter.DBNullStringHandler(rdr["Unit"]);
                    entity.AlarmTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmTimeDesc"]);
                    entity.NormalTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalTimeDesc"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerType = SqlTypeConverter.DBNullStringHandler(rdr["TriggerType"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Desc = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Point> GetEntities(int protcol) {
            SqlParameter[] parms = { new SqlParameter("@ProtcolId", SqlDbType.Int) };

            parms[0].Value = protcol;

            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Point_Repository_GetEntitiesByProtcol, parms)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmNodeHandler(rdr["Type"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["StaTypeId"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["DeviceTypeId"]);
                    entity.LogicTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["LogicTypeId"]);
                    entity.Unit = SqlTypeConverter.DBNullStringHandler(rdr["Unit"]);
                    entity.AlarmTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmTimeDesc"]);
                    entity.NormalTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalTimeDesc"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerType = SqlTypeConverter.DBNullStringHandler(rdr["TriggerType"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Desc = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Point> GetEntities(int protcol, EnmNode nodeType) {
            SqlParameter[] parms = { new SqlParameter("@ProtcolId", SqlDbType.Int),
                                     new SqlParameter("@Type", SqlDbType.Int) };

            parms[0].Value = protcol;
            parms[1].Value = (int)nodeType;

            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Point_Repository_GetEntitiesByProtcolAndType, parms)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmNodeHandler(rdr["Type"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["StaTypeId"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["DeviceTypeId"]);
                    entity.LogicTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["LogicTypeId"]);
                    entity.Unit = SqlTypeConverter.DBNullStringHandler(rdr["Unit"]);
                    entity.AlarmTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmTimeDesc"]);
                    entity.NormalTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalTimeDesc"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerType = SqlTypeConverter.DBNullStringHandler(rdr["TriggerType"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Desc = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Point> GetEntities() {
            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Point_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmNodeHandler(rdr["Type"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["StaTypeId"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["DeviceTypeId"]);
                    entity.LogicTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["LogicTypeId"]);
                    entity.Unit = SqlTypeConverter.DBNullStringHandler(rdr["Unit"]);
                    entity.AlarmTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmTimeDesc"]);
                    entity.NormalTimeDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalTimeDesc"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerType = SqlTypeConverter.DBNullStringHandler(rdr["TriggerType"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Desc = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
