using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{
    public class Newsletter
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string NewsletterType { get; set; }
        public string NewsletterBanner { get; set; }
        public string TemplateName { get; set; }
        public string NewsletterTitle { get; set; }
        public string NewsletterContent { get; set; }
        public string NewsletterFile { get; set; }
        public string ShortText { get; set; }
        public string URL { get; set; }

        public int NewsletterID { get; set; }
        public string ItemType { get; set; }
        public int ItemID { get; set; }
       
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion

        #region Methods
        public int InsertNewsletter()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[9];

                arrParam[0] = new SqlParameter("@NewsletterType", NewsletterType);
                arrParam[1] = new SqlParameter("@TemplateName", TemplateName);
                arrParam[2] = new SqlParameter("@NewsletterTitle", NewsletterTitle);
                arrParam[3] = new SqlParameter("@NewsletterContent", NewsletterContent);
                arrParam[4] = new SqlParameter("@NewsletterFile", NewsletterFile);
                arrParam[5] = new SqlParameter("@Active", Active);
                arrParam[6] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[7] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[7].Direction = ParameterDirection.InputOutput;
                arrParam[8] = new SqlParameter("@NewsletterBanner", NewsletterBanner);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_newsletter", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[7].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditNewsletter(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[9];

                arrParam[0] = new SqlParameter("@NewsletterType", NewsletterType);
                arrParam[1] = new SqlParameter("@TemplateName", TemplateName);
                arrParam[2] = new SqlParameter("@NewsletterTitle", NewsletterTitle);
                arrParam[3] = new SqlParameter("@NewsletterContent", NewsletterContent);
                arrParam[4] = new SqlParameter("@NewsletterFile", NewsletterFile);
                arrParam[5] = new SqlParameter("@Active", Active);
                arrParam[6] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[7] = new SqlParameter("@ID", ID);
                arrParam[8] = new SqlParameter("@NewsletterBanner", NewsletterBanner);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_newsletter", arrParam);
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
        public int DeleteNewsletter(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_newsletter", arrParam);
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

        public int InsertNewsletterItems(int NewsletterID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[8];

                arrParam[0] = new SqlParameter("@NewsletterID", NewsletterID);
                arrParam[1] = new SqlParameter("@ItemType", ItemType);
                arrParam[2] = new SqlParameter("@ItemID", ItemID);
                arrParam[3] = new SqlParameter("@Active", Active);
                arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;
                arrParam[6] = new SqlParameter("@ShortText", ShortText);
                arrParam[7] = new SqlParameter("@URL", URL);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_newsletter_items", arrParam);
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
        public int EditNewsletterItems(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                
                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);
                
                arrParam[2] = new SqlParameter("@ShortText", ShortText);
                arrParam[3] = new SqlParameter("@URL", URL);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_newsletter_items", arrParam);
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
        public int DeleteNewsletterItems(int NewsletterID,int ItemID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ItemID", ItemID);
                arrParam[2] = new SqlParameter("@NewsletterID", NewsletterID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_newsletter_items", arrParam);
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
     
        
        public DataTable GetNewsletter(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_newsletter", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetNewsletterItems(int NewsletterID,string ItemType)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@NewsletterID", NewsletterID);
                arrParam[1] = new SqlParameter("@ItemType", ItemType);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_newsletter_items", arrParam);

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
