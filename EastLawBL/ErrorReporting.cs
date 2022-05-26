using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace EastLawBL
{
    public class ErrorReporting
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string Type { get; set; }
        public string ItemType { get; set; }
        public int ItemID { get; set; }
        public int UserID { get; set; }
        public string UserComment { get; set; }
        public int WorkflowID { get; set; }
        public string AdminComment { get; set; }
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion
        #region General Area
        public int InsertReportError()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[7];

                arrParam[0] = new SqlParameter("@Type", Type);
                arrParam[1] = new SqlParameter("@ItemType", ItemType);
                arrParam[2] = new SqlParameter("@ItemID", ItemID);
                arrParam[3] = new SqlParameter("@UserID", UserID);
                arrParam[4] = new SqlParameter("@UserComment", UserComment);
                arrParam[5] = new SqlParameter("@WorkflowID", WorkflowID);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_errorreport", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[6].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditUpdateErrorStatus(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@AdminComment", AdminComment);
                arrParam[1] = new SqlParameter("@WorkflowID", WorkflowID);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@ID", ID);
               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_updateerrorstatus", arrParam);
                mTransaction.Commit();

                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
       
        public DataTable GetPendingErrors()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pendingerrors");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetPendingErrorsSearch(int Year,int JournalID,string PageNo)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@Year", Year);
                arrParam[1] = new SqlParameter("@JournalID", JournalID);
                arrParam[2] = new SqlParameter("@Pageno", PageNo);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pendingerrors_search",arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetPendingErrorsGeneral()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pendingerrors_general");

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
