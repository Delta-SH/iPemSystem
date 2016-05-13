using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class StationRepository : IStationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Station GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Station entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Station_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Station();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.Longitude = SqlTypeConverter.DBNullStringHandler(rdr["Longitude"]);
                    entity.Latitude = SqlTypeConverter.DBNullStringHandler(rdr["Latitude"]);
                    entity.Altitude = SqlTypeConverter.DBNullStringHandler(rdr["Altitude"]);
                    entity.CityElecLoadTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["CityElecLoadTypeID"]);
                    entity.CityElecCap = SqlTypeConverter.DBNullStringHandler(rdr["CityElecCap"]);
                    entity.CityElecLoad = SqlTypeConverter.DBNullStringHandler(rdr["CityElecLoad"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.LineRadiusSize = SqlTypeConverter.DBNullStringHandler(rdr["LineRadiusSize"]);
                    entity.LineLength = SqlTypeConverter.DBNullStringHandler(rdr["LineLength"]);
                    entity.SuppPowerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["SuppPowerTypeID"]);
                    entity.TranInfo = SqlTypeConverter.DBNullStringHandler(rdr["TranInfo"]);
                    entity.TranContNo = SqlTypeConverter.DBNullStringHandler(rdr["TranContNo"]);
                    entity.TranPhone = SqlTypeConverter.DBNullStringHandler(rdr["TranPhone"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Station> GetEntities() {
            var entities = new List<Station>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Station_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Station();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.Longitude = SqlTypeConverter.DBNullStringHandler(rdr["Longitude"]);
                    entity.Latitude = SqlTypeConverter.DBNullStringHandler(rdr["Latitude"]);
                    entity.Altitude = SqlTypeConverter.DBNullStringHandler(rdr["Altitude"]);
                    entity.CityElecLoadTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["CityElecLoadTypeID"]);
                    entity.CityElecCap = SqlTypeConverter.DBNullStringHandler(rdr["CityElecCap"]);
                    entity.CityElecLoad = SqlTypeConverter.DBNullStringHandler(rdr["CityElecLoad"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.LineRadiusSize = SqlTypeConverter.DBNullStringHandler(rdr["LineRadiusSize"]);
                    entity.LineLength = SqlTypeConverter.DBNullStringHandler(rdr["LineLength"]);
                    entity.SuppPowerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["SuppPowerTypeID"]);
                    entity.TranInfo = SqlTypeConverter.DBNullStringHandler(rdr["TranInfo"]);
                    entity.TranContNo = SqlTypeConverter.DBNullStringHandler(rdr["TranContNo"]);
                    entity.TranPhone = SqlTypeConverter.DBNullStringHandler(rdr["TranPhone"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Station> GetEntitiesInParent(string parent) {
            SqlParameter[] parms = { new SqlParameter("@AreaId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(parent);

            var entities = new List<Station>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Station_Repository_GetEntitiesInParent, parms)) {
                while(rdr.Read()) {
                    var entity = new Station();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.Longitude = SqlTypeConverter.DBNullStringHandler(rdr["Longitude"]);
                    entity.Latitude = SqlTypeConverter.DBNullStringHandler(rdr["Latitude"]);
                    entity.Altitude = SqlTypeConverter.DBNullStringHandler(rdr["Altitude"]);
                    entity.CityElecLoadTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["CityElecLoadTypeID"]);
                    entity.CityElecCap = SqlTypeConverter.DBNullStringHandler(rdr["CityElecCap"]);
                    entity.CityElecLoad = SqlTypeConverter.DBNullStringHandler(rdr["CityElecLoad"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.LineRadiusSize = SqlTypeConverter.DBNullStringHandler(rdr["LineRadiusSize"]);
                    entity.LineLength = SqlTypeConverter.DBNullStringHandler(rdr["LineLength"]);
                    entity.SuppPowerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["SuppPowerTypeID"]);
                    entity.TranInfo = SqlTypeConverter.DBNullStringHandler(rdr["TranInfo"]);
                    entity.TranContNo = SqlTypeConverter.DBNullStringHandler(rdr["TranContNo"]);
                    entity.TranPhone = SqlTypeConverter.DBNullStringHandler(rdr["TranPhone"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
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
