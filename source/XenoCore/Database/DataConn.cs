using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

using Dapper;
using System.IO;
using Microsoft.Identity.Client;


namespace XenoCore.Database
{
    public class DataConn
    {

        private IDbConnection sqlConnection;

       public void Config(string connString)
        {
            try
            {
                sqlConnection = new SqlConnection(connString);
            }
            catch (Exception e){
                throw new Exception($"Failed to connect to DB with string\n{connString}");
            }
        }

        public IDbConnection GetConn()
        {
            return sqlConnection;
        }

    }


}
