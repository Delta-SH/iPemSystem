using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
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
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Point_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmNodeHandler(rdr["Type"]);
                    entity.LogicType = new LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.SubLogicType = new SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Unit = SqlTypeConverter.DBNullStringHandler(rdr["Unit"]);
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        /// <summary>
        /// Gets entities from the repository by the specific protocol
        /// </summary>
        /// <param name="protocol">the protocol</param>
        /// <returns>the point list</returns>
        public List<Point> GetEntitiesByProtocol(string protocol) {
            SqlParameter[] parms = { new SqlParameter("@ProtocolId", SqlDbType.VarChar, 100) };
            parms[0].Value = protocol;

            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Point_Repository_GetEntitiesByProtcol, parms)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmNodeHandler(rdr["Type"]);
                    entity.LogicType = new LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.SubLogicType = new SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Unit = SqlTypeConverter.DBNullStringHandler(rdr["Unit"]);
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
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
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Point_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmNodeHandler(rdr["Type"]);
                    entity.LogicType = new LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.SubLogicType = new SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Unit = SqlTypeConverter.DBNullStringHandler(rdr["Unit"]);
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
