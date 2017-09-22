using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class V_ChannelRepository : IV_ChannelRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public V_ChannelRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<V_Channel> GetEntities(string camera) {
            SqlParameter[] parms = { new SqlParameter("@CameraId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(camera);

            var entities = new List<V_Channel>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_V_Channel_Repository_GetEntitiesInCamera, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Channel();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Mask = SqlTypeConverter.DBNullInt32Handler(rdr["Mask"]);
                    entity.Channel = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Zero = SqlTypeConverter.DBNullBooleanHandler(rdr["Zero"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.CameraId = SqlTypeConverter.DBNullStringHandler(rdr["CameraId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_Channel> GetEntities() {
            var entities = new List<V_Channel>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_V_Channel_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new V_Channel();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Mask = SqlTypeConverter.DBNullInt32Handler(rdr["Mask"]);
                    entity.Channel = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Zero = SqlTypeConverter.DBNullBooleanHandler(rdr["Zero"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entity.CameraId = SqlTypeConverter.DBNullStringHandler(rdr["CameraId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
