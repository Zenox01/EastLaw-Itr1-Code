using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{
    public class Keywords
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string Keyword { get; set; }
        public int Active { get; set; }
        public int ParentID { get; set; }
        public int PracticeAreaSubCatID { get; set; }
       
        public string GlossoryName { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion
        #region Keywords
        public int InsertKeyword()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@Keywords", Keyword);
                arrParam[1] = new SqlParameter("@Active", Active);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_keyword", arrParam);
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
        public int EditKeywords(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@Keywords", Keyword);
                arrParam[1] = new SqlParameter("@Active", Active);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_keyword", arrParam);
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
        public int DeleteKeywords(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_keyword", arrParam);
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
        public int UpdateKeywords()
        {
            try
            {
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_cases_keywords");
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
        
        
        public DataTable GetKeywords(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_keywords", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetKeywordsByLike(string txt)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Keywords", txt);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_like_keywords", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveKeywords()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_keywords");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeyword(string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordTest(string Keywords, int startRowIndex, int maximumRows)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public DataTable GetSearchResultsByKeywordTest(string Keywords,)
        //{
        //    try
        //    {
        //        DataSet dsTemp = new DataSet();
        //        SqlParameter[] arrParam = new SqlParameter[3];
        //        arrParam[0] = new SqlParameter("@Keywords", Keywords);
        //        arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
        //        arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
        //        dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test", arrParam);

        //        return dsTemp.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public DataTable GetSearchResultsByKeywordCountTest(string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_count_test", arrParam);

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
        public DataTable GetSearchWithInResultsByKeywordCountTest(string Keywords, string withinkeyword)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", withinkeyword);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_count_test", arrParam);
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
        public DataTable GetSearchResultsByKeywordGrouping(string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_grouping", arrParam);
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordGrouping_PracticeArea(string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_grouping_practice_area", arrParam);
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeywordGrouping(string Keywords,string SearchWithin)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_grouping", arrParam);
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeyword(string Keywords,string withinkeyword)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", withinkeyword);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeywordTest(string Keywords, string withinkeyword, int startRowIndex, int maximumRows)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", withinkeyword);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_test", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeywordTestFilter(string Keywords,string SearchWithin, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[7];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@StartYear", StartYear);
                arrParam[5] = new SqlParameter("@EndYear", EndYear);
                arrParam[6] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_test_filter", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeywordTestFilterCount(string Keywords,string SearchWithin, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@StartYear", StartYear);
                arrParam[3] = new SqlParameter("@EndYear", EndYear);
                arrParam[4] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_test_filter_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeywordFilter_Court_Year(string Keywords, string SearchWithin, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[7];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@StartYear", StartYear);
                arrParam[5] = new SqlParameter("@EndYear", EndYear);
                arrParam[6] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_filter_court_year", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeywordFilter_Court_Year_Count(string Keywords, string SearchWithin, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@StartYear", StartYear);
                arrParam[3] = new SqlParameter("@EndYear", EndYear);
                arrParam[4] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_filter_court_year_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordTestFilter(string Keywords, int startRowIndex, int maximumRows,int StartYear,int EndYear,string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[3] = new SqlParameter("@StartYear", StartYear);
                arrParam[4] = new SqlParameter("@EndYear", EndYear);
               arrParam[5] = new SqlParameter("@Court", Courts);
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test_filter", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordTestFilterCount(string Keywords, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@StartYear", StartYear);
                arrParam[2] = new SqlParameter("@EndYear", EndYear);
                arrParam[3] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test_filter_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordTestFilterGrouping(string Keywords, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@StartYear", StartYear);
                arrParam[2] = new SqlParameter("@EndYear", EndYear);
                arrParam[3] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test_filter_year_grouping", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordTestFilterCourt(string Keywords, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[3] = new SqlParameter("@StartYear", StartYear);
                arrParam[4] = new SqlParameter("@EndYear", EndYear);
                arrParam[5] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test_filter_court", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordTestFilterCourtCount(string Keywords, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@StartYear", StartYear);
                arrParam[2] = new SqlParameter("@EndYear", EndYear);
                arrParam[3] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test_filter_court_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordTestFilterCourtGrouping(string Keywords, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test_filter_court_count_grouping", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetSearchWithinResultsByKeywordFilter_Court(string Keywords, string SearchWithin, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[7];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[4] = new SqlParameter("@StartYear", StartYear);
                arrParam[5] = new SqlParameter("@EndYear", EndYear);
                arrParam[6] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_filter_court", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchWithinResultsByKeywordFilter_Court_Count(string Keywords, string SearchWithin, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@SearchWithin", SearchWithin);
                arrParam[2] = new SqlParameter("@StartYear", StartYear);
                arrParam[3] = new SqlParameter("@EndYear", EndYear);
                arrParam[4] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_within_results_by_keyword_filter_court_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetSearchResultsByKeywordFilter_Court_Year(string Keywords, int startRowIndex, int maximumRows, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[3] = new SqlParameter("@StartYear", StartYear);
                arrParam[4] = new SqlParameter("@EndYear", EndYear);
                arrParam[5] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_filter_court_year", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordFilter_Court_YearCount(string Keywords, int StartYear, int EndYear, string Courts)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@StartYear", StartYear);
                arrParam[2] = new SqlParameter("@EndYear", EndYear);
                arrParam[3] = new SqlParameter("@Court", Courts);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_filter_court_year_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordFilterContentType(string KeyParameter,string Keywords, int startRowIndex, int maximumRows)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@Keyparameter", KeyParameter);
                arrParam[1] = new SqlParameter("@Keywords", Keywords);
                arrParam[2] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[3] = new SqlParameter("@maximumRows", maximumRows);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_content_filter", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSearchResultsByKeywordFilterContentTypeCount(string KeyParameter, string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Keyparameter", KeyParameter);
                arrParam[1] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_content_filter_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Filter PA
        public DataTable GetSearchResultsByKeywordTestFilterPA(string Keywords, int startRowIndex, int maximumRows, string PA)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                arrParam[3] = new SqlParameter("@pa", PA);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_results_by_keyword_test_filter_pa", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        public int InsertKeywordLinking(string LinkType, int KeywordID, int ItemID, int CreatedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@LinkType", LinkType);
                arrParam[1] = new SqlParameter("@KeywordID", KeywordID);
                arrParam[2] = new SqlParameter("@ItemID", ItemID);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_keywordlinking", arrParam);
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
        public int DeleteKeywordLinking(int KeywordID, int ItemID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

               
                arrParam[0] = new SqlParameter("@KeywordID", KeywordID);
                arrParam[1] = new SqlParameter("@ItemID", ItemID);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
               
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_keywordlinking", arrParam);
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

        #region GlosoryTree
        public int InsertGlossory()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@ParentID", ParentID);
                arrParam[1] = new SqlParameter("@GlossoryName", GlossoryName);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
                arrParam[5] = new SqlParameter("@PracticeAreaSubCatID", PracticeAreaSubCatID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_glossory", arrParam);
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
        public int EditGlossory(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@ParentID", ParentID);
                arrParam[1] = new SqlParameter("@GlossoryName", GlossoryName);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID",ID);
                arrParam[5] = new SqlParameter("@PracticeAreaSubCatID", PracticeAreaSubCatID);
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_glossory", arrParam);
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
        public int DeleteGlossory(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_glossory", arrParam);
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


        public int InsertGlossoryKeywords(int GlossoryID,int KeywordID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@GlossoryID", GlossoryID);
                arrParam[1] = new SqlParameter("@KeywordID", KeywordID);
                arrParam[2] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[2].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_glossory_keyword", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[2].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DeleteGlossoryKeywords(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[1];

                arrParam[0] = new SqlParameter("@ID", ID);
              
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_glossory_keyword", arrParam);
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
        public DataTable GetGlossoryKeyword(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_glossory_keyword", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertGlossoryCitation(int GlossoryID, int CaseID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@GlossoryID", GlossoryID);
                arrParam[1] = new SqlParameter("@CaseId", CaseID);
                arrParam[2] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[2].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_glossory_citation", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[2].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DeleteGlossoryCitation(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[1];

                arrParam[0] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_glossory_citation", arrParam);
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
        public DataTable GetGlossoryCitation(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_glossory_citation", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public int InsertGlossoryLaw(int GlossoryID, int LawID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@GlossoryID", GlossoryID);
                arrParam[1] = new SqlParameter("@LawID", LawID);
                arrParam[2] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[2].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_glossory_law", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[2].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DeleteGlossoryLaw(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[1];

                arrParam[0] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_glossory_law", arrParam);
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
        public DataTable GetGlossoryLaw(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_glossory_law", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetGlossory(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_glossory", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveGlossoryParent()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_glossory_parent");
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetActiveGlossoryByParent(int ParentID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ParentID", ParentID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_glossory_byparent", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetGlossoryByKeywordsSearch(string Keywords)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Keywords", Keywords);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_glossorybykeywords", arrParam);

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
