using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class ProductorRepository : IProductorRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProductorRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Productor GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Productor entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Productor_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Productor();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.Fax = SqlTypeConverter.DBNullStringHandler(rdr["Fax"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Productor> GetEntities() {
            var entities = new List<Productor>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Productor_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Productor();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.Fax = SqlTypeConverter.DBNullStringHandler(rdr["Fax"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
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
