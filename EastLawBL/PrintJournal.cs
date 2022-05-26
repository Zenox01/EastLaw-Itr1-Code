using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{
   public class PrintJournal
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string RefNo { get; set; }
        public string RefName { get; set; }
        public string JournalTitle { get; set; }
        public string Pwd { get; set; }
        public int Active { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion
        #region Methods
        public int InsertPrintJournal()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[7];

                arrParam[0] = new SqlParameter("@RefNo", RefNo);
                arrParam[1] = new SqlParameter("@RefName", RefName);
                arrParam[2] = new SqlParameter("@JournalTitle", JournalTitle);
                arrParam[3] = new SqlParameter("@Pwd", Pwd);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_print_journal", arrParam);
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
        public int EditPrintJournal(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[7];

                arrParam[0] = new SqlParameter("@RefNo", RefNo);
                arrParam[1] = new SqlParameter("@RefName", RefName);
                arrParam[2] = new SqlParameter("@JournalTitle", JournalTitle);
                arrParam[3] = new SqlParameter("@Pwd", Pwd);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[6] = new SqlParameter("@ID", ID);
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "[sp_edit_print_journal]", arrParam);
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
        public int DeletePrintJournal(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_print_journal", arrParam);
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

        public int InsertPrintJournalItem(int PrintJournalID, string ItemType, int ItemID, int Active, int CreatedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@PrintJournalID", PrintJournalID);
                arrParam[1] = new SqlParameter("@ItemType", ItemType);
                arrParam[2] = new SqlParameter("@ItemID", ItemID);
                arrParam[3] = new SqlParameter("@Active", Active);
                arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_print_journal_items", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[5].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DeletePrintJournalItem(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_print_journal_items", arrParam);
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
       
        public DataTable GetPrintJournal(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@ID", ID);
             
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_print_journal", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     
        public DataTable GetJournalItemsByJournalID(int JournalID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@JournalID", JournalID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_print_journal_items_byJournalID", arrParam);

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
