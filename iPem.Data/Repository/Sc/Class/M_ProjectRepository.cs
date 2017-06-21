using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class M_ProjectRepository : IM_ProjectRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public M_ProjectRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public M_Project GetProject(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            M_Project entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Project_Repository_GetProject, parms)) {
                if (rdr.Read()) {
                    entity = new M_Project();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Responsible = SqlTypeConverter.DBNullStringHandler(rdr["Responsible"]);
                    entity.ContactPhone = SqlTypeConverter.DBNullStringHandler(rdr["ContactPhone"]);
                    entity.Company = SqlTypeConverter.DBNullStringHandler(rdr["Company"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<M_Project> GetProjects() {
            var entities = new List<M_Project>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Project_Repository_GetProjects, null)) {
                while(rdr.Read()) {
                    var entity = new M_Project();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Responsible = SqlTypeConverter.DBNullStringHandler(rdr["Responsible"]);
                    entity.ContactPhone = SqlTypeConverter.DBNullStringHandler(rdr["ContactPhone"]);
                    entity.Company = SqlTypeConverter.DBNullStringHandler(rdr["Company"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<M_Project> GetProjectsInSpan(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@StartTime",SqlDbType.DateTime),
                                     new SqlParameter("@EndTime",SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<M_Project>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Project_Repository_GetProjectsInSpan, parms)) {
                while(rdr.Read()) {
                    var entity = new M_Project();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Responsible = SqlTypeConverter.DBNullStringHandler(rdr["Responsible"]);
                    entity.ContactPhone = SqlTypeConverter.DBNullStringHandler(rdr["ContactPhone"]);
                    entity.Company = SqlTypeConverter.DBNullStringHandler(rdr["Company"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(M_Project entity) {
            SqlParameter[] parms ={ new SqlParameter("@Id",SqlDbType.VarChar,100),
                                    new SqlParameter("@Name",SqlDbType.VarChar,200),
                                    new SqlParameter("@StartTime",SqlDbType.DateTime),
                                    new SqlParameter("@EndTime",SqlDbType.DateTime),
                                    new SqlParameter("@Responsible",SqlDbType.VarChar,100),
                                    new SqlParameter("@ContactPhone",SqlDbType.VarChar,100),
                                    new SqlParameter("@Company",SqlDbType.VarChar,100),
                                    new SqlParameter("@Creator",SqlDbType.VarChar,100),
                                    new SqlParameter("@CreatedTime",SqlDbType.DateTime),
                                    new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                    new SqlParameter("@Enabled", SqlDbType.Bit) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                    parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                    parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.StartTime);
                    parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.EndTime);
                    parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Responsible);
                    parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.ContactPhone);
                    parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Company);
                    parms[7].Value = SqlTypeConverter.DBNullStringChecker(entity.Creator);
                    parms[8].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                    parms[9].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                    parms[10].Value = entity.Enabled;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_Project_Repository_Insert, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(M_Project entity) {
            SqlParameter[] parms = { new SqlParameter("@Id",SqlDbType.VarChar,100),
                                     new SqlParameter("@Name",SqlDbType.VarChar,200),
                                     new SqlParameter("@StartTime",SqlDbType.DateTime),
                                     new SqlParameter("@EndTime",SqlDbType.DateTime),
                                     new SqlParameter("@Responsible",SqlDbType.VarChar,100),
                                     new SqlParameter("@ContactPhone",SqlDbType.VarChar,100),
                                     new SqlParameter("@Company",SqlDbType.VarChar,100),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                    parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                    parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.StartTime);
                    parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.EndTime);
                    parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Responsible);
                    parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.ContactPhone);
                    parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Company);
                    parms[7].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                    parms[8].Value = entity.Enabled;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_Project_Repository_Update, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion

    }
}