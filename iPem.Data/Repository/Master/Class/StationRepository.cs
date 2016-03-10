using iPem.Core.Domain.Master;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class StationRepository : IStationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual List<Station> GetEntities() {
            var entities = new List<Station>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Station_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Station();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
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
