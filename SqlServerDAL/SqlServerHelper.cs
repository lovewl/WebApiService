using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace SqlServerDAL
{
    public class SqlServerHelper
    {
        private static SqlConnection sqlConnection = null;
        private static SqlCommand sqlCommand = null;
        private static SqlCommand sqlCommandTransaction = null;
        // private static readonly string sqlConnectionString = "Data Source=127.0.0.1;Initial Catalog=VRPIAOWU;User ID=sa;Password=sql2017;Connect Timeout=10";
        private static string sqlConnectionString = string.Empty;
        private SqlServerHelper() { }
        public static SqlConnection CreateSqlConnection()
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                IConfiguration config = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
                sqlConnectionString = config.GetConnectionString("SqlServer");
            }
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                throw new Exception("ConnectionString empty!");
            }
            return new SqlConnection(sqlConnectionString);
        }
        private static void CreateSqlCommand(string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                if (string.IsNullOrWhiteSpace(sqlConnectionString))
                {
                    IConfiguration config = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
                    sqlConnectionString = config.GetConnectionString("SqlServer");
                }
                if (string.IsNullOrWhiteSpace(sqlConnectionString))
                {
                    throw new Exception("ConnectionString empty!");
                }
                sqlConnection = new SqlConnection(sqlConnectionString);
                sqlConnection.Open();
            }
            if (sqlCommand == null) sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandTimeout = commandTimeOut;// CommandTimeout 系统默认值：30； ConnectionTimeout 系统默认值：15；
            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.Clear();
            if (sqlParameters != null) sqlCommand.Parameters.AddRange(sqlParameters);
        }
        private static void CreateSqlCommandTransaction(SqlConnection connection, SqlTransaction tran, string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            sqlCommandTransaction = connection.CreateCommand();
            sqlCommandTransaction.CommandType = commandType;
            sqlCommandTransaction.CommandTimeout = commandTimeOut;// CommandTimeout 系统默认值：30； ConnectionTimeout 系统默认值：15；
            sqlCommandTransaction.CommandText = sql;
            sqlCommandTransaction.Transaction = tran;
            sqlCommandTransaction.Parameters.Clear();
            if (sqlParameters != null) sqlCommandTransaction.Parameters.AddRange(sqlParameters);
        }
        public static int ExecuteNonQuery(string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommand(sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommand.ExecuteNonQuery();
        }
        public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction tran, string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommandTransaction(connection, tran, sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommandTransaction.ExecuteNonQuery();
        }
        public static object ExecuteScalar(string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommand(sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommand.ExecuteScalar();
        }
        public static object ExecuteScalar(SqlConnection connection, SqlTransaction tran, string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommandTransaction(connection, tran, sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommandTransaction.ExecuteScalar();
        }
        public static SqlDataReader ExecuteReader(string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommand(sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommand.ExecuteReader();
        }
        public static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction tran, string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommandTransaction(connection, tran, sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommandTransaction.ExecuteReader();
        }
        public static XmlReader ExecuteXmlReader(string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommand(sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommand.ExecuteXmlReader();
        }
        public static XmlReader ExecuteXmlReader(SqlConnection connection, SqlTransaction tran, string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            CreateSqlCommandTransaction(connection, tran, sql, commandType, commandTimeOut, sqlParameters);
            return sqlCommandTransaction.ExecuteXmlReader();
        }
        public static DataTable Execute(string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            DataTable dataTable = new DataTable();
            CreateSqlCommand(sql, commandType, commandTimeOut, sqlParameters);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        public static DataTable Execute(SqlConnection connection, SqlTransaction tran, string sql, CommandType commandType = CommandType.Text, int commandTimeOut = 30, params SqlParameter[] sqlParameters)
        {
            DataTable dataTable = new DataTable();
            CreateSqlCommandTransaction(connection, tran, sql, commandType, commandTimeOut, sqlParameters);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommandTransaction);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
