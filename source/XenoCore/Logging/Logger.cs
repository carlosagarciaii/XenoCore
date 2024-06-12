

using System.Data.Common;
using System.Data;

namespace InSightWebdriver.Controllers
{
    public class LogController
    {
        IDbConnection sqlConnection;

        public LogController()
        {
            sqlConnection = new SqlConnection(DBConnection.ConString);
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