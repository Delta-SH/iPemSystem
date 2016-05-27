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

        public virtual List<IdValuePair<string, string>> GetEntities() {
            var entities = new List<IdValuePair<string, string>>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_PointsInProtocol_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    entities.Add(new IdValuePair<string, string>() {
                        Id = SqlTypeConverter.DBNullStringHandler(rdr["ProtcolId"]),
                        Value = SqlTypeConverter.DBNullStringHandler(rdr["PointId"])
                    });
                }
            }
            return entities;
        }

        public virtual List<IdValuePair<string, string>> GetRelation() {
            var entities = new List<IdValuePair<string, string>>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_PointsInProtocol_Repository_GetRelation, null)) {
                while(rdr.Read()) {
                    entities.Add(new IdValuePair<string, string>() {
                        Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]),
                        Value = SqlTypeConverter.DBNullStringHandler(rdr["PointId"])
                    });
                }
            }
            return entities;
        }

        #endregion
 
    }
}
