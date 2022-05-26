using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;


using System.Text;
using System.Globalization;
using System.Net;
using System.Net.Http;

namespace EastlawUI_v2.adminpanel
{
    public partial class AddUsers : System.Web.UI.Page
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Common objcom = new EastLawBL.Common();
        EastLawBL.Plans objPlan = new EastLawBL.Plans();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserID"] == null)
                    {
                        Response.Redirect("default.aspx");
                    }
                    if (!ValidateUserGroup.ValidateGroup(int.Parse(Session["UserTypeID"].ToString()), ValidateUserGroup.getPageName(Request.Url.AbsolutePath)))
                        Response.Redirect("NotAuthorize.aspx");
                    GetActiveUserTypes();
                    GetOrgTypes();
                    GetCountries();
                    GetActivePlans(0);
                    if (Request.QueryString["param"] != null)
                        GetUser(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "Page_Load", ex.Message);
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

                    ddlPlan.SelectedValue = dt.Rows[0]["PlanID"].ToString();
                    GetActivePlans(int.Parse(dt.Rows[0]["PlanID"].ToString()));
                    ddlPlan.Enabled = false;

                    txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    txtEmailID.Enabled = false;

                    divPassword.Style["Display"] = "none";
                    rfvtxtPassword.Enabled = false;
                    rfvtxtConfirmPassword.Enabled = false;
                    cvPassword.Enabled = false;

                    txtFullName.Text = dt.Rows[0]["FullName"].ToString(); ;
                    txtPhone.Text = dt.Rows[0]["PhoneNo"].ToString(); ;
                    txtAdd.Text = dt.Rows[0]["Address"].ToString(); ;
                    txtOrgName.Text = dt.Rows[0]["OrgName"].ToString();
                    txtPostalAddress.Text = dt.Rows[0]["PostalAddress"].ToString(); 
                    ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString();
                    GetCitiesByCoutry(ddlCountry.SelectedValue);
                        ddlCity.SelectedValue = dt.Rows[0]["CityID"].ToString();
                    txtNoOfPCAllowed.Text = dt.Rows[0]["NoOfPCAllowd"].ToString(); ;
                    ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString(); ;
                    ddlStatus.Enabled = false;
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;



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
        void GetCitiesByCoutry(string CountryCode)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objcom.GetCitiesByCountry(CountryCode);
                ddlCity.DataValueField = "ID";
                ddlCity.DataTextField = "Name";
                ddlCity.DataSource = dt;
                ddlCity.DataBind();

                ddlCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUser.aspx", "GetCitiesByCoutry", ex.Message);
            }
        }
        void GetActiveCompanies()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetActiveCompanies();
                ddlCompany.DataValueField = "ID";
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
        void GetActivePlans(int PlanID)
        {
            try
            {
                DataTable dt = new DataTable();
                if (PlanID == 0)
                {
                    dt = objPlan.GetActivePlans();
                    ddlPlan.DataValueField = "ID";
                    ddlPlan.DataTextField = "PlanName";
                    ddlPlan.DataSource = dt;
                    ddlPlan.DataBind();

                    ddlPlan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                }
                else
                {
                    dt = objPlan.GetPlans(PlanID);
                    if(dt.Rows.Count >0)
                    {
                        lblPlanNoOfDays.Text = dt.Rows[0]["NoofDays"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "GetActivePlans", ex.Message);
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
        void ClearFields()
        {
            ddlUserType.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            ddlPlan.SelectedIndex = 0;
            lblPlanNoOfDays.Text = "0";
            txtEmailID.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtFullName.Text = "";
            txtPhone.Text = "";
            txtAdd.Text = "";
            ddlCountry.SelectedIndex = 0;
            txtNoOfPCAllowed.Text = "";
            ddlStatus.SelectedIndex = 0;
            chkActive.Checked = false;

        }
        void SaveRecord()
        {
            try
            {
                objUsr.OrgTypeID = int.Parse(ddlOrgType.SelectedValue);
                objUsr.OrgName = txtOrgName.Text.Trim();
                objUsr.PostalAddress = txtPostalAddress.Text.Trim();
                objUsr.UserTypeID = int.Parse(ddlUserType.SelectedValue);
                objUsr.EmailID = txtEmailID.Text.Trim();
                objUsr.Pwd = EncryptDecryptHelper.Encrypt(txtPassword.Text.Trim());
                objUsr.FullName = txtFullName.Text.Trim();
                objUsr.PhoneNo = txtPhone.Text.Trim();
                objUsr.Address = txtAdd.Text.Trim();
                objUsr.Country = ddlCountry.SelectedValue;
                objUsr.CityID =int.Parse(ddlCity.SelectedValue);
                objUsr.PlanID = int.Parse(ddlPlan.SelectedValue);
                if (ddlUserType.SelectedValue == "4")
                    objUsr.CompanyID = int.Parse(ddlCompany.SelectedValue);
                else
                    objUsr.CompanyID = 0;

                objUsr.CompUser = int.Parse(txtCompUsers.Text);
                objUsr.CompanyUserAbbr = txtUserAbbriviations.Text.Trim();
                if (chkVerify.Checked == true)
                    objUsr.Verify = 0;
                else
                    objUsr.Verify = 1;
                objUsr.Status = ddlStatus.SelectedValue;
                if (chkActive.Checked == true)
                    objUsr.Active = 1;
                else
                    objUsr.Active = 0;
                objUsr.CreatedBy = int.Parse(Session["UserID"].ToString());
                objUsr.NoOfPCAllowd = int.Parse(txtNoOfPCAllowed.Text.Trim());
                objUsr.AccessExpireOn = DateTime.Now.AddDays(double.Parse(lblPlanNoOfDays.Text)).ToString("MM/dd/yyyy HH:MM:ss");
                int chk = objUsr.InsertUser();
                if (chk > 0)
                {
                    if (chkGenerateOrder.Checked == true)
                        PlaceOrder(txtEmailID.Text.Trim());
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
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", e.Message);
            }
        }
        void EditRecord(int ID)
        {
            try
            {
                objUsr.OrgTypeID = int.Parse(ddlOrgType.SelectedValue);
                objUsr.OrgName = txtOrgName.Text.Trim();
                objUsr.PostalAddress = txtPostalAddress.Text.Trim();
                objUsr.UserTypeID = int.Parse(ddlUserType.SelectedValue);
                objUsr.FullName = txtFullName.Text.Trim();
                objUsr.PhoneNo = txtPhone.Text.Trim();
                objUsr.Address = txtAdd.Text.Trim();
                objUsr.Country = ddlCountry.SelectedValue;
                objUsr.CityID =int.Parse(ddlCity.SelectedValue);
                objUsr.PlanID = int.Parse(ddlPlan.SelectedValue);
                if (ddlUserType.SelectedValue == "4")
                    objUsr.CompanyID = int.Parse(ddlCompany.SelectedValue);
                else
                    objUsr.CompanyID = 0;
                if (chkVerify.Checked == true)
                    objUsr.Verify = 0;
                else
                    objUsr.Verify = 1;
                objUsr.Status = ddlStatus.SelectedValue;
                if (chkActive.Checked == true)
                    objUsr.Active = 1;
                else
                    objUsr.Active = 0;
                objUsr.ModifiedBy = int.Parse(Session["UserID"].ToString());
                objUsr.NoOfPCAllowd = int.Parse(txtNoOfPCAllowed.Text.Trim());
                //objUsr.AccessExpireOn = DateTime.Now.AddDays(double.Parse(lblPlanNoOfDays.Text)).ToString();
                int chk = objUsr.EditUser(ID);
                if (chk > 0)
                {
                    DataTable dt = objUsr.GetUsers(ID);
                    if(chkVerify.Checked == true)
                    {
                        Email.SendMail(txtEmailID.Text, EmailContent(ID), "Welcome to eastlaw.pk", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), AdminEmailContent(), "Welcome to eastlaw.pk", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), AdminEmailContent(), "Welcome to eastlaw.pk", "EastLaw", "");
                    }
                    //if (ddlStatus.SelectedValue == "Approved")
                    //{

                    //    Email.SendMail(dt.Rows[0]["EmailID"].ToString(), WelcomeEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString()), "Account Verified - Welcome to eastlaw.pk", "EastLaw", "");
                    //    Email.SendMail("registration@eastlaw.pk", WelcomeEmailContentAdmin(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Pwd"].ToString()), "Account Verified  - Welcome to eastlaw.pk", "EastLaw", "");
                    //}
                    else
                    {
                        Email.SendMail(dt.Rows[0]["EmailID"].ToString(), UserProfileUpdateEmail(dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), UserProfileUpdateEmail(dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");
                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), UserProfileUpdateEmail(dt.Rows[0]["FullName"].ToString(), ddlStatus.SelectedItem.Text), "Account Update", "EastLaw", "");
 
                    }
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
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", e.Message);
            }
        }
        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlUserType.SelectedValue == "4")
                {
                    GetActiveCompanies();
                    divCompany.Style["Display"] = "";
                    rfvddlCompany.Enabled = true;
                    rfvCompanyAbbr.Enabled = true;
                    rfvCompUser.Enabled = true;
                }
                else
                {
                    divCompany.Style["Display"] = "none";
                    rfvddlCompany.Enabled = false;
                    rfvCompanyAbbr.Enabled = false;
                    rfvCompUser.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", ex.Message);
            }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetActivePlans(int.Parse(ddlPlan.SelectedValue));
            }
            catch(Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", ex.Message);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        int CheckEmailExist(string EmailID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckUserExistByEmail(EmailID);
                if (dt.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch { return 0; }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["param"] == null)
                {
                    
                    int chk = CheckEmailExist(txtEmailID.Text.Trim());
                    if (chk == 0)
                    {
                        SaveRecord();
                    }
                    else
                    {
                        lblExist.Text = "Email ID already exist";
                        lblExist.Visible = true;

                       

                    }
                   
                }
                else
                    EditRecord(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "btnSave_Click", ex.Message);
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetCitiesByCoutry(ddlCountry.SelectedValue);
            }
            catch { }
        }

        protected void chkVerify_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVerify.Checked == true)
                ddlStatus.SelectedValue = "Pending - Activation";
            else
                ddlStatus.SelectedValue = "0";

        }
        string EmailContent(int ID)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("../EmailTemplates/NewRegistration.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", txtFullName.Text.Trim());
                html = html.Replace("##ClickHere##", "<a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + "' target='_blank'>Click Here</a>");
                html = html.Replace("##FullLink##", ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "Member/Member-Activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string AdminEmailContent()
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("../EmailTemplates/NewRegistrationAdmin.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }



                html = html.Replace("##FullName##", txtFullName.Text);
                html = html.Replace("##USRNME##", txtEmailID.Text);
                html = html.Replace("##MBLNM##", txtPhone.Text);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }

        string WelcomeEmailContent(string Name, string EmailID, string Pwd)
        {
            try
            {

                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("../EmailTemplates/Welcome.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##USRNME##", EmailID);
                html = html.Replace("##PWD##", EncryptDecryptHelper.Decrypt(Pwd));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;

            }
            catch
            {
                return "";
            }



        }
        string WelcomeEmailContentAdmin(string Name, string EmailID, string Pwd)
        {
            try
            {

                string file = "";


                string html = "";

                html = "User: " + EmailID + " Activated.";


                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;

            }
            catch
            {
                return "";
            }



        }
        string UserProfileUpdateEmail(string Name, string Status)
        {
            try
            {

                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("../EmailTemplates/UserUpdateProfile.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
               
             
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;

            }
            catch
            {
                return "";
            }



        }

        protected void txtEmailID_TextChanged(object sender, EventArgs e)
        {
            int chk = CheckEmailExist(txtEmailID.Text.Trim());
            if (chk == 1)
            {
                lblExist.Text = "Email ID already exist";
                lblExist.Visible = true;
            }
            else
            {
                lblExist.Text = "";
                lblExist.Visible = false;
            }
        }
        #region Generate Order
        bool PlaceOrder(string EmailID)
        {
            try
            {
                int PlanID = int.Parse(ddlPlan.SelectedValue);
                string PaymentMethod = "N/A";

                //string txtNoOfdays = "";
                string PlanName = "";
                string PlanAmt = "";
                string PlanNoOfDays = "";
                GetActivePlansDetails(int.Parse(ddlPlan.SelectedValue), ref PlanName, ref PlanNoOfDays, ref PlanAmt);

                DataTable dt = objUsr.CheckUserExistByEmail(EmailID);

                DataTable dtExist = objUsr.CheckOrderExist(int.Parse(dt.Rows[0]["ID"].ToString()), PlanID);
                if (dtExist != null)
                {
                    //if (dtExist.Rows.Count > 0)
                    //{
                    //    //lblEmailIDForExist.Text = dt.Rows[0]["EmailID"].ToString();
                    //    divError.InnerHtml = "Order Already Exist";
                    //    divError.Style["Display"] = "";

                    //}
                    //else
                    //{
                    int chk = objUsr.AddInvoice(int.Parse(dt.Rows[0]["ID"].ToString()), PlanID, dt.Rows[0]["ID"].ToString(), "Unpaid", 0, 999999, PaymentMethod);
                    if (chk > 0)
                    {
                        //lblPlan.Text = "1 x " + lblPlanName.Text;
                        //lblPrice1.Text = lblPrice.Text;
                        //lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                        //lblInvNo.Text = chk.ToString().PadLeft(6, '0');
                        string CustName = dt.Rows[0]["FullName"].ToString();

                        GeneratePDF(chk.ToString(), (dt.Rows[0]["ID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), ddlPlan.SelectedItem.Text, PlanAmt.ToString());

                        string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), chk.ToString().PadLeft(6, '0'), ddlPlan.SelectedItem.Text, PlanAmt.ToString(), PlanNoOfDays.ToString());
                        Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                        string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), chk.ToString().PadLeft(6, '0'), (dt.Rows[0]["ID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), ddlPlan.SelectedItem.Text, PlanAmt.ToString());
                        Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));
                        Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                        SendOrderSMS(chk.ToString().PadLeft(6, '0'), dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString(), PlanNoOfDays, PlanAmt.ToString());

                        return true;

                    }
                    // }
                }


                return false;

            }
            catch
            {
                return false;
            }
        }
        void GeneratePDF(string InvID, string InvNo, string CustomerName, string CityCountry, string EmailID, string PlanName, string Price)
        {
            string html = FillDynamicValues(InvID, InvNo, CustomerName, CityCountry, EmailID, PlanName, Price);


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    StringReader sr = new StringReader(html);
                    //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    Document pdfDoc = new Document(PageSize.A4);
                    // pdfDoc.Add(new Paragraph(pdfDoc.BottomMargin, "<span style='font-style:italic'>This is a computer generated Invoice and does not require signature. </span><br /><div style='border-top: 1px solid #000;width:100%'><br />www.eastlaw.pk<br />Lahore, Pakistan.<br /></div>"));
                    //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("/store/users/invoices/INV_") + InvID + ".pdf", FileMode.Create));
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

                    writer.PageEvent = new Footer();

                    //Paragraph welcomeParagraph = new Paragraph("Hello, World!");

                    //pdfDoc.Add(welcomeParagraph);

                    pdfDoc.Close();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.Write(pdfDoc);
                    //Response.End();
                }
            }
        }
        public class PDFFooter : PdfPageEventHelper
        {
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                tabFot.SpacingAfter = 10F;
                PdfPCell cell;
                tabFot.TotalWidth = 300F;
                cell = new PdfPCell(new Phrase("Header"));
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                PdfPCell cell;
                tabFot.TotalWidth = 300F;
                cell = new PdfPCell(new Phrase("Footer"));
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }
        public partial class Footer : PdfPageEventHelper
        {

            public override void OnEndPage(PdfWriter writer, Document doc)
            {

                Paragraph footer = new Paragraph("This is a computer generated Invoice and does not require signature", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                footer.Alignment = Element.ALIGN_CENTER;

                PdfPTable footerTbl = new PdfPTable(1);
                footerTbl.TotalWidth = 400;
                footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell = new PdfPCell(footer);
                cell.Border = 0;
                cell.PaddingLeft = 10;
                footerTbl.AddCell(cell);

                footerTbl.WriteSelectedRows(0, -1, 0, 40, writer.DirectContent);

                Paragraph footer1 = new Paragraph("www.eastlaw.pk", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL));

                footer1.Alignment = Element.ALIGN_CENTER;

                PdfPTable footerTbl1 = new PdfPTable(1);
                footerTbl1.TotalWidth = 400;
                footerTbl1.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell1 = new PdfPCell(footer1);
                cell1.Border = 0;
                cell1.PaddingLeft = 10;
                footerTbl1.AddCell(cell1);

                footerTbl1.WriteSelectedRows(0, -1, 0, 30, writer.DirectContent);


                //Paragraph footer11 = new Paragraph("Lahore, Pakistan", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                //footer11.Alignment = Element.ALIGN_CENTER;

                //PdfPTable footerTbl11 = new PdfPTable(1);
                //footerTbl11.TotalWidth = 400;
                //footerTbl11.HorizontalAlignment = Element.ALIGN_CENTER;
                //PdfPCell cell11 = new PdfPCell(footer11);
                //cell11.Border = 0;
                //cell11.PaddingLeft = 10;
                //footerTbl11.AddCell(cell11);
                // footerTbl11.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

            }

        }
        string FillDynamicValues(string OrderNo, string InvNo, string InvoiceTo, string CityCountry, string EmailID, string PlanName, string Price)
        {
            try
            {



                string html = "";

                string file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/Invoice.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }



                html = html.Replace("##INVNO##", InvNo);
                html = html.Replace("##ORDNO##", OrderNo.PadLeft(6, '0'));
                html = html.Replace("##INVDT##", String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now.Date));
                html = html.Replace("##INVDUE##", String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now.Date.AddDays(3)));
                html = html.Replace("##FULLNAME##", InvoiceTo);
                html = html.Replace("##CITYCOUNTRY##", CityCountry);
                html = html.Replace("##EMLID##", EmailID);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
                html = html.Replace("##PRCTOTAL##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string EmailContent(string Name, string InvNo, string PlanName, string Price, string Days)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/InvoiceEmail.html");
                //file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/InvoiceEmailComp.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##INVNO##", InvNo);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
                html = html.Replace("##PLNDYS##", Days);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string AdminEmailContent(string Name, string CUSTEMAIL, string InvNo, string OrdNo, string PlanName, string Price)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/AdminInvoiceEmail.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##INVNO##", InvNo);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", "Rs. " + String.Format("{0:0,0}", int.Parse(Price.Replace("Rs. ", ""))));
                html = html.Replace("##CUSTEMAIL##", CUSTEMAIL);
                html = html.Replace("##ORDNM##", OrdNo);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void SendOrderSMS(string OrderNo, string Name, string MobileNo, string NoOfDays, string Price)
        {
            try
            {
                //string smstxt = " Dear "+Name+", Thank you for placing Order "+OrderNo+". Kindly make the required payment and send us a "
                //+"copy of the Deposit Slip at info@eastlaw.pk to confirm your order.";

                //string smstxt = " Dear " + Name + ", Thank you for placing free renewal request of eastlaw.pk, your request will be process within 24 hours.";
                string smstxt = " Thank you for placing Order No. " + OrderNo + " for Subscription Package of " + NoOfDays + " Days. Please make the payment of Rs. " + string.Format("{0:0,0}", int.Parse(Price.ToString().Replace("Rs. ", ""))) + "/- and update us for Account Activation. Customer Care No. 03108131610";
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
        #endregion
    }
}