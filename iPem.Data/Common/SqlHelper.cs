using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Common {
    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public abstract class SqlHelper {
        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            var cmd = new SqlCommand();
            using(var conn = new SqlConnection(connectionString)) {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                var val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            var val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            var val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)  {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            } catch {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset using an existing SQL Transaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(trans, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            var rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return rdr;
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataTable r = ExecuteDataTable(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A datatable containing the results</returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                var dataTable = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);

                cmd.Parameters.Clear();
                conn.Close();
                return dataTable;
            } catch {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            var cmd = new SqlCommand();
            using(var connection = new SqlConnection(connectionString)) {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                var val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            var val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms) {
            if(conn.State != ConnectionState.Open) { conn.Open(); }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if(trans != null) { cmd.Transaction = trans; }
            cmd.CommandType = cmdType;
            if(cmdParms != null) {
                foreach(var parm in cmdParms) {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        /// Tests the given connection parameters
        /// </summary>
        /// <param name="trustedConnection">Avalue that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true)</param>
        /// <param name="serverName">The name or network address of the instance of SQL Server to connect to</param>
        /// <param name="databasePort">The port number of the instance of SQL Server to connect to</param>
        /// <param name="databaseName">The name of the database associated with the connection</param>
        /// <param name="userName">The user ID to be used when connecting to SQL Server</param>
        /// <param name="password">The password for the SQL Server account</param>
        /// <returns>Returns true if an attempt to open the database by using the connection succeeds.</returns>
        public static void TestConnection(bool trustedConnection, string serverName, int databasePort, string databaseName, string userName, string password, int timeout = 5) {
            var connectionString = CreateConnectionString(trustedConnection, serverName, databasePort, databaseName, userName, password, timeout);
            TestConnection(connectionString, timeout);
        }

        /// <summary>
        /// Tests the given connection string
        /// </summary>
        /// <param name="connectionString">SqlConnection string</param>
        /// <param name="timeout">SqlConnection timeout interval</param>
        /// <returns>Returns true if an attempt to open the database by using the connection succeeds.</returns>
        public static void TestConnection(string connectionString, int timeout = 5) {
            var alive = true;
            var success = false;
            var error = string.Empty;

            var conn = new SqlConnection(connectionString);
            var thread = new System.Threading.Thread(() => {
                try {
                    conn.Open();
                    conn.Close();
                    if(alive) { success = true; }
                } catch(Exception err) {
                    error = err.Message;
                } finally {
                    if(conn.State == ConnectionState.Open) {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            });

            thread.IsBackground = true;
            var sw = System.Diagnostics.Stopwatch.StartNew();
            thread.Start();
            var ts = TimeSpan.FromSeconds(timeout);
            while(sw.Elapsed < ts) {
                thread.Join(TimeSpan.FromMilliseconds(100));
                if(success || !string.IsNullOrWhiteSpace(error)) { break; }
            }
            sw.Stop();

            if(!success) {
                alive = false;
                throw new Exception(string.IsNullOrWhiteSpace(error) ? "超时时间已到，无法连接到数据库。" : error);
            }
        }

        /// <summary>
        /// Create contents of connection strings used by the SqlConnection class
        /// </summary>
        /// <param name="trustedConnection">Avalue that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true)</param>
        /// <param name="serverName">The name or network address of the instance of SQL Server to connect to</param>
        /// <param name="databasePort">The port number of the instance of SQL Server to connect to</param>
        /// <param name="databaseName">The name of the database associated with the connection</param>
        /// <param name="userName">The user ID to be used when connecting to SQL Server</param>
        /// <param name="password">The password for the SQL Server account</param>
        /// <param name="timeout">The connection timeout</param>
        /// <returns>Connection string</returns>
        public static string CreateConnectionString(bool trustedConnection, string serverName, int databasePort, string databaseName, string userName, string password, int timeout = 120) {
            var builder = new SqlConnectionStringBuilder();
            builder.IntegratedSecurity = trustedConnection;
            builder.DataSource = String.Format("{0},{1}", serverName, databasePort);
            builder.InitialCatalog = databaseName;
            if(!trustedConnection) { builder.UserID = userName; builder.Password = password; }
            builder.PersistSecurityInfo = false;
            builder.MultipleActiveResultSets = true;
            builder.ConnectTimeout = timeout;
            builder.Pooling = true;
            builder.MaxPoolSize = 500;
            builder.MinPoolSize = 0;
            return builder.ConnectionString;
        }

        /// <summary>
        /// Checks if the specified database exists, returns true if database exists
        /// </summary>
        /// <param name="trustedConnection">A value that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true)</param>
        /// <param name="serverName">The name or network address of the instance of SQL Server to connect to</param>
        /// <param name="databasePort">The port of the database associated with the connection</param>
        /// <param name="databaseName">The name of the database associated with the connection</param>
        /// <param name="userName">The user ID to be used when connecting to SQL Server</param>
        /// <param name="password">The password for the SQL Server account</param>
        /// <returns>Returns true if the database exists.</returns>
        public static bool DatabaseExists(bool trustedConnection, string serverName, int databasePort, string databaseName, string userName, string password) {
            int dbCnt = 0;
            try {
                var connectionString = CreateConnectionString(trustedConnection, serverName, databasePort, "master", userName, password);
                var query = "SELECT Count(1) FROM sysdatabases WHERE name = '" + databaseName.Replace("'", "''") + "'";
                using(var conn = new SqlConnection(connectionString)) {
                    var cnt = SqlHelper.ExecuteScalar(conn, CommandType.Text, query, null);
                    if(cnt != null && cnt != DBNull.Value) { 
                        dbCnt = Convert.ToInt32(cnt); 
                    }
                }
            } catch { }
            return dbCnt > 0;
        }
    }
}
