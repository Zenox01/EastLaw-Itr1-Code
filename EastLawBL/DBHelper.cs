using System.Data;
using System.Configuration;
//using Risk2Value.ApplicationBlocks.Data;





	public class DBHelper
	{
		private DBHelper() {}

//		public static AdoHelper GetDBHelper()
//		{
//			string assembly = null;
//			string type = null;
//				
//			switch( ConfigurationSettings.AppSettings["CurrentConnection"] )
//			{
//				case "SqlServer":
//					assembly = ConfigurationSettings.AppSettings["SqlServerHelperAssembly"];
//					type = ConfigurationSettings.AppSettings["SqlServerHelperType"];
//					break;
//				case "OleDb":
//					assembly = ConfigurationSettings.AppSettings["OleDbHelperAssembly"];
//					type = ConfigurationSettings.AppSettings["OleDbHelperType"];
//					break;
//				case "Odbc":
//					assembly = ConfigurationSettings.AppSettings["OdbcHelperAssembly"];
//					type = ConfigurationSettings.AppSettings["OdbcHelperType"];
//					break;
//				case "Oracle":
//					assembly = ConfigurationSettings.AppSettings["OracleHelperAssembly"];
//					type = ConfigurationSettings.AppSettings["OracleHelperType"];
//					break;
//			}
//
//			return AdoHelper.CreateHelper( assembly, type ); 
//		}
//

		public static string GetConnectionString()
		{
			
			string connectionString = System.Configuration.ConfigurationSettings.AppSettings["SqlServerConnectionString"].ToString();

		//switch( ConfigurationSettings.AppSettings["CurrentConnection"] )
		//{
		//    case "SqlServer":
		//        connectionString=GetDecryptConnectionString(EncryptDecryptHelper.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["SqlServerConnectionString"].ToString()));
		//        //connectionString = ConfigurationSettings.AppSettings["SqlServerConnectionString"];
		//        break;
		//    case "OleDb":
		//        connectionString=GetDecryptConnectionString(EncryptDecryptHelper.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["OleDbConnectionString"].ToString()));
		//        //connectionString = ConfigurationSettings.AppSettings["OleDbConnectionString"];
		//        break;
		//    case "Odbc":
		//        //connectionString = ConfigurationSettings.AppSettings["OdbcConnectionString"];
		//        connectionString=GetDecryptConnectionString(EncryptDecryptHelper.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["OdbcConnectionString"].ToString()));
		//        break;
		//    case "Oracle":
		//        //connectionString = ConfigurationSettings.AppSettings["OracleConnectionString"];
		//        connectionString=GetDecryptConnectionString(EncryptDecryptHelper.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["OracleConnectionString"].ToString()));
		//        break;
		//}

		//return connectionString;
		//string myDecrypt = EastLawBL.CryptographyManager.DecryptString(connectionString, "LoqTs1");
		//string myEncrypt = EastLawBL.CryptographyManager.EncryptString(myDecrypt, "LoqTs1");

			return EastLawBL.CryptographyManager.DecryptString(connectionString, "LoqTs1");
		}
		private static string GetDecryptConnectionString(string connstr)
		{
			
			/// Date   : 06th March, 2006
			/// Version: risk2value 2.1
			/// BugNo  : 3
			/// Phase:  Phase 1
			/// Developer: Asif
			/// Previous Code : Commented below
			/// Description: The first screen should be loaded much faster. There is no need of the code written below because the connection string comming in this method is accurate and there is no need to split it and then to put ;
			/// this method is called everytime when database operation is performed
			///return connstr;
			
			string s = connstr;
			string ret="";
			string[] parts = s.Split(';');
			int i;
			for (i = 0; i <= parts.Length - 1; i++) 
			{
				string[] cparts = parts[i].Split('=');
				if (cparts[0].ToUpper() == "SERVER" || cparts[0].ToUpper() == "DATABASE" || cparts[0].ToUpper() == "UID" || cparts[0].ToUpper() == "PWD") 
				{
					ret += parts[i] + ";";
				}
			}
			return ret;
		}
	}


