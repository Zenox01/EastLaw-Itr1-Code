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

namespace EastlawUI_v2
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string a = "";


                if (Request.Form.Count > 0)
                {
                    //Session["ResponseMessage"] = Request.Form["pp_ResponseMessage"];
                    //Session["ResponseCode"] = Request.Form["pp_ResponseCode"];
                    //Session["RRN"] = Request.Form["pp_RetreivalReferenceNo"];


                  //  a = a + Request.Form["pp_ResponseMessage"] + "<br>";
                  //  a = a + Request.Form["pp_ResponseCode"] + "<br>";
                  //  a = a + Request.Form["pp_RetreivalReferenceNo"] + "<br>";
                  //  a = a + Request.Form["pp_BillReference"] + "<br>";
                  //  Response.Write("redirect from jazz 1");

                  //  //
                  //  a = a + Request.Form["pp_RetreivalReferenceNo"] + "<br>";
                  //  a = a + Request.Form["pp_ResponseCode"] + "<br>";
                  //  a = a + Request.Form["pp_ResponseMessage"] + ", Msg It: " + Request.Form["msg_it"] + "<br>";

                  ////  GetInvoiceDetails(int.Parse(Request.Form["pp_BillReference"]));

                  //  if (!string.IsNullOrEmpty(Request.Form["pp_BillReference"]))
                  //  {
                  //      UpdateUserPlans(int.Parse(Request.Form["pp_BillReference"]), Request.Form["pp_ResponseCode"],int.Parse(Request.Form["pp_Amount"]),
                  //         Request.Form["pp_ResponseMessage"], Request.Form["pp_RetreivalReferenceNo"]);
                  //  }

                    UpdateUserPlans(int.Parse(Request.Form["pp_BillReference"]), Request.Form["pp_ResponseCode"], int.Parse(Request.Form["pp_Amount"]),
                           Request.Form["pp_ResponseMessage"], Request.Form["pp_RetreivalReferenceNo"]);
                    // Response.Write()
                }
               // Response.Write(a);
                //if (Session["JazzCash"] != null)
                //{
                //    Response.Write("redirect from jazz 11");
                   
                //    try
                //    {
                //        if (Request.Form.Count > 0)
                //        {
                //            //Session["ResponseMessage"] = Request.Form["pp_ResponseMessage"];
                //            //Session["ResponseCode"] = Request.Form["pp_ResponseCode"];
                //            //Session["RRN"] = Request.Form["pp_RetreivalReferenceNo"];


                //            a = a + Request.Form["pp_ResponseMessage"] + "<br>";
                //            a = a + Request.Form["pp_ResponseCode"] + "<br>";
                //            a = a + Request.Form["pp_RetreivalReferenceNo"] + "<br>";
                //            Response.Write("redirect from jazz 1");

                //            //
                //            a = a + Request.Form["pp_RetreivalReferenceNo"] + "<br>";
                //            a = a + Request.Form["pp_ResponseCode"] + "<br>";
                //            a = a + Request.Form["pp_ResponseMessage"] + ", Msg It: " + Request.Form["msg_it"] + "<br>";

                //            // Response.Write()
                //        }


                //        string[] keys = Request.Form.AllKeys;
                //        var value = "";
                //        for (int i = 0; i < keys.Length; i++)
                //        {
                //            // here you get the name eg test[0].quantity
                //            // keys[i];
                //            // to get the value you use
                //            value = Request.Form[keys[i]];
                //        }

                //    }
                //    catch (Exception ex) {
                //        Response.Write(ex.Message);
                //    }
                //    Response.Write("redirect from jazz 2");
                //    Response.Write(a);
                //}
                //if (Session["OrderRefNum"] != null)
                //{
                //    GetInvoiceDetails(int.Parse(Session["OrderRefNum"].ToString()));
                //}
                //Response.Write("redirect from jazz 3 " + a);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        void GetPendingInvoiceDetails(int InvoiceID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetPendingInvoiceByID(InvoiceID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblPlan.Text = "1 x " + dt.Rows[0]["PlanName"].ToString();
                        lblPrice1.Text = "Rs " + String.Format("{0:0,0}", int.Parse(dt.Rows[0]["Price"].ToString()));
                        lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                        lblInvNo.Text = dt.Rows[0]["ID"].ToString().PadLeft(6, '0');
                        divConfirm.Style["Display"] = "";
                        lblPaymentStatus.Text = "Payment Pending";
                    }
                }
            }
            catch { }
        }
        void GetInvoiceDetails(int InvoiceID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetInvoiceByID(InvoiceID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblPlan.Text = "1 x " + dt.Rows[0]["PlanName"].ToString();
                        lblPrice1.Text = "Rs " + String.Format("{0:0,0}", int.Parse(dt.Rows[0]["Price"].ToString()));
                        lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                        lblInvNo.Text = dt.Rows[0]["ID"].ToString().PadLeft(6, '0');
                        divConfirm.Style["Display"] = "";
                        lblPaymentStatus.Text = "Completed";
                    }
                }
            }
            catch { }
        }
        void SessionNumber()
        {
            int num;
            Random random = new Random();
            num = random.Next(0, 100000);
            Session["tempID"] = num;
        }
        void UpdateUserPlans(int InvoiceID,string ResponseCode,int Amount,string Remarks,string PaymentReceiptNumber)
        {
            try
            {
                DataTable dtinv = new DataTable();
                dtinv = objusr.GetPendingInvoiceByID(InvoiceID);
                if (ResponseCode=="000")
                { 
                EastLawBL.Users objUsr = new EastLawBL.Users();
                string uploaddoc = "";

                if (Session["tempID"] == null)
                {
                    SessionNumber();
                }

                
                    if (dtinv != null && dtinv.Rows.Count > 0)
                    {
                        objUsr.UserID = int.Parse(dtinv.Rows[0]["UserID"].ToString());
                        objUsr.PlanID = int.Parse(dtinv.Rows[0]["PlanID"].ToString());
                        if (int.Parse(dtinv.Rows[0]["ExpireIn"].ToString()) > 0)
                            objUsr.PlanStart = DateTime.Now.AddDays(int.Parse(dtinv.Rows[0]["ExpireIn"].ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanStart = DateTime.Now.ToString("MM/dd/yyyy HH:MM:ss");

                        if (int.Parse(dtinv.Rows[0]["ExpireIn"].ToString()) > 0)
                            objUsr.PlanEnd = DateTime.Now.AddDays(double.Parse(dtinv.Rows[0]["NoofDays"].ToString()) + int.Parse(dtinv.Rows[0]["ExpireIn"].ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        else
                            objUsr.PlanEnd = DateTime.Now.AddDays(double.Parse(dtinv.Rows[0]["NoofDays"].ToString())).ToString("MM/dd/yyyy HH:MM:ss");
                        objUsr.Amt = Amount;
                        objUsr.InvoiceID = InvoiceID;
                        objUsr.CreatedBy = int.Parse(dtinv.Rows[0]["UserID"].ToString());
                        objUsr.Remarks = Remarks;
                        objUsr.Uploadfile = uploaddoc;
                        objUsr.ReceiptNo = PaymentReceiptNumber;

                        int chk = objUsr.InsertUserPlanUpdate();
                        if (chk > 0)
                        {


                            //string CustName =txtFullName.Text+ "<br/>" + txtPhone.Text + "<br/>" + txtAdd.Text;

                            DataTable dt = objUsr.GetUsers(int.Parse(dtinv.Rows[0]["UserID"].ToString()));
                            string CustName = dt.Rows[0]["FullName"].ToString();

                            //GeneratePDF(chk.ToString(), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), lblPlanName.Text, lblPrice.Text);


                            GeneratePDF(lblInvNo.Text, (objUsr.UserID.ToString().PadLeft(6, '0') + "0" + InvoiceID.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), dtinv.Rows[0]["PlanName"].ToString(), Amount.ToString());

                            Email.SendMail(dtinv.Rows[0]["EmailID"].ToString(), ConfirmationEmail(InvoiceID, CustName,Amount.ToString(), dtinv.Rows[0]["PlanName"].ToString()), "Order Confirmation", "EastLaw", Server.MapPath("/store/users/paidinvoices/RCP_" + lblInvNo.Text + ".pdf"));
                            Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), ConfirmationEmailAdmin(lblInvNo.Text.PadLeft(6, '0'), dtinv.Rows[0]["EmailID"].ToString()), "Order # " + lblInvNo.Text.PadLeft(6, '0') + "  Confirmation", "EastLaw", Server.MapPath("/store/users/paidinvoices/RCP_" + lblInvNo.Text + ".pdf"));
                            Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), ConfirmationEmailAdmin(lblInvNo.Text.PadLeft(6, '0'), dtinv.Rows[0]["EmailID"].ToString()), "Order # " + lblInvNo.Text.PadLeft(6, '0') + "  Confirmation", "EastLaw", Server.MapPath("/store/users/paidinvoices/RCP_" + lblInvNo.Text + ".pdf"));

                            SendOrderConfirmationSMS(lblInvNo.Text, dtinv.Rows[0]["PhoneNo"].ToString());
                            GetInvoiceDetails(InvoiceID);
                            //Response.Redirect("Manageusers.aspx");

                        }
                        else
                        {
                            //divSuccess.Style["Display"] = "none";
                            //divError.Style["Display"] = "";
                        }
                    }
                   
                }
                else if (ResponseCode != "000")
                {
                    int chk = objusr.UpdateInvoiceStatus(InvoiceID, "Reject-Payment", int.Parse(dtinv.Rows[0]["UserID"].ToString()), Remarks);
                    GetPendingInvoiceDetails(InvoiceID);
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        string ConfirmationEmail(int invoiceno,string fullname,string amount,string plan)
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


                html = html.Replace("##FullName##", fullname);
                html = html.Replace("##ORDNUM##", invoiceno.ToString().PadLeft(6, '0'));
                html = html.Replace("##PLNPRICE##", amount);
                html = html.Replace("##PLNNAME##", plan);

                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void SendOrderConfirmationSMS(string OrderNo,string MobileNo)
        {
            try
            {
                //string smstxt = "Dear " + txtFullName.Text.Trim() + ", We are pleased to confirm your Order No. " + OrderNo + ""
                //+ "and wish to inform you that your account at www.eastlaw.pk is now active ";

                string smstxt = "Dear User, Congratulations as your Order " + OrderNo.ToString().PadLeft(6, '0') + " has been approved and your Subscription Package is now Active. Helpline#. 04237311670";

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
        string ConfirmationEmailAdmin(string OrderNo,string EmailID)
        {
            try
            {
                string file = "";


                string html = "";

                html = "<b>Order #: " + OrderNo + " Confirmed <br>Username: " + EmailID + "</b>";



                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
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
        string FillDynamicValues(string OrderNo, string InvNo, string InvoiceTo, string CityCountry, string EmailID, string PlanName, string Price)
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