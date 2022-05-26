using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;

namespace EastlawUI_v2.adminpanel
{
    public partial class Default : System.Web.UI.Page
    {
        EastLawBL.Users objurs = new EastLawBL.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!Page.IsPostBack)
            {
                InsertBackEndAuditLog("Hit", "Admin Login Page", "");
                if (Request.QueryString["parm"] != null)
                {
                    Session.RemoveAll();
                }

                //Response.Write(EastLawBL.CryptographyManager.EncryptString("Data Source=108.178.32.46,2433;Initial Catalog=EastLaw;User ID=eastlawdb;Password=9#ghDs3K%3s", "CONElsS"));
                // Response.Write(EastLawBL.CryptographyManager.EncryptString("Data Source=eastlawdblive.cd2r3jygscwa.ap-south-1.rds.amazonaws.com,2433;Initial Catalog=EastLaw;User ID=masterestlvdb;Password=oQvRPNOlPmUBO4hUChKn", "CONElsS"));
                //Response.Write(EastLawBL.CryptographyManager.EncryptString("Data Source=ec2-15-207-80-67.ap-south-1.compute.amazonaws.com,1433;Initial Catalog=EastLaw;User ID=masterdbusr;Password=Pass1242#2", "CONElsS"));
                //Response.Write(EastLawBL.CryptographyManager.EncryptString("Data Source=EC2AMAZ-7DH2BB6;Initial Catalog=EastLaw;User ID=masterdbusr;Password=Pass1242#2", "CONElsS"));
                //Response.Write(EastLawBL.CryptographyManager.EncryptString("Data Source=EC2AMAZ-7DH2BB6;Initial Catalog=EastLaw;User ID=masterdbusr;Password=Js1jd#ls1Hs391hd", "LoqTs1"));
               // Response.Write(EastLawBL.CryptographyManager.EncryptString("Data Source=ec2-15-207-80-67.ap-south-1.compute.amazonaws.com,1433;Initial Catalog=EastLaw;User ID=masterdbusr;Password=Js1jd#ls1Hs391hd", "LoqTs1"));
                //Response.Write(sRet);
            }
        }
        void CheckLogin(string EmailID,string Pwd)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objurs.CheckLoginBackend(EmailID, EncryptDecryptHelper.Encrypt(Pwd));
                if (dt.Rows.Count > 0)
                {
                    InsertBackEndAuditLog("Login/Logout", "Login Success "+txtusrName.Text, "");
                    if (dt.Rows[0]["UserTypeID"].ToString() != "5" )
                    {
                        Session["UserID"] = dt.Rows[0]["ID"].ToString();
                        Session["UserName"] = dt.Rows[0]["FullName"].ToString();
                        Session["UserTypeID"] = dt.Rows[0]["UserTypeID"].ToString();
                        lblCon.Visible = false;

                        if (dt.Rows[0]["UserTypeID"].ToString() == "6" || dt.Rows[0]["UserTypeID"].ToString() == "7" || dt.Rows[0]["UserTypeID"].ToString() == "8" || dt.Rows[0]["UserTypeID"].ToString() == "10" || dt.Rows[0]["UserTypeID"].ToString() == "12" || dt.Rows[0]["UserTypeID"].ToString() == "13" || dt.Rows[0]["UserTypeID"].ToString() == "14")
                        {
                            TimeSpan start = new TimeSpan(08, 0, 0); //10 o'clock
                            TimeSpan end = new TimeSpan(19, 0, 0); //12 o'clock
                            TimeSpan now = DateTime.Now.TimeOfDay;

                            if ((now > start) && (now < end))
                            {
                                InsertBackEndAuditLog("Login/Logout", "Login Authorize " + txtusrName.Text, "Normal Users - In Working Hours");
                                if (dt.Rows[0]["UserTypeID"].ToString() == "1")
                                    Response.Redirect("AdminMain.aspx");
                                if (dt.Rows[0]["UserTypeID"].ToString() == "6")
                                    Response.Redirect("AdminMain.aspx");

                                if (dt.Rows[0]["UserTypeID"].ToString() == "11")
                                    Response.Redirect("AdminUsers.aspx");
                                else
                                    Response.Redirect("AdminGeneral.aspx");
                            }
                            else
                            {
                                InsertBackEndAuditLog("Login/Logout", "Login Authorization Failed " + txtusrName.Text, "In Not Working Hours");
                                lblCon.Text = "Not Authorized";
                                lblCon.Visible = true;
                            }
                                

                        }
                        else
                        {
                            InsertBackEndAuditLog("Login/Logout", "Login Authorize " + txtusrName.Text, "Super Users Login");

                            if (dt.Rows[0]["UserTypeID"].ToString() == "1")
                                Response.Redirect("AdminMain.aspx");
                            if (dt.Rows[0]["UserTypeID"].ToString() == "6")
                                Response.Redirect("AdminMain.aspx");

                            if (dt.Rows[0]["UserTypeID"].ToString() == "11")
                                Response.Redirect("AdminUsers.aspx");
                            else
                                Response.Redirect("AdminGeneral.aspx");

                        }
                        
                    }
                    else
                    {
                        InsertBackEndAuditLog("Login/Logout", "Login Authorization Failed " + txtusrName.Text, "Website User Were try to access.");
                        lblCon.Visible = true;
                    }
                }
                else
                {
                    InsertBackEndAuditLog("Login/Logout", "Login Failed " + txtusrName.Text, "Username or password incorrect");
                    lblCon.Visible = true;
                }

            }
                
            catch { }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           
            CheckLogin(txtusrName.Text.Trim(), txtpwd.Text.Trim());


            //string result = EncryptDecryptHelper.Decrypt("RWhkZXdhZmE=");

            //string result = EncryptString("Data Source =WIN-3T21MVEOIKF; Initial Catalog = EastLaw; User ID = sa; Pwd = Js1jd#ls1Hs391hd; Pooling = False; ", "LoqTs1");

        }

        public static string Decrypt(string key)
        {
            byte[] data;
            string decryptedstring;

            data = Convert.FromBase64String(key);


            decryptedstring = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return decryptedstring;
        }
        public static string EncryptString(string message, string passPhrase)
        {
            byte[] results;
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passPhrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] dataToEncrypt = utf8.GetBytes(message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(results);
        }

        public static string DecryptString(string message, string passphrase)
        {
            byte[] results;
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] dataToDecrypt = Convert.FromBase64String(message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return utf8.GetString(results);
        }

        void InsertBackEndAuditLog(string ActType, string Action, string txt)
        {
            try
            {
                EastLawBL.Users objusr = new EastLawBL.Users();
                string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(visitorIPAddress))
                    visitorIPAddress = Request.UserHostAddress;

                int chk = 0;
                Location location = new Location();
                //string APIKey = "76511e33ff8498c62f458bea0a641b144b031bdb1e3eade661df53a39815cb27";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, visitorIPAddress);
                
                //try
                //{
                //    using (System.Net.WebClient client = new System.Net.WebClient())
                //    {
                //        string json = client.DownloadString(url);
                //        location = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Location>(json);

                //    }
                //}
                //catch
                //{ }
                if (Session["UserID"] != null)
                    chk = objusr.InsertBackendAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, int.Parse(Session["UserID"].ToString()), location.CountryName, location.RegionName, location.CityName, txt);
                else
                    chk = objusr.InsertBackendAuditLog(ActType, Action, Request.Url.AbsoluteUri.ToString(), visitorIPAddress, 0, location.CountryName, location.RegionName, location.CityName, txt);
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}