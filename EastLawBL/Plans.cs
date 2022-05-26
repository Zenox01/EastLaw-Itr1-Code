using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{
    public class Plans
    {
        #region Properties
       private SqlConnection mSqlConnection;
       private SqlTransaction mTransaction;

       public int ID { get; set; }
       public string PlanName { get; set; }
       public int NoofDays { get; set; }
       public int Price { get; set; }
       public int nooflogin_perday { get; set; }
       public int noofcasesview_perday { get; set; }
       public int noofstatutesview_perday { get; set; }
       public int Active { get; set; }
       public int ShowOnFront { get; set; }
       public int NoOfUsers { get; set; }
       public string PlanType { get; set; }
       public float Tax { get; set; }
       public int CreatedBy { get; set; }
       public string CreatedOn { get; set; }
       public int ModifiedBy { get; set; }
       public string ModifiedOn { get; set; }
        #endregion

       #region Methods
       public int InsertPlan()
       {
           try
           {
               SqlParameter[] arrParam = new SqlParameter[13];

               arrParam[0] = new SqlParameter("@PlanName", PlanName);
               arrParam[1] = new SqlParameter("@NoofDays", NoofDays);
               arrParam[2] = new SqlParameter("@Price", Price);
               arrParam[3] = new SqlParameter("@Active", Active);
               arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
               arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
               arrParam[5].Direction = ParameterDirection.InputOutput;
               arrParam[6] = new SqlParameter("@nooflogin_perday", nooflogin_perday);
               arrParam[7] = new SqlParameter("@noofcasesview_perday", noofcasesview_perday);
               arrParam[8] = new SqlParameter("@noofstatutesview_perday", noofstatutesview_perday);
               arrParam[9] = new SqlParameter("@Tax", Tax);
               arrParam[10] = new SqlParameter("@ShowOnFront", ShowOnFront);
               arrParam[11] = new SqlParameter("@NoOfUsers", NoOfUsers);
               arrParam[12] = new SqlParameter("@PlanType", PlanType);

               mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

               mSqlConnection.Open();
               mTransaction = mSqlConnection.BeginTransaction();
               SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_plan", arrParam);
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
       public int EditPlan(int ID)
       {
           try
           {
               SqlParameter[] arrParam = new SqlParameter[13];

               arrParam[0] = new SqlParameter("@PlanName", PlanName);
               arrParam[1] = new SqlParameter("@NoofDays", NoofDays);
               arrParam[2] = new SqlParameter("@Price", Price);
               arrParam[3] = new SqlParameter("@Active", Active);
               arrParam[4] = new SqlParameter("@ModifiedBy", ModifiedBy);
               arrParam[5] = new SqlParameter("@ID", ID);
               arrParam[6] = new SqlParameter("@nooflogin_perday", nooflogin_perday);
               arrParam[7] = new SqlParameter("@noofcasesview_perday", noofcasesview_perday);
               arrParam[8] = new SqlParameter("@noofstatutesview_perday", noofstatutesview_perday);
               arrParam[9] = new SqlParameter("@Tax", Tax);
               arrParam[10] = new SqlParameter("@ShowOnFront", ShowOnFront);
               arrParam[11] = new SqlParameter("@NoOfUsers", NoOfUsers);
               arrParam[12] = new SqlParameter("@PlanType", PlanType);

               mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

               mSqlConnection.Open();
               mTransaction = mSqlConnection.BeginTransaction();
               SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_plan", arrParam);
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
       //public int DeleteAdvocate(int ID, int ModifiedBy)
       //{
       //    try
       //    {
       //        SqlParameter[] arrParam = new SqlParameter[2];

       //        arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
       //        arrParam[1] = new SqlParameter("@ID", ID);

       //        mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

       //        mSqlConnection.Open();
       //        mTransaction = mSqlConnection.BeginTransaction();
       //        SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_advocate", arrParam);
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
     
       public DataTable GetPlans(int ID)
       {
           try
           {
               DataSet dsTemp = new DataSet();
               SqlParameter[] arrParam = new SqlParameter[1];
               arrParam[0] = new SqlParameter("@ID", ID);
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_plans", arrParam);

               return dsTemp.Tables[0];
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataTable GetActivePlans()
       {
           try
           {
               DataSet dsTemp = new DataSet();
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_plans");
               return dsTemp.Tables[0];
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataTable GetActivePlansFrontEnd()
       {
           try
           {
               DataSet dsTemp = new DataSet();
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_plans_frontend");
               return dsTemp.Tables[0];
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataTable GetActiveCorporatePlansFrontEnd()
       {
           try
           {
               DataSet dsTemp = new DataSet();
               dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_corporate_plans_frontend");
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
