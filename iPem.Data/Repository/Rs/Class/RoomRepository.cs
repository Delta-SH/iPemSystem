﻿using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class RoomRepository : IRoomRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Room GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Room entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Room_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Room();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = new RoomType { Id = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeName"]) };
                    entity.Floor = SqlTypeConverter.DBNullInt32Handler(rdr["Floor"]);
                    entity.PropertyId = SqlTypeConverter.DBNullInt32Handler(rdr["PropertyId"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.Length = SqlTypeConverter.DBNullStringHandler(rdr["Length"]);
                    entity.Width = SqlTypeConverter.DBNullStringHandler(rdr["Width"]);
                    entity.Heigth = SqlTypeConverter.DBNullStringHandler(rdr["Heigth"]);
                    entity.FloorLoad = SqlTypeConverter.DBNullStringHandler(rdr["FloorLoad"]);
                    entity.LineHeigth = SqlTypeConverter.DBNullStringHandler(rdr["LineHeigth"]);
                    entity.Square = SqlTypeConverter.DBNullStringHandler(rdr["Square"]);
                    entity.EffeSquare = SqlTypeConverter.DBNullStringHandler(rdr["EffeSquare"]);
                    entity.FireFighEuip = SqlTypeConverter.DBNullStringHandler(rdr["FireFighEuip"]);
                    entity.Owner = SqlTypeConverter.DBNullStringHandler(rdr["Owner"]);
                    entity.QueryPhone = SqlTypeConverter.DBNullStringHandler(rdr["QueryPhone"]);
                    entity.PowerSubMain = SqlTypeConverter.DBNullStringHandler(rdr["PowerSubMain"]);
                    entity.TranSubMain = SqlTypeConverter.DBNullStringHandler(rdr["TranSubMain"]);
                    entity.EnviSubMain = SqlTypeConverter.DBNullStringHandler(rdr["EnviSubMain"]);
                    entity.FireSubMain = SqlTypeConverter.DBNullStringHandler(rdr["FireSubMain"]);
                    entity.AirSubMain = SqlTypeConverter.DBNullStringHandler(rdr["AirSubMain"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Room> GetEntities(string parent) {
            SqlParameter[] parms = { new SqlParameter("@StationId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(parent);

            var entities = new List<Room>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Room_Repository_GetEntitiesByParent, parms)) {
                while(rdr.Read()) {
                    var entity = new Room();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = new RoomType { Id = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeName"]) };
                    entity.Floor = SqlTypeConverter.DBNullInt32Handler(rdr["Floor"]);
                    entity.PropertyId = SqlTypeConverter.DBNullInt32Handler(rdr["PropertyId"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.Length = SqlTypeConverter.DBNullStringHandler(rdr["Length"]);
                    entity.Width = SqlTypeConverter.DBNullStringHandler(rdr["Width"]);
                    entity.Heigth = SqlTypeConverter.DBNullStringHandler(rdr["Heigth"]);
                    entity.FloorLoad = SqlTypeConverter.DBNullStringHandler(rdr["FloorLoad"]);
                    entity.LineHeigth = SqlTypeConverter.DBNullStringHandler(rdr["LineHeigth"]);
                    entity.Square = SqlTypeConverter.DBNullStringHandler(rdr["Square"]);
                    entity.EffeSquare = SqlTypeConverter.DBNullStringHandler(rdr["EffeSquare"]);
                    entity.FireFighEuip = SqlTypeConverter.DBNullStringHandler(rdr["FireFighEuip"]);
                    entity.Owner = SqlTypeConverter.DBNullStringHandler(rdr["Owner"]);
                    entity.QueryPhone = SqlTypeConverter.DBNullStringHandler(rdr["QueryPhone"]);
                    entity.PowerSubMain = SqlTypeConverter.DBNullStringHandler(rdr["PowerSubMain"]);
                    entity.TranSubMain = SqlTypeConverter.DBNullStringHandler(rdr["TranSubMain"]);
                    entity.EnviSubMain = SqlTypeConverter.DBNullStringHandler(rdr["EnviSubMain"]);
                    entity.FireSubMain = SqlTypeConverter.DBNullStringHandler(rdr["FireSubMain"]);
                    entity.AirSubMain = SqlTypeConverter.DBNullStringHandler(rdr["AirSubMain"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Room> GetEntities() {
            var entities = new List<Room>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Room_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Room();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = new RoomType { Id = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeName"]) };
                    entity.Floor = SqlTypeConverter.DBNullInt32Handler(rdr["Floor"]);
                    entity.PropertyId = SqlTypeConverter.DBNullInt32Handler(rdr["PropertyId"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.Length = SqlTypeConverter.DBNullStringHandler(rdr["Length"]);
                    entity.Width = SqlTypeConverter.DBNullStringHandler(rdr["Width"]);
                    entity.Heigth = SqlTypeConverter.DBNullStringHandler(rdr["Heigth"]);
                    entity.FloorLoad = SqlTypeConverter.DBNullStringHandler(rdr["FloorLoad"]);
                    entity.LineHeigth = SqlTypeConverter.DBNullStringHandler(rdr["LineHeigth"]);
                    entity.Square = SqlTypeConverter.DBNullStringHandler(rdr["Square"]);
                    entity.EffeSquare = SqlTypeConverter.DBNullStringHandler(rdr["EffeSquare"]);
                    entity.FireFighEuip = SqlTypeConverter.DBNullStringHandler(rdr["FireFighEuip"]);
                    entity.Owner = SqlTypeConverter.DBNullStringHandler(rdr["Owner"]);
                    entity.QueryPhone = SqlTypeConverter.DBNullStringHandler(rdr["QueryPhone"]);
                    entity.PowerSubMain = SqlTypeConverter.DBNullStringHandler(rdr["PowerSubMain"]);
                    entity.TranSubMain = SqlTypeConverter.DBNullStringHandler(rdr["TranSubMain"]);
                    entity.EnviSubMain = SqlTypeConverter.DBNullStringHandler(rdr["EnviSubMain"]);
                    entity.FireSubMain = SqlTypeConverter.DBNullStringHandler(rdr["FireSubMain"]);
                    entity.AirSubMain = SqlTypeConverter.DBNullStringHandler(rdr["AirSubMain"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
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