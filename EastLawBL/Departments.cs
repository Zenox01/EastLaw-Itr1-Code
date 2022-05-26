using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EastLawBL
{

    public class Departments
    {
        #region Properties
        private SqlConnection mSqlConnection;
        private SqlTransaction mTransaction;

        public int ID { get; set; }
        public int ParentID { get; set; }
        public string DeptName { get; set; }
        public int DeptID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string No { get; set; }
        public string DDate { get; set; }
        public string DType { get; set; }
        public string HTMLFile { get; set; }
        public string WordFile { get; set; }
        public string FileContent { get; set; }
        public string FileType { get; set; }

        public int DateFormated { get; set; }
        public int Active { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        #endregion

        #region Department
        public int InsertDepartment()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@ParentID", ParentID);
                arrParam[1] = new SqlParameter("@DeptName", DeptName);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[4] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[4].Direction = ParameterDirection.InputOutput;
                

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_dept", arrParam);
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

        public int EditDepartment(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[5];

                arrParam[0] = new SqlParameter("@ParentID", ParentID);
                arrParam[1] = new SqlParameter("@DeptName", DeptName);
                arrParam[2] = new SqlParameter("@Active", Active);
                arrParam[3] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[4] = new SqlParameter("@ID", ID);
     


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_dept", arrParam);
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

        public int DeleteDepartment(int ID,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_dept", arrParam);
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

        public int InsertDepartmentFile()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[14];

                arrParam[0] = new SqlParameter("@DeptID", DeptID);
                arrParam[1] = new SqlParameter("@HTMLFile", HTMLFile);
                arrParam[2] = new SqlParameter("@WordFile", WordFile);
                arrParam[3] = new SqlParameter("@FileContent", FileContent);
                arrParam[4] = new SqlParameter("@Active", Active);
                arrParam[5] = new SqlParameter("@CreatedBy", CreatedBy);
                arrParam[6] = new SqlParameter("@ID", SqlDbType.Int);
                arrParam[6].Direction = ParameterDirection.InputOutput;
                arrParam[7] = new SqlParameter("@Title", Title);
                arrParam[8] = new SqlParameter("@Year", Year);
                arrParam[9] = new SqlParameter("@No", No);
                arrParam[10] = new SqlParameter("@DDate", DDate);
                arrParam[11] = new SqlParameter("@DType", DType);
                arrParam[12] = new SqlParameter("@DateFormated", DateFormated);
                arrParam[13] = new SqlParameter("@FileType", FileType);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_add_dept_file", arrParam);
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

        public int EditDepartmentFile(int ID)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[10];

                //arrParam[0] = new SqlParameter("@DeptID", DeptID);
                //arrParam[1] = new SqlParameter("@HTMLFile", HTMLFile);
                //arrParam[2] = new SqlParameter("@WordFile", WordFile);
                arrParam[0] = new SqlParameter("@FileContent", FileContent);
                arrParam[1] = new SqlParameter("@Active", Active);
                arrParam[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[3] = new SqlParameter("@ID", ID);
                arrParam[4] = new SqlParameter("@Title", Title);
                arrParam[5] = new SqlParameter("@Year", Year);
                arrParam[6] = new SqlParameter("@No", No);
                arrParam[7] = new SqlParameter("@DDate", DDate);
                arrParam[8] = new SqlParameter("@DType", DType);
                arrParam[9] = new SqlParameter("@DateFormated", DateFormated);


                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_dept_file", arrParam);
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
        public int EditDepartmentFileExcel(int DateFormated)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[8];

                
                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);
                arrParam[2] = new SqlParameter("@Title", Title);
                arrParam[3] = new SqlParameter("@Year", Year);
                arrParam[4] = new SqlParameter("@No", No);
                arrParam[5] = new SqlParameter("@DDate", DDate);
                arrParam[6] = new SqlParameter("@DType", DType);
                arrParam[7] = new SqlParameter("@DateFormated", DateFormated);



                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_dept_file_excel", arrParam);
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
        public int EditDepartmentDTypeExcel()
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[3];


                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);
                arrParam[2] = new SqlParameter("@DType", DType);
                



                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_dept_file_DType", arrParam);
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
        public int EditDepartmentFileDateFormating(int ID,string DDate,int Formated,int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[4];


                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);
                arrParam[2] = new SqlParameter("@DDate", DDate);
                arrParam[3] = new SqlParameter("@DateFormated", Formated);



                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_edit_dept_file_dateformating", arrParam);
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
        public int DeleteDepartmentFile(int ID, int ModifiedBy)
        {
            try
            {
                SqlParameter[] arrParam = new SqlParameter[2];

                arrParam[0] = new SqlParameter("@ModifiedBy", ModifiedBy);
                arrParam[1] = new SqlParameter("@ID", ID);

                mSqlConnection = new SqlConnection(DBHelper.GetConnectionString());

                mSqlConnection.Open();
                mTransaction = mSqlConnection.BeginTransaction();
                SqlHelper.ExecuteNonQuery(mTransaction, CommandType.StoredProcedure, "sp_delete_dept_file", arrParam);
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

        public DataTable GetDepartments(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetActiveDeptParent()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_parent");
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetActiveDeptByParent(int ParentID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ParentID", ParentID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_byparent", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetGroupParent(int GroupID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@GroupID", GroupID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_group_parent", arrParam);
                if(dsTemp != null)
                {
                    if (dsTemp.Tables[0].Rows.Count > 0)
                        return int.Parse(dsTemp.Tables[0].Rows[0]["ParentID"].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable GetDepartmentFilesByDepartments(int DeptID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@DeptID", DeptID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_bydept", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFilesByGroupAndDeptParent(int DeptParent,string Group)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@DeptParentID", DeptParent);
                arrParam[1] = new SqlParameter("@Group", Group);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_bygroup_and_parentid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFilesByDepartmentsParent(int DeptParentID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@DeptParentID", DeptParentID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_bydeptparent", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFilesByDepartmentsParentForPracticeArea(int DeptParentID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@DeptParentID", DeptParentID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_bydeptparent_for_practicearea", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFilesByDepartmentsParentForPracticeAreaDynamic(string Cri)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@cri", Cri);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_bydeptparent_for_practicearea_dynamic", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentDDTypeByDepartmentsParentForPracticeArea(int DeptParentID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@DeptParentID", DeptParentID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_DType_bydeptparent_for_practicearea", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetActiveDepartmentsLessInfo()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_less_info");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFilesLatest()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_latest");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFilesTypesGroup()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_types_group");

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFilesTypesGroupByDeptID(int DeptID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@DeptID", DeptID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_types_group_byDeptID", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable DepartmentSearch(string Cri)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Cri", Cri);
                
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_search", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable DepartmentSearchWithPagging(string Cri, int startRowIndex, int maximumRows)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@Cri", Cri);
                arrParam[1] = new SqlParameter("@startRowIndex", startRowIndex);
                arrParam[2] = new SqlParameter("@maximumRows", maximumRows);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_search_withpagging", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable DepartmentSearchCount(string Cri)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Cri", Cri);

                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_search_count", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFileDetailsByID(int ID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@ID", ID);
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_detailsbyid", arrParam);

                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDepartmentFileDetailsAll()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = SqlHelper.ExecuteDataset(DBHelper.GetConnectionString(), CommandType.StoredProcedure, "sp_get_dept_file_details_all");

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
