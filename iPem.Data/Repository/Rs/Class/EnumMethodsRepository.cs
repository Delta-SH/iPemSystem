using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class EnumMethodsRepository : IEnumMethodsRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        public EnumMethodsRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public EnumMethods GetEntity(int id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.Int) };
            parms[0].Value = SqlTypeConverter.DBNullInt32Checker(id);

            EnumMethods entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_EnumMethods_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new EnumMethods();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TypeId"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                }
            }
            return entity;
        }

        public List<EnumMethods> GetEntities(EnmMethodType type, string comment) {
            SqlParameter[] parms = { new SqlParameter("@TypeId", SqlDbType.Int),
                                     new SqlParameter("@Comment", SqlDbType.VarChar, 512) };

            parms[0].Value = (int)type;
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(comment);

            var entities = new List<EnumMethods>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_EnumMethods_Repository_GetEntitiesByType, parms)) {
                while(rdr.Read()) {
                    var entity = new EnumMethods();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TypeId"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<EnumMethods> GetEntities() {
            var entities = new List<EnumMethods>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_EnumMethods_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new EnumMethods();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TypeId"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
