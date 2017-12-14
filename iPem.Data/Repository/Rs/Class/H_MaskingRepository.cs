using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace iPem.Data.Repository.Rs {
    public partial class H_MaskingRepository : IH_MaskingRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_MaskingRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_Masking> GetEntities() {
            var entities = new List<H_Masking>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_H_Masking_Repository_GetEntities, null)) {
                while (rdr.Read()) {
                    var entity = new H_Masking();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullMaskTypeHandler(rdr["Type"]);
                    entity.Time = SqlTypeConverter.DBNullDateTimeHandler(rdr["Time"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
