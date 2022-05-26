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
    public class Cases
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string Vari { get; set; }
        public int JournalID { get; set; }
        public int Year { get; set; }
        public string Citation { get; set; }
        public string CitationRef { get; set; }
        public int Judge { get; set; }
        public string JudgeStr { get; set; }
        public string Appeal { get; set; }
        public string Appeallant { get; set; }
        public string Appeallant2 { get; set; }
        public string Appeallant3 { get; set; }
        public string AppeallantType { get; set; }
        public string Respondent { get; set; }
        public string Respondent2 { get; set; }
        public string Respondent3 { get; set; }
        public string JDate { get; set; }
        public int AdvocateA { get; set; }
        public string AdvocateASTR { get; set; }
        public int AdvocateR { get; set; }
        public string AdvocateRSTR { get; set; }
        public string HearDate { get; set; }
        public string HeadNotes { get; set; }
        public string Judgment { get; set; }
        public string JudgmentType { get; set; }
        public string Result { get; set; }
        public string Court { get; set; }
        public string CourtCityName { get; set; }
        public string FileName { get; set; }
        public string Keywords { get; set; }
        public string PageNo { get; set; }
        public string CaseSummary { get; set; }
        public int DateFormated { get; set; }
        public int Status { get; set; }
        public int Active { get; set; }
        public string CourtName { get; set; }
        public int SortOrder { get; set; }
        public int CourtMasterID { get; set; }

        public string GUID { get; set; }

        public string FileNm { get; set; }
        public string ShortDes { get; set; }
        public string Jrd_Cunty { get; set; }
        public string Jrd_Area { get; set; }
        public string Jrd_Distrct { get; set; }

        public byte[] ImageData { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int PriorityTagging { get; set; }
        #endregion
        #region Methods
        public int InsertCase()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[22];

                arrParam[0] = new SqlParameter("@Citation", Citation);
                arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
                arrParam[2] = new SqlParameter("@Judge", Judge);
                arrParam[3] = new SqlParameter("@Appeal", Appeal);
                arrParam[4] = new SqlParameter("@Appeallant", Appeallant);
                arrParam[5] = new SqlParameter("@Respondent", Respondent);
                arrParam[6] = new SqlParameter("@JDate", JDate);
                arrParam[7] = new SqlParameter("@AdvocateA", AdvocateA);
                arrParam[8] = new SqlParameter("@AdvocateR", AdvocateR);
                arrParam[9] = new SqlParameter("@HearDate", HearDate);
                arrParam[10] = new SqlParameter("@HeadNotes", HeadNotes);
                arrParam[11] = new SqlParameter("@Judgment", Judgment);
                arrParam[12] = new SqlParameter("@JudgmentType", JudgmentType);
                arrParam[13] = new SqlParameter("@Result", Result);
                arrParam[14] = new SqlParameter("@Court", Court);
                arrParam[15] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[16] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[16].Direction = ParameterDirection.InputOutput;
                arrParam[17] = new SqlParameter("@AppeallantType", AppeallantType);
                arrParam[18] = new SqlParameter("@JournalID", JournalID);
                arrParam[19] = new SqlParameter("@Year", Year);
                arrParam[20] = new SqlParameter("@FileName", FileName);
                arrParam[21] = new SqlParameter("@Status", Status);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[16].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int InsertCaseAlternate(int CaseID,int JournalID,int Year,string PageNo,string Citation,int Active,int CreatedBy,ref string msg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[9];

                arrParam[0] = new SqlParameter("@CaseID", CaseID);
                arrParam[1] = new SqlParameter("@JournalID", JournalID);
                arrParam[2] = new SqlParameter("@Year", Year);
                arrParam[3] = new SqlParameter("@PageNo", PageNo);
                arrParam[4] = new SqlParameter("@Citation", Citation);
                arrParam[5] = new SqlParameter("@Active", Active);
                arrParam[6] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[7] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[7].Direction = ParameterDirection.InputOutput;
                arrParam[8] = new SqlParameter("@Msg", SqlDbType.VarChar,500);
                arrParam[8].Direction = ParameterDirection.InputOutput;


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_alternate_citation", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[7].Value.ToString());
                msg = arrParam[8].Value.ToString();
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int DeleteCaseAlternate(int ID,  int ModifiedBy, ref string msg)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@ID", ID);
              
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
              
                arrParam[2] = new SqlParameter("@Msg", SqlDbType.VarChar, 500);
                arrParam[2].Direction = ParameterDirection.InputOutput;


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_case_alternate_citation", arrParam);
                mTransaction.Commit();
               
                msg = arrParam[2].Value.ToString();
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int InsertCaseMigrate()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[31];

                arrParam[0] = new SqlParameter("@Citation", Citation);
                arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
                arrParam[2] = new SqlParameter("@Judge", JudgeStr);
                arrParam[3] = new SqlParameter("@Appeal", Appeal);
                arrParam[4] = new SqlParameter("@Appeallant", Appeallant);
                arrParam[5] = new SqlParameter("@Respondent", Respondent);
                arrParam[6] = new SqlParameter("@JDate", JDate);
                arrParam[7] = new SqlParameter("@AdvocateA", AdvocateASTR);
                arrParam[8] = new SqlParameter("@AdvocateR", AdvocateRSTR);
                arrParam[9] = new SqlParameter("@HearDate", HearDate);
                arrParam[10] = new SqlParameter("@HeadNotes", HeadNotes);
                arrParam[11] = new SqlParameter("@Judgment", Judgment);
                arrParam[12] = new SqlParameter("@JudgmentType", JudgmentType);
                arrParam[13] = new SqlParameter("@Result", Result);
                arrParam[14] = new SqlParameter("@Court", Court);
                arrParam[15] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[16] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[16].Direction = ParameterDirection.InputOutput;
                arrParam[17] = new SqlParameter("@AppeallantType", AppeallantType);
                arrParam[18] = new SqlParameter("@JournalID", JournalID);
                arrParam[19] = new SqlParameter("@Year", Year);
                arrParam[20] = new SqlParameter("@FileName", FileName);
                arrParam[21] = new SqlParameter("@Status", Status);
                arrParam[22] = new SqlParameter("@Court_Area", CourtCityName);
                arrParam[23] = new SqlParameter("@CaseSummary", CaseSummary);
                arrParam[24] = new SqlParameter("@DateFormated", DateFormated);
                arrParam[25] = new SqlParameter("@PageNo", PageNo);
                arrParam[26] = new SqlParameter("@PriorityTagging", PriorityTagging);
                arrParam[27] = new SqlParameter("@Appeallant2", Appeallant2);
                arrParam[28] = new SqlParameter("@Appeallant3", Appeallant3);
                arrParam[29] = new SqlParameter("@Respondent2", Respondent2);
                arrParam[30] = new SqlParameter("@Respondent3", Respondent3);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_migration", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[16].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
         public int InsertCaseMigrateTemp()
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[31];

                 arrParam[0] = new SqlParameter("@Citation", Citation);
                 arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
                 arrParam[2] = new SqlParameter("@Judge", JudgeStr);
                 arrParam[3] = new SqlParameter("@Appeal", Appeal);
                 arrParam[4] = new SqlParameter("@Appeallant", Appeallant);
                 arrParam[5] = new SqlParameter("@Respondent", Respondent);
                 arrParam[6] = new SqlParameter("@JDate", JDate);
                 arrParam[7] = new SqlParameter("@AdvocateA", AdvocateASTR);
                 arrParam[8] = new SqlParameter("@AdvocateR", AdvocateRSTR);
                 arrParam[9] = new SqlParameter("@HearDate", HearDate);
                 arrParam[10] = new SqlParameter("@HeadNotes", HeadNotes);
                 arrParam[11] = new SqlParameter("@Judgment", Judgment);
                 arrParam[12] = new SqlParameter("@JudgmentType", JudgmentType);
                 arrParam[13] = new SqlParameter("@Result", Result);
                 arrParam[14] = new SqlParameter("@Court", Court);
                 arrParam[15] = new SqlParameter("@CreatedBy", CreatedBy);
                 arrParam[16] = new SqlParameter("@ID", SqlDbType.Int);
                 arrParam[16].Direction = ParameterDirection.InputOutput;
                 arrParam[17] = new SqlParameter("@AppeallantType", AppeallantType);
                 arrParam[18] = new SqlParameter("@JournalID", JournalID);
                 arrParam[19] = new SqlParameter("@Year", Year);
                 arrParam[20] = new SqlParameter("@FileName", FileName);
                 arrParam[21] = new SqlParameter("@Status", Status);
                 arrParam[22] = new SqlParameter("@Court_Area", CourtCityName);
                 arrParam[23] = new SqlParameter("@CaseSummary", CaseSummary);
                 arrParam[24] = new SqlParameter("@DateFormated", DateFormated);
                 arrParam[25] = new SqlParameter("@PageNo", PageNo);
                 arrParam[26] = new SqlParameter("@Appeallant2", Appeallant2);
                 arrParam[27] = new SqlParameter("@Appeallant3", Appeallant3);
                 arrParam[28] = new SqlParameter("@Respondent2", Respondent2);
                 arrParam[29] = new SqlParameter("@Respondent3", Respondent3);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_migration_temp", arrParam);
                 mTransaction.Commit();
                 int chk = int.Parse(arrParam[16].Value.ToString());
                 mSqlConnection.Close();
                 return chk;
             }
             catch (Exception ex)
             {
                 //Utility.ExceptionHelper.Log(ex);
                 return 0;
             }
         }
         public int InsertCaseUpdateHistory(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[23];

                 arrParam[0] = new SqlParameter("@Citation", Citation);
                 arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
                 arrParam[2] = new SqlParameter("@Appeal", Appeal);
                 arrParam[3] = new SqlParameter("@Appeallant", Appeallant);
                 arrParam[4] = new SqlParameter("@Respondent", Respondent);
                 arrParam[5] = new SqlParameter("@JDate", JDate);
                 arrParam[6] = new SqlParameter("@HearDate", HearDate);
                 arrParam[7] = new SqlParameter("@HeadNotes", HeadNotes);
                 arrParam[8] = new SqlParameter("@Judgment", Judgment);
                 arrParam[9] = new SqlParameter("@JudgmentType", JudgmentType);
                 arrParam[10] = new SqlParameter("@Result", Result);
                 arrParam[11] = new SqlParameter("@Court", Court);
                 arrParam[12] = new SqlParameter("@CreatedBy", CreatedBy);
                 arrParam[13] = new SqlParameter("@ID", SqlDbType.Int);
                 arrParam[13].Direction = ParameterDirection.InputOutput;
                 arrParam[14] = new SqlParameter("@AppeallantType", AppeallantType);
                 arrParam[15] = new SqlParameter("@Year", Year);
                 arrParam[16] = new SqlParameter("@FileName", FileName);
                 arrParam[17] = new SqlParameter("@Status", Status);
                 arrParam[18] = new SqlParameter("@Court_Area", CourtCityName);
                 arrParam[19] = new SqlParameter("@CaseID", CaseID);
                 arrParam[20] = new SqlParameter("@PageNo", PageNo);
                 arrParam[21] = new SqlParameter("@Keywords", Keywords);
                 arrParam[22] = new SqlParameter("@Judge", JudgeStr);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_update_history", arrParam);
                 mTransaction.Commit();
                 int chk = int.Parse(arrParam[13].Value.ToString());
                 mSqlConnection.Close();
                 return chk;
             }
             catch (Exception ex)
             {
                 //Utility.ExceptionHelper.Log(ex);
                 return 0;
             }
         }
        public int UdpdateWrongJDate(int ID, ref string err)
        {
            mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@CaseID", ID);
                


                

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_wrong_jdate", arrParam);
                mTransaction.Commit();
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                mSqlConnection.Close();
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int UpdateCaseUpdateHistoryStatus(int ID, int UserID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@ModifiedBy", UserID);


                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_update_history_status", arrParam);
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
         public int DeleteCasesID(int ID, int UserID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@ModifiedBy", UserID);


                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_casesbyID", arrParam);
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
         public int UpdateCasePublicEnable_Disable(int CaseID, int @PublicDisplay, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@PublicDisplay", PublicDisplay);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
              

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_public_enable_disable", arrParam);
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
         public int DeleteCasesByFileName(string FileName, int UserID, int JournalID, int Year)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[4];

                 arrParam[0] = new SqlParameter("@Filename", FileName);
                 arrParam[1] = new SqlParameter("@ModifiedBy", UserID);
                 arrParam[2] = new SqlParameter("@JournalID", JournalID);
                 arrParam[3] = new SqlParameter("@Year", Year);
                

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_casesbyfilename", arrParam);
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
         public int Publish_UnpublishCasesByFileName(int Publish_UnPublish, string FileName, int UserID, int JournalID, int Year)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[5];

                 arrParam[0] = new SqlParameter("@Filename", FileName);
                 arrParam[1] = new SqlParameter("@ModifiedBy", UserID);
                 arrParam[2] = new SqlParameter("@JournalID", JournalID);
                 arrParam[3] = new SqlParameter("@Year", Year);
                 arrParam[4] = new SqlParameter("@Publish", Publish_UnPublish);


                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_publish_unpublish_casesbyfilename", arrParam);
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

         public int InsertCaseImagesMigrate()
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@GUID", GUID);
                 arrParam[1] = new SqlParameter("@Data",ImageData);
                 arrParam[2] = new SqlParameter("@ID", SqlDbType.Int);
                 arrParam[2].Direction = ParameterDirection.InputOutput;

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_images_migration", arrParam);
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

         public int EditCase(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[21];

                 arrParam[0] = new SqlParameter("@Citation", Citation);
                 arrParam[1] = new SqlParameter("@Judge", Judge);
                 arrParam[2] = new SqlParameter("@Appeal", Appeal);
                 arrParam[3] = new SqlParameter("@Appeallant", Appeallant);
                 arrParam[4] = new SqlParameter("@Respondent", Respondent);
                 arrParam[5] = new SqlParameter("@JDate", JDate);
                 arrParam[6] = new SqlParameter("@AdvocateA", AdvocateA);
                 arrParam[7] = new SqlParameter("@AdvocateR", AdvocateR);
                 arrParam[8] = new SqlParameter("@HearDate", HearDate);
                 arrParam[9] = new SqlParameter("@HeadNotes", HeadNotes);
                 arrParam[10] = new SqlParameter("@Judgment", Judgment);
                 arrParam[11] = new SqlParameter("@JudgmentType", JudgmentType);
                 arrParam[12] = new SqlParameter("@Result", Result);
                 arrParam[13] = new SqlParameter("@Court", Court);
                 arrParam[14] = new SqlParameter("@Modifiedby", ModifiedBy);
                 arrParam[15] = new SqlParameter("@ID", CaseID);
                 arrParam[16] = new SqlParameter("@AppeallantType", AppeallantType);
                 arrParam[17] = new SqlParameter("@JournalID", JournalID);
                 arrParam[18] = new SqlParameter("@Year", Year);
                 arrParam[19] = new SqlParameter("@Status", Status);
                 arrParam[20] = new SqlParameter("@Court_Area", CourtCityName);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_case", arrParam);
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
         public int EditCaseSummary(int CaseID,string CaseSummary)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];

                 arrParam[0] = new SqlParameter("@ID", CaseID);
                 arrParam[1] = new SqlParameter("@CaseSummary", CaseSummary);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_casesummary", arrParam);
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
         public int AddCasesInsideCitations(int CaseID, string Citation, string SearchLevel,string LinkSource,string JournalName,int LinkedCaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[6];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@Citation", Citation);
                 arrParam[2] = new SqlParameter("@SearchLevel", SearchLevel);
                 arrParam[3] = new SqlParameter("@LinkSource", LinkSource);
                 arrParam[4] = new SqlParameter("@JournalName", JournalName);
                 arrParam[5] = new SqlParameter("@LinkedCaseID", LinkedCaseID);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_insidecitations", arrParam);
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
         public int AddCasesInsideCitationsTemp(int CaseID, string Citation, string SearchLevel,  int LinkedCaseID,string Journal)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[6];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@Citation", Citation);
                 arrParam[2] = new SqlParameter("@SearchLevel", SearchLevel);
                 arrParam[3] = new SqlParameter("@LinkedCaseID", LinkedCaseID);
                 arrParam[4] = new SqlParameter("@Journal", Journal);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_insidecitations_temp", arrParam);
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
         public int DeleteCasesInsideCitations(int ID,int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[5];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_case_insidecitations", arrParam);
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
         public int UpdateCasesInsideCitations(int ID, string Citation, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@Citation", Citation);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_insidecitations", arrParam);
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
         public int TaggedCitaionLinking(int ID, int CaseID ,int LinkedCaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@CaseID", CaseID);
                 arrParam[2] = new SqlParameter("@LinkedCaseID", LinkedCaseID);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_insidecitations_tagged_linking", arrParam);
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
         public int AddCasesInsideSection_Rule_Article(int CaseID, string LinkText, string SearchLevel, string LinkType, string LinkSource)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[5];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@LinkText", LinkText);
                 arrParam[2] = new SqlParameter("@SearchLevel", SearchLevel);
                 arrParam[3] = new SqlParameter("@LinkType", LinkType);
                 arrParam[4] = new SqlParameter("@LinkSource", LinkSource);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_InsideSection_Rule_Article", arrParam);
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
         public int DeleteCasesInsideSection_Rule_Article(int ID,int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
               
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_case_InsideSection_Rule_Article", arrParam);
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
         public int UpdateInsideSection_Rule_Article(int ID, string LinkText, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@LinkText", LinkText);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_InsideSection_Rule_Article", arrParam);
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
         public int EditCase_FromUpdateHistory(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[19];

                 arrParam[0] = new SqlParameter("@Citation", Citation);
                 arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
                 arrParam[2] = new SqlParameter("@Appeal", Appeal);
                 arrParam[3] = new SqlParameter("@Appeallant", Appeallant);
                 arrParam[4] = new SqlParameter("@Respondent", Respondent);
                 arrParam[5] = new SqlParameter("@JDate", JDate);
                 arrParam[6] = new SqlParameter("@HearDate", HearDate);
                 arrParam[7] = new SqlParameter("@HeadNotes", HeadNotes);
                 arrParam[8] = new SqlParameter("@Judgment", Judgment);
                 arrParam[9] = new SqlParameter("@JudgmentType", JudgmentType);
                 arrParam[10] = new SqlParameter("@Result", Result);
                 arrParam[11] = new SqlParameter("@Court", Court);
                 arrParam[12] = new SqlParameter("@Modifiedby", ModifiedBy);
                 arrParam[13] = new SqlParameter("@ID", CaseID);
                 arrParam[14] = new SqlParameter("@AppeallantType", AppeallantType);
                 arrParam[15] = new SqlParameter("@Year", Year);
                 arrParam[16] = new SqlParameter("@Status", Status);
                 arrParam[17] = new SqlParameter("@Court_Area", CourtCityName);
                 arrParam[18] = new SqlParameter("@PageNo", PageNo);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_case_from_update_history", arrParam);
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
         public int KeywordTaggingByFileName(string filename)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];

                 arrParam[0] = new SqlParameter("@FileName", filename);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_cases_keywords_byfilename", arrParam);
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
         public int CaseLinkingWithOtherCases()
         {
             try
             {
                 
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_linking_with_cases");
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
         

         public int StatutesTaggingDailyJob(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_tagging_dailyjob", arrParam);
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
         public int IsCasePublic(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseId", CaseID);

                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_get_case_ispublic", arrParam);
                 if (dsTemp != null)
                 {
                     if (dsTemp.Tables[0].Rows.Count > 0)
                     {
                         return 1;
                     }
                     else
                     {
                         return 0;
                     }
                 }
                 return 0;
                 

             }
             catch (Exception ex)
             {
                 //Utility.ExceptionHelper.Log(ex);
                 return 0;
             }
         }
         public int GetSimilarCitation(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_statutes_tagging_dailyjob", arrParam);
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
         public int UpdateStatutesTaggingFlagOnCases(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_statutes_tagging_flag_oncases", arrParam);
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
         public int UpdateInsideCitationsSearchFlagOnCases(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_InsideCitationsSearch_flag_oncases", arrParam);
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
         public int UpdateInsideSectionsSearchFlagOnCases(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_InsideSectionsSearch_flag_oncases", arrParam);
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
         public int UpdateInsideArticleSearchFlagOnCases(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_InsideArtileSearch_flag_oncases", arrParam);
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
         public int UpdateInsideRuleSearchFlagOnCases(int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_InsideRuleSearch_flag_oncases", arrParam);
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
         public int AddJudgesListBycase(int CaseID, string JudgeName)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@JudgeName", JudgeName);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_judges_bycase", arrParam);
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
         public int DeleteJudgesListBycase(int ID, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_judges_bycase", arrParam);
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
         public int UpdateJudgesListBycase(int ID, string JudgeName, int ModifiedBy, int Author)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[4];
                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@JudgeName", JudgeName);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 arrParam[3] = new SqlParameter("@Author", Author);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_judges_bycase", arrParam);
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
         public int UpdateNoofJudgesByCaseID(int CaseID,int NoOfJudges)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@NoOfJudges", NoOfJudges);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_noofjudges_bycase", arrParam);
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
         public int MarkAsFinalreview(int CaseID, int FinalReview, int Publish, int ReadyForPrint_Export)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[4];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@Publish", Publish);
                 arrParam[2] = new SqlParameter("@FinalReview", FinalReview);
                 arrParam[3] = new SqlParameter("@ReadyForPrint_Export", ReadyForPrint_Export);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_mark_final_review_case", arrParam);
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
        public int TaggeCaseinPracticeAreaTemp(int PAID, int CaseID, int CreatedBy, ref string err)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@PAID", PAID);
                arrParam[1] = new SqlParameter("@TempCaseID", CaseID);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_practicearea_temp", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[3].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int TaggeCaseinPracticeArea(int PAID, int CaseID, int CreatedBy, ref string err)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@PAID", PAID);
                arrParam[1] = new SqlParameter("@CaseID", CaseID);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_practicearea_linking", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[3].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public DataTable GetCasesForStatutesTagging()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_for_statutes_tagging");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public bool isCitationExist(string CitationNumber)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CitationNo", CitationNumber);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_is_citation_exist",arrParam);
                 if (dsTemp != null)
                 {
                     if (dsTemp.Tables[0].Rows.Count > 0)
                         return true;
                     else
                         return false;
                 }
                 return true;
                 
             }
             catch (Exception ex)
             {
                 return false;
             }
         }
         public bool isCitationExistInTemp(string CitationNumber)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CitationNo", CitationNumber);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_is_citation_exist_intemp", arrParam);
                 if (dsTemp != null)
                 {
                     if (dsTemp.Tables[0].Rows.Count > 0)
                         return true;
                     else
                         return false;
                 }
                 return true;

             }
             catch (Exception ex)
             {
                 return false;
             }
         }
         public DataTable GetAlternateCitationByCaseID(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_alternate_citation_bycaseid", arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
        public DataTable GetAlternateCitationByCaseID_ForBackend(int CaseID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CaseID", CaseID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_alternate_citation_bycaseid_for_backend", arrParam);
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCheckStatutesTaggingCondition(int CaseID,string StatutesTitle)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@CaseId", CaseID);
                 arrParam[1] = new SqlParameter("@StatutesTitle", StatutesTitle);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_check_statutes_tagging_variations", arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetLatestCases()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_get_latest_cases");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetLatestCasesFromMain()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_latest_cases_from_main");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public int InsertLatestCasesIntoFrontView(int CaseID, string Citation, string Appeallant, string Respondent,
             string JDate, string Court, string CasePracticeArea, string CaseTaggedStatutes, string CaseSummary, int CreatedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[10];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@Citation", Citation);
                 arrParam[2] = new SqlParameter("@Appeallant", Appeallant);
                 arrParam[3] = new SqlParameter("@Respondent", Respondent);
                 arrParam[4] = new SqlParameter("@JDate", JDate);
                 arrParam[5] = new SqlParameter("@Court", Court);
                 arrParam[6] = new SqlParameter("@CasePracticeArea", CasePracticeArea);
                 arrParam[7] = new SqlParameter("@CaseTaggedStatutes", CaseTaggedStatutes);
                 arrParam[8] = new SqlParameter("@CaseSummary", CaseSummary);
                 arrParam[9] = new SqlParameter("@CreatedBy", CreatedBy);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_cases_latest_front", arrParam);
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
         public DataTable GetLatestCasesFront()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_get_latest_cases_front");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
        public DataTable GetLatestCasesFront_Public()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_get_latest_cases_front_public_api");
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCasesByCourt(string Court)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@Court", Court);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_cases_bycourt", arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesByCourtFront(string Court)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@Court", Court);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_cases_bycourt_front", arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesForInsideCitationsSearch()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "[sp_get_cases_for_InsideCitationsSearch]");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesForInsideSectionsSearch()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_for_InsideSectionsSearch");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesTagged_Section_Rule_Article(int CaseID,string SearchLevel)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@SearchLevel", SearchLevel);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_tagged_Sections_Rule_Article",arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesForInsideArticleSearch()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_for_InsideArticleSearch");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesForInsideRuleSearch()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_for_InsideRuleSearch");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesForJudgesFind()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_for_judgesfind");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }

         public DataTable GetListofJudgesByCase(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_list_of_judges_bycase", arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetListofJudgesByCaseNew(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_list_of_judges_bycase_new", arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesMarkedIsDeleted1()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_mark_isdeleted1");
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public int DeleteCasesMarkIsDeleted1(int ID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@ID", ID);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_cases_mark_isdeleted1",arrParam);
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
         public int UpdateCourtNames_Area(string CourtName, string NewCourtName, string Court_Area, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[5];

                 arrParam[0] = new SqlParameter("@CourtName", CourtName);
                 arrParam[1] = new SqlParameter("@NewCourtName", NewCourtName);
                 arrParam[2] = new SqlParameter("@Court_Area", Court_Area);
                 arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                // arrParam[4] = new SqlParameter("@CourtMasterID", CourtMasterID);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_cases_courts_area", arrParam);
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
         public int UpdateCourtNames_Area_ByCaseID(int CaseID, string NewCourtName, string Court_Area, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[4];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@NewCourtName", NewCourtName);
                 arrParam[2] = new SqlParameter("@Court_Area", Court_Area);
                 arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_cases_courts_area_bycase", arrParam);
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
         public int UpdateAppealant_ByCaseID(int CaseID, string Appealant, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@Appealant", Appealant);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_cases_Appealant_bycase", arrParam);
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
         public int UpdateRespondent_ByCaseID(int CaseID, string Respondent, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@Respondent", Respondent);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_cases_Respondent_bycase", arrParam);
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
         public int EditCaseDateFormating(int ID, string DDate, int Formated, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[4];


                 arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 arrParam[1] = new SqlParameter("@ID", ID);
                 arrParam[2] = new SqlParameter("@JDate", DDate);
                 arrParam[3] = new SqlParameter("@DateFormated", Formated);



                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_dateformating", arrParam);
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
         public int AddUserCaseAnnotation(int CaseID,int UserID,string SelectedTex,string AnnotedText)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[5];


                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@UserID", UserID);
                 arrParam[2] = new SqlParameter("@SelectedText", SelectedTex);
                 arrParam[3] = new SqlParameter("@AnnotedText", AnnotedText);
                 arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                 arrParam[4].Direction = ParameterDirection.InputOutput;


                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_user_case_annotation", arrParam);
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
         public DataTable GetUserCaseAnnotation(int CaseID, int UserID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@UserID", UserID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_user_case_annotation", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         static byte[] GetBytes(SqlDataReader reader)
         {
             const int CHUNK_SIZE = 2 * 1024;
             byte[] buffer = new byte[CHUNK_SIZE];
             long bytesRead;
             long fieldOffset = 0;
             using (MemoryStream stream = new MemoryStream())
             {
                 while ((bytesRead = reader.GetBytes(2, fieldOffset, buffer, 0, buffer.Length)) > 0)
                 {
                     stream.Write(buffer, 0, (int)bytesRead);
                     fieldOffset += bytesRead;
                 }
                 return stream.ToArray();
             }
         }
         public DataTable GetCasesImagesByGUID(string GUID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp.Tables.Add("CaseImage");
                dsTemp.Tables[0].Columns.Add("GUID");
                dsTemp.Tables[0].Columns.Add("ImgData");
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@GUID", GUID);
                 //dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_images_byGUID", arrParam);


                 //SqlDataReader rdr = new SqlDataReader;
                 SqlDataReader rdr = SqlHelper.ExecuteReader(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_images_byGUID", arrParam);
                 while (rdr.Read())
                 {
                     byte[] buffer = GetBytes(rdr);
                     DataRow dr =dsTemp.Tables[0].NewRow();
                     //dr["ImgData"] = ;
                     dr["ImgData"] = System.Text.Encoding.ASCII.GetString(buffer);
                     dr["GUID"] = rdr.GetString(1);
                    dsTemp.Tables[0].Rows.Add(dr);
                 }
                 dsTemp.AcceptChanges();


                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }

         public DataTable GetCasesByJudge(int JudgeID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@JudgeID", JudgeID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_byJudge", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCaseHistoryForUpdate(int ID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@ID", ID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_history_for_update", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCases(int ID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@ID", ID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesAll(int ID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@ID", ID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_all", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public int GetCasesAllCount()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
             
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_all_count");

                 return int.Parse(dsTemp.Tables[0].Rows[0]["TotalCount"].ToString());
             }
             catch (Exception ex)
             {
                 return 0;
             }
         }
         public DataTable GetCasesAll_ForSimillarCitation(int StartRow,int EndRow)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@startRowIndex", StartRow);
                 arrParam[1] = new SqlParameter("@maximumRows", EndRow);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_for_Similar_Citation", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public int AddSimilarCitation(int SourceCitation, int SimilarCitation, double MatchPercentage)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];
                 arrParam[0] = new SqlParameter("@SourceCitation", SourceCitation);
                 arrParam[1] = new SqlParameter("@SimilarCitation", SimilarCitation);
                 arrParam[2] = new SqlParameter("@MatchPercentage", MatchPercentage);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_citation_similar", arrParam);
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
         public double GetSimilarCiationWithLevenshtein(int Judgment1, int Judgment2)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@Judgment01", Judgment1);
                 arrParam[1] = new SqlParameter("@Judgment02", Judgment2);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "getsimilarciationbyLevenshtein", arrParam);

                 return double.Parse(dsTemp.Tables[0].Rows[0]["Per"].ToString());
             }
             catch (Exception ex)
             {
                 return 0;
             }
         }
         public DataTable GetCasesYears(int JournalID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@JournalID", JournalID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_casesyears_byjournal", arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesFilesByJournalAndYear(int JournalID,int Year)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@JournalID", JournalID);
                 arrParam[1] = new SqlParameter("@Year", Year);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_casesfilenames",arrParam);
                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesSearch(string Param, int startRowIndex, int maximumRows)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[3];
                 arrParam[0] = new SqlParameter("@Cri", Param);
                 arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                 arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_search", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesSearch_Year_Journal_PageNo(int Year,int JournalID,string PageNo)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[3];
                 arrParam[0] = new SqlParameter("@Year", Year);
                 arrParam[1] = new SqlParameter("@JournalID", JournalID);
                 arrParam[2] = new SqlParameter("@Pageno", PageNo);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_search_by_journal_year_pageno", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesSearchBackend(string Param)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@Cri", Param);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_search_backend", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesAdvanceSearch(string Param)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@Cri", Param);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_advance_search", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }

         public DataTable GetCaseForDynamicExcel(string Param,string Columns)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[2];
                 arrParam[0] = new SqlParameter("@Cri", Param);
                 arrParam[1] = new SqlParameter("@Columns", Columns);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_for_dynamic_excel", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCitationsforFormat(int JournalID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@JournalID", JournalID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_citations_for_format", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public int FormatCitation(int ID,string CitationRef)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_format_citation", arrParam);
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
         public int UpdateJdate(int ID, string JDate)
         {
             try
             {
                 string qry = "update tbl_cases set JDate='"+JDate+ "', ModifiedOn=getdate(),DateFormated=1 where ID=" + ID;
                 //SqlParameter[] arrParam = new SqlParameter[2];

                 //arrParam[0] = new SqlParameter("@ID", ID);
                 //arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.Text,qry);
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
         public int UpdatePageNo(int ID, string PageNo)
         {
             try
             {
                 string qry = "update tbl_cases set PageNo='" + PageNo + "', ModifiedOn=getdate() where ID=" + ID;
                 //SqlParameter[] arrParam = new SqlParameter[2];

                 //arrParam[0] = new SqlParameter("@ID", ID);
                 //arrParam[1] = new SqlParameter("@CitationRef", CitationRef);
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
         public int UpdateStatuteTaggedAttribute(int ID, string AttriLink,int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@AttriLink", AttriLink);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_get_linked_Statutes_update_attribute", arrParam);
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
         public DataTable GetCasesCourtsGroup()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_casescourts_group");

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }

         public DataTable GetLinkedCases(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_linked_cases", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetLinkedCasesWithDetails(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_linked_cases_withdetails", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetLinkedStatutes(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_linked_Statutes", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetLinkedStatutesBackend(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_linked_Statutes_backend", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCasesByCitationNo(string CitationNo)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CitationNo", CitationNo);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_search_bycitation", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCaseseInsideCitations(int CaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_insidecitation", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCaseseInsideCitationsPendingLinking()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
             
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_insidecitation_pending_linking");

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         #region Cases Temp
         public DataTable GetCaseTempPendingMove()
         {
             try
             {
                 DataSet dsTemp = new DataSet();

                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_temp_pending_move");

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCaseseJuges_TempPendingMove(int TempCaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@TempCaseID", TempCaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_temp_judges_pending_move", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
        public DataTable GetCasesePracticeArea_TempPendingMove(int TempCaseID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@TempCaseID", TempCaseID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_temp_practicearea_pending_move", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCaseseInsideCitation_TempPendingMove(int TempCaseID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@TempCaseID", TempCaseID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_temp_insidecitation_pending_move", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public int UpdateCaseTemp(int ID,int Move,int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@Move", Move);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_temp_move", arrParam);
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
         public int UpdateCaseJudgesTemp(int ID, int Move, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@Move", Move);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_judges_temp_move", arrParam);
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
        public int UpdateCasePracticeAreaTemp(int ID, int Move, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@Move", Move);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_practicearea_temp_move", arrParam);
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
        public int UpdateCaseInsideCitationTemp(int ID, int Move, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@ID", ID);
                 arrParam[1] = new SqlParameter("@Move", Move);
                 arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_case_insidecitation_temp_move", arrParam);
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
         #region Citations Variation
         public int InsertCitationVariation()
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@Vari", Vari);
                 arrParam[1] = new SqlParameter("@CreatedBy", CreatedBy);
                 arrParam[2] = new SqlParameter("@ID", SqlDbType.Int);
                 arrParam[2].Direction = ParameterDirection.InputOutput;

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_citation_variation", arrParam);
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
         public int EditCitationVariation(int ID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@Vari", Vari);
                 arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 arrParam[2] = new SqlParameter("@ID", ID);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_citation_variation", arrParam);
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
         public int DeleteCitationVariation(int ID, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[2];
                    arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 arrParam[1] = new SqlParameter("@ID", ID);

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_citation_variation", arrParam);
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
         public DataTable GetCitationVariation(int ID)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@ID", ID);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_citation_variation", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetCitationVariationByVari(string Vari)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@Vari", Vari);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_citation_variation_byvari", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetActiveCasesLessInfo()
         {
             try
             {
                 DataSet dsTemp = new DataSet();

                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_less_info");

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }

         public int InsertCitationVariationTagging(int CitationVariationID,int CaseID)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@CaseID", CaseID);
                 arrParam[1] = new SqlParameter("@CitationVariation", CitationVariationID);
                 arrParam[2] = new SqlParameter("@ID", SqlDbType.Int);
                 arrParam[2].Direction = ParameterDirection.InputOutput;

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_citation_variation_tagging", arrParam);
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
        #endregion

        #region "Users Comments"
         public int AddUserComment(string ItemType, int ItemID, string UserComment, string Status, int CreatedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[6];

                 arrParam[0] = new SqlParameter("@ItemType", ItemType);
                 arrParam[1] = new SqlParameter("@ItemID", ItemID);
                 arrParam[2] = new SqlParameter("@UserComment", UserComment);
                 arrParam[3] = new SqlParameter("@Status", Status);
                 arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                 arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                 arrParam[5].Direction = ParameterDirection.InputOutput;

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_usercomment", arrParam);
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
         public int UpdateUserComment(int ID,string Status, int ModifiedBy)
         {
             try
             {
                 SqlParameter[] arrParam = new SqlParameter[3];

                 arrParam[0] = new SqlParameter("@Status", Status);
                 arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                 
                 arrParam[2] = new SqlParameter("@ID", ID);
                 

                 mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                 mSqlConnection.Open();
                 mTransaction = mSqlConnection.BeginTransaction();
                 SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_usercomment", arrParam);
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

         public DataTable GetPendingUsersComments()
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pending_usercomment");

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
        #endregion
        
         public DataSet GetAllTaggingByCase(int CaseID)
         {
             try
             {
                 //DataTable dtSRA = new DataTable("Sections_Rule_Articles");
                 //dtSRA = GetCasesTagged_Section_Rule_Article(CaseID, "");

                 DataTable dtStatutes = new DataTable("TaggedStatutes");
                 dtStatutes = GetLinkedStatutesBackend(CaseID);

                 DataTable dtInsideCitations = new DataTable("InsideCitations");
                 dtInsideCitations = GetCaseseInsideCitations(CaseID);

                 DataTable dtJudges = new DataTable("CaseJudges");
                 dtJudges = GetListofJudgesByCase(CaseID);

                 
                 
                 //DataSet dsTemp = new DataSet();
                 //dsTemp.Tables.Add(dtSRA.Copy(),"Sections_Rule_Articles");
                 //dsTemp.Tables.Add(dtStatutes.Copy(),"TaggedStatutes");

                 DataSet dsTemp = new DataSet();
                 //dtSRA.TableName = "Sections_Rule_Articles";
                 dtStatutes.TableName = "TaggedStatutes";
                 dtInsideCitations.TableName = "InsideCitations";
                 dtJudges.TableName = "CaseJudges";
                 dsTemp.Tables.AddRange(new DataTable[] {  dtStatutes.Copy(), dtInsideCitations.Copy(),dtJudges.Copy() });

                 return dsTemp;
             }
             catch (Exception ex)
             {
                 return null;
             }
         }
         public DataTable GetFullyTaggedCases(int FinalReview)
         {
             try
             {
                 DataSet dsTemp = new DataSet();
                 SqlParameter[] arrParam = new SqlParameter[1];
                 arrParam[0] = new SqlParameter("@FinalReview", FinalReview);
                 dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_cases_fullytagged", arrParam);

                 return dsTemp.Tables[0];
             }
             catch (Exception ex)
             {
                 return null;
             }
         }

        #endregion
        #region Courts Clubing
        public int AddCourtMaster()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[10];

                arrParam[0] = new SqlParameter("@CourtName", CourtName);
                arrParam[1] = new SqlParameter("@SortOrder", SortOrder);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
                arrParam[5] = new SqlParameter("@FileNm", FileNm);
                arrParam[6] = new SqlParameter("@ShortDes", ShortDes);
                arrParam[7] = new SqlParameter("@Jrd_Cunty", Jrd_Cunty);
                arrParam[8] = new SqlParameter("@Jrd_Area", Jrd_Area);
                arrParam[9] = new SqlParameter("@Jrd_Distrct", Jrd_Distrct);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_court_master", arrParam);
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
        public int EditCourtMaster(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[10];

                arrParam[0] = new SqlParameter("@CourtName", CourtName);
                arrParam[1] = new SqlParameter("@SortOrder", SortOrder);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID", ID);
                arrParam[5] = new SqlParameter("@FileNm", FileNm);
                arrParam[6] = new SqlParameter("@ShortDes", ShortDes);
                arrParam[7] = new SqlParameter("@Jrd_Cunty", Jrd_Cunty);
                arrParam[8] = new SqlParameter("@Jrd_Area", Jrd_Area);
                arrParam[9] = new SqlParameter("@Jrd_Distrct", Jrd_Distrct);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_court_master", arrParam);
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
        public int DeleteCourtMaster(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_court_master", arrParam);
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

        public DataTable GetCourtMasters(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_court_master", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveCourtMasters()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_court_master_active");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        

        public int AddCourtMasterMapping()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@MapCourtName", CourtName);
                arrParam[1] = new SqlParameter("@CourtMasterID", CourtMasterID);
                arrParam[2] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[2].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_court_master_mapping", arrParam);
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
        public int AddCourtMasterLinking(int crt_ref,string Trantag,int refcode,int CreatedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@crt_ref", crt_ref);
                arrParam[1] = new SqlParameter("@Trantag", Trantag);
                arrParam[2] = new SqlParameter("@refcode", refcode);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_court_master_lnking", arrParam);
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
        public int DeleteCourtMasterLinking(int crt_ref, string Trantag,  int modifiedby)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@crt_ref", crt_ref);
                arrParam[1] = new SqlParameter("@Trantag", Trantag);
                arrParam[2] = new SqlParameter("@ModifiedBy", modifiedby);
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_court_master_lnking", arrParam);
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
        public DataTable GetCourtMasterslinking(int Crtid,string Trantag)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Crtid", Crtid);
                arrParam[1] = new SqlParameter("@Trantag", Trantag);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_court_master_lnking", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Courts Appeal Master
        public int AddCourtAppealMaster(string appl_mstr,int active,int createdby)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@AppealMaster", appl_mstr);
                arrParam[1] = new SqlParameter("@Active", active);
                arrParam[2] = new SqlParameter("@CreatedBy", createdby);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_court_appeal_master", arrParam);
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
        public int EditCourtAppealMaster(int ID,string appeal,int active,int modifiedby)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@AppealMaster", appeal);
                arrParam[1] = new SqlParameter("@Active", active);
                arrParam[2] = new SqlParameter("@ModifiedBy", modifiedby);
                arrParam[3] = new SqlParameter("@ID", ID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_court_appeal_master", arrParam);
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
        public int DeleteCourtAppealMaster(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_court_appeal_master", arrParam);
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

        public DataTable GetCourtAppealMasters(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_court_appeal_master", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveCourtAppealMasters()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_court_appeal_master_active");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region CourtMastersHeirchy
        public int AddCourtHeirchyMaster(string Crt, int Sort, int active )
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@Crt", Crt);
                arrParam[1] = new SqlParameter("@Active", active);
                arrParam[2] = new SqlParameter("@Sort", Sort);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_court_heirchy_master", arrParam);
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
        public int EditCourtHeirchyMaster(int ID, string Crt, int Sort, int active)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@Crt", Crt);
                arrParam[1] = new SqlParameter("@Active", active);
                arrParam[2] = new SqlParameter("@Sort", Sort);
                arrParam[3] = new SqlParameter("@ID", ID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_court_heirchy_master", arrParam);
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
        public int DeleteCourtHeirchyMaster(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                
                arrParam[0] = new SqlParameter("@ID", ID);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_court_heirchy_master", arrParam);
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

        public DataTable GetCourtHeirchyMasters(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_court_heirchy__master", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveCourtMastersHeirchy()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_court_master_heirchy_active");

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
