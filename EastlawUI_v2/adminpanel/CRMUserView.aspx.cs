using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Configuration;

namespace EastlawUI_v2.adminpanel
{
    public partial class CRMUserView : System.Web.UI.Page
    {
        EastLawBL.Plans objPlan = new EastLawBL.Plans();
        public string SortDireaction
        {
            get
            {
                if (ViewState["SortDireaction"] == null)
                    return string.Empty;
                else
                    return ViewState["SortDireaction"].ToString();
            }
            set
            {
                ViewState["SortDireaction"] = value;
            }
        }
        private string _sortDirection;
        EastLawBL.Users objUsr = new EastLawBL.Users();
        DataTable dt = new DataTable();
        Image sortImage = new Image();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                    Response.Redirect("NotAuthorize.aspx");
            }
        }
        void UsersSearch()
        {
            try
            {
                if(!string.IsNullOrEmpty(txtName.Text) || (!string.IsNullOrEmpty(txtSearchEmailID.Text)) || (!string.IsNullOrEmpty(txSearchtMobileNo.Text)))
                {
                    string cri = "Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn) as FormatedExpire,E.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,D.CompanyName,dbo.ReturnUserCRMComments(A.ID) as UserCRMComments "
                        //",(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin"
                        + " , '' as FirstLogin"
                        + " from dbo.tbl_Users A"
            + " inner join Country B on A.Country=B.Code"
            + " inner join tbl_Plans C on A.PlanID=C.ID"
            + " inner join tbl_Companies D on A.CompanyID=D.ID"
            + " inner join dbo.tbl_UserType E on A.UserTypeID=E.ID Where A.CreatedOn is not null and A.isdeleted=0";

              

              

                if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                    cri = cri + " AND A.FullName Like '%" + txtName.Text.Trim() + "%' ";
                    //cri = cri + " AND A.FullName= '" + txtName.Text.Trim() + "' ";
                    

                if (!string.IsNullOrEmpty(txtSearchEmailID.Text.Trim()))
                    cri = cri + " AND  A.EmailID='" + txtSearchEmailID.Text + "' ";

                if (!string.IsNullOrEmpty(txSearchtMobileNo.Text.Trim()))
                    cri = cri + " AND  A.PhoneNo='" + txSearchtMobileNo.Text + "' ";

                cri = cri + " union all "
            + " Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn),D.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,'' as CompanyName,dbo.ReturnUserCRMComments(A.ID) as UserCRMComments"
            //+" ,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin"
            + " ,'' as FirstLogin"
            + " from dbo.tbl_Users A"
            + " inner join Country B on A.Country=B.Code"
            + " inner join tbl_Plans C on A.PlanID=C.ID"
            + " inner join dbo.tbl_UserType D on A.UserTypeID=D.ID  Where A.CreatedOn is not null and A.isdeleted=0";

                //if (ddlCiJournal.SelectedValue != "0" && ddlCYear.SelectedValue != "0" && txtCNumber.Text != "")
                //    cri = cri + " AND E.JournalName='" + ddlCiJournal.SelectedValue + "' AND A.Year=" + ddlCYear.SelectedValue + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";

              


                if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                    cri = cri + " AND A.FullName= '" + txtName.Text.Trim() + "' ";
                    //cri = cri + " AND A.FullName Like '%" + txtName.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(txtSearchEmailID.Text.Trim()))
                    cri = cri + " AND  A.EmailID='" + txtSearchEmailID.Text + "' ";

                if (!string.IsNullOrEmpty(txSearchtMobileNo.Text.Trim()))
                    cri = cri + " AND  A.PhoneNo='" + txSearchtMobileNo.Text + "' ";

                DataTable dt = new DataTable();
                dt = objUsr.GetUsersSearchBackendOpenQuery(cri);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("strActive");
                        dt.Columns.Add("CustNo");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["Active"].ToString() == "1")
                                dt.Rows[a]["strActive"] = "Yes";
                            else
                                dt.Rows[a]["strActive"] = "No";

                            dt.Rows[a]["CustNo"] = dt.Rows[a]["ID"].ToString().PadLeft(6, '0');
                        }
                        dt.AcceptChanges();
                        DataView dv = dt.DefaultView;
                        if (this.ViewState["SortExpression"] != null)
                        {
                            dv.Sort = string.Format("{0} {1}", ViewState["SortExpression"].ToString(), this.ViewState["SortOrder"].ToString());
                        }
                        gv.DataSource = dt;
                        gv.DataBind();
                    }
                    else
                    {
                        gv.DataSource = null;
                        gv.DataBind();
                    }
                }
                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UsersSearch();

        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                UsersSearch();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gv.Rows[e.RowIndex];
                HiddenField hd = default(HiddenField);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int ID = Convert.ToInt32(hd.Value);

                    int chk = objUsr.DeleteUser(ID, int.Parse(Session["UserID"].ToString()));
                    if (chk > 0)
                    {
                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";

                    }
                    else
                    {
                        divSuccess.Style["Display"] = "none";
                        divError.Style["Display"] = "";
                    }
                }
                gv.EditIndex = -1;
                UsersSearch();

            }
            catch (Exception ex)
            {
              
            }
        }

        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //try
            //{
            //    GridViewRow row = gv.Rows[e.NewEditIndex];
            //    HiddenField hd = default(HiddenField);

            //    if ((row != null))
            //    {
            //        hd = (HiddenField)row.FindControl("hdID");
            //        int ID = Convert.ToInt32(hd.Value);

            //        Response.Redirect("AddUsers.aspx?param=" + EncryptDecryptHelper.Encrypt(ID.ToString()));



            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandling.SendErrorReport("ManageUsers.aspx", "gv_RowEditing", ex.Message);
            //}
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ImageButton imgBtn = default(ImageButton);
                Button btnUpdatePlan = default(Button);
                Button btnUpdatePlan3Days = default(Button);
                Button btnUpdatePlan30Days = default(Button);
                Button btnUpdatePlan3Months = default(Button);
                
                Label lblStatus = default(Label);
                string script = null;
                script = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lblStatus = (Label)e.Row.Controls[0].FindControl("lblStatus");
                    btnUpdatePlan = (Button)e.Row.Controls[0].FindControl("btnUpdatePlan");
                    btnUpdatePlan3Days = (Button)e.Row.Controls[0].FindControl("btnUpdatePlan3Days");
                    btnUpdatePlan30Days = (Button)e.Row.Controls[0].FindControl("btnUpdatePlan30Days");
                    btnUpdatePlan3Months = (Button)e.Row.Controls[0].FindControl("btnUpdatePlan3Months");
                    if (lblStatus.Text == "Expired")
                    {
                        btnUpdatePlan.Visible = true;
                        btnUpdatePlan3Days.Visible = true;
                        btnUpdatePlan30Days.Visible = true;
                        btnUpdatePlan3Months.Visible = true;
                    }
                    else
                    {
                        btnUpdatePlan.Visible = false;
                        btnUpdatePlan3Days.Visible = false;
                        btnUpdatePlan30Days.Visible = false;
                        btnUpdatePlan3Months.Visible = false;
                    }

                    imgBtn = (ImageButton)e.Row.Controls[0].FindControl("ibtnDelete");
                    script = "javascript:return(confirm_delete())";
                    imgBtn.Attributes.Add("onclick", script);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("ManageUsers.aspx", "gv_RowDataBound", ex.Message);
            }
        }
        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
               
                if (e.CommandName == "ResetPassword")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("ResetUserPassword.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "UpdatePlan")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();

                    UpdatePlan7Days(int.Parse(val.ToString()));
                    UsersSearch();
                    
                }
                
                if (e.CommandName == "UpdatePlan3Days")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];

                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();

                    UpdatePlan3Days(int.Parse(val.ToString()));
                    UsersSearch();

                }
                if (e.CommandName == "UpdatePlan30Days")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];

                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();

                    UpdatePlan30Days(int.Parse(val.ToString()));
                    UsersSearch();

                }
                if (e.CommandName == "UpdatePlan3Months")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];

                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();

                    UpdatePlan3Months(int.Parse(val.ToString()));
                    UsersSearch();

                }
                if (e.CommandName == "UpdateStatus")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("UpdateUserStatus.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName == "ResetPassword")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gv.Rows[index];
                    //HiddenField hdnField = (HiddenField)row.FindControl("hdID");
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                    Response.Redirect("ResetUserPassword.aspx?param=" + EncryptDecryptHelper.Encrypt(val.ToString()));
                }
                if (e.CommandName == "AddComment")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    GridViewRow row = gv.Rows[index];

                    TextBox txtComments = ((TextBox)gv.Rows[index].FindControl("txtComments"));
                    Label lblCommentsConfirmation = ((Label)gv.Rows[index].FindControl("lblCommentsConfirmation"));
                    
                    string val = (string)this.gv.DataKeys[index]["Id"].ToString();
                  

                    if (!string.IsNullOrEmpty(txtComments.Text))
                    {
                        int chk = objUsr.InsertCRMUserComments(int.Parse(val.ToString()), txtComments.Text.Trim(), "", int.Parse(Session["UserID"].ToString()));
                        if (chk > 0)
                        {
                            lblCommentsConfirmation.ForeColor = System.Drawing.Color.Green;
                            lblCommentsConfirmation.Text = "Comment Added.";
                            txtComments.Text = "";
                        }
                    }
                }
               
                if (e.CommandName.Equals("Sort"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("UserType_Sort"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("Sort_email"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("Sort_PlanName"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("Sort_Company"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("Sort_Phoneno"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("CustNo_Sort"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("Status_Sort"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("FormatedExpire"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("sort_noofdays"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                if (e.CommandName.Equals("sort_active"))
                {
                    if (ViewState["SortExpression"] != null)
                    {
                        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                        {
                            if (ViewState["SortOrder"].ToString() == "ASC")
                                ViewState["SortOrder"] = "DESC";
                            else
                                ViewState["SortOrder"] = "ASC";
                        }
                        else
                        {
                            ViewState["SortOrder"] = "ASC";
                            ViewState["SortExpression"] = e.CommandArgument.ToString();
                        }

                    }
                    else
                    {
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                        ViewState["SortOrder"] = "ASC";
                    }
                    UsersSearch();
                }
                //if (e.CommandName.Equals("sort_active"))
                //{
                //    if (ViewState["SortExpression"] != null)
                //    {
                //        if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                //        {
                //            if (ViewState["SortOrder"].ToString() == "ASC")
                //                ViewState["SortOrder"] = "DESC";
                //            else
                //                ViewState["SortOrder"] = "ASC";
                //        }
                //        else
                //        {
                //            ViewState["SortOrder"] = "ASC";
                //            ViewState["SortExpression"] = e.CommandArgument.ToString();
                //        }

                //    }
                //    else
                //    {
                //        ViewState["SortExpression"] = e.CommandArgument.ToString();
                //        ViewState["SortOrder"] = "ASC";
                //    }
                //    GetUser(0);
                //}

            }
            catch (Exception ex)
            {

            }
        }
        
        void UpdatePlan7Days(int UserID)
        {
            try
            {
                int PlanID = 8;
                string lblPlanName = "Complimentary Renewal - 7 Days Free Subscription Package";
                string lblPrice = "0";
                string PaymentMethod = "N/A";
                int chkInv = objUsr.AddInvoice(UserID, PlanID, UserID.ToString(), "Unpaid", 0, 999999, PaymentMethod);
                if (chkInv > 0)
                {
                    DataTable dtUser = new DataTable();
                    dtUser = objUsr.GetUsers(UserID);
                    string txtNoOfdays = "";
                    string PlanName = "";
                    string PlanAmt = "";
                    string PlanNoOfDays = "";
                    string lblInvNo = chkInv.ToString();
                    GetActivePlansDetails(8, ref PlanName, ref PlanNoOfDays, ref PlanAmt);
                    if (dtUser.Rows.Count > 0)
                    {
                        txtNoOfdays = dtUser.Rows[0]["ExpireIn"].ToString();
                        objUsr.UserID = UserID;
                        objUsr.PlanID = 8;
                        if (int.Parse(txtNoOfdays.ToString()) > 0)
                            objUsr.PlanStart = DateTime.Now.AddDays(int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanStart = DateTime.Now.ToString("MM/dd/yyyy HH:MM:ss");

                        if (int.Parse(txtNoOfdays) > 0)
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString()) + int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");

                        objUsr.Amt = int.Parse(PlanAmt);
                        objUsr.InvoiceID = int.Parse(chkInv.ToString());
                        objUsr.CreatedBy = 999999;
                        objUsr.Remarks = "Complimentary Renewal - 7 Days Free Subscription Package";
                        objUsr.Uploadfile = "";
                        objUsr.ReceiptNo = "";

                        int chk = objUsr.InsertUserPlanUpdate();
                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Complimentary Renewal - 7 Days Free Subscription Package"), "Complimentary Renewal - 7 Days", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Complimentary Renewal - 7 Days Free Subscription Package"), "Complimentary Renewal - 7 Days", "EastLaw", "");

                        SendOrderConfirmationSMS(dtUser.Rows[0]["FullName"].ToString(), dtUser.Rows[0]["PhoneNo"].ToString(), "Complimentary Renewal - 7 Days Free Subscription");

                    }
                }
            }
            catch { }
        }
        void UpdatePlan3Days(int UserID)
        {
            try
            {
                int PlanID = 25;
                string lblPlanName = "Special Complimentary Renewal - 3 Days - Free Trial";
                string lblPrice = "0";
                string PaymentMethod = "N/A";
                int chkInv = objUsr.AddInvoice(UserID, PlanID, UserID.ToString(), "Unpaid", 0, 999999, PaymentMethod);
                if (chkInv > 0)
                {
                    DataTable dtUser = new DataTable();
                    dtUser = objUsr.GetUsers(UserID);
                    string txtNoOfdays = "";
                    string PlanName = "";
                    string PlanAmt = "";
                    string PlanNoOfDays = "";
                    string lblInvNo = chkInv.ToString();
                    GetActivePlansDetails(25, ref PlanName, ref PlanNoOfDays, ref PlanAmt);
                    if (dtUser.Rows.Count > 0)
                    {
                        txtNoOfdays = dtUser.Rows[0]["ExpireIn"].ToString();
                        objUsr.UserID = UserID;
                        objUsr.PlanID = 25;
                        if (int.Parse(txtNoOfdays.ToString()) > 0)
                            objUsr.PlanStart = DateTime.Now.AddDays(int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanStart = DateTime.Now.ToString("MM/dd/yyyy HH:MM:ss");

                        if (int.Parse(txtNoOfdays) > 0)
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString()) + int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");

                        objUsr.Amt = int.Parse(PlanAmt);
                        objUsr.InvoiceID = int.Parse(chkInv.ToString());
                        objUsr.CreatedBy = 999999;
                        objUsr.Remarks = "Special Complimentary Renewal - 3 Days - Free Trial";
                        objUsr.Uploadfile = "";
                        objUsr.ReceiptNo = "";

                        int chk = objUsr.InsertUserPlanUpdate();
                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial"), "Complimentary Renewal - 3 Days", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial"), "Complimentary Renewal - 3 Days", "EastLaw", "");

                        SendOrderConfirmationSMS(dtUser.Rows[0]["FullName"].ToString(), dtUser.Rows[0]["PhoneNo"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial");

                    }
                }
            }
            catch { }
        }
        void UpdatePlan30Days(int UserID)
        {
            try
            {
                int PlanID = 25;
                string lblPlanName = "Special Complimentary Renewal - 30 Days - Free Trial";
                string lblPrice = "0";
                string PaymentMethod = "N/A";
                int chkInv = objUsr.AddInvoice(UserID, PlanID, UserID.ToString(), "Unpaid", 0, 999999, PaymentMethod);
                if (chkInv > 0)
                {
                    DataTable dtUser = new DataTable();
                    dtUser = objUsr.GetUsers(UserID);
                    string txtNoOfdays = "";
                    string PlanName = "";
                    string PlanAmt = "";
                    string PlanNoOfDays = "";
                    string lblInvNo = chkInv.ToString();
                    GetActivePlansDetails(18, ref PlanName, ref PlanNoOfDays, ref PlanAmt);
                    if (dtUser.Rows.Count > 0)
                    {
                        txtNoOfdays = dtUser.Rows[0]["ExpireIn"].ToString();
                        objUsr.UserID = UserID;
                        objUsr.PlanID = 25;
                        if (int.Parse(txtNoOfdays.ToString()) > 0)
                            objUsr.PlanStart = DateTime.Now.AddDays(int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanStart = DateTime.Now.ToString("MM/dd/yyyy HH:MM:ss");

                        if (int.Parse(txtNoOfdays) > 0)
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString()) + int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");

                        objUsr.Amt = int.Parse(PlanAmt);
                        objUsr.InvoiceID = int.Parse(chkInv.ToString());
                        objUsr.CreatedBy = 999999;
                        objUsr.Remarks = "Special Complimentary Renewal - 30 Days - Free Trial";
                        objUsr.Uploadfile = "";
                        objUsr.ReceiptNo = "";

                        int chk = objUsr.InsertUserPlanUpdate();
                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial"), "Complimentary Renewal - 3 Days", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial"), "Complimentary Renewal - 3 Days", "EastLaw", "");

                        SendOrderConfirmationSMS(dtUser.Rows[0]["FullName"].ToString(), dtUser.Rows[0]["PhoneNo"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial");

                    }
                }
            }
            catch { }
        }
        void UpdatePlan3Months(int UserID)
        {
            try
            {
                int PlanID = 25;
                string lblPlanName = "Special Complimentary Renewal - 3 Months - Free Trial";
                string lblPrice = "0";
                string PaymentMethod = "N/A";
                int chkInv = objUsr.AddInvoice(UserID, PlanID, UserID.ToString(), "Unpaid", 0, 999999, PaymentMethod);
                if (chkInv > 0)
                {
                    DataTable dtUser = new DataTable();
                    dtUser = objUsr.GetUsers(UserID);
                    string txtNoOfdays = "";
                    string PlanName = "";
                    string PlanAmt = "";
                    string PlanNoOfDays = "";
                    string lblInvNo = chkInv.ToString();
                    GetActivePlansDetails(30, ref PlanName, ref PlanNoOfDays, ref PlanAmt);
                    if (dtUser.Rows.Count > 0)
                    {
                        txtNoOfdays = dtUser.Rows[0]["ExpireIn"].ToString();
                        objUsr.UserID = UserID;
                        objUsr.PlanID = 25;
                        if (int.Parse(txtNoOfdays.ToString()) > 0)
                            objUsr.PlanStart = DateTime.Now.AddDays(int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanStart = DateTime.Now.ToString("MM/dd/yyyy HH:MM:ss");

                        if (int.Parse(txtNoOfdays) > 0)
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString()) + int.Parse(txtNoOfdays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanEnd = DateTime.Now.AddDays(int.Parse(PlanNoOfDays.ToString())).ToString("MM/dd/yyyy HH:MM:ss");

                        objUsr.Amt = int.Parse(PlanAmt);
                        objUsr.InvoiceID = int.Parse(chkInv.ToString());
                        objUsr.CreatedBy = 999999;
                        objUsr.Remarks = "Special Complimentary Renewal - 3 Months - Free Trial";
                        objUsr.Uploadfile = "";
                        objUsr.ReceiptNo = "";

                        int chk = objUsr.InsertUserPlanUpdate();
                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial"), "Complimentary Renewal - 3 Days", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ConfirmationEmailAdmin(dtUser.Rows[0]["EmailID"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial"), "Complimentary Renewal - 3 Days", "EastLaw", "");

                        SendOrderConfirmationSMS(dtUser.Rows[0]["FullName"].ToString(), dtUser.Rows[0]["PhoneNo"].ToString(), "Special Complimentary Renewal - 3 Days - Free Trial");

                    }
                }
            }
            catch { }
        }
        string ConfirmationEmailAdmin(string EmailID,string PlanName)
        {
            try
            {
                string file = "";


                string html = "";

                html = PlanName+", Updated on " + EmailID.ToString();



                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void SendOrderConfirmationSMS(string CustomerName,string MobileNo,string PlanName)
        {
            try
            {
                string smstxt = "Dear " + CustomerName + ", We are pleased to confirm your "+ PlanName+" has been updated, "
                + "and wish to inform you that your account at www.eastlaw.pk is now active. Helpline#. 03-111-116-670  ";

                string mobilenumber = MobileNo;
                //string url = "http://bulksms.com.pk/api/sms.php?username=923214264174&password=5974&sender=eastlaw.pk&mobile=923214264174&message=" + smstxt + "";
                string url = "http://bulksms.com.pk/api/sms.php?username=923228451969&password=1813&sender=eastlaw.pk&mobile=" + mobilenumber + "&message=" + smstxt + "";

                //HTTP connection
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch { }

        }
        void GetActivePlansDetails(int PlanID, ref string PlanName, ref string NoOfDays, ref string Amt)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = objPlan.GetPlans(PlanID);
                if (dt.Rows.Count > 0)
                {
                    PlanName = dt.Rows[0]["PlanName"].ToString();
                    NoOfDays = dt.Rows[0]["NoofDays"].ToString();
                    Amt = dt.Rows[0]["Price"].ToString();

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            SetSortDirection(SortDireaction);
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                gv.DataSource = dt;
                gv.DataBind();
                SortDireaction = _sortDirection;
                int columnIndex = 0;
                foreach (DataControlFieldHeaderCell headerCell in gv.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression == e.SortExpression)
                    {
                        columnIndex = gv.HeaderRow.Cells.GetCellIndex(headerCell);
                    }
                }

                gv.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            }
        }
        protected void SetSortDirection(string sortDirection)
        {
            if (sortDirection == "ASC")
            {
                _sortDirection = "DESC";
                sortImage.ImageUrl = "media/img/view_sort_ascending.png";

            }
            else
            {
                _sortDirection = "ASC";
                sortImage.ImageUrl = "media/img/view_sort_descending.png";
            }
        }

    }
}