using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{
   public class Common
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        #endregion

        public DataTable GetCountries()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_countries");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCitiesByCountry(string CountryCode)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CountryCode", CountryCode);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_citiesbycountry", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public DataTable RunInlineQuery(string Query,ref string Err)
        {
            try
            {
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                DataSet dsTemp = new DataSet();
                
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.Text, Query);
                mSqlConnection.Close();
                return dsTemp.Tables[0];
                
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }
        }
        //#region Appeal Type Master
        //public int InsertAppealTypeMaster()
        //{
        //    try
        //    {
        //        SqlParameter[] arrParam = new SqlParameter[8];

        //        arrParam[0] = new SqlParameter("@JudgeName", JudgeName);
        //        arrParam[1] = new SqlParameter("@CurrentCourtMasterID", CurrentCourtMasterID);
        //        arrParam[2] = new SqlParameter("@Approve", Approve);
        //        arrParam[3] = new SqlParameter("@Active", Active);
        //        arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
        //        arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
        //        arrParam[5].Direction = ParameterDirection.InputOutput;
        //        arrParam[6] = new SqlParameter("@ServiceStart", ServiceStart);
        //        arrParam[7] = new SqlParameter("@ServieEnd", ServieEnd);

        //        mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

        //        mSqlConnection.Open();
        //        mTransaction = mSqlConnection.BeginTransaction();
        //        SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_judge_new", arrParam);
        //        mTransaction.Commit();
        //        int chk = int.Parse(arrParam[5].Value.ToString());
        //        mSqlConnection.Close();
        //        return chk;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Utility.ExceptionHelper.Log(ex);
        //        return 0;
        //    }
        //}
        //public int EditJudgeNew(int ID)
        //{
        //    try
        //    {
        //        SqlParameter[] arrParam = new SqlParameter[8];


        //        arrParam[0] = new SqlParameter("@JudgeName", JudgeName);
        //        arrParam[1] = new SqlParameter("@CurrentCourtMasterID", CurrentCourtMasterID);
        //        arrParam[2] = new SqlParameter("@Approve", Approve);
        //        arrParam[3] = new SqlParameter("@Active", Active);
        //        arrParam[4] = new SqlParameter("@ModifiedBy", ModifiedBy);
        //        arrParam[5] = new SqlParameter("@ID", ID);
        //        arrParam[6] = new SqlParameter("@ServiceStart", ServiceStart);
        //        arrParam[7] = new SqlParameter("@ServieEnd", ServieEnd);

        //        mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

        //        mSqlConnection.Open();
        //        mTransaction = mSqlConnection.BeginTransaction();
        //        SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_judge_new", arrParam);
        //        mTransaction.Commit();
        //        mSqlConnection.Close();
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Utility.ExceptionHelper.Log(ex);
        //        return 0;
        //    }
        //}
        //public int TaggeCaseinJudgesNew(int NewJudgeID, int CaseID, int CreatedBy, int OldTaggedID, ref string err)
        //{
        //    try
        //    {
        //        SqlParameter[] arrParam = new SqlParameter[5];

        //        arrParam[0] = new SqlParameter("@NewJudgeID", NewJudgeID);
        //        arrParam[1] = new SqlParameter("@CaseID", CaseID);
        //        arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
        //        arrParam[3] = new SqlParameter("@OldTaggedID", OldTaggedID);
        //        arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
        //        arrParam[4].Direction = ParameterDirection.InputOutput;


        //        mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

        //        mSqlConnection.Open();
        //        mTransaction = mSqlConnection.BeginTransaction();
        //        SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_case_judges_linking_new", arrParam);
        //        mTransaction.Commit();
        //        int chk = int.Parse(arrParam[4].Value.ToString());
        //        mSqlConnection.Close();
        //        return chk;
        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex.Message;
        //        //Utility.ExceptionHelper.Log(ex);
        //        return 0;
        //    }
        //}
        //public DataTable GetJudgeNew(int ID)
        //{
        //    try
        //    {
        //        DataSet dsTemp = new DataSet();
        //        SqlParameter[] arrParam = new SqlParameter[3];
        //        arrParam[0] = new SqlParameter("@ID", ID);
        //        dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judges_new", arrParam);

        //        return dsTemp.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //public DataTable GetJudgeNewByCourtMaster(int CourtMasterID)
        //{
        //    try
        //    {
        //        DataSet dsTemp = new DataSet();
        //        SqlParameter[] arrParam = new SqlParameter[3];
        //        arrParam[0] = new SqlParameter("@CourtMasterID", CourtMasterID);
        //        dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_judges_new_byCourtMaster", arrParam);

        //        return dsTemp.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion
    }
}
