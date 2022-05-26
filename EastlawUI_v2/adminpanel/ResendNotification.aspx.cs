using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Configuration;

namespace EastlawUI_v2.adminpanel
{
    public partial class ResendNotification : System.Web.UI.Page
    {
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
            if (Session["UserID"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                Response.Redirect("NotAuthorize.aspx");
        }
        void UsersSearch(string Status)
        {
            try
            {
                string cri = "Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn) as FormatedExpire,E.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,D.CompanyName ,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin from dbo.tbl_Users A"
            + " inner join Country B on A.Country=B.Code"
            + " inner join tbl_Plans C on A.PlanID=C.ID"
            + " inner join tbl_Companies D on A.CompanyID=D.ID"
            + " inner join dbo.tbl_UserType E on A.UserTypeID=E.ID"
            + " union all "
            + " Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn),D.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,'' as CompanyName,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin  from dbo.tbl_Users A"
            + " inner join Country B on A.Country=B.Code"
            + " inner join tbl_Plans C on A.PlanID=C.ID"
            + " inner join dbo.tbl_UserType D on A.UserTypeID=D.ID  Where A.CreatedOn is not null";

                //if (ddlCiJournal.SelectedValue != "0" && ddlCYear.SelectedValue != "0" && txtCNumber.Text != "")
                //    cri = cri + " AND E.JournalName='" + ddlCiJournal.SelectedValue + "' AND A.Year=" + ddlCYear.SelectedValue + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";


                if (!string.IsNullOrEmpty(txtFromDate.Text))
                {
                    if (Status == "Pending - Activation")
                        cri = cri + " AND CAST(A.Createdon AS DATE) >= CAST('" + txtFromDate.Text + "' AS DATE) AND CAST(A.Createdon AS DATE) <= CAST('" + txtToDate.Text + "' AS DATE)";
                    if (Status == "Expired")
                        cri = cri + " AND CAST(A.AccessExpireOn AS DATE) >= CAST('" + txtFromDate.Text + "' AS DATE) AND CAST(A.AccessExpireOn AS DATE) <= CAST('" + txtToDate.Text + "' AS DATE)";
                }

                if (ddlStatus.SelectedValue != "0")
                    cri = cri + " AND A.Status Like '" + ddlStatus.SelectedValue + "' ";

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

                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        gv.DataSource = null;
                        gv.DataBind();
                    }
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
                    UsersSearch(ddlStatus.SelectedValue);
                }
                //if (e.CommandName.Equals("UserType_Sort"))
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
                //if (e.CommandName.Equals("Sort_email"))
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
                //if (e.CommandName.Equals("Sort_PlanName"))
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
                //if (e.CommandName.Equals("Sort_Company"))
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
                //if (e.CommandName.Equals("Sort_Phoneno"))
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
                //if (e.CommandName.Equals("CustNo_Sort"))
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
                //if (e.CommandName.Equals("Status_Sort"))
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
                //if (e.CommandName.Equals("FormatedExpire"))
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
                //if (e.CommandName.Equals("sort_noofdays"))
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                UsersSearch(ddlStatus.SelectedValue);
            }
            catch { }
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gv.HeaderRow.FindControl("chkHeader");
            foreach (GridViewRow row in gv.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkRow");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }
        void GetSelectedValuesAndSendEmails(int Selected)
        {
            try
            {

                //GridViewRow row = default(GridViewRow);
                CheckBox chkSend = default(CheckBox);
                Label lblFullName = default(Label);
                Label lblEmailID = default(Label);
                Label lblPhone = default(Label);
                HiddenField hdID = default(HiddenField);

                foreach (GridViewRow row in gv.Rows)
                {
                    if ((row != null))
                    {

                        chkSend = (CheckBox)row.FindControl("chkRow");
                        lblFullName = (Label)row.FindControl("lblFullName");
                        lblEmailID = (Label)row.FindControl("lblEmailID");
                        lblPhone = (Label)row.FindControl("lblPhoneNo");
                        hdID = (HiddenField)row.FindControl("hdID");

                        if (Selected == 1)
                        {
                            if (chkSend.Checked == true)
                            {
                                if (ddlStatus.SelectedValue == "Expired")
                                {
                                    string emailcontent = ExpiredNotificationEmailContent(int.Parse(hdID.Value), lblFullName.Text, lblEmailID.Text);
                                    objUsr.AddUserNotificationLog(int.Parse(hdID.Value), "Account Expired Notification", emailcontent);
                                    Email.SendMail(lblEmailID.Text, emailcontent, "Account Expired ", "Eastlaw - Notification", "");

                                    SendExpireSMS(int.Parse(hdID.Value), lblFullName.Text, lblPhone.Text);
                                }
                                else if (ddlStatus.SelectedValue == "Pending - Activation")
                                {
                                    Email.SendMail(lblEmailID.Text, StatusChangedEmail(int.Parse(hdID.Value), lblFullName.Text, ddlStatus.SelectedValue), "Account Update", "EastLaw", "");
                                    StatusChangedEmail(int.Parse(hdID.Value), lblFullName.Text, ddlStatus.SelectedValue);
                                    SendWelcomeSMS(int.Parse(hdID.Value), lblFullName.Text, lblPhone.Text);
                                }
                            }
                        }
                        else
                        {
                            if (ddlStatus.SelectedValue == "Expired")
                            {
                                string emailcontent = ExpiredNotificationEmailContent(int.Parse(hdID.Value), lblFullName.Text, lblEmailID.Text);
                                objUsr.AddUserNotificationLog(int.Parse(hdID.Value), "Account Expired Notification", emailcontent);
                                Email.SendMail(lblEmailID.Text, emailcontent, "Account Expired ", "Eastlaw - Notification", "");

                                SendExpireSMS(int.Parse(hdID.Value), lblFullName.Text, lblPhone.Text);
                            }
                            else if (ddlStatus.SelectedValue == "Pending - Activation")
                            {
                                StatusChangedEmail(int.Parse(hdID.Value), lblFullName.Text, ddlStatus.SelectedValue);
                                SendWelcomeSMS(int.Parse(hdID.Value), lblFullName.Text, lblPhone.Text);
                            }
                        }

                        divSuccess.Style["Display"] = "";
                        divError.Style["Display"] = "none";

                    }
                }
            }
            catch (Exception ex)
            {
                //    string script = "alert('" + ex.Message() + "')";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
            }
        }
        #region Expiry Notification
        string ExpiredNotificationEmailContent(int UserID, string Name, string EmailID)
        {
            try
            {
                string file = "";


                string html = "";
                file = System.Web.HttpContext.Current.Server.MapPath("../EmailTemplates/AccountExpiredNotificationComp.html");
                
                
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##COMPPKG##", "http://eastlaw.pk/Member/Complementary-Subscription?nval=" + EncryptDecryptHelper.Encrypt(UserID.ToString()));
                return html;
            }
            catch
            {
                return "";
            }



        }
        void SendExpireSMS(int UserID, string Name, string MobileNo)
        {
            try
            {
                //string smstxt = "Dear " + Name + ", Thank you for using EastLaw.pk. Kindly note that your"
                //+ "Subscription Package has Expired. Please login to place order for new Subscription Package.";

                string smstxt = "Dear " + Name + ", Thank you for using EastLaw.pk. Kindly note that your"
                + "Subscription Package has Expired. Please login and get free renewal.";

                string mobilenumber = MobileNo;
                //string url = "http://bulksms.com.pk/api/sms.php?username=923214264174&password=5974&sender=eastlaw.pk&mobile=923214264174&message=" + smstxt + "";
                string url = "http://bulksms.com.pk/api/sms.php?username=923228451969&password=1943&sender=eastlaw.pk&mobile=" + mobilenumber + "&message=" + smstxt + "";

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
        #endregion
        #region Pending Notification
        string StatusChangedEmail(int UserID, string Name, string Status)
        {
            try
            {

                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("../EmailTemplates/UserChangeEmail.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                if (Status != "Pending - Activation")
                {
                    html = html.Replace("##STATUS##", Status);
                    html = html.Replace("##EXRAMSG##", "");
                }
                else
                {
                    string qry = " Please <b><a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + "' target='_blank'>Click Here</a></b> to activate your account or alternatively copy & paste below link.<br /><br />" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString());
                    html = html.Replace("##STATUS##", Status);
                    html = html.Replace("##EXRAMSG##", qry);
                }

                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;

            }
            catch
            {
                return "";
            }



        }
        void SendWelcomeSMS(int ID,string Name,string PhoneNo)
        {
            try
            {
                string smstxt = "Dear " + Name + ", Thank you for Registering at EastLaw.pk."
                + "Please check your email to activate your account or Click " + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + " to activate it now.";

                string mobilenumber = PhoneNo;
                //string url = "http://bulksms.com.pk/api/sms.php?username=923214264174&password=5974&sender=eastlaw.pk&mobile=923214264174&message=" + smstxt + "";
                string url = "http://bulksms.com.pk/api/sms.php?username=923228451969&password=1943&sender=eastlaw.pk&mobile=" + mobilenumber + "&message=" + smstxt + "";

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
        #endregion

        protected void btnSend_Click(object sender, EventArgs e)
        {
            GetSelectedValuesAndSendEmails(1);
        }

        protected void btnSendAll_Click(object sender, EventArgs e)
        {
            GetSelectedValuesAndSendEmails(0);
        }


    }
}