using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{
   public class GeneralArea
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public int TypeID { get; set; }
        public string Subject { get; set; }
        public string DDate { get; set; }
        public string ShortDes { get; set; }
        public string FullDes { get; set; }
        public string Author { get; set; }
        public int WorkFlowStatus { get; set; }
        public int GeneralAreasID { get; set; }
        public int PracticeAreaSubCatID { get; set; }

        public string DocFileName { get; set; }
        public string DocTitle { get; set; }
     
        
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion


        #region General Area Types
        public DataTable GetGeneralAreaTypes(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_generalarea_types", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region General Area
        public int InsertGeneralArea()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[10];

                arrParam[0] = new SqlParameter("@TypeID", TypeID);
                arrParam[1] = new SqlParameter("@Subject", Subject);
                arrParam[2] = new SqlParameter("@DDate", DDate);
                arrParam[3] = new SqlParameter("@ShortDes", ShortDes);
                arrParam[4] = new SqlParameter("@FullDes", FullDes);
                arrParam[5] = new SqlParameter("@Author", Author);
                arrParam[6] = new SqlParameter("@WorkFlowStatus", WorkFlowStatus);
                arrParam[7] = new SqlParameter("@Active", Active);
                arrParam[8] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[9] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[9].Direction = ParameterDirection.InputOutput;
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_generalarea", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[9].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditGeneralArea(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[10];

                arrParam[0] = new SqlParameter("@TypeID", TypeID);
                arrParam[1] = new SqlParameter("@Subject", Subject);
                arrParam[2] = new SqlParameter("@DDate", DDate);
                arrParam[3] = new SqlParameter("@ShortDes", ShortDes);
                arrParam[4] = new SqlParameter("@FullDes", FullDes);
                arrParam[5] = new SqlParameter("@Author", Author);
                arrParam[6] = new SqlParameter("@WorkFlowStatus", WorkFlowStatus);
                arrParam[7] = new SqlParameter("@Active", Active);
                arrParam[8] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[9] = new SqlParameter("@ID", ID);
               


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_generalarea", arrParam);
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
        public int DeleteGeneralArea(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_generalarea", arrParam);
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

       /// <summary>
       /// General Area - Practice Area Cat
       /// </summary>
       /// <returns></returns>
        public int InsertGeneralAreaPracticeAreaSubCat()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@GeneralAreasID", GeneralAreasID);
                arrParam[1] = new SqlParameter("@PracticeAreaSubCatID", PracticeAreaSubCatID);
                arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_generalarea_practiceAreasSubCat", arrParam);
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
        public int DeleteGeneralAreaByPracticeAreaSubCat()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];

                arrParam[0] = new SqlParameter("@GeneralAreasID", GeneralAreasID);
                arrParam[1] = new SqlParameter("@PracticeAreaSubCatID", PracticeAreaSubCatID);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_generalarea_BypracticeAreasSubCat", arrParam);
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
        public int DeleteGeneralAreaPracticeAreaSubCatALL()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@GeneralAreasID", GeneralAreasID);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_generalarea_practiceAreasSubCat_ByAll", arrParam);
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

       /// <summary>
       /// General Area - Documents
       /// </summary>
       /// <returns></returns>
        public int InsertGeneralAreaDocuments()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@GeneralAreasID", GeneralAreasID);
                arrParam[1] = new SqlParameter("@DocFileName", DocFileName);
                arrParam[2] = new SqlParameter("@DocTitle", DocTitle);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_generalarea_Documents", arrParam);
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
        public int DeleteGeneralAreaDocumentsByGeneralID()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@GeneralAreasID", GeneralAreasID);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_generalarea_Documents_ByGeneralAreaID", arrParam);
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
        public int DeleteGeneralAreaDocumentsByID()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_generalarea_Documents_ByID", arrParam);
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


        public DataTable GetGeneralAreas(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_generalarea", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetGeneralAreasByTypeID(int TypeID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@TypeID", TypeID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_generalareabytypes", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetGeneralAreasPracticeSubCat(int GeneralAreaID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@GeneralAreasID", GeneralAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_generalareabytypespracticeareascat", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetGeneralAreasDocuments(int GeneralAreaID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@GeneralAreasID", GeneralAreaID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_generalareadocuments", arrParam);

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
