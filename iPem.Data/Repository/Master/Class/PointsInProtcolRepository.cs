using iPem.Core;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class PointsInProtcolRepository : IPointsInProtcolRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public PointsInProtcolRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual List<IdValuePair<int, string>> GetEntities() {
            var entities = new List<IdValuePair<int, string>>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_PointsInProtocol_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    entities.Add(new IdValuePair<int, string>() {
                        Id = SqlTypeConverter.DBNullInt32Handler(rdr["ProtcolId"]),
                        Value = SqlTypeConverter.DBNullStringHandler(rdr["PointId"])
                    });
                }
            }
            return entities;
        }

        #endregion
 
    }
}
