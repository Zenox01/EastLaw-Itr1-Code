using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EastLawBL
{
    public class Statutes
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string CatName { get; set; }
        public int CatID { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Act { get; set; }
        public string Cntnt { get; set; }
        public int PrimaryStatutesID { get; set; }
        public int WorkflowID { get; set; }
        public string TitleVariation1 { get; set; }
        public string TitleVariation2 { get; set; }
        public string TitleVariation3 { get; set; }
        public int StatutesID { get; set; }
        public string IndexTitle { get; set; }
        public string Section { get; set; }
        public string IndexContent { get; set; }
        public string IndexLink { get; set; }
        public int GroupID { get; set; }
        public int SubGroupID { get; set; }
        public string Primary_Secondary { get; set; }
        public string LastUpdatedDocumentDate { get; set; }

        public string StatutesContentType { get; set; }
        public string Archive { get; set; }
        public string Certified { get; set; }
        public string IndexNo { get; set; }
        public string PDFFileName { get; set; }
        public string WordFileName { get; set; }
        public string Chapter_Schedule { get; set; }
        public string Chapter_Schedule_Title { get; set; }
        public string Part_Schedule { get; set; }
        public string Footnote { get; set; }

        public int SYear { get; set; }
        public string SType { get; set; }
        public int IsDeleted { get; set; }
        public int Active { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion

        #region Categories
        public int InsertStatutesCat()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@CatName", CatName);
                arrParam[1] = new SqlParameter("@Active", Active);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_cat", arrParam);
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
        public int EditStatutesCat(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];


                arrParam[0] = new SqlParameter("@CatName", CatName);
                arrParam[1] = new SqlParameter("@Active", Active);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_statutes_cat", arrParam);
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
        public int DeleteStatutesCat(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_statutes_cat", arrParam);
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

        public DataTable GetStatutesCategories(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_categories", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveStatutesCategories()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_statutes_categories");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Methods
        public int InsertStatutesUtility()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[21];

                arrParam[0] = new SqlParameter("@Title", Title);
                arrParam[1] = new SqlParameter("@Date", Date);
                arrParam[2] = new SqlParameter("@Cntnt", Cntnt);
                arrParam[3] = new SqlParameter("@WorkflowID", WorkflowID);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@CatName", CatName);
                arrParam[8] = new SqlParameter("@Act", Act);
                arrParam[9] = new SqlParameter("@GroupID", GroupID);
                arrParam[10] = new SqlParameter("@SubGroupID", SubGroupID);
                arrParam[11] = new SqlParameter("@Pri_Sec", Primary_Secondary);
                arrParam[12] = new SqlParameter("@StatutesContentType", StatutesContentType);
                arrParam[13] = new SqlParameter("@PDFFileName", PDFFileName);
                arrParam[14] = new SqlParameter("@WordFileName", WordFileName);
                arrParam[15] = new SqlParameter("@SYear", SYear);
                arrParam[16] = new SqlParameter("@SType", SType);
                arrParam[17] = new SqlParameter("@Archive", Archive);
                arrParam[18] = new SqlParameter("@Certified", Certified);
                arrParam[19] = new SqlParameter("@LastDocumentUpdateDate", LastUpdatedDocumentDate);
                arrParam[20] = new SqlParameter("@PrimaryStatutes", PrimaryStatutesID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_migration", arrParam);
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
        public int InsertStatutesUtilityAttributes()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

              
                arrParam[0] = new SqlParameter("@Date", Date);
                arrParam[1] = new SqlParameter("@WorkflowID", WorkflowID);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", ID);
                arrParam[4] = new SqlParameter("@CatName", CatName);
                arrParam[5] = new SqlParameter("@Act", Act);
               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_migration_attributes", arrParam);
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
        public int InsertStatutes()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[12];

                arrParam[0] = new SqlParameter("@Title", Title);
                arrParam[1] = new SqlParameter("@Date", Date);
                arrParam[2] = new SqlParameter("@Cntnt", Cntnt);
                arrParam[3] = new SqlParameter("@WorkflowID", WorkflowID);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@CatID", CatID);
                arrParam[8] = new SqlParameter("@Act", Act);
                arrParam[9] = new SqlParameter("@TitleVariation1", TitleVariation1);
                arrParam[10] = new SqlParameter("@TitleVariation2", TitleVariation3);
                arrParam[11] = new SqlParameter("@TitleVariation3", TitleVariation3);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes", arrParam);
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
        public int UpdateStatutesFilesName(string FileType, string FileName, int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@FileType", FileType);
                arrParam[1] = new SqlParameter("@FileName", FileName);
                arrParam[2] = new SqlParameter("@ID", ID);
               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_migration_filesname", arrParam);
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
        public int UpdateStatutesSOATagEnable(int StatuteID,int SOATagEnable,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@StatutesID", StatuteID);
                arrParam[1] = new SqlParameter("@SOATagEnable", SOATagEnable);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_statutes_soatagenable", arrParam);
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
        public int UpdateStatutesPublicEnable_Disable(int StatueID, int @PublicDisplay, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@StatueID", StatueID);
                arrParam[1] = new SqlParameter("@PublicDisplay", PublicDisplay);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_statute_public_enable_disable", arrParam);
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
        private int RunInlineQuery(string Query)
        {
            try
            {


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.Text, Query);
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
        public int AddStatuteMultiCategory(int StatuteID,int CategoryID,int UserID,ref string msg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                arrParam[1] = new SqlParameter("@CategoryID", CategoryID);
                arrParam[2] = new SqlParameter("@CreatedBy", UserID);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;
                
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statute_multi_category", arrParam);
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
        public int InsertStatutesIndexDetailsUtility()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[13];

                arrParam[0] = new SqlParameter("@StatutesID", StatutesID);
                arrParam[1] = new SqlParameter("@IndexTitle", IndexTitle);
                arrParam[2] = new SqlParameter("@IndexContent", IndexContent);
                arrParam[3] = new SqlParameter("@IndexLink", IndexLink);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@Section", Section);
                arrParam[8] = new SqlParameter("@IndexNo", IndexNo);
                arrParam[9] = new SqlParameter("@Chapter_Schedule", Chapter_Schedule);
                arrParam[10] = new SqlParameter("@Part_Schedule", Part_Schedule);
                arrParam[11] = new SqlParameter("@Footnote", Footnote);
                arrParam[12] = new SqlParameter("@Chapter_SchduleTitle", Chapter_Schedule_Title);
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_indexdetails_migration", arrParam);
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
        public int EditStatutesIndexDetails(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[11];

                arrParam[0] = new SqlParameter("@IndexTitle", IndexTitle);
                arrParam[1] = new SqlParameter("@IndexContent", IndexContent);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID", ID);
                arrParam[5] = new SqlParameter("@Section", Section);
                arrParam[6] = new SqlParameter("@IndexNo", IndexNo);
                arrParam[7] = new SqlParameter("@Chapter_Schedule", Chapter_Schedule);
                arrParam[8] = new SqlParameter("@Part_Schedule", Part_Schedule);
                arrParam[9] = new SqlParameter("@Footnote", Footnote);
                arrParam[10] = new SqlParameter("@Chapter_SchduleTitle", Chapter_Schedule_Title);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_statutes_indexdetails", arrParam);
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
        public int DeleteStatutesIndexDetails(int ID,int ModifiedBy )
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

               
                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);



                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_statutes_indexdetails", arrParam);
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
        public int EditStatutes(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[20];

                arrParam[0] = new SqlParameter("@Title", Title);
                arrParam[1] = new SqlParameter("@Date", Date);
                arrParam[2] = new SqlParameter("@Cntnt", Cntnt);
                arrParam[3] = new SqlParameter("@WorkflowID", WorkflowID);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[6] = new SqlParameter("@ID", ID);
                arrParam[7] = new SqlParameter("@CatID", CatID);
                arrParam[8] = new SqlParameter("@Act", Act);
                arrParam[9] = new SqlParameter("@TitleVariation1", TitleVariation1);
                arrParam[10] = new SqlParameter("@TitleVariation2", TitleVariation3);
                arrParam[11] = new SqlParameter("@TitleVariation3", TitleVariation3);
                arrParam[12] = new SqlParameter("@GroupID", GroupID);
                arrParam[13] = new SqlParameter("@SubGroupID", SubGroupID);
                arrParam[14] = new SqlParameter("@Pri_Sec", Primary_Secondary);
                //arrParam[15] = new SqlParameter("@StatutesContentType", StatutesContentType);
                arrParam[15] = new SqlParameter("@PDFFileName", PDFFileName);
                arrParam[16] = new SqlParameter("@WordFileName", WordFileName);
                arrParam[17] = new SqlParameter("@LastDocumentUpdateDate", LastUpdatedDocumentDate);
                arrParam[18] = new SqlParameter("@PrimaryStatutes", PrimaryStatutesID);
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_statutes", arrParam);
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
        public int DeleteStatutes(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_statutes", arrParam);
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

        public int InsertStatutesLinking(string LinkType, int StatutesID, int ItemID, int CreatedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@LinkType", LinkType);
                arrParam[1] = new SqlParameter("@StatutesID", StatutesID);
                arrParam[2] = new SqlParameter("@ItemID", ItemID);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_linking", arrParam);
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
        public int DeleteStatutesLinking(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_statutes_linking", arrParam);
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
        public int InsertStatutesTaggingCases()
        {
            try
            {
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_linking_case");
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

        public int UpdateStatutesCategoriesBulk(string CatName,int CreatedBy,int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@CatName", CatName);
              
                arrParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[2] = new SqlParameter("@ID", ID);
              

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_statutes_category_bulk", arrParam);
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
        public DataTable GetStatutesMultiCategory(int StatuteID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_multi_category", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutes(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveStatutes()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_statutes");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetLatestStatutes()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_get_latest_statutes");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetLatestStatutesSearch(string Param)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Cri", Param);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_get_latest_statutes_search", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public DataTable GetActiveStatusByPri_Sec(string Pri_Sec)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Pri_Sec", Pri_Sec);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_by", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable InlineQueryDatatable(string query)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string qry = query;
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.Text, qry);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetStatutesIDGroup()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string qry = "select StatutesID from tbl_Statutes_IndexDetails where IsDeleted=0 group by StatutesID";
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.Text,qry);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesIndexShortByStatutesID(int StatutesID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string qry = "select ID,IndexNo,SortOrder,IsDeleted,Active,IndexTitle from [dbo].[tbl_Statutes_IndexDetails] where StatutesID=" + StatutesID + " order by id";
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.Text,qry);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int UpdateStatutesIndexSortOrder(int StatutesIndexID,int SortOrder,int ModifiedBy)
        {
            try
            {

                string qry = "update tbl_Statutes_IndexDetails set SortOrder=" + SortOrder + ",ModifiedOn=getdate(),modifiedby=" + ModifiedBy + " where ID=" + StatutesIndexID;


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.Text, qry);
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
        public DataTable GetStatutesIndex(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutesindex", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesIndexByStatutesID(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutesindex_by_statutesid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesIndexDetailedByChapter_Schdule(int StatutesID, string Chapter_Shcdule)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StatutesID", StatutesID);
                arrParam[1] = new SqlParameter("@Chapter_Shcdule", Chapter_Shcdule);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutesindex_by_ByChapter_Schdule_statutesid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesIndexSectionGroupByStatutesID(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_section_group_statutesindex_by_statutesid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SqlDataReader GetStatutesListBySectionReader(string Type)
        {
            try
            {
               
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Type", Type);
               SqlDataReader  sdr = SqlHelper.ExecuteReader(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_list_by_section", arrParam);

                return sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesListBySection(string Type)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Type", Type);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_list_by_section", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTaggedSectionsByStatutes(int StatuteID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_tagges_sections_bysectionserch", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCasesSearchByStatueAndSection(int StatuteID,string Keyword)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                arrParam[1] = new SqlParameter("@Keywords", Keyword);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_case_search_bystatutes_and_section", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetCasesSearchByStatueAndSectionNew(int StatuteID, int ElementID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                arrParam[1] = new SqlParameter("@ElementID", ElementID);
               
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_case_search_bystatutes_and_section_soa", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSearch(string Param)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Cri", Param);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_search", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetStatutesByTitle(string Title)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string qry = "select ID,Title from tbl_Statutes where title='" + Title + "'";
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.Text,qry);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesByQuery(string qry)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Title", qry);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_like_search", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSearchFreeText(string Param1,string Param2)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Cri1", Param1);
                arrParam[1] = new SqlParameter("@Cri2", Param2);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_search_freetext", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSearchWithinIndex(string Param)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Cri", Param);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_search_withinindex", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesCatWithYear()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_statutes_catwithyear");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        
        }
        public DataTable GetStatutesCatWithYearByCatID(int CatID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CatID", CatID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_statutes_catwithyear_bycatid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveStatutesLessInfo()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_less_info");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveStatutes_TaggesWithCaseLessInfo()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_tagged_with_cases_less_info");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveStatutesLessInfo_ForSOATagging()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_less_info_forsoatag");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesLessInfo()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_less_info_without_active");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesByPrimaryStatutes(int StatutesID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PrimaryStatutes", StatutesID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_byPrimary", arrParam);


                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Common
        public DataTable GetStatutesGroup(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_group", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSubGroupByGroup(int GroupID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@GroupID", GroupID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statutes_subgroup_bygroup", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Statute SOA
        public int InsertStatutesSOA(int StatuteID, int ParentID, string ElementData, int SortOrder, int Active, int CreatedBy,ref string strMsg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[8];

                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                arrParam[1] = new SqlParameter("@ParentID", ParentID);
                arrParam[2] = new SqlParameter("@ElementData", ElementData);
                arrParam[3] = new SqlParameter("@SortOrder", SortOrder);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@Msg", SqlDbType.VarChar,1000);
                arrParam[7].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statute_soa", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[6].Value.ToString());
                strMsg = arrParam[7].Value.ToString();
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditStatutesSOA(int ID, string ElementData, int SortOrder,string IndexContent, int Active, int ModifiedBy, ref string strMsg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@ElementData", ElementData);
                arrParam[1] = new SqlParameter("@SortOrder", SortOrder);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID", ID);
                arrParam[5] = new SqlParameter("@IndexContent", IndexContent);

                //arrParam[5] = new SqlParameter("@Msg", SqlDbType.VarChar);
                //arrParam[5].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_statute_soa", arrParam);
                mTransaction.Commit();
                //strMsg = arrParam[5].Value.ToString();
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditStatutesSOA_without_content(int ID, string ElementData,  int ModifiedBy, ref string strMsg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@ElementData", ElementData);
                
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[2] = new SqlParameter("@ID", ID);
                

                //arrParam[5] = new SqlParameter("@Msg", SqlDbType.VarChar);
                //arrParam[5].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_statute_soa_without_content", arrParam);
                mTransaction.Commit();
                //strMsg = arrParam[5].Value.ToString();
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DeleteStatutesSOA(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_statute_soa", arrParam);
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

        public int DragAndDropUpdate(int source, int ParentID,  int ModifiedBy, ref string strMsg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[8];

                arrParam[0] = new SqlParameter("@source", source);
                arrParam[1] = new SqlParameter("@ParentID", ParentID);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@Msg", SqlDbType.VarChar, 1000);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_Edit_statute_soa_dragdroptree", arrParam);
                mTransaction.Commit();
                
                strMsg = arrParam[3].Value.ToString();
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DragAndDropMerging(int source, int destination, int ModifiedBy, ref string strMsg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[8];

                arrParam[0] = new SqlParameter("@source", source);
                arrParam[1] = new SqlParameter("@destination", destination);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@Msg", SqlDbType.VarChar, 1000);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_Edit_statute_soa_dragdroptree_merging", arrParam);
                mTransaction.Commit();

                strMsg = arrParam[3].Value.ToString();
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int InsertStatutesSOATagging(int StatuteSOAID, string ItemType, int ItemID, int Active, int CreatedBy, ref string strMsg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[8];

                arrParam[0] = new SqlParameter("@StatuteSOAID", StatuteSOAID);
                arrParam[1] = new SqlParameter("@ItemType", ItemType);
                arrParam[2] = new SqlParameter("@ItemID", ItemID);
                arrParam[3] = new SqlParameter("@Active", Active);
                arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;
                arrParam[6] = new SqlParameter("@Msg", SqlDbType.VarChar, 1000);
                arrParam[6].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statute_soa_tagging", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[5].Value.ToString());
                strMsg = arrParam[6].Value.ToString();
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DeleteStatutesSOATagging(int ID,  int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ID", ID);
               
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
              

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_statute_soa_tagging", arrParam);
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


        public DataTable GetStatutesSOAIndex(int ID, int StatuteID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@StatuteID", StatuteID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_section_soa_index_bystatute_ByID", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSOAParentIndex(int StatuteID, int ParentID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                arrParam[1] = new SqlParameter("@ParentID", ParentID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_statute_soa_index_parent_bystatutes_ByID", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSOATaggingbySOAID(int SOAID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@SectionAOAID", SOAID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_section_soa_tagging_bysoaid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSOATaggingbyCitationSearch(int StatuteID,int Year,int JournalID,string PageNo)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@StatuteID", StatuteID);
                arrParam[1] = new SqlParameter("@Year", Year);
                arrParam[2] = new SqlParameter("@JournalID", JournalID);
                arrParam[3] = new SqlParameter("@PageNo", PageNo);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_section_soa_tagging_bysearch_citation", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetStatutesSOATaggingbyCaseID(int CaseID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CaseID", CaseID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_section_soa_tagging_byCaseid", arrParam);

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
