using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class C_EnumMethodRepository : IC_EnumMethodRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        public C_EnumMethodRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public C_EnumMethod GetEnumById(int id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.Int) };
            parms[0].Value = SqlTypeConverter.DBNullInt32Checker(id);

            C_EnumMethod entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_EnumMethod_Repository_GetEnumById, parms)) {
                if(rdr.Read()) {
                    entity = new C_EnumMethod();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TypeId"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                }
            }
            return entity;
        }

        public List<C_EnumMethod> GetEnumsByType(EnmMethodType type, string comment) {
            SqlParameter[] parms = { new SqlParameter("@TypeId", SqlDbType.Int),
                                     new SqlParameter("@Comment", SqlDbType.VarChar, 512) };

            parms[0].Value = (int)type;
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(comment);

            var entities = new List<C_EnumMethod>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_EnumMethod_Repository_GetEnumsByType, parms)) {
                while(rdr.Read()) {
                    var entity = new C_EnumMethod();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TypeId"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<C_EnumMethod> GetEnums() {
            var entities = new List<C_EnumMethod>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_EnumMethod_Repository_GetEnums, null)) {
                while(rdr.Read()) {
                    var entity = new C_EnumMethod();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TypeId"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
