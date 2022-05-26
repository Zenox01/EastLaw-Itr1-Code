using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EastLawBL
{
  public  class EBook
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string EBookCat { get; set; }
        public int EBookCatID { get; set; }
        public string Title { get; set; }
        public string CoverPhoto { get; set; }
        public string ShortInfo { get; set; }
        public string Author { get; set; }
        public string PublishedOn { get; set; }
        public int NoOfPages { get; set; }
        public string Overview { get; set; }
        public double SubscriptionPrice { get; set; }
        public string DType { get; set; }


        public int EBookID { get; set; }
        public int ParentIndex { get; set; }
        public string IndexGroup { get; set; }
        public string IndexTitle { get; set; }
        public string PageNo { get; set; }
        public string IndexContent { get; set; }
        public string IndexGroupTag { get; set; }
        public string DataMode { get; set; }
        public string FleName { get; set; }
        public string OutputFle { get; set; }
        public int SortOrder { get; set; }
        

       
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion

        #region EBook Categories
        public int InsertEBookCategory()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@EBookCat", EBookCat);
                arrParam[1] = new SqlParameter("@Active", Active);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_ebook_category", arrParam);
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
        public int EditEBookCategory(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@EBookCat", EBookCat);
                arrParam[1] = new SqlParameter("@Active", Active);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_ebook_category", arrParam);
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
        public int DeleteEBookCategory(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_ebook_category", arrParam);
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
        public DataTable GetActiveEBookCategories()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_ebook_active_categories");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetEBookCategories(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_ebook_categories", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
        #region EBook
        public int InsertEBook()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[13];

                arrParam[0] = new SqlParameter("@EBookCatID", EBookCatID);
                arrParam[1] = new SqlParameter("@Title", Title);
                arrParam[2] = new SqlParameter("@CoverPhoto", CoverPhoto);
                arrParam[3] = new SqlParameter("@ShortInfo", ShortInfo);
                arrParam[4] = new SqlParameter("@Author", Author);
                arrParam[5] = new SqlParameter("@PublishedOn", PublishedOn);
                arrParam[6] = new SqlParameter("@NoOfPages", NoOfPages);
                arrParam[7] = new SqlParameter("@Overview", Overview);
                arrParam[8] = new SqlParameter("@SubscriptionPrice", SubscriptionPrice);
                arrParam[9] = new SqlParameter("@Active", Active);
                arrParam[10] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[11] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[11].Direction = ParameterDirection.InputOutput;
                arrParam[12] = new SqlParameter("@DType", DType);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_ebook", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[11].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditEBook(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[12];

                arrParam[0] = new SqlParameter("@EBookCatID", EBookCatID);
                arrParam[1] = new SqlParameter("@Title", Title);
                arrParam[2] = new SqlParameter("@CoverPhoto", CoverPhoto);
                arrParam[3] = new SqlParameter("@ShortInfo", ShortInfo);
                arrParam[4] = new SqlParameter("@Author", Author);
                arrParam[5] = new SqlParameter("@PublishedOn", PublishedOn);
                arrParam[6] = new SqlParameter("@NoOfPages", NoOfPages);
                arrParam[7] = new SqlParameter("@Overview", Overview);
                arrParam[8] = new SqlParameter("@SubscriptionPrice", SubscriptionPrice);
                arrParam[9] = new SqlParameter("@Active", Active);
                arrParam[10] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[11] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_ebook", arrParam);
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
        public int DeleteEBook(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_ebook", arrParam);
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
       
        public DataTable GetEBook(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_ebook", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveEBook(string Type)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Type", Type);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_ebook_bytype", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
        #region EBook Indexes
        public int InsertEBookIndex()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[10];

                arrParam[0] = new SqlParameter("@EBookID", EBookID);
                arrParam[1] = new SqlParameter("@IndexGroup", IndexGroup);
                arrParam[2] = new SqlParameter("@IndexTitle", IndexTitle);
                arrParam[3] = new SqlParameter("@PageNo", PageNo);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@IndexContent", IndexContent);
                arrParam[8] = new SqlParameter("@SortOrder", SortOrder);
                arrParam[9] = new SqlParameter("@ParentIndex", ParentIndex);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_ebook_index", arrParam);
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
        public int EditEBookIndex(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[14];

                arrParam[0] = new SqlParameter("@EBookID", EBookID);
                arrParam[1] = new SqlParameter("@IndexGroup", IndexGroup);
                arrParam[2] = new SqlParameter("@IndexTitle", IndexTitle);
                arrParam[3] = new SqlParameter("@PageNo", PageNo);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[6] = new SqlParameter("@ID", ID);
                arrParam[7] = new SqlParameter("@IndexContent", IndexContent);
                arrParam[8] = new SqlParameter("@SortOrder", SortOrder);
                arrParam[9] = new SqlParameter("@ParentIndex", ParentIndex);
                arrParam[10] = new SqlParameter("@IndexGroupTag", IndexGroupTag);
                arrParam[11] = new SqlParameter("@DataMode", DataMode);
                arrParam[12] = new SqlParameter("@FleName", FleName);
                arrParam[13] = new SqlParameter("@OutputFle", OutputFle);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_ebook_index", arrParam);
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
        public int EditEBookIndexTitle(int IndexID,string Title,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[9];


                arrParam[0] = new SqlParameter("@IndexTitle", Title);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[2] = new SqlParameter("@ID", IndexID);
               
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_ebook_index_title", arrParam);
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
        public int DeleteEBookIndex(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_ebook_index", arrParam);
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
        
        public DataTable GetEBookIndex(int ID,int EBookID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@EBookID", EBookID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_ebook_index_byebook_ByID", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetEBookIndexSearch(int EBookID,string SearchWord)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@EBookID", EBookID);
                arrParam[1] = new SqlParameter("@SearchWord", SearchWord);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_ebook_index_byebook_search", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetEBookParentIndex(int EBookID,int ParentID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@EBookID", EBookID);
                arrParam[1] = new SqlParameter("@ParentID", ParentID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_ebook_index_parent_byebook_ByID", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int RunOpenQuery(string Qry)
        {
            try
            {

               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.Text, Qry);
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
        public DataTable GetPendingIndexContent()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string qry = "select * from tbl_EBook_Indexes where datamode='Word File' and IndexContent ='' and flename!='' and IsDeleted=0";
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.Text,qry);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int UpdateEbookIndexContent(int ID,string IndexContent)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@IndexContent", IndexContent);
               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp__update_ebook_indexcontent_update", arrParam);
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

        #endregion
    }
}
