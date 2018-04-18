using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class C_FtpRepository : IC_FtpRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public C_FtpRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<C_Ftp> GetEntities() {
            var entities = new List<C_Ftp>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_FTP_Repository_GetFTPs, null)) {
                while (rdr.Read()) {
                    var entity = new C_Ftp();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullFtpHandler(rdr["Type"]);
                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.User = SqlTypeConverter.DBNullStringHandler(rdr["User"]);
                    entity.Password = SqlTypeConverter.DBNullStringHandler(rdr["Password"]);
                    entity.Directory = SqlTypeConverter.DBNullStringHandler(rdr["Directory"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<C_Ftp> GetEntities(EnmFtp type) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int) };
            parms[0].Value = (int)type;

            var entities = new List<C_Ftp>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_C_FTP_Repository_GetFTPInType, parms)) {
                while (rdr.Read()) {
                    var entity = new C_Ftp();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullFtpHandler(rdr["Type"]);
                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.User = SqlTypeConverter.DBNullStringHandler(rdr["User"]);
                    entity.Password = SqlTypeConverter.DBNullStringHandler(rdr["Password"]);
                    entity.Directory = SqlTypeConverter.DBNullStringHandler(rdr["Directory"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
