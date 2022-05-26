using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Http;

namespace EastlawUI_v2
{
    public partial class MySubscription : System.Web.UI.Page
    {
        EastLawBL.Plans objplan = new EastLawBL.Plans();
        EastLawBL.Users objusr = new EastLawBL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetPendingOrders(int.Parse(Session["MemberID"].ToString()));
                GetUserStatus();
                GetActivePlans();
            }
        }
        void GetPendingOrders(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetPendingInvoiceByUserID(UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("OrdNo");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            dt.Rows[a]["OrdNo"] = dt.Rows[a]["ID"].ToString().PadLeft(6, '0');
                        }
                        dt.AcceptChanges();
                        divPendingOrders.Style["Display"] = "";
                        gvPendingOrders.DataSource = dt;
                        gvPendingOrders.DataBind();
                    }
                    else
                    {
                        divPendingOrders.Style["Display"] = "none";
                    }
                }

            }
            catch { }
        }
        void GetUserStatus()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objusr.GetUsers(int.Parse(Session["MemberID"].ToString()));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DateTime dtExpiryOn = DateTime.Parse(dt.Rows[0]["FormatedExpire"].ToString());
                        lblUserState.Text = "Your Current Subscription " + dt.Rows[0]["PlanName"].ToString() + " will be expire on " + String.Format("{0:dddd, MMMM d, yyyy}", dtExpiryOn) + ".";
                    }
                }
            }
            catch { }
        }
        void GetActivePlans()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objplan.GetActivePlansFrontEnd();
                gvPlans.DataSource = dt;
                gvPlans.DataBind();

                DataTable dtCorporate = new DataTable();
                dtCorporate = objplan.GetActiveCorporatePlansFrontEnd();
                gvCorporatePlans.DataSource = dtCorporate;
                gvCorporatePlans.DataBind();
            }
            catch { }
        }
        protected void gvPlans_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvPlans.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                Label lblPlanName = default(Label);
                Label lblPrice = default(Label);
                Label lblPlanDays = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int PlanID = Convert.ToInt32(hd.Value);
                    lblPlanName = (Label)row.FindControl("lblPlanName");
                    lblPrice = (Label)row.FindControl("lblPrice");
                    lblPlanDays = (Label)row.FindControl("lblNoOfDays");

                    Session["SelectedPlanID"] = hd.Value;
                    Session["SelectedPlanName"] = lblPlanName.Text;
                    Session["SelectedPlanPrice"] = lblPrice.Text;
                    Session["SelectedPlanDays"] = lblPlanDays.Text;
                    Response.Redirect("/member/review-order/");
                    //if (Session["ExpiredMemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["ExpiredMemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID, Session["ExpiredMemberID"].ToString(), "Unpaid", 0, int.Parse(Session["ExpiredMemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text,dt.Rows[0]["FullName"].ToString(),dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["ExpiredMemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString() ;


                    //            }
                    //        }
                    //    }
                    //}
                    //else if (Session["MemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["MemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["MemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["MemberID"].ToString()), PlanID, Session["MemberID"].ToString(), "Unpaid", 0, int.Parse(Session["MemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text, dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["MemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString();


                    //            }
                    //        }
                    //    }
                }
                else
                {
                    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                }



            }



            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Subscription.aspx", "gvPlans_RowEditing", ex.Message);
            }
        }
        protected void gvCorporatePlans_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvCorporatePlans.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                Label lblPlanName = default(Label);
                Label lblPrice = default(Label);
                Label lblPlanDays = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int PlanID = Convert.ToInt32(hd.Value);
                    lblPlanName = (Label)row.FindControl("lblPlanName");
                    lblPrice = (Label)row.FindControl("lblPrice");
                    lblPlanDays = (Label)row.FindControl("lblNoOfDays");

                    Session["SelectedPlanID"] = hd.Value;
                    Session["SelectedPlanName"] = lblPlanName.Text;
                    Session["SelectedPlanPrice"] = lblPrice.Text;
                    Session["SelectedPlanDays"] = lblPlanDays.Text;
                    Response.Redirect("/member/review-order/");
                    //if (Session["ExpiredMemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["ExpiredMemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["ExpiredMemberID"].ToString()), PlanID, Session["ExpiredMemberID"].ToString(), "Unpaid", 0, int.Parse(Session["ExpiredMemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["ExpiredMemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text,dt.Rows[0]["FullName"].ToString(),dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["ExpiredMemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString() ;


                    //            }
                    //        }
                    //    }
                    //}
                    //else if (Session["MemberID"] != null)
                    //{
                    //    DataTable dt = objusr.GetUsers(int.Parse(Session["MemberID"].ToString()));

                    //    DataTable dtExist = objusr.CheckOrderExist(int.Parse(Session["MemberID"].ToString()), PlanID);
                    //    if (dtExist != null)
                    //    {
                    //        if (dtExist.Rows.Count > 0)
                    //        {
                    //            lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //            divOrderExist.Style["Display"] = "";
                    //            divPlans.Style["Display"] = "none";
                    //            divConfirm.Style["Display"] = "none";

                    //        }
                    //        else
                    //        {
                    //            int chk = objusr.AddInvoice(int.Parse(Session["MemberID"].ToString()), PlanID, Session["MemberID"].ToString(), "Unpaid", 0, int.Parse(Session["MemberID"].ToString()));
                    //            if (chk > 0)
                    //            {
                    //                lblPlan.Text = "1 x " + lblPlanName.Text;
                    //                lblPrice1.Text = lblPrice.Text;
                    //                lblEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    //                // gvPlans.Visible = false;
                    //                lblInvNo.Text = chk.ToString().PadLeft(6, '0');

                    //                // string CustName = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[0]["PhoneNo"].ToString() + "<br/>" + dt.Rows[0]["Address"].ToString();
                    //                string CustName = dt.Rows[0]["FullName"].ToString();


                    //                GeneratePDF(chk.ToString(), (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), CustName, dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblPlanName.Text, lblPrice.Text);

                    //                string mailcntn = EmailContent(dt.Rows[0]["FullName"].ToString(), lblInvNo.Text, lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail(dt.Rows[0]["EmailID"].ToString(), mailcntn, "Order Receive - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                string Adminmailcntn = AdminEmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString(), lblInvNo.Text, (Session["MemberID"].ToString() + "0" + chk.ToString()).PadLeft(6, '0'), lblPlanName.Text, lblPrice.Text);
                    //                Email.SendMail("registration@eastlaw.pk", Adminmailcntn, "Order Request - Invoice", "EastLaw", Server.MapPath("/store/users/invoices/INV_" + chk.ToString() + ".pdf"));

                    //                SendOrderSMS(lblInvNo.Text, dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["PhoneNo"].ToString());

                    //                divPlans.Style["Display"] = "none";
                    //                divConfirm.Style["Display"] = "";

                    //                Session["OrderAmt"] = lblPrice.Text;
                    //                Session["OrderRefNum"] = lblInvNo.Text;
                    //                Session["OrderMemberID"] = Session["MemberID"].ToString();
                    //                Session["OrderPlanID"] = PlanID.ToString();


                    //            }
                    //        }
                    //    }
                }
                else
                {
                    Response.Redirect("/member/member-login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                }



            }



            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Subscription.aspx", "gvPlans_RowEditing", ex.Message);
            }
        }
        protected void gvPendingOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = gvPendingOrders.Rows[e.NewEditIndex];
                HiddenField hd = default(HiddenField);
                Label lblPlanName = default(Label);
                Label lblPrice = default(Label);
                Label lblPlanDays = default(Label);

                if ((row != null))
                {
                    hd = (HiddenField)row.FindControl("hdID");
                    int PlanID = Convert.ToInt32(hd.Value);
                    lblPlanName = (Label)row.FindControl("lblPlanName");
                    lblPrice = (Label)row.FindControl("lblPrice");
                    lblPlanDays = (Label)row.FindControl("lblNoOfDays");

                    EasyPaisa(lblPrice.Text, hd.Value.ToString());

                }
                else
                {
                    Response.Redirect("/member/member-Login?req=" + HttpContext.Current.Request.Url.AbsolutePath);
                }



            }



            catch (Exception ex)
            {
                ExceptionHandling.SendErrorReport("Subscription.aspx", "gvPlans_RowEditing", ex.Message);
            }
        }
        async void EasyPaisa(string Amt, string OrderNo)
        {
            try
            {
                // Response.Write("Start Easypaisa");
                using (var client = new HttpClient())
                {
                    var values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("storeId", "4023"));
                    values.Add(new KeyValuePair<string, string>("amount", Amt.ToString().Replace("Rs. ", "")));
                    values.Add(new KeyValuePair<string, string>("postBackURL", "https://eastlaw.pk/Member/Order-Confirmation"));
                    values.Add(new KeyValuePair<string, string>("orderRefNum", OrderNo.ToString().PadLeft(6, '0')));
                    values.Add(new KeyValuePair<string, string>("expiryDate", DateTime.Now.AddDays(30).ToString("yyyyMMdd hhmmss")));
                    values.Add(new KeyValuePair<string, string>("merchantHashedReq", "as;dlkjfaslk==asdfasdfasdf"));
                    values.Add(new KeyValuePair<string, string>("autoRedirect", "0"));
                    //  values.Add(new KeyValuePair<string, string>(" paymentMethod", " OTC_PAYMENT_METHOD"));
                    var content = new FormUrlEncodedContent(values);
                    //  var response = client.PostAsync("http://202.69.8.50:9080/easypay/Index.jsf", content);
                    //  var response = await client.PostAsync("http://202.69.8.50:9080/easypay/Index.jsf", content);
                    var response = await client.PostAsync("https://easypay.easypaisa.com.pk/easypay/Index.jsf", content);
                    var responseString = await response.Content.ReadAsStringAsync();


                    string url = response.RequestMessage.RequestUri.Query;

                    //Response.Write("URL Val: " + values[0].ToString() + "<br>");
                    //Response.Write("URL Val: " + values[1].ToString() + "<br>");
                    //Response.Write("URL Val: " + values[2].ToString() + "<br>");
                    //Response.Write("URL Val: " + values[3].ToString() + "<br>");
                    //Response.Write("URL Val: " + values[4].ToString() + "<br>"); 
                    //Response.Write("URL Val: " + values[5].ToString() + "<br>");
                    //Response.Write("URL Val: " + values[6].ToString() + "<br>");
                    // Response.Write("URL Val: " + response.RequestMessage.ToString() + "<br>");
                    //Response.Write("URL Val: " + values[7].ToString() + "<br>");
                    //  Response.Write( "URL Response: "+url);
                    string[] parts = url.Split(new char[] { '?', '&' });

                    easypaisa obje = new easypaisa();
                    obje.Aut_Code = parts[1].ToString().Replace("auth_token=", "");

                    Session["autcode"] = parts[1].ToString().Replace("auth_token=", "");

                    var formPostText = @"<html><body><div>
                                    <form method=""POST"" action=""https://easypay.easypaisa.com.pk/easypay/Confirm.jsf"" name=""frm2Post"">
                                    <input type=""hidden"" name=""auth_token"" value=""" + parts[1].ToString().Replace("auth_token=", "") + @""" /> 
                                    <input type=""hidden"" name=""postBackURL"" value=""https://eastlaw.pk//Member/Order-Confirmation"" /> 
                                    </form></div><script type=""text/javascript"">document.frm2Post.submit();</script></body></html>
                                    ";
                    Response.Write(formPostText);

                }
                //  Response.Write("End Easypaisa");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}