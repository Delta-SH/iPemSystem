using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class SupplierRepository : ISupplierRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SupplierRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Supplier GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Supplier entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Supplier_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Supplier();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.Fax = SqlTypeConverter.DBNullStringHandler(rdr["Fax"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.Level = SqlTypeConverter.DBNullStringHandler(rdr["Level"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Supplier> GetEntities() {
            var entities = new List<Supplier>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Supplier_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Supplier();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.Fax = SqlTypeConverter.DBNullStringHandler(rdr["Fax"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.Level = SqlTypeConverter.DBNullStringHandler(rdr["Level"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
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
