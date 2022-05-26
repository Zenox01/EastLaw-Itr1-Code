using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{
   public class Users
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string OrgName { get; set; }
        public int OrgTypeID { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyLogo { get; set; }
        public string WebURL { get; set; }
        public int UserTypeID { get; set; }
        public string EmailID { get; set; }
        public string Pwd { get; set; }
        public string FullName { get; set; }
        public int PlanID { get; set; }
        public int CompanyID { get; set; }
        public int Verify { get; set; }
        public string Status { get; set; }
        public int NoOfPCAllowd { get; set; }
        public int UserID { get; set; }
        public int ParentFolder { get; set; }
        public string FolderName { get; set; }
        public int FolderID { get; set; }
        public string ItemType { get; set; }
        public string PostalAddress { get; set; }
        public int ItemID { get; set; }
        public int CompUser { get; set; }
        public string CompanyUserAbbr { get; set; }
        public string PlanStart { get; set; }
        public string PlanEnd { get; set; }
        public int Amt { get; set; }
        public int InvoiceID { get; set; }
        public string Remarks { get; set; }
        public string ReceiptNo { get; set; }
        public string Uploadfile { get; set; }
        public string AccessExpireOn { get; set; }
        public int CityID { get; set; }
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion
        #region Companies
        public int InsertCompany()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[14];

                arrParam[0] = new SqlParameter("@CompanyName", CompanyName);
                arrParam[1] = new SqlParameter("@Country", Country);
                arrParam[2] = new SqlParameter("@Address", Address);
                arrParam[3] = new SqlParameter("@PhoneNo", PhoneNo);
                arrParam[4] = new SqlParameter("@ContactPersonName", ContactPersonName);
                arrParam[5] = new SqlParameter("@ContactPersonEmail", ContactPersonEmail);
                arrParam[6] = new SqlParameter("@ContactPersonPhone", ContactPersonPhone);
                arrParam[7] = new SqlParameter("@CompanyEmail", CompanyEmail);
                arrParam[8] = new SqlParameter("@CompanyLogo", CompanyLogo);
                arrParam[9] = new SqlParameter("@WebURL", WebURL);
                arrParam[10] = new SqlParameter("@Active", Active);
                arrParam[11] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[12] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[12].Direction = ParameterDirection.InputOutput;
                arrParam[13] = new SqlParameter("@OrgTypeID", OrgTypeID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_companies", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[12].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditCompany(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[14];

                arrParam[0] = new SqlParameter("@CompanyName", CompanyName);
                arrParam[1] = new SqlParameter("@Country", Country);
                arrParam[2] = new SqlParameter("@Address", Address);
                arrParam[3] = new SqlParameter("@PhoneNo", PhoneNo);
                arrParam[4] = new SqlParameter("@ContactPersonName", ContactPersonName);
                arrParam[5] = new SqlParameter("@ContactPersonEmail", ContactPersonEmail);
                arrParam[6] = new SqlParameter("@ContactPersonPhone", ContactPersonPhone);
                arrParam[7] = new SqlParameter("@CompanyEmail", CompanyEmail);
                arrParam[8] = new SqlParameter("@CompanyLogo", CompanyLogo);
                arrParam[9] = new SqlParameter("@WebURL", WebURL);
                arrParam[10] = new SqlParameter("@Active", Active);
                arrParam[11] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[12] = new SqlParameter("@ID", ID);
                arrParam[13] = new SqlParameter("@OrgTypeID", OrgTypeID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_companies", arrParam);
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
        public int DeleteCompany(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_companies", arrParam);
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

        public DataTable GetCompanies(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_companies", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveCompanies()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_companies");
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetActiveOrgTypes()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_orgtypes");
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Users
        public int InsertUser()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[22];

                arrParam[0] = new SqlParameter("@UserTypeID", UserTypeID);
                arrParam[1] = new SqlParameter("@EmailID", EmailID);
                arrParam[2] = new SqlParameter("@Pwd", Pwd);
                arrParam[3] = new SqlParameter("@FullName", FullName);
                arrParam[4] = new SqlParameter("@PhoneNo", PhoneNo);
                arrParam[5] = new SqlParameter("@Address", Address);
                arrParam[6] = new SqlParameter("@Country", Country);
                arrParam[7] = new SqlParameter("@PlanID", PlanID);
                arrParam[8] = new SqlParameter("@CompanyID", CompanyID);
                arrParam[9] = new SqlParameter("@Verify", Verify);
                arrParam[10] = new SqlParameter("@Status", Status);
                arrParam[11] = new SqlParameter("@Active", Active);
                arrParam[12] = new SqlParameter("@NoOfPCAllowd", NoOfPCAllowd);
                arrParam[13] = new SqlParameter("@AccessExpireOn", AccessExpireOn);
                arrParam[14] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[15] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[15].Direction = ParameterDirection.InputOutput;
                arrParam[16] = new SqlParameter("@OrgTypeID", OrgTypeID);
                arrParam[17] = new SqlParameter("@OrgName", CompanyName);
                arrParam[18] = new SqlParameter("@CityID", CityID);
                arrParam[19] = new SqlParameter("@CompanyUserAbbr", CompanyUserAbbr);
                arrParam[20] = new SqlParameter("@CompUser", CompUser);
                arrParam[21] = new SqlParameter("@PostalAddress", PostalAddress);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_user", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[15].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int EditUser(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[18];

                arrParam[0] = new SqlParameter("@UserTypeID", UserTypeID);
                arrParam[1] = new SqlParameter("@FullName", FullName);
                arrParam[2] = new SqlParameter("@PhoneNo", PhoneNo);
                arrParam[3] = new SqlParameter("@Address", Address);
                arrParam[4] = new SqlParameter("@Country", Country);
                arrParam[5] = new SqlParameter("@PlanID", PlanID);
                arrParam[6] = new SqlParameter("@CompanyID", CompanyID);
                arrParam[7] = new SqlParameter("@Verify", Verify);
                arrParam[8] = new SqlParameter("@Status", Status);
                arrParam[9] = new SqlParameter("@Active", Active);
                arrParam[10] = new SqlParameter("@NoOfPCAllowd", NoOfPCAllowd);
                arrParam[11] = new SqlParameter("@AccessExpireOn", AccessExpireOn);
                arrParam[12] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[13] = new SqlParameter("@ID", ID);
                arrParam[14] = new SqlParameter("@OrgTypeID", OrgTypeID);
                arrParam[15] = new SqlParameter("@CityID", CityID);
                arrParam[16] = new SqlParameter("@OrgName", OrgName);
                arrParam[17] = new SqlParameter("@PostalAddress", PostalAddress);
              
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_user", arrParam);
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
        public int DeleteUser(int ID,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);
               
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_user", arrParam);
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
        public int AddUserPracticeArea(int UserID, int PracticeAreaID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@PracticeAreaID", PracticeAreaID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_userpracticearea", arrParam);
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
        public int AddNewsletterEmailID(string EmailID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@EmailID", EmailID);
                arrParam[1] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[1].Direction = ParameterDirection.InputOutput;
              

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_newsletter_email", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[1].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public int VerifyAccount(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[1];

                arrParam[0] = new SqlParameter("@ID", ID);
                
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_userverify", arrParam);
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
       
        public int MobileVerifyCode(int UserID,string MobileCode)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@mobilecode", MobileCode);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_mobileverifycode", arrParam);
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
        public int MobileVerify(int UserID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[1];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_mobileverify", arrParam);
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
        public int PasswordReset(string EmailID,string Pwd)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@EmailID", EmailID);
                arrParam[1] = new SqlParameter("@Pwd", Pwd);
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_user_password", arrParam);
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
        public DataTable GetUserTypes()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_usertypes");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetUsers(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetUsersByCompany(int CompanyID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@CompanyID", CompanyID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_byCompany", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveAndApprovedUser(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_approved_users", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetExpiredUsersByNoOfDays(int NoOfDays)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@NoOfDays", NoOfDays);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_expired_users_noofdays", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetUsersSearchBackend(string Param)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Cri", Param);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_search_backend", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetUsersSearchBackendOpenQuery(string Param)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                //SqlParameter[] arrParam = new SqlParameter[1];
                //arrParam[0] = new SqlParameter("@Cri", Param);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.Text, Param);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CheckUserExistByEmail(string EmailID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Emailid", EmailID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_check_userexist", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CheckUserExistByPhone(string PhoneNo)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@PhoneNo", PhoneNo);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_check_userexist_byphone", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CheckLogin(string EmailID,string Pwd)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Emailid", EmailID);
                arrParam[1] = new SqlParameter("@Pwd", Pwd);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_check_login", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CheckLogin_IPIntegration(int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@UserID", UserID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_check_login_byIPIntegration", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CheckLoginBackend(string EmailID, string Pwd)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@Emailid", EmailID);
                arrParam[1] = new SqlParameter("@Pwd", Pwd);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_check_login_backend", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetUserPreExpiryList()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_user_list_pre_expiry");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int UpdateUserStatus(int UserID,string Status)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ID", UserID);
                arrParam[1] = new SqlParameter("@Status", Status);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_user_status", arrParam);
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



        public int AddUserNotificationLog(int UserID, string NotiType,string EmailContent)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@NotiType", NotiType);
                arrParam[2] = new SqlParameter("@EmailContent", EmailContent);
                arrParam[3] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_user_notification_log", arrParam);
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
        public DataTable CheckPreExpiryNotificationSent(int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@UserID", UserID);

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_user_pre_expiry_notification_sent", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable UserReport(string startDate, string EndDate)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StartDate", startDate);
                arrParam[1] = new SqlParameter("@EndDate", EndDate);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_report", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable UserOrdersReport(string startDate, string EndDate)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StartDate", startDate);
                arrParam[1] = new SqlParameter("@EndDate", EndDate);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_orders", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable UserElementViewReport(string startDate, string EndDate,int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@StartDate", startDate);
                arrParam[1] = new SqlParameter("@EndDate", EndDate);
                arrParam[2] = new SqlParameter("@UserID", UserID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_elements_view_report", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public DataTable UserLoginReport(string startDate, string EndDate)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StartDate", startDate);
                arrParam[1] = new SqlParameter("@EndDate", EndDate);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_login_report", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable UserLoginChart()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_login_chart");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable UserRegistrationChart()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_reg_chart");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region IPBaseIntegration
        public DataTable GetActiveIPPool()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_active_ippool");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Invoices
        public int AddInvoice(int UserID, int PlanID,string InvoiceNo,string Status,int OrderID,int CreatedBy,string PaymentMethod)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[8];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@PlanID", PlanID);
                arrParam[2] = new SqlParameter("@InvoiceNo", InvoiceNo);
                arrParam[3] = new SqlParameter("@Status", Status);
                arrParam[4] = new SqlParameter("@Order_UserPlanUpdate", OrderID);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@PaymentMethod", PaymentMethod);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_invoice", arrParam);
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
        public int UpdateInvoiceStatus(int ID,string Status,int ModifiedBy,string Remarks)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[7];

                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@Status", Status);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@Remarks", Remarks);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_update_invoice_status", arrParam);
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
        public DataTable GetPendingInvoices()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pending_invoices");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetUserHistory(int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@UserID", UserID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_user_history", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetPendingInvoiceByID(int InvoiceID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@InvoiceID", InvoiceID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pending_invoices_byinvoiceid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetInvoiceByID(int InvoiceID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@InvoiceID", InvoiceID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_invoices_byinvoiceid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetPendingInvoiceByUserID(int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@UserID", UserID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_pending_invoices_byUserID", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable CheckOrderExist(int UserID,int PlanID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@PlanID", PlanID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_check_invoice_exist",arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Users Folders
        public int InsertUserFolder()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@ParentFolder", ParentFolder);
                arrParam[2] = new SqlParameter("@FolderName", FolderName);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
                
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_usersfolder", arrParam);
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
        public int EditUserFolder(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@ParentFolder", ParentFolder);
                arrParam[2] = new SqlParameter("@FolderName", FolderName);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID",ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_usersfolder", arrParam);
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
        public int DeleteUserFolder(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());
                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_usersfolder", arrParam);
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
        public int GetAnnotationFolder(int UserID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[1].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_usersfolder_Annotations", arrParam);
                mTransaction.Commit();
                int chk = int.Parse(arrParam[1].Value.ToString());
                mSqlConnection.Close();
                return chk;
            }
            catch (Exception ex)
            {
                //Utility.ExceptionHelper.Log(ex);
                return 0;
            }
        }
        public DataTable GetUserFolderByUser(int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@UserID", UserID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_userfolderbyuser", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetUserParentFolderByUser(int UserID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@UserID", UserID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_userparentfolder_byuser", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetUserFolderByParent(int ParentFolderID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ParentFolderID", ParentFolderID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_userfolder_byparent", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertUserFolderItem()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[6];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@FolderID", FolderID);
                arrParam[2] = new SqlParameter("@ItemType", ItemType);
                arrParam[3] = new SqlParameter("@ItemID", ItemID);
                arrParam[4] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_usersfolder_items", arrParam);
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
        public int DeleteUserFolderItem(int ID,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ID", ID);
                arrParam[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                
                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_usersfolder_items", arrParam);
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

        public DataTable GetUserFolderItemsByFolder(int FolderID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@FolderID", FolderID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_userfolder_items_byfolder", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region AuditLog
        public int InsertAuditLog(string ActivityType, string Action, string URL, string IPAdd, int UserID, string countryName, string regionName, string cityName, string txt, string ClientBrowser, string Platform,string Source)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[13];

                arrParam[0] = new SqlParameter("@ActivityType", ActivityType);
                arrParam[1] = new SqlParameter("@Action", Action);
                arrParam[2] = new SqlParameter("@URL", URL);
                arrParam[3] = new SqlParameter("@IPAdd", IPAdd);
                arrParam[4] = new SqlParameter("@UserID", UserID);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;
                arrParam[6] = new SqlParameter("@countryName", countryName);
                arrParam[7] = new SqlParameter("@regionName", regionName);
                arrParam[8] = new SqlParameter("@cityName", cityName);
                arrParam[9] = new SqlParameter("@txt", txt);
                arrParam[10] = new SqlParameter("@ClientBrowser", ClientBrowser);
                arrParam[11] = new SqlParameter("@Platform", Platform);
                arrParam[12] = new SqlParameter("@AccessSource", Source);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_auditlog", arrParam);
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
        public int InsertBackendAuditLog(string ActivityType, string Action, string URL, string IPAdd, int UserID, string countryName, string regionName, string cityName, string txt)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[10];

                arrParam[0] = new SqlParameter("@ActivityType", ActivityType);
                arrParam[1] = new SqlParameter("@Action", Action);
                arrParam[2] = new SqlParameter("@URL", URL);
                arrParam[3] = new SqlParameter("@IPAdd", IPAdd);
                arrParam[4] = new SqlParameter("@UserID", UserID);
                arrParam[5] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[5].Direction = ParameterDirection.InputOutput;
                arrParam[6] = new SqlParameter("@countryName", countryName);
                arrParam[7] = new SqlParameter("@regionName", regionName);
                arrParam[8] = new SqlParameter("@cityName", cityName);
                arrParam[9] = new SqlParameter("@txt", txt);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_backend_auditlog", arrParam);
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

        public DataTable GetAuditLog(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_auditlog", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetAuditLogByActivityTypes(string ActivityType, string startDate, string EndDate)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@ActivityType", ActivityType);
                arrParam[1] = new SqlParameter("@StartDate", startDate);
                arrParam[2] = new SqlParameter("@EndDate", EndDate);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_auditlog_byActivityType", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetAuditLogByUserEmail(string UserEmailID, string startDate, string EndDate)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@UserEmailID", UserEmailID);
                arrParam[1] = new SqlParameter("@StartDate", startDate);
                arrParam[2] = new SqlParameter("@EndDate", EndDate);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_auditlog_byUserEmail", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetAuditLogTypes()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_log_types");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetBackendAuditLog(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_backend_auditlog", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region User Process
        public int InsertUserPlanUpdate()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[11];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@PlanID", PlanID);
                arrParam[2] = new SqlParameter("@PlanStart", PlanStart);
                arrParam[3] = new SqlParameter("@PlanEnd", PlanEnd);
                arrParam[4] = new SqlParameter("@Amt", Amt);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@InvoiceID", InvoiceID);
                arrParam[8] = new SqlParameter("@Remarks", Remarks);
                arrParam[9] = new SqlParameter("@ReceiptNo", ReceiptNo);
                arrParam[10] = new SqlParameter("@UploadFile", Uploadfile);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_user_plan_update", arrParam);
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
        #endregion
        #region CRM
        public int InsertCRMUserComments(int UserID, string Comments, string NextFollowUp, int CreatedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@UserID", UserID);
                arrParam[1] = new SqlParameter("@Comments", Comments);
                arrParam[2] = new SqlParameter("@NextFollowUp", NextFollowUp);
              
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
             

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_crm_user_comments", arrParam);
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
        public DataTable GetNearUserExpiryList()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_users_expiry_list_crm");

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
