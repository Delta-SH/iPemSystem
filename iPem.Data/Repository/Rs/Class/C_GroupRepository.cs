using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class C_GroupRepository : IC_GroupRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        public C_GroupRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public C_Group GetGroup(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            C_Group entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_Group_Repository_GetGroup, parms)) {
                if (rdr.Read()) {
                    entity = new C_Group();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.ChangeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ChangeTime"]);
                    entity.LastTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Status = SqlTypeConverter.DBNullBooleanHandler(rdr["Status"]);
                }
            }
            return entity;
        }

        public List<C_Group> GetGroups() {
            var entities = new List<C_Group>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_Group_Repository_GetGroups, null)) {
                while (rdr.Read()) {
                    var entity = new C_Group();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["Type"]);
                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.ChangeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ChangeTime"]);
                    entity.LastTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Status = SqlTypeConverter.DBNullBooleanHandler(rdr["Status"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
