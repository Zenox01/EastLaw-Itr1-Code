using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EastLawBL
{
   public class Advocates
   { 
       #region Properties
       private SqlConnection mSqlConnection;
       private SqlTransaction mTransaction;

       public int ID { get; set; }
       public string AdvocateName { get; set; }
       public string Details { get; set; }
       public int CreatedBy { get; set; }
       public string CreatedOn { get; set; }
       public int ModifiedBy { get; set; }
       public string ModifiedOn { get; set; }
#endregion
       #region Methods
       public int InsertAdvocate()
       {
           try
           {
               SqlParameter[] arrParam = new SqlParameter[4];

               arrParam[0] = new SqlParameter("@AdvocateName", AdvocateName);
               arrParam[1] = new SqlParameter("@Details", Details);
               arrParam[2] = new SqlParameter("@CreatedBy", CreatedBy);
               arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
               arrParam[3].Direction = ParameterDirection.InputOutput;

               mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

               mSqlConnection.Open();
               mTransaction = mSqlConnection.BeginTransaction();
               SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_advocate", arrParam);
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
       public int EditAdvocate(int ID)
       {
           try
           {
               SqlParameter[] arrParam = new SqlParameter[4];

               arrParam[0] = new SqlParameter("@AdvocateName", AdvocateName);
               arrParam[1] = new SqlParameter("@Details", Details);
               arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
               arrParam[3] = new SqlParameter("@ID", ID);
               
               mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

               mSqlConnection.Open();
               mTransaction = mSqlConnection.BeginTransaction();
               SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_advocate", arrParam);
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
       public int DeleteAdvocate(int ID, int ModifiedBy)
       {
           try
           {
               SqlParameter[] arrParam = new SqlParameter[2];

               arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
               arrParam[1] = new SqlParameter("@ID", ID);

               mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

               mSqlConnection.Open();
               mTransaction = mSqlConnection.BeginTransaction();
               SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_advocate", arrParam);
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
       public DataTable GetAdvocateByName(string Name)
       {
           try
           {
               DataSet dsTemp = new DataSet();
               SqlParameter[] arrParam = new SqlParameter[1];
               arrParam[0] = new SqlParameter("@AdvocateName", Name);
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_advocate_byname", arrParam);

               return dsTemp.Tables[0];
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataTable GetAdvocates(int ID, int PageStart, int PageEnd)
       {
           try
           {
               DataSet dsTemp = new DataSet();
               SqlParameter[] arrParam = new SqlParameter[3];
               arrParam[0] = new SqlParameter("@ID", ID);
               arrParam[1] = new SqlParameter("@startRowIndex", PageStart);
               arrParam[2] = new SqlParameter("@maximumRows", PageEnd);
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_advocates", arrParam);

               return dsTemp.Tables[0];
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataTable GetAdvocatesCount()
       {
           try
           {
               DataSet dsTemp = new DataSet();
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_advocates_count");

               return dsTemp.Tables[0];
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataTable GetAdvocatesForLoad(int ID)
       {
           try
           {
               DataSet dsTemp = new DataSet();
               SqlParameter[] arrParam = new SqlParameter[1];
               arrParam[0] = new SqlParameter("@ID", ID);
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_advocatesForLoad", arrParam);

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
