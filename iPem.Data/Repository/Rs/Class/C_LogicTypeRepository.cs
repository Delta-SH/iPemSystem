using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class C_LogicTypeRepository : IC_LogicTypeRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        public C_LogicTypeRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public C_LogicType GetLogicType(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            C_LogicType entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_LogicType_Repository_GetLogicType, parms)) {
                if(rdr.Read()) {
                    entity = new C_LogicType();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]);
                }
            }
            return entity;
        }

        public C_SubLogicType GetSubLogicType(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            C_SubLogicType entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_LogicType_Repository_GetSubLogicType, parms)) {
                if(rdr.Read()) {
                    entity = new C_SubLogicType();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.LogicTypeId = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]);
                }
            }
            return entity;
        }

        public List<C_LogicType> GetLogicTypes() {
            var entities = new List<C_LogicType>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_LogicType_Repository_GetLogicTypes, null)) {
                while(rdr.Read()) {
                    var entity = new C_LogicType();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<C_SubLogicType> GetSubLogicTypes() {
            var entities = new List<C_SubLogicType>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_LogicType_Repository_GetSubLogicTypes, null)) {
                while(rdr.Read()) {
                    var entity = new C_SubLogicType();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.LogicTypeId = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
