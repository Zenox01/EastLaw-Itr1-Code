using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EastLawBL
{
   public class PracticeAreas
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string PracticeAreaCatName { get; set; }
        public int PracticeAreaCatID { get; set; }
        public string PracticeAreaSubCatName { get; set; }
        public int PracticeAreaID { get; set; }
        public string Keyword { get; set; }
        public string Des { get; set; }
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion

        #region Methods
        public int InsertPracticeAreaCat()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@PracticeAreaCatName", PracticeAreaCatName);
                arrParam[1] = new SqlParameter("@Des", Des);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_practicearea_cat", arrParam);
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
        public int EditPracticeAreaCat(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@PracticeAreaCatName", PracticeAreaCatName);
                arrParam[1] = new SqlParameter("@Des", Des);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID", ID);
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_practicearea_cat", arrParam);
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
        public int DeletePracticeAreaCat(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_practicearea_cat", arrParam);
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
        public DataTable GetPracticeAreaCategories(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_practicearea_cat", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActivePracticeAreaCategories()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_practicearea_cat");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertPracticeAreaSubCat()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@PracticeAreaCatID", PracticeAreaCatID);
                arrParam[1] = new SqlParameter("@PracticeAreaSubCatName", PracticeAreaSubCatName);
                arrParam[2] = new SqlParameter("@Des", Des);
                arrParam[3] = new SqlParameter("@Active", Active);
                arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;
                
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_practicearea_subcat", arrParam);
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
        public int EditPracticeAreaSubCat(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@PracticeAreaCatID", PracticeAreaCatID);
                arrParam[1] = new SqlParameter("@PracticeAreaSubCatName", PracticeAreaSubCatName);
                arrParam[2] = new SqlParameter("@Des", Des);
                arrParam[3] = new SqlParameter("@Active", Active);
                arrParam[4] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[5] = new SqlParameter("@ID", ID);
               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_practicearea_subcat", arrParam);
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

        public int TagPracticeAreaWithStatues(int PracticeArea,int StatutesID,int CreatedBy,int IsDelete)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeArea);
                arrParam[1] = new SqlParameter("@StatutesID", StatutesID);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;
                arrParam[4] = new SqlParameter("@IsDelete", IsDelete);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_tag_practicearea_statues", arrParam);
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
        public int DeleteTaggedPracticeAreaWithStatues(int ID,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_tag_practicearea_statues", arrParam);
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
        public DataTable GetTaggesStatuesWithPracticeArea(int PracticeAreaID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_tagged_practicearea_statues", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTaggesStatuesWithPracticeArea_List(int PracticeAreaID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_tagged_practicearea_statues_list", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTaggesStatuesWithPracticeAreaLess(int PracticeAreaID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_tagged_practicearea_statues_less", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTaggesStatuesWithPracticeAreaByStatuesID(int StatuesID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@StatuesID", StatuesID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_tagged_practicearea_statues_bystatutesID", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTaggesCasesWithPracticeArea(int PracticeAreaID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_tagged_practicearea_cases", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int DeletePracticeAreaSubCat(int ID,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_practicearea_subcat", arrParam);
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

        public DataTable GetPracticeAreaSubCategories(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_practicearea_subcat", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetPracticeAreaSubCategoriesByCategory(int CatID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CatID", CatID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_practicearea_subcatbyCat", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetPracticeAreaSubCategoriesByName(string Name)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PracticeAreaName", Name);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_practicearea_subcatbyName", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActivePracticeAreaSubCategoriesByCategory(int CatID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CatID", CatID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_practicearea_subcatbyCat", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActivePracticeAreaSubCategories()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_practicearea_subcat");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Search
        public DataTable GetSearchResultsByPracticeArea(int PracticeAreaID, string Keywords, int startRowIndex, int maximumRows)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                arrParam[1] = new SqlParameter("@Keywords", Keywords);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_practice_area", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaCount(int PracticeAreaID,string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                arrParam[1] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_practice_area_count", arrParam);

                return dsTemp.Tables[0];
                //if(dsTemp != null)
                //{
                //    if (dsTemp.Tables[0].Rows.Count > 0)
                //        return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                //}
                //return 0;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaGrouping(int PracticeAreaID, string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                arrParam[1] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_practice_area_grouping", arrParam);
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetSearchResultsByPracticeAreaFilter_Court_Year(int PracticeAreaID, string Keywords, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[7];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[3] = new SqlParameter("@StartYear", StartYear);
                arrParam[4] = new SqlParameter("@EndYear", EndYear);
                arrParam[5] = new SqlParameter("@Court", Courts);
                arrParam[6] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_filter_court_year", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaFilter_Court_YearCount(int PracticeAreaID, string Keywords, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@StartYear", StartYear);
                arrParam[2] = new SqlParameter("@EndYear", EndYear);
                arrParam[3] = new SqlParameter("@Court", Courts);
                arrParam[4] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_filter_court_year_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaFilter(int PracticeAreaID, string Keywords, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[7];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[3] = new SqlParameter("@StartYear", StartYear);
                arrParam[4] = new SqlParameter("@EndYear", EndYear);
                arrParam[5] = new SqlParameter("@Court", Courts);
                arrParam[6] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_filter", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaFilterCount(int PracticeAreaID, string Keywords, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@StartYear", StartYear);
                arrParam[2] = new SqlParameter("@EndYear", EndYear);
                arrParam[3] = new SqlParameter("@Court", Courts);
                arrParam[4] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_filter_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaFilterCourt(int PracticeAreaID, string Keywords, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[7];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[3] = new SqlParameter("@StartYear", StartYear);
                arrParam[4] = new SqlParameter("@EndYear", EndYear);
                arrParam[5] = new SqlParameter("@Court", Courts);
                arrParam[6] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_filter_court", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaFilterCourtCount(int PracticeAreaID, string Keywords, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@StartYear", StartYear);
                arrParam[2] = new SqlParameter("@EndYear", EndYear);
                arrParam[3] = new SqlParameter("@Court", Courts);
                arrParam[4] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_filter_court_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       //Search Within
        public DataTable GetSearchWithinResultsByPracticeArea(int PracticeAreaID, string Keywords, string withinkeyword, int startRowIndex, int maximumRows)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", withinkeyword);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByPracticeAreaFilter(int PracticeAreaID, string Keywords, string SearchWithin, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[8];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@StartYear", StartYear);
                arrParam[5] = new SqlParameter("@EndYear", EndYear);
                arrParam[6] = new SqlParameter("@Court", Courts);
                arrParam[7] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_filter", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByPracticeAreaFilterCount(int PracticeAreaID, string Keywords, string SearchWithin, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@StartYear", StartYear);
                arrParam[3] = new SqlParameter("@EndYear", EndYear);
                arrParam[4] = new SqlParameter("@Court", Courts);
                arrParam[5] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_filter_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByPracticeAreaFilter_Court_Year(int PracticeAreaID, string Keywords, string SearchWithin, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[8];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@StartYear", StartYear);
                arrParam[5] = new SqlParameter("@EndYear", EndYear);
                arrParam[6] = new SqlParameter("@Court", Courts);
                arrParam[7] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_filter_court_year", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByPracticeAreaFilter_Court_Year_Count(int PracticeAreaID, string Keywords, string SearchWithin, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@StartYear", StartYear);
                arrParam[3] = new SqlParameter("@EndYear", EndYear);
                arrParam[4] = new SqlParameter("@Court", Courts);
                arrParam[5] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_filter_court_year_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithInResultsByPracticeAreaCount(int PracticeAreaID, string Keywords, string withinkeyword)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", withinkeyword);
                arrParam[2] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_count", arrParam);
                return dsTemp.Tables[0];
                //if (dsTemp != null)
                //{
                //    if (dsTemp.Tables[0].Rows.Count > 0)
                //        return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                //}
                //return 0;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByPracticeAreaGrouping(int PracticeAreaID, string Keywords, string SearchWithin)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_grouping", arrParam);
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetSearchWithinResultsByPracticeAreaFilter_Court(int PracticeAreaID, string Keywords, string SearchWithin, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[8];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@StartYear", StartYear);
                arrParam[5] = new SqlParameter("@EndYear", EndYear);
                arrParam[6] = new SqlParameter("@Court", Courts);
                arrParam[7] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_filter_court", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByPracticeAreaFilter_Court_Count(int PracticeAreaID, string Keywords, string SearchWithin, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@StartYear", StartYear);
                arrParam[3] = new SqlParameter("@EndYear", EndYear);
                arrParam[4] = new SqlParameter("@Court", Courts);
                arrParam[5] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_practice_area_filter_court_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetSearchResultsByPracticeAreaFilterContentType(int PracticeAreaID, string KeyParameter, string Keywords, int startRowIndex, int maximumRows)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keyparameter", KeyParameter);
                arrParam[1] = new SqlParameter("@Keywords", Keywords);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_content_filter", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByPracticeAreaFilterContentTypeCount(int PracticeAreaID, string KeyParameter, string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@Keyparameter", KeyParameter);
                arrParam[1] = new SqlParameter("@Keywords", Keywords);
                arrParam[2] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_practice_area_content_filter_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #endregion
        #region Practice Area Keywords
        public int InsertPracticeAreaKeyword()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@PracticeAreaID", PracticeAreaID);
                arrParam[1] = new SqlParameter("@Keyword", Keyword);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_practice_area_keyword", arrParam);
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
        #endregion
    }
}
