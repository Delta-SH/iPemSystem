using iPem.Core.Domain.Master;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class ProjectRepository : IProjectRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProjectRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual List<Project> GetEntities(DateTime startTime, DateTime endTime) {
            SqlParameter[] parms = { new SqlParameter("@StartTime", SqlDbType.DateTime),
                                     new SqlParameter("@EndTime", SqlDbType.DateTime) };

            parms[0].Value = startTime;
            parms[1].Value = endTime;

            var entities = new List<Project>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_WebEvent_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    //var entity = new Project();
                    //entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    //entity.Level = SqlTypeConverter.DBNullEventLevelHandler(rdr["Level"]);
                    //entity.Type = SqlTypeConverter.DBNullEventTypeHandler(rdr["Type"]);
                    //entity.ShortMessage = SqlTypeConverter.DBNullStringHandler(rdr["ShortMessage"]);
                    //entity.FullMessage = SqlTypeConverter.DBNullStringHandler(rdr["FullMessage"]);
                    //entity.IpAddress = SqlTypeConverter.DBNullStringHandler(rdr["IpAddress"]);
                    //entity.PageUrl = SqlTypeConverter.DBNullStringHandler(rdr["PageUrl"]);
                    //entity.ReferrerUrl = SqlTypeConverter.DBNullStringHandler(rdr["ReferrerUrl"]);
                    //entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    //entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    //entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
