using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Net;

namespace EastlawUI_v2.adminpanel
{
    public partial class UpdateUserStatus : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        EastLawBL.Plans objPlan = new EastLawBL.Plans();
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
                if (Session["UserTypeID"].ToString() == "18")
                    usersearch_actionpanel1.Visible = false;
                GetActiveUserTypes();
                GetOrgTypes();
                GetCountries();
               
                if (Request.QueryString["param"] != null)
                    GetUser(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
        }
        void GetOrgTypes()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetActiveOrgTypes();
                ddlOrgType.DataValueField = "ID";
                ddlOrgType.DataTextField = "OrgTypes";
                ddlOrgType.DataSource = dt;
                ddlOrgType.DataBind();

                ddlOrgType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetOrgTypes", ex.Message);
            }
        }
        void GetUser(int ID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetUsers(ID);
                if (ID == 0)
                {
                    //dt.Columns.Add("strActive");
                    //for (int a = 0; a < dt.Rows.Count; a++)
                    //{
                    //    if (dt.Rows[a]["Active"].ToString() == "1")
                    //        dt.Rows[a]["strActive"] = "Yes";
                    //    else
                    //        dt.Rows[a]["strActive"] = "No";
                    //}
                    //dt.AcceptChanges();
                    //gv.DataSource = dt;
                    //gv.DataBind();
                }
                else
                {

                    ddlOrgType.SelectedValue = dt.Rows[0]["OrgTypeID"].ToString();
                    ddlUserType.SelectedValue = dt.Rows[0]["UserTypeID"].ToString();
                    if (dt.Rows[0]["UserTypeID"].ToString() == "4")
                    {
                        GetActiveCompanies();
                        divCompany.Style["Display"] = "";
                        rfvddlCompany.Enabled = true;
                        ddlCompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString();
                    }

                   
                    txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    txtEmailID.Enabled = false;



                    txtFullName.Text = dt.Rows[0]["FullName"].ToString(); ;
                    txtPhone.Text = dt.Rows[0]["PhoneNo"].ToString(); ;
                    txtAdd.Text = dt.Rows[0]["Address"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString(); ;
                   
                    ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString(); ;
                    txtNoOfdays.Text = dt.Rows[0]["ExpireIn"].ToString();
                    //if (dt.Rows[0]["Active"].ToString() == "1")
                    //    chkActive.Checked = true;
                    //else
                    //    chkActive.Checked = false;



                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";

                 
                  



                }
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetUser", e.Message);
            }
        }
        void GetCountries()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcom.GetCountries();
                ddlCountry.DataValueField = "Code";
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataSource = dt;
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetCountries", ex.Message);
            }
        }
        void GetActiveCompanies()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetActiveCompanies();
                ddlCompany.DataValueField = "Code";
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataSource = dt;
                ddlCompany.DataBind();

                ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetActiveCompanies", ex.Message);
            }
        }
        
        void GetActiveUserTypes()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetUserTypes();
                ddlUserType.DataValueField = "ID";
                ddlUserType.DataTextField = "UserType";
                ddlUserType.DataSource = dt;
                ddlUserType.DataBind();

                ddlUserType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetActiveUserTypes", ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = objUsr.UpdateUserStatus(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), ddlStatus.SelectedValue);
                if (chk > 0)
                {
                    DataTable dt = objUsr.GetUsers(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));

                    Email.SendMail(dt.Rows[0]["EmailID"].ToString(), StatusChangedEmail(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");
                    Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), StatusChangedEmail(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");
                    Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), StatusChangedEmail(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");

                    SendSMS(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())), txtFullName.Text, ddlStatus.SelectedValue, txtPhone.Text.Trim());

                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    //ClearFields();

                }
                else
                {
                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "";
                }
            }
            catch 
            { }
        }
        string StatusChangedEmail(int UserID,string Name, string Status)
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
                    string qry = " Please <b><a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + "' target='_blank'>Click Here</a></b> to activate your account or alternatively copy & paste below link.<br /><br />"+ ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString());
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
        void SendSMS(int ID,string Name,string Status,string MobileNumber)
        {
            try
            {
                string smstxt = "";
                if (Status != "Pending - Activation")
                {

                    smstxt = "Dear " + Name + ", Please note that your account status has been changed " + Status + ". Regards, Team EastLaw.pk  Helpline#. 03-111-116-670";
                }
                else
                {
                    smstxt = "Dear " + Name + ", your account status is changed "+Status +"."
                + "Please check your email to activate your account or Click " + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + " to activate it now.  Helpline#. 03-111-116-670";

                }
                

                string mobilenumber = MobileNumber;
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
        //void UsersSearch()
        //{
        //    try
        //    {
        //        string cri = "Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn) as FormatedExpire,E.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,D.CompanyName ,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin from dbo.tbl_Users A"
        //    + " inner join Country B on A.Country=B.Code"
        //    + " inner join tbl_Plans C on A.PlanID=C.ID"
        //    + " inner join tbl_Companies D on A.CompanyID=D.ID"
        //    + " inner join dbo.tbl_UserType E on A.UserTypeID=E.ID Where A.CreatedOn is not null and A.isdeleted=0";

              

        //        if (!string.IsNullOrEmpty(txtSearchEmailID.Text.Trim()))
        //            cri = cri + " AND  A.EmailID='" + txtSearchEmailID.Text + "' ";

        //        if (!string.IsNullOrEmpty(txSearchtMobileNo.Text.Trim()))
        //            cri = cri + " AND  A.PhoneNo='" + txSearchtMobileNo.Text + "' ";

        //        cri = cri + " union all "
        //    + " Select A.*,DATEDIFF(DAY, getdate(),CONVERT(datetime,A.AccessExpireOn)) as ExpireIn,CONVERT(datetime,A.AccessExpireOn),D.UserType,B.Name as CountryName,C.PlanName,C.NoofDays,'' as CompanyName,(select top 1 convert(varchar(101), Createdon)  from tbl_auditlog where activitytype='Login/Logout' and [Action] like '%Success%' and userid=A.ID) as FirstLogin  from dbo.tbl_Users A"
        //    + " inner join Country B on A.Country=B.Code"
        //    + " inner join tbl_Plans C on A.PlanID=C.ID"
        //    + " inner join dbo.tbl_UserType D on A.UserTypeID=D.ID  Where A.CreatedOn is not null and A.isdeleted=0";

        //        //if (ddlCiJournal.SelectedValue != "0" && ddlCYear.SelectedValue != "0" && txtCNumber.Text != "")
        //        //    cri = cri + " AND E.JournalName='" + ddlCiJournal.SelectedValue + "' AND A.Year=" + ddlCYear.SelectedValue + " AND A.Citation like '%" + txtCNumber.Text.Trim() + "%'";

        //        if (!string.IsNullOrEmpty(txtSearchEmailID.Text.Trim()))
        //            cri = cri + " AND  A.EmailID='" + txtSearchEmailID.Text + "' ";

        //        if (!string.IsNullOrEmpty(txSearchtMobileNo.Text.Trim()))
        //            cri = cri + " AND  A.PhoneNo='" + txSearchtMobileNo.Text + "' ";

        //        DataTable dt = new DataTable();
        //        dt = objUsr.GetUsersSearchBackendOpenQuery(cri);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count == 1)
        //            {
        //                Response.Redirect("UpdateUserStatus.aspx?param=" + EncryptDecryptHelper.Encrypt(dt.Rows[0]["ID"].ToString()));
        //            }
        //            else
        //            {
        //                lblMsg.Text = "User not found.";
        //                    lblMsg.ForeColor = System.Drawing.Color.Red;
                   
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    UsersSearch();

        //}
    }
}