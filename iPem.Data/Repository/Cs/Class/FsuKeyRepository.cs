using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class FsuKeyRepository : IFsuKeyRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuKeyRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<FsuKey> GetEntities() {
            var entities = new List<FsuKey>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_FsuKey_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new FsuKey();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.ChangeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ChangeTime"]);
                    entity.LastTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastTime"]);
                    entity.Status = SqlTypeConverter.DBNullBooleanHandler(rdr["Status"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
