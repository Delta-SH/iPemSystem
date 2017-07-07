using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class H_IStationRepository : IH_IStationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_IStationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_IStation> GetStations(DateTime date) {
            var entities = new List<H_IStation>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, string.Format(SqlCommands_Cs.Sql_H_IStation_Repository_GetStations, date.ToString("yyyyMM")), null)) {
                while (rdr.Read()) {
                    var entity = new H_IStation();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
