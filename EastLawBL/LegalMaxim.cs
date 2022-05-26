using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace EastLawBL
{
   public class LegalMaxim
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string FileName { get; set; }
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion

        #region Legal Maxim
        public int InsertLegalMaximWord()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@Word", Word);
                arrParam[1] = new SqlParameter("@Meaning", Meaning);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
                arrParam[5] = new SqlParameter("@FileName", FileName);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_legalmaximword", arrParam);
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
        public int EditLegalMaximWord(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@Word", Word);
                arrParam[1] = new SqlParameter("@Meaning", Meaning);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_legalmaximword", arrParam);
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
        public int DeleteLegalMaximWord(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_legalmaximword", arrParam);
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

        public DataTable GetLegalMaximWord(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_legalmaximWords", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetActiveLegalMaximWord()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_legalmaximWords");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetLegalMaximMatchWordByCase(int CaseID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CaseID", CaseID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_legalmaxim_match_cases", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetLegalMaximMeaningByWord(string Word)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Word", Word);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_legalmaxim_byword", arrParam);

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
