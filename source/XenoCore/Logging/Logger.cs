﻿

using System.Data.Common;
using System.Data;
using Serilog;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace XenoCore.Logging
{
    public class LogController
    {
        private IDbConnection sqlConnection;
        private string LogFilePath;


        public LogController(string logFilePath = "", string sqlConnectionString = "")
        {
            if (logFilePath.Trim() != "")
            {
                LogFilePath = logFilePath;
            }
            if (sqlConnectionString.Trim() != "")
            {
                sqlConnection = new SqlConnection(sqlConnectionString);
            }
        }

/// <summary>
/// Creates the t_log table for the desired SQL Server
/// </summary>
/// <param name="sqlType">SQL Server Type (IE: MySQL, MSSQL, PostGres, etc)</param>
/// <exception cref="Exception"></exception>
        public void SetupLogTable(string sqlType)
        {
            string funcName = MethodBase.GetCurrentMethod().Name;
            string query = "";

            switch (sqlType.ToLower().Trim())
            {
                case "mssql":
                    query = @"

CREATE TABLE t_log (
    id SERIAL PRIMARY KEY,
    func_name VARCHAR NULL,
    class_name VARCHAR NULL,
    message TEXT NOT NULL,
    log_time TIMESTAMP NOT NULL DEFAULT NOW()
);

";

                    break;

                case "mysql":
                    query = @"
CREATE TABLE t_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    func_name VARCHAR(255) NULL,
    class_name VARCHAR(255) NULL,
    message NVARCHAR(MAX) NOT NULL,
    log_time DATETIME NOT NULL DEFAULT NOW()
);

";
                    break;

                case "postgres":
                case "postgresql":
                    query = @"
CREATE TABLE t_log (
    id SERIAL PRIMARY KEY,
    func_name VARCHAR NULL,
    class_name VARCHAR NULL,
    message TEXT NOT NULL,
    log_time TIMESTAMP NOT NULL DEFAULT NOW()
);

";
                    break;

                case "sqllite":
                    query = @"
CREATE TABLE t_log (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    func_name TEXT NULL,
    class_name TEXT NULL,
    message TEXT NOT NULL,
    log_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

";
                    break;

                case "oracle":
                case "oraclesql":
                    query = @"
CREATE TABLE t_log (
    id NUMBER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    func_name VARCHAR2(255) NULL,
    class_name VARCHAR2(255) NULL,
    message NVARCHAR2(2000) NOT NULL,  -- Oracle does not support NVARCHAR(MAX), using NVARCHAR2 with max length
    log_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
);
"

;

                    break;

                case "db2":
                    query = @"
CREATE TABLE t_log (
    id INT GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    func_name VARCHAR(255) NULL,
    class_name VARCHAR(255) NULL,
    message VARCHAR(4000) NOT NULL,  -- DB2 does not support NVARCHAR(MAX), using VARCHAR with max length
    log_time TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

"
;

                    break;


                default:
                    throw new Exception("No valid SQL Server Provided.")
                        ;

            }

            try {
            
            }
            catch (Exception e)
            {
                string msg = $"Func: {funcName} | Class: {this.GetType().Name} | Msg: {e.Message}";

                throw new Exception(msg);
            }

        }



        public void Write(string message, string funcName, int duration = 0, string testName = "")
        {

            string writeMsg = $"{funcName}:\t{message}";

            Console.WriteLine(writeMsg);


            var query = "usp_trace_log";
            var param = new DynamicParameters();





            // PARAMETERS

            param.Add("@in_func_name", funcName);
            param.Add("@in_duration", duration);
            param.Add("@in_message", message);

            try
            {
                sqlConnection.Query(query, param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        /// <summary>
        /// Logs the Test Results to the DB
        /// </summary>
        /// <param name="testName">Name fo the Test</param>
        /// <param name="isPassed">Did the test pass?</param>
        /// <param name="comments">Optional Comments</param>
        public void LogTestResult(string testName, bool isPassed, int duration = 0, string comments = "")
        {

            var query = "usp_results";
            var param = new DynamicParameters();


            // PARAMETERS
            param.Add("@in_name", testName);
            param.Add("@in_result", (isPassed) ? "PASS" : "FAIL");
            param.Add("@in_duration", duration);
            param.Add("@in_comments", comments);

            try
            {
                sqlConnection.Query(query, param, commandType: System.Data.CommandType.StoredProcedure);
                string result = (isPassed) ? "PASS" : "FAIL";
                Console.WriteLine($"Test:\t{testName}\tResult:\t{result}\t{comments}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }



    }
}