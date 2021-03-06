using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;

namespace BusinessLogic
{
    public class Helper
    {
        public static OdbcConnection Connection(string connectionString)
        {
            //In this page, we open the connection to the Database
            //Our Access database is contained in ../DB/FactoryDB.mdb
            //It's a very simple database with just 2 tables (for the sake of demo)	
            OdbcConnection connection = new OdbcConnection();
            //Connect
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }

        public static string ConnectionStringFactory
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["ConnectionStringFactory"];
            }
        }

        public static string ConnectionStringFisionChart
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["ConnectionStringFusionChart"];
            }
        }
    }
}
