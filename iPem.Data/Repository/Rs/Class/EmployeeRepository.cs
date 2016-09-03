using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
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
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullStringHandler(rdr["DutyId"]);
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
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public Employee GetEntityByCode(string code) {
            SqlParameter[] parms = { new SqlParameter("@Code", SqlDbType.VarChar, 20) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(code);

            Employee entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Employee_Repository_GetEntityByCode, parms)) {
                if(rdr.Read()) {
                    entity = new Employee();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullStringHandler(rdr["DutyId"]);
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
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Employee> GetEntitiesByDept(string dept) {
            SqlParameter[] parms = { new SqlParameter("@DeptId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(dept);

            var entities = new List<Employee>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Employee_Repository_GetEntitiesByDept, parms)) {
                while(rdr.Read()) {
                    var entity = new Employee();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullStringHandler(rdr["DutyId"]);
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
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Employee> GetEntities() {
            var entities = new List<Employee>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Employee_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Employee();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.EngName = SqlTypeConverter.DBNullStringHandler(rdr["EngName"]);
                    entity.UsedName = SqlTypeConverter.DBNullStringHandler(rdr["UsedName"]);
                    entity.Sex = SqlTypeConverter.DBNullEnmSexHandler(rdr["Sex"]);
                    entity.DeptId = SqlTypeConverter.DBNullStringHandler(rdr["DeptId"]);
                    entity.DutyId = SqlTypeConverter.DBNullStringHandler(rdr["DutyId"]);
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
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
