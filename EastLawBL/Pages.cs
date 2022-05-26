using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace EastLawBL
{
   public class Pages
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Meta { get; set; }
        public string Keywords { get; set; }
        public string ShortContent { get; set; }
        public string FullContent { get; set; }
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion
        #region Methods
        public int EditPage(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[7];

                arrParam[0] = new SqlParameter("@Title", Title);
                arrParam[1] = new SqlParameter("@Meta", Meta);
                arrParam[2] = new SqlParameter("@Keywords", Keywords);
                arrParam[3] = new SqlParameter("@ShortContent", ShortContent);
                arrParam[4] = new SqlParameter("@FullContent", FullContent);
                arrParam[5] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[6] = new SqlParameter("@ID", ID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_page", arrParam);
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
        public DataTable GetPages(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pages", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetPageByName(string PageName)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PageName", PageName);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pages_byname", arrParam);

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
