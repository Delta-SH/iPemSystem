using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class H_CardRecordRepository : IH_CardRecordRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        public H_CardRecordRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Method

        public List<H_CardRecord> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms ={ new SqlParameter("@Start",SqlDbType.DateTime),
                                    new SqlParameter("@End",SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<H_CardRecord>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_CardRecord_Repository_GetEntities, parms)) {
                while (rdr.Read()) {
                    var entity = new H_CardRecord();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["CardId"]);
                    entity.PunchTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["PunchTime"]);
                    entity.Status = SqlTypeConverter.DBNullStringHandler(rdr["Status"]);
                    entity.Remark = SqlTypeConverter.DBNullRecRemarkHandler(rdr["Remark"]);
                    entity.Direction = SqlTypeConverter.DBNullDirectionHandler(rdr["Direction"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_CardRecord> GetEntitiesInCard(DateTime start, DateTime end, string id) {
            SqlParameter[] parms ={ new SqlParameter("@Start",SqlDbType.DateTime),
                                    new SqlParameter("@End",SqlDbType.DateTime),
                                    new SqlParameter("@CardId",SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);
            parms[2].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<H_CardRecord>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_CardRecord_Repository_GetEntitiesInCard, parms)) {
                while (rdr.Read()) {
                    var entity = new H_CardRecord();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["CardId"]);
                    entity.PunchTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["PunchTime"]);
                    entity.Status = SqlTypeConverter.DBNullStringHandler(rdr["Status"]);
                    entity.Remark = SqlTypeConverter.DBNullRecRemarkHandler(rdr["Remark"]);
                    entity.Direction = SqlTypeConverter.DBNullDirectionHandler(rdr["Direction"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_CardRecord> GetEntitiesInDevice(DateTime start, DateTime end, string id) {
            SqlParameter[] parms ={ new SqlParameter("@Start",SqlDbType.DateTime),
                                    new SqlParameter("@End",SqlDbType.DateTime),
                                    new SqlParameter("@DeviceId",SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);
            parms[2].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<H_CardRecord>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_CardRecord_Repository_GetEntitiesInDevice, parms)) {
                while (rdr.Read()) {
                    var entity = new H_CardRecord();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["CardId"]);
                    entity.PunchTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["PunchTime"]);
                    entity.Status = SqlTypeConverter.DBNullStringHandler(rdr["Status"]);
                    entity.Remark = SqlTypeConverter.DBNullRecRemarkHandler(rdr["Remark"]);
                    entity.Direction = SqlTypeConverter.DBNullDirectionHandler(rdr["Direction"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_CardRecord> GetEntitiesInRoom(DateTime start, DateTime end, string id) {
            SqlParameter[] parms ={ new SqlParameter("@Start",SqlDbType.DateTime),
                                    new SqlParameter("@End",SqlDbType.DateTime),
                                    new SqlParameter("@RoomId",SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);
            parms[2].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<H_CardRecord>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_CardRecord_Repository_GetEntitiesInRoom, parms)) {
                while (rdr.Read()) {
                    var entity = new H_CardRecord();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["CardId"]);
                    entity.PunchTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["PunchTime"]);
                    entity.Status = SqlTypeConverter.DBNullStringHandler(rdr["Status"]);
                    entity.Remark = SqlTypeConverter.DBNullRecRemarkHandler(rdr["Remark"]);
                    entity.Direction = SqlTypeConverter.DBNullDirectionHandler(rdr["Direction"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_CardRecord> GetEntitiesInStation(DateTime start, DateTime end, string id) {
            SqlParameter[] parms ={ new SqlParameter("@Start",SqlDbType.DateTime),
                                    new SqlParameter("@End",SqlDbType.DateTime),
                                    new SqlParameter("@StationId",SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);
            parms[2].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<H_CardRecord>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_CardRecord_Repository_GetEntitiesInStation, parms)) {
                while (rdr.Read()) {
                    var entity = new H_CardRecord();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["CardId"]);
                    entity.PunchTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["PunchTime"]);
                    entity.Status = SqlTypeConverter.DBNullStringHandler(rdr["Status"]);
                    entity.Remark = SqlTypeConverter.DBNullRecRemarkHandler(rdr["Remark"]);
                    entity.Direction = SqlTypeConverter.DBNullDirectionHandler(rdr["Direction"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_CardRecord> GetEntitiesInArea(DateTime start, DateTime end, string id) {
            SqlParameter[] parms ={ new SqlParameter("@Start",SqlDbType.DateTime),
                                    new SqlParameter("@End",SqlDbType.DateTime),
                                    new SqlParameter("@AreaId",SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);
            parms[2].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<H_CardRecord>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_CardRecord_Repository_GetEntitiesInArea, parms)) {
                while (rdr.Read()) {
                    var entity = new H_CardRecord();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.CardId = SqlTypeConverter.DBNullStringHandler(rdr["CardId"]);
                    entity.PunchTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["PunchTime"]);
                    entity.Status = SqlTypeConverter.DBNullStringHandler(rdr["Status"]);
                    entity.Remark = SqlTypeConverter.DBNullRecRemarkHandler(rdr["Remark"]);
                    entity.Direction = SqlTypeConverter.DBNullDirectionHandler(rdr["Direction"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
