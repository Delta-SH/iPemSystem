using iPem.Core.Domain.Master;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class ProtocolRepository : IProtocolRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProtocolRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<Protocol> GetEntities(string deviceType) {
            SqlParameter[] parms = { new SqlParameter("@DeviceTypeId", SqlDbType.Int) };
            parms[0].Value = deviceType;

            var entities = new List<Protocol>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Protocol_Repository_GetEntitiesByDeviceType, parms)) {
                while(rdr.Read()) {
                    var entity = new Protocol();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]);
                    entity.SubDevTypeId = SqlTypeConverter.DBNullStringHandler(rdr["SubDevTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Protocol> GetEntities(string deviceType, string subDevType) {
            SqlParameter[] parms = { new SqlParameter("@DeviceTypeId", SqlDbType.Int),
                                     new SqlParameter("@SubDevTypeId", SqlDbType.Int) };

            parms[0].Value = deviceType;
            parms[1].Value = subDevType;

            var entities = new List<Protocol>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Protocol_Repository_GetEntitiesByDevAndSub, parms)) {
                while(rdr.Read()) {
                    var entity = new Protocol();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]);
                    entity.SubDevTypeId = SqlTypeConverter.DBNullStringHandler(rdr["SubDevTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Protocol> GetEntities() {
            var entities = new List<Protocol>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Protocol_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Protocol();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]);
                    entity.SubDevTypeId = SqlTypeConverter.DBNullStringHandler(rdr["SubDevTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
