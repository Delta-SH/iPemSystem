using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class SubDeviceTypeRepository : ISubDeviceTypeRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SubDeviceTypeRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public SubDeviceType GetEntity(int id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.Int) };
            parms[0].Value = SqlTypeConverter.DBNullInt32Checker(id);

            SubDeviceType entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_SubDeviceType_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new SubDeviceType();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["DeviceTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                }
            }
            return entity;
        }

        public List<SubDeviceType> GetEntities() {
            var entities = new List<SubDeviceType>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_SubDeviceType_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new SubDeviceType();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["DeviceTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
