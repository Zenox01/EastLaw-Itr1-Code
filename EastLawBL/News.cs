using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EastLawBL
{
   public class News
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public int PracticeAreaID { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string NDate { get; set; }
        public string Source { get; set; }
        public string SourceLink { get; set; }
        public string Author { get; set; }
        public string ShortContent { get; set; }
        public string FullContent { get; set; }
        public string ImgFile { get; set; }
        public string ImageType { get; set; }
        public string DType { get; set; }
        public int CourtMasterID { get; set; }
        public int JudgeID { get; set; }
        public int IsDeleted { get; set; }
        public int Active { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion
        #region Methods
        public int InsertNews()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[17];

                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                arrParam[1] = new SqlParameter("@Title", Title);
                arrParam[2] = new SqlParameter("@Keywords", Keywords);
                arrParam[3] = new SqlParameter("@NDate", NDate);
                arrParam[4] = new SqlParameter("@Source", Source);
                arrParam[5] = new SqlParameter("@SourceLink", SourceLink);
                arrParam[6] = new SqlParameter("@ShortContent", ShortContent);
                arrParam[7] = new SqlParameter("@FullContent", FullContent);
                arrParam[8] = new SqlParameter("@Active", Active);
                arrParam[9] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[10] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[10].Direction = ParameterDirection.InputOutput;
                arrParam[11] = new SqlParameter("@imgfile", ImgFile);
                arrParam[12] = new SqlParameter("@DType", DType);
                arrParam[13] = new SqlParameter("@Author", Author);
                arrParam[14] = new SqlParameter("@ImageType", ImageType);
                arrParam[15] = new SqlParameter("@CourtMasterID", CourtMasterID);
                arrParam[16] = new SqlParameter("@JudgeID", JudgeID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_news", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[10].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditNews(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[17];

                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                arrParam[1] = new SqlParameter("@Title", Title);
                arrParam[2] = new SqlParameter("@Keywords", Keywords);
                arrParam[3] = new SqlParameter("@NDate", NDate);
                arrParam[4] = new SqlParameter("@Source", Source);
                arrParam[5] = new SqlParameter("@SourceLink", SourceLink);
                arrParam[6] = new SqlParameter("@ShortContent", ShortContent);
                arrParam[7] = new SqlParameter("@FullContent", FullContent);
                arrParam[8] = new SqlParameter("@Active", Active);
                arrParam[9] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[10] = new SqlParameter("@ID", ID);
                arrParam[11] = new SqlParameter("@imgfile", ImgFile);
                arrParam[12] = new SqlParameter("@DType", DType);
                arrParam[13] = new SqlParameter("@Author", Author);
                arrParam[14] = new SqlParameter("@ImageType", ImageType);
                arrParam[15] = new SqlParameter("@CourtMasterID", CourtMasterID);
                arrParam[16] = new SqlParameter("@JudgeID", JudgeID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_news", arrParam);
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
        public int DeleteNews(int ID,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];


                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_news", arrParam);
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
        public int AddNewsStatuteCategory(int NewsID,int StatuteCategoryID, int Active)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];


                arrParam[0] = new SqlParameter("@NewsID", NewsID);
                arrParam[1] = new SqlParameter("@StatuteCategoryID", StatuteCategoryID);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_news_statute_category", arrParam);
                mTransaction.Commit();
                  int chk = int.Parse(arrParam[3].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public DataTable GetNewsStatutesCategory(int NewsID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@NewsID", NewsID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_new_statutes_category", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetNews(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_news", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveNewsByPracticeArea(int PracticeAreaID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_news_bypracticearea", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveNews()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_news");

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
