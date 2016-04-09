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

        /// <summary>
        /// Gets entities from the repository by the specific device
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>the point list</returns>
        public List<Point> GetEntitiesByDevice(string device) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar,100) };
            parms[0].Value = device;

            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Point_Repository_GetEntitiesByDevice, parms)) {
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

        /// <summary>
        /// Gets entities from the repository by the specific types
        /// </summary>
        /// <param name="type">the point type array</param>
        /// <returns>the point list</returns>
        public List<Point> GetEntitiesByType(int type) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int) };
            parms[0].Value = type;

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

        /// <summary>
        /// Gets entities from the repository by the specific protcol
        /// </summary>
        /// <param name="protcol">the protcol</param>
        /// <returns>the point list</returns>
        public List<Point> GetEntitiesByProtcol(int protcol) {
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

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>the point list</returns>
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
