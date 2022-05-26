using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Net;
using System.Configuration;

namespace EastlawUI_v2.adminpanel
{
    public partial class ManageUserPlanUpdate : System.Web.UI.Page
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

                    txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    txtEmailID.Enabled = false;

                   

                    txtFullName.Text = dt.Rows[0]["FullName"].ToString(); ;
                    txtPhone.Text = dt.Rows[0]["PhoneNo"].ToString(); ;
                    txtAdd.Text = dt.Rows[0]["Address"].ToString(); 
                    ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString(); ;
                    txtNoOfPCAllowed.Text = dt.Rows[0]["NoOfPCAllowd"].ToString(); ;
                    ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString(); ;
                    txtNoOfdays.Text = dt.Rows[0]["ExpireIn"].ToString(); 
                    if (dt.Rows[0]["Active"].ToString() == "1")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;



                    divSuccess.Style["Display"] = "none";
                    divError.Style["Display"] = "none";

                    if (Request.QueryString["inv"] != null)
                        lblInvNo.Text = EncryptDecryptHelper.Decrypt(Request.QueryString["inv"].ToString());

                    if (Request.QueryString["pl"] != null)
                    {
                        ddlPlan.SelectedValue = EncryptDecryptHelper.Decrypt(Request.QueryString["pl"].ToString());
                        GetActivePlans(int.Parse(ddlPlan.SelectedValue));
                    }




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
                    if (dt.Rows.Count > 0)
                    {
                        lblPlanNoOfDays.Text = dt.Rows[0]["NoofDays"].ToString();
                        txtAmt.Text = dt.Rows[0]["Price"].ToString();
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
        void SessionNumber()
        {
            int num;
            Random random = new Random();
            num = random.Next(0, 100000);
            Session["tempID"] = num;
        }
        void UpdateUserPlans()
        {
            try
            {
                string uploaddoc = "";

                if (Session["tempID"] == null)
                {
                    SessionNumber();
                }
                if (fupload.HasFile)
                {
                    string destDir = Server.MapPath("../store/users/updateplandocs/");

                    string FileName = Path.GetFileName(fupload.PostedFile.FileName);
                    string destPath = Path.Combine(destDir, Session["tempID"].ToString() + FileName.Replace(" ", ""));
                    fupload.SaveAs(destPath);

                    uploaddoc = Session["tempID"].ToString() + fupload.FileName.Replace(" ", "");
                }
                objUsr.UserID =int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString()));
                objUsr.PlanID = int.Parse(ddlPlan.SelectedValue);
                if (int.Parse(txtNoOfdays.Text) > 0)
                    objUsr.PlanStart = DateTime.Now.AddDays(int.Parse(txtNoOfdays.Text)).ToString("MM/dd/yyyy HH:MM:ss");
                else
                    objUsr.PlanStart = DateTime.Now.ToString("MM/dd/yyyy HH:MM:ss");
                
                if(int.Parse(txtNoOfdays.Text) > 0)
                    objUsr.PlanEnd = DateTime.Now.AddDays(double.Parse(lblPlanNoOfDays.Text) + int.Parse(txtNoOfdays.Text)).ToString("MM/dd/yyyy HH:MM:ss");
                else
                    objUsr.PlanEnd = DateTime.Now.AddDays(double.Parse(lblPlanNoOfDays.Text)).ToString("MM/dd/yyyy HH:MM:ss");
                objUsr.Amt = int.Parse(txtAmt.Text);
                objUsr.InvoiceID = int.Parse(lblInvNo.Text);
                objUsr.CreatedBy = int.Parse(Session["UserID"].ToString());
                objUsr.Remarks = txtRemarks.Text;
                objUsr.Uploadfile = uploaddoc;
                objUsr.ReceiptNo = txtPaymentReceiptNo.Text;
             
                int chk = objUsr.InsertUserPlanUpdate();
                if (chk > 0)
                {
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";

                    //string CustName =txtFullName.Text+ "<br/>" + txtPhone.Text + "<br/>" + txtAdd.Text;

                    DataTable dt = objUsr.GetUsers(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
                    string CustName = dt.Rows[0]["FullName"].ToString();

                    //GeneratePDF(chk.ToString(), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), lblPlanName.Text, lblPrice.Text);


                    GeneratePDF(lblInvNo.Text, (objUsr.UserID.ToString().PadLeft(6, '0') + "0" + lblInvNo.Text).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), ddlPlan.SelectedItem.Text, txtAmt.Text);

                    Email.SendMail(txtEmailID.Text, ConfirmationEmail(), "Order Confirmation", "EastLaw", Server.MapPath("/store/users/paidinvoices/RCP_" + lblInvNo.Text + ".pdf"));
                    Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ConfirmationEmailAdmin(lblInvNo.Text.PadLeft(6, '0')), "Order # " + lblInvNo.Text.PadLeft(6, '0') + "  Confirmation", "EastLaw", Server.MapPath("/store/users/paidinvoices/RCP_" + lblInvNo.Text + ".pdf"));
                    Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ConfirmationEmailAdmin(lblInvNo.Text.PadLeft(6, '0')), "Order # " + lblInvNo.Text.PadLeft(6, '0') + "  Confirmation", "EastLaw", Server.MapPath("/store/users/paidinvoices/RCP_" + lblInvNo.Text + ".pdf"));

                    SendOrderConfirmationSMS(lblInvNo.Text);
                    divSuccess.Style["Display"] = "";
                    divError.Style["Display"] = "none";
                    //Response.Redirect("Manageusers.aspx");

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
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetActivePlans(int.Parse(ddlPlan.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "SaveRecord", ex.Message);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
           // ClearFields();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["param"] != null)

                    UpdateUserPlans();
            }
            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("AddUsers.aspx", "btnSave_Click", ex.Message);
            }
        }
        string ConfirmationEmail()
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/OrderConfirmation.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", txtFullName.Text);
                html = html.Replace("##ORDNUM##", lblInvNo.Text.ToString().PadLeft(6,'0'));
                html = html.Replace("##PLNPRICE##", txtAmt.Text);
                html = html.Replace("##PLNNAME##", ddlPlan.SelectedItem.Text);
                
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void SendOrderConfirmationSMS(string OrderNo)
        {
            try
            {
                //string smstxt = "Dear " + txtFullName.Text.Trim() + ", We are pleased to confirm your Order No. " + OrderNo + ""
                //+ "and wish to inform you that your account at www.eastlaw.pk is now active ";

                string smstxt = "Dear User, Congratulations as your Order " + OrderNo.ToString().PadLeft(6, '0') + " has been approved and your Subscription Package is now Active. Helpline#. 04237311670";

                string mobilenumber = txtPhone.Text.Trim();
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
        string ConfirmationEmailAdmin(string OrderNo)
        {
            try
            {
                string file = "";


                string html = "";

                html = "<b>Order #: " + OrderNo +" Confirmed <br>Username: " + txtEmailID.Text+"</b>";

              

                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void GeneratePDF(string InvID, string InvNo, string CustomerName,string CityCountry,string EmailID , string PlanName, string Price)
        {
            string html = FillDynamicValues(InvID, InvNo, CustomerName,CityCountry,EmailID, PlanName, Price);


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    StringReader sr = new StringReader(html);
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("/store/users/paidinvoices/RCP_") + InvID + ".pdf", FileMode.Create));
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    writer.PageEvent = new Footer();
                    pdfDoc.Close();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.Write(pdfDoc);
                    //Response.End();
                }
            }
        }
        string FillDynamicValues(string OrderNo, string InvNo, string InvoiceTo, string CityCountry,string EmailID, string PlanName, string Price)
        {
            try
            {



                string html = "";

                string file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/InvoicePaid.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##INVNO##", InvNo);
                html = html.Replace("##ORDNO##", OrderNo.PadLeft(6, '0'));
                html = html.Replace("##INVDT##", DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                html = html.Replace("##INVDUE##", DateTime.Now.Date.AddDays(3).ToString("dd/MMM/yyyy"));
                html = html.Replace("##FULLNAME##", InvoiceTo);
                html = html.Replace("##CITYCOUNTRY##", CityCountry);
                html = html.Replace("##EMLID##", EmailID);
                html = html.Replace("##PLNNAME##", PlanName);
                html = html.Replace("##PLNPRICE##", Price);
                html = html.Replace("##PRCTOTAL##", Price);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
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


                Paragraph footer11 = new Paragraph("Lahore, Pakistan", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                footer11.Alignment = Element.ALIGN_CENTER;

                PdfPTable footerTbl11 = new PdfPTable(1);
                footerTbl11.TotalWidth = 400;
                footerTbl11.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell11 = new PdfPCell(footer11);
                cell11.Border = 0;
                cell11.PaddingLeft = 10;
                footerTbl11.AddCell(cell11);

                footerTbl11.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

            }

        }
    }
}