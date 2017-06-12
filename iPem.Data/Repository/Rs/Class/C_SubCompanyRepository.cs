using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class C_SubCompanyRepository : IC_SubCompanyRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public C_SubCompanyRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public C_SubCompany GetCompany(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            C_SubCompany entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_SubCompany_Repository_GetSubCompany, parms)) {
                if(rdr.Read()) {
                    entity = new C_SubCompany();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["linkMan"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.Fax = SqlTypeConverter.DBNullStringHandler(rdr["Fax"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.Level = SqlTypeConverter.DBNullStringHandler(rdr["Level"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<C_SubCompany> GetCompanies() {
            var entities = new List<C_SubCompany>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_SubCompany_Repository_GetSubCompanies, null)) {
                while(rdr.Read()) {
                    var entity = new C_SubCompany();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["linkMan"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.Fax = SqlTypeConverter.DBNullStringHandler(rdr["Fax"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.Level = SqlTypeConverter.DBNullStringHandler(rdr["Level"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
