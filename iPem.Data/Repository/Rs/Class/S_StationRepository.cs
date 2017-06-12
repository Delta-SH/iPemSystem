using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class S_StationRepository : IS_StationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public S_StationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public S_Station GetStation(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            S_Station entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_S_Station_Repository_GetStation, parms)) {
                if(rdr.Read()) {
                    entity = new S_Station();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = new C_StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeName"]) };
                    entity.Longitude = SqlTypeConverter.DBNullStringHandler(rdr["Longitude"]);
                    entity.Latitude = SqlTypeConverter.DBNullStringHandler(rdr["Latitude"]);
                    entity.Altitude = SqlTypeConverter.DBNullStringHandler(rdr["Altitude"]);
                    entity.CityElecLoadTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["CityElecLoadTypeID"]);
                    entity.CityElectNumber = SqlTypeConverter.DBNullInt32Handler(rdr["CityElectNumber"]);
                    entity.CityElecCap = SqlTypeConverter.DBNullStringHandler(rdr["CityElecCap"]);
                    entity.CityElecLoad = SqlTypeConverter.DBNullStringHandler(rdr["CityElecLoad"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.LineRadiusSize = SqlTypeConverter.DBNullStringHandler(rdr["LineRadiusSize"]);
                    entity.LineLength = SqlTypeConverter.DBNullStringHandler(rdr["LineLength"]);
                    entity.SuppPowerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["SuppPowerTypeID"]);
                    entity.TranInfo = SqlTypeConverter.DBNullStringHandler(rdr["TranInfo"]);
                    entity.TranContNo = SqlTypeConverter.DBNullStringHandler(rdr["TranContNo"]);
                    entity.TranPhone = SqlTypeConverter.DBNullStringHandler(rdr["TranPhone"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<S_Station> GetStationsInArea(string id) {
            SqlParameter[] parms = { new SqlParameter("@AreaId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<S_Station>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_S_Station_Repository_GetStationsInArea, parms)) {
                while(rdr.Read()) {
                    var entity = new S_Station();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = new C_StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeName"]) };
                    entity.Longitude = SqlTypeConverter.DBNullStringHandler(rdr["Longitude"]);
                    entity.Latitude = SqlTypeConverter.DBNullStringHandler(rdr["Latitude"]);
                    entity.Altitude = SqlTypeConverter.DBNullStringHandler(rdr["Altitude"]);
                    entity.CityElecLoadTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["CityElecLoadTypeID"]);
                    entity.CityElectNumber = SqlTypeConverter.DBNullInt32Handler(rdr["CityElectNumber"]);
                    entity.CityElecCap = SqlTypeConverter.DBNullStringHandler(rdr["CityElecCap"]);
                    entity.CityElecLoad = SqlTypeConverter.DBNullStringHandler(rdr["CityElecLoad"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.LineRadiusSize = SqlTypeConverter.DBNullStringHandler(rdr["LineRadiusSize"]);
                    entity.LineLength = SqlTypeConverter.DBNullStringHandler(rdr["LineLength"]);
                    entity.SuppPowerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["SuppPowerTypeID"]);
                    entity.TranInfo = SqlTypeConverter.DBNullStringHandler(rdr["TranInfo"]);
                    entity.TranContNo = SqlTypeConverter.DBNullStringHandler(rdr["TranContNo"]);
                    entity.TranPhone = SqlTypeConverter.DBNullStringHandler(rdr["TranPhone"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<S_Station> GetStations() {
            var entities = new List<S_Station>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_S_Station_Repository_GetStations, null)) {
                while(rdr.Read()) {
                    var entity = new S_Station();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = new C_StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeName"]) };
                    entity.Longitude = SqlTypeConverter.DBNullStringHandler(rdr["Longitude"]);
                    entity.Latitude = SqlTypeConverter.DBNullStringHandler(rdr["Latitude"]);
                    entity.Altitude = SqlTypeConverter.DBNullStringHandler(rdr["Altitude"]);
                    entity.CityElecLoadTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["CityElecLoadTypeID"]);
                    entity.CityElectNumber = SqlTypeConverter.DBNullInt32Handler(rdr["CityElectNumber"]);
                    entity.CityElecCap = SqlTypeConverter.DBNullStringHandler(rdr["CityElecCap"]);
                    entity.CityElecLoad = SqlTypeConverter.DBNullStringHandler(rdr["CityElecLoad"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.LineRadiusSize = SqlTypeConverter.DBNullStringHandler(rdr["LineRadiusSize"]);
                    entity.LineLength = SqlTypeConverter.DBNullStringHandler(rdr["LineLength"]);
                    entity.SuppPowerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["SuppPowerTypeID"]);
                    entity.TranInfo = SqlTypeConverter.DBNullStringHandler(rdr["TranInfo"]);
                    entity.TranContNo = SqlTypeConverter.DBNullStringHandler(rdr["TranContNo"]);
                    entity.TranPhone = SqlTypeConverter.DBNullStringHandler(rdr["TranPhone"]);
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
