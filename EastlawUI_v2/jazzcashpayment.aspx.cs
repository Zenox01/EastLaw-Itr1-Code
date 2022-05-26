using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EastlawUI_v2
{
    public partial class jazzcashpayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {

                if (Request.Form.Count > 0)
                {
                    Session["ResponseMessage"] = Request.Form["pp_ResponseMessage"];
                    Session["ResponseCode"] = Request.Form["pp_ResponseCode"];
                    Session["RRN"] = Request.Form["pp_RetreivalReferenceNo"];

                    //
                    lblRrn.Text = Request.Form["pp_RetreivalReferenceNo"];
                    lblCode.Text = Request.Form["pp_ResponseCode"];
                    lblResponse.Text = Request.Form["pp_ResponseMessage"] + ", Msg It: " + Request.Form["msg_it"];
                }

                string[] keys = Request.Form.AllKeys;
                var value = "";
                for (int i = 0; i < keys.Length; i++)
                {
                    // here you get the name eg test[0].quantity
                    // keys[i];
                    // to get the value you use
                    value = Request.Form[keys[i]];
                }
            }
            //DoTransaction(this.Page);
            Session["JazzCash"] = "Yes";

        }
        // Transaction Method with All Parameters
        void DoTransaction(Page page)
        {
            string MerchantID = "00137155";
            string Password = "v0w02wa0x5";
            string TxnDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string TxnExpiryDateTime = DateTime.Now.AddDays(7).ToString("yyyyMMddHHmmss");
            string TxnReNo = "TXN" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string Version = "1.1";
            int Amount =int.Parse(txtAmt.Text.Trim());
            string url = "http://testpayments.jazzcash.com.pk/PayAxisCustomerPortal/transactionmanagement/merchantform";
            string TxnCurrency = "PKR";
            string IntegeratedSalt = "t025aa0t64";
            string TxnType = txtTrnType.Text.Trim();
            //string TxnType = "OTC";
            //string ReturnUrl = "localhost:7837/Payments.aspx";
            string ReturnUrl = txtRetunURL.Text.Trim();
            //string ReturnUrl = "https://eastlaw.pk/member/order-confirmation";

            NameValueCollection data = new NameValueCollection();
            data.Add("pp_Version", Version);
            data.Add("pp_TxnType", TxnType);
            data.Add("pp_Language", "EN");
            data.Add("pp_MerchantID", MerchantID);
            data.Add("pp_SubMerchantID", "");
            data.Add("pp_Password", Password);
            data.Add("pp_BankID", "");
            data.Add("pp_ProductID", "");
            data.Add("pp_TxnRefNo", TxnReNo);
            data.Add("pp_Amount", Amount.ToString());
            data.Add("pp_TxnCurrency", TxnCurrency);
            data.Add("pp_TxnDateTime", TxnDateTime);
            data.Add("pp_BillReference", "12345");
            data.Add("pp_Description", "Thankyou for using Jazz Cash");
            data.Add("pp_TxnExpiryDateTime", TxnExpiryDateTime);
            data.Add("pp_ReturnURL", ReturnUrl);
            data.Add("ppmpf_1", "1");
            data.Add("ppmpf_2", "2");
            data.Add("ppmpf_3", "3");
            data.Add("ppmpf_4", "4");
            data.Add("ppmpf_5", "5");

            //Amount             Bill ref       Description        Language        MerchantID        Password        Return URL       Currency    TxnDateTime        TxnRefNo        Version
            string secureHash = IntegeratedSalt
                + "&" + data.Get("pp_Amount")
                + "&" + data.Get("pp_BillReference")
                + "&" + data.Get("pp_Description")
                + "&" + data.Get("pp_Language")
                + "&" + data.Get("pp_MerchantID")
                + "&" + data.Get("pp_Password")
                + "&" + data.Get("pp_ReturnURL")
                + "&" + data.Get("pp_TxnCurrency")
                + "&" + data.Get("pp_TxnDateTime")
                + "&" + data.Get("pp_TxnExpiryDateTime")
                + "&" + data.Get("pp_TxnRefNo")
                + "&" + data.Get("pp_TxnType")
                + "&" + data.Get("pp_Version")
                + "&" + data.Get("ppmpf_1")
                + "&" + data.Get("ppmpf_2")
                + "&" + data.Get("ppmpf_3")
                + "&" + data.Get("ppmpf_4")
                + "&" + data.Get("ppmpf_5");

            data.Add("pp_SecureHash", HashMach(secureHash, IntegeratedSalt));

            //Prepare the Posting form
            string strForm = PreparePOSTForm(url, data);

            //Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
            page.Controls.Add(new LiteralControl(strForm));
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            try
            {
                //1st 
                DoTransaction(this.Page);
                Session["JazzCash"] = "Yes";
                //2nd 
                //JazzCash();

                //3rd
                //Pay();
            }
            catch (Exception ex)
            {
                string exc = ex.Message;
            }
        }


        // Form Creation method for submission
        private static String PreparePOSTForm(string url, NameValueCollection data)
        {
            //Set a name for the form
            string formID = "onlineform";

            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");

            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
            }

            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //Return the form and the script concatenated. (The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
            //return strForm.ToString();
        }
        // Hash Generated Method
        private static string HashMach(String data, String key)
        {
            //String data = " Your Concatenated String ";
            //String key = "Your Hashkey";
            var secretBytes = Encoding.UTF8.GetBytes(key);
            using (var hmac = new HMACSHA256(secretBytes))
            {
                byte[] iso88591data = Encoding.GetEncoding("ISO-8859-1").GetBytes(data);
                var hash = hmac.ComputeHash(iso88591data);
                StringBuilder hex = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                    hex.AppendFormat("{0:x2}", b);

                return hex.ToString();
            }
        }
    }
}