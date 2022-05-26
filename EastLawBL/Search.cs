using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EastLawBL
{
   public class Search
    {
       #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;
       #endregion

        #region Methods
        public int InsertSearchText(string SearchTxt,int FoundResult,string IP,int UserID, int NoOfRecords,string Area_Source)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[7];

                arrParam[0] = new SqlParameter("@SearchText", SearchTxt);
                arrParam[1] = new SqlParameter("@FoundResult", FoundResult);
                arrParam[2] = new SqlParameter("@IPAdd", IP);
                arrParam[3] = new SqlParameter("@UserID", UserID);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
                arrParam[5] = new SqlParameter("@NoOfRecords", NoOfRecords);
                arrParam[6] = new SqlParameter("@Area_Source", Area_Source);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_search", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[4].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public DataTable GetSearchTxt(int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@UserID", UserID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_usersearchtxt", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
