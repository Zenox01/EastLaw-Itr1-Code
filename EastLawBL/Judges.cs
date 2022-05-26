using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EastLawBL
{
    public class Judges
    { 
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string JudgeName { get; set; }
        public string Details { get; set; }
        public int CurrentCourtMasterID { get; set; }
        public int Approve { get; set; }
        public string ServiceStart { get; set; }
        public string ServieEnd { get; set; }
        public string ProfileImg { get; set; }

        public int Active { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion
        #region Methods
        public int InsertJudge()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@JudgeName", JudgeName);
                arrParam[1] = new SqlParameter("@Details", Details);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;
                
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_judge", arrParam);
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
        
        public int EditJudge(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@JudgeName", JudgeName);
                arrParam[1] = new SqlParameter("@Details", Details);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@ID", ID);
                
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_judge", arrParam);
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
        public int DeleteJudge(int ID,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_judge", arrParam);
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
        public int UpdateJudgesName(int ID, string NewName)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@JudgeName", NewName);
                arrParam[1] = new SqlParameter("@ModifiedBy", "999999");
                arrParam[2] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_judgenames", arrParam);
                mTransaction.Commit();
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
            //try
            //{
                

            //   string qry = "update tbl_Judges set JudgeName='" + JudgeName + "', ModifiedBy=9999999,ModifiedOn=getdate() where ID=" + ID + "";
               

            //    mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

            //    mSqlConnection.Open();
            //    mTransaction = mSqlConnection.BeginTransaction();
            //    SqlHelper.ExecuteNonQuery(mTransaction, CommandType.Text, qry);
            //    mTransaction.Commit();
            //    mSqlConnection.Close();
            //    return 1;
            //}
            //catch (Exception ex)
            //{
            //    //Utility.ExceptionHelper.Log(ex);
            //    return 0;
            //}
        }
        public DataTable GetJudgeByName(string Name)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@JudgeName", Name);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judge_byname", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetJudge(int ID,int PageStart,int PageEnd)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@startRowIndex", PageStart);
                arrParam[2] = new SqlParameter("@maximumRows", PageEnd);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judges", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetJudgeCount()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judges_count");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetJudgeForLoad(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judgesForLoad", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        #endregion
        #region New Judges
        public int InsertJudgeNew()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[9];

                arrParam[0] = new SqlParameter("@JudgeName", JudgeName);
                arrParam[1] = new SqlParameter("@CurrentCourtMasterID", CurrentCourtMasterID);
                arrParam[2] = new SqlParameter("@Approve", Approve);
                arrParam[3] = new SqlParameter("@Active", Active);
                arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;
                arrParam[6] = new SqlParameter("@ServiceStart", ServiceStart);
                arrParam[7] = new SqlParameter("@ServieEnd", ServieEnd);
                arrParam[8] = new SqlParameter("@profileimg", ProfileImg);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_judge_new", arrParam);
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
        public int EditJudgeNew(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[9];


                arrParam[0] = new SqlParameter("@JudgeName", JudgeName);
                arrParam[1] = new SqlParameter("@CurrentCourtMasterID", CurrentCourtMasterID);
                arrParam[2] = new SqlParameter("@Approve", Approve);
                arrParam[3] = new SqlParameter("@Active", Active);
                arrParam[4] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[5] = new SqlParameter("@ID", ID);
                arrParam[6] = new SqlParameter("@ServiceStart", ServiceStart);
                arrParam[7] = new SqlParameter("@ServieEnd", ServieEnd);
                arrParam[8] = new SqlParameter("@profileimg", ProfileImg);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_judge_new", arrParam);
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
        public int TaggeCaseinJudgesNew(int NewJudgeID, int CaseID, int CreatedBy, int OldTaggedID,ref string err)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@NewJudgeID", NewJudgeID);
                arrParam[1] = new SqlParameter("@CaseID", CaseID);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@OldTaggedID", OldTaggedID);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
              

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_judges_linking_new", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[4].Value.ToString());
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
        public int TaggeCaseinJudgesNewTemp(int NewJudgeID, int CaseID, int CreatedBy, int OldTaggedID, ref string err)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@NewJudgeID", NewJudgeID);
                arrParam[1] = new SqlParameter("@TempCaseID", CaseID);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[3] = new SqlParameter("@OldTaggedID", OldTaggedID);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_judges_linking_new_temp", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[4].Value.ToString());
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
        public int RemoveTaggeCaseinJudgesNew(int CaseID, int ModifiedBy, ref string err)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@CaseID", CaseID);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
              

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_case_judges_linking_new", arrParam);
                mTransaction.Commit();
                
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int RemoveTaggeCaseinJudgesNewByID(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_case_judges_linking_new_byid", arrParam);
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
        public int EditAuthor(int JudgeID, int CaseID, int ModifiedBy,  ref string err)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@JudgeID", JudgeID);
                arrParam[1] = new SqlParameter("@CaseID", CaseID);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
               

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edut_case_judges_linking_new_Author_Update", arrParam);
                mTransaction.Commit();
                
                mSqlConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public DataTable GetJudgeNew(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judges_new", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetJudgeNewByCourtMaster(int CourtMasterID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@CourtMasterID", CourtMasterID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judges_new_byCourtMaster", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetJudgeNewSearch(string Cri)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Cri", Cri);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judges_search_new", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Merge Cases Judges
        public DataTable GetMergeJudgesOld()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_judges_tagged_old");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCasesMergeJudgesOldByJudgeName(string Name,int CourtMasterID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@JudgeName", Name);
                arrParam[1] = new SqlParameter("@CourtMasterID", CourtMasterID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "get_judges_tagged_cases_old_byJudges", arrParam);

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
