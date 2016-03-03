using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class EmployeeRepository : IEmployeeRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeeRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Employee GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Employee entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Employee_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Employee();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.EmpNo = SqlTypeConverter.DBNullStringHandler(rdr["EmpNo"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullInt32Handler(rdr["DutyId"]);
                    entity.ICardId = SqlTypeConverter.DBNullStringHandler(rdr["ICardId"]);
                    entity.Birthday = SqlTypeConverter.DBNullDateTimeHandler(rdr["Birthday"]);
                    entity.Degree = SqlTypeConverter.DBNullEnmDegreeHandler(rdr["Degree"]);
                    entity.Marriage = SqlTypeConverter.DBNullEnmMarriageHandler(rdr["Marriage"]);
                    entity.Nation = SqlTypeConverter.DBNullStringHandler(rdr["Nation"]);
                    entity.Provinces = SqlTypeConverter.DBNullStringHandler(rdr["Provinces"]);
                    entity.Native = SqlTypeConverter.DBNullStringHandler(rdr["Native"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.AddrPhone = SqlTypeConverter.DBNullStringHandler(rdr["AddrPhone"]);
                    entity.WorkPhone = SqlTypeConverter.DBNullStringHandler(rdr["WorkPhone"]);
                    entity.MobilePhone = SqlTypeConverter.DBNullStringHandler(rdr["MobilePhone"]);
                    entity.Email = SqlTypeConverter.DBNullStringHandler(rdr["Email"]);
                    entity.Photo = SqlTypeConverter.DBNullBytesHandler(rdr["Photo"]);
                    entity.IsLeft = SqlTypeConverter.DBNullBooleanHandler(rdr["IsLeft"]);
                    entity.EntryTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EntryTime"]);
                    entity.RetireTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["RetireTime"]);
                    entity.IsFormal = SqlTypeConverter.DBNullBooleanHandler(rdr["IsFormal"]);
                    entity.Remarks = SqlTypeConverter.DBNullStringHandler(rdr["Remarks"]);
                    entity.Creater = SqlTypeConverter.DBNullStringHandler(rdr["Creater"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Modifier = SqlTypeConverter.DBNullStringHandler(rdr["Modifier"]);
                    entity.ModifiedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ModifiedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public Employee GetEntityByNo(string no) {
            SqlParameter[] parms = { new SqlParameter("@EmpNo", SqlDbType.VarChar, 20) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(no);

            Employee entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Employee_Repository_GetEntityByNo, parms)) {
                if(rdr.Read()) {
                    entity = new Employee();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.EmpNo = SqlTypeConverter.DBNullStringHandler(rdr["EmpNo"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullInt32Handler(rdr["DutyId"]);
                    entity.ICardId = SqlTypeConverter.DBNullStringHandler(rdr["ICardId"]);
                    entity.Birthday = SqlTypeConverter.DBNullDateTimeHandler(rdr["Birthday"]);
                    entity.Degree = SqlTypeConverter.DBNullEnmDegreeHandler(rdr["Degree"]);
                    entity.Marriage = SqlTypeConverter.DBNullEnmMarriageHandler(rdr["Marriage"]);
                    entity.Nation = SqlTypeConverter.DBNullStringHandler(rdr["Nation"]);
                    entity.Provinces = SqlTypeConverter.DBNullStringHandler(rdr["Provinces"]);
                    entity.Native = SqlTypeConverter.DBNullStringHandler(rdr["Native"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.AddrPhone = SqlTypeConverter.DBNullStringHandler(rdr["AddrPhone"]);
                    entity.WorkPhone = SqlTypeConverter.DBNullStringHandler(rdr["WorkPhone"]);
                    entity.MobilePhone = SqlTypeConverter.DBNullStringHandler(rdr["MobilePhone"]);
                    entity.Email = SqlTypeConverter.DBNullStringHandler(rdr["Email"]);
                    entity.Photo = SqlTypeConverter.DBNullBytesHandler(rdr["Photo"]);
                    entity.IsLeft = SqlTypeConverter.DBNullBooleanHandler(rdr["IsLeft"]);
                    entity.EntryTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EntryTime"]);
                    entity.RetireTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["RetireTime"]);
                    entity.IsFormal = SqlTypeConverter.DBNullBooleanHandler(rdr["IsFormal"]);
                    entity.Remarks = SqlTypeConverter.DBNullStringHandler(rdr["Remarks"]);
                    entity.Creater = SqlTypeConverter.DBNullStringHandler(rdr["Creater"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Modifier = SqlTypeConverter.DBNullStringHandler(rdr["Modifier"]);
                    entity.ModifiedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ModifiedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Employee> GetEntities() {
            var entities = new List<Employee>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Employee_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Employee();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.EmpNo = SqlTypeConverter.DBNullStringHandler(rdr["EmpNo"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullInt32Handler(rdr["DutyId"]);
                    entity.ICardId = SqlTypeConverter.DBNullStringHandler(rdr["ICardId"]);
                    entity.Birthday = SqlTypeConverter.DBNullDateTimeHandler(rdr["Birthday"]);
                    entity.Degree = SqlTypeConverter.DBNullEnmDegreeHandler(rdr["Degree"]);
                    entity.Marriage = SqlTypeConverter.DBNullEnmMarriageHandler(rdr["Marriage"]);
                    entity.Nation = SqlTypeConverter.DBNullStringHandler(rdr["Nation"]);
                    entity.Provinces = SqlTypeConverter.DBNullStringHandler(rdr["Provinces"]);
                    entity.Native = SqlTypeConverter.DBNullStringHandler(rdr["Native"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.AddrPhone = SqlTypeConverter.DBNullStringHandler(rdr["AddrPhone"]);
                    entity.WorkPhone = SqlTypeConverter.DBNullStringHandler(rdr["WorkPhone"]);
                    entity.MobilePhone = SqlTypeConverter.DBNullStringHandler(rdr["MobilePhone"]);
                    entity.Email = SqlTypeConverter.DBNullStringHandler(rdr["Email"]);
                    entity.Photo = SqlTypeConverter.DBNullBytesHandler(rdr["Photo"]);
                    entity.IsLeft = SqlTypeConverter.DBNullBooleanHandler(rdr["IsLeft"]);
                    entity.EntryTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EntryTime"]);
                    entity.RetireTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["RetireTime"]);
                    entity.IsFormal = SqlTypeConverter.DBNullBooleanHandler(rdr["IsFormal"]);
                    entity.Remarks = SqlTypeConverter.DBNullStringHandler(rdr["Remarks"]);
                    entity.Creater = SqlTypeConverter.DBNullStringHandler(rdr["Creater"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Modifier = SqlTypeConverter.DBNullStringHandler(rdr["Modifier"]);
                    entity.ModifiedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ModifiedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Employee> GetEntitiesByDept(string id) {
            SqlParameter[] parms = { new SqlParameter("@DeptId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<Employee>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Employee_Repository_GetEntitiesByDept, parms)) {
                while(rdr.Read()) {
                    var entity = new Employee();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.EmpNo = SqlTypeConverter.DBNullStringHandler(rdr["EmpNo"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullInt32Handler(rdr["DutyId"]);
                    entity.ICardId = SqlTypeConverter.DBNullStringHandler(rdr["ICardId"]);
                    entity.Birthday = SqlTypeConverter.DBNullDateTimeHandler(rdr["Birthday"]);
                    entity.Degree = SqlTypeConverter.DBNullEnmDegreeHandler(rdr["Degree"]);
                    entity.Marriage = SqlTypeConverter.DBNullEnmMarriageHandler(rdr["Marriage"]);
                    entity.Nation = SqlTypeConverter.DBNullStringHandler(rdr["Nation"]);
                    entity.Provinces = SqlTypeConverter.DBNullStringHandler(rdr["Provinces"]);
                    entity.Native = SqlTypeConverter.DBNullStringHandler(rdr["Native"]);
                    entity.Address = SqlTypeConverter.DBNullStringHandler(rdr["Address"]);
                    entity.PostalCode = SqlTypeConverter.DBNullStringHandler(rdr["PostalCode"]);
                    entity.AddrPhone = SqlTypeConverter.DBNullStringHandler(rdr["AddrPhone"]);
                    entity.WorkPhone = SqlTypeConverter.DBNullStringHandler(rdr["WorkPhone"]);
                    entity.MobilePhone = SqlTypeConverter.DBNullStringHandler(rdr["MobilePhone"]);
                    entity.Email = SqlTypeConverter.DBNullStringHandler(rdr["Email"]);
                    entity.Photo = SqlTypeConverter.DBNullBytesHandler(rdr["Photo"]);
                    entity.IsLeft = SqlTypeConverter.DBNullBooleanHandler(rdr["IsLeft"]);
                    entity.EntryTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EntryTime"]);
                    entity.RetireTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["RetireTime"]);
                    entity.IsFormal = SqlTypeConverter.DBNullBooleanHandler(rdr["IsFormal"]);
                    entity.Remarks = SqlTypeConverter.DBNullStringHandler(rdr["Remarks"]);
                    entity.Creater = SqlTypeConverter.DBNullStringHandler(rdr["Creater"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Modifier = SqlTypeConverter.DBNullStringHandler(rdr["Modifier"]);
                    entity.ModifiedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ModifiedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
