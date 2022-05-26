using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;

namespace EastlawUI_v2.inte
{
    /// <summary>
    /// Summary description for rabta
    /// </summary>
    [WebService(Namespace = "eastlaw")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class rabta : System.Web.Services.WebService
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
        EastLawBL.Keywords objkeyword = new EastLawBL.Keywords();
        EastLawBL.Search objsrc = new EastLawBL.Search();
        EastLawBL.Cases objcases = new EastLawBL.Cases();
        ValidateUsrsForapi objvaluser = new ValidateUsrsForapi();
        EastLawBL.Journals objJo = new EastLawBL.Journals();
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
        

        [WebMethod]
        public string GetSecureData(string txt, string keyval)
        {
            try
            {
                if (keyval == "333s0a04462%2134")
                    return EncryptDecryptHelper.Encrypt(txt);
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [WebMethod]
        public bool Forgetpwd(string keyval,ref string msg)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckUserExistByEmail(EncryptDecryptHelper.Decrypt(keyval));
                if (dt.Rows.Count > 0)
                {
                    string emailcnt = EmailContent(dt.Rows[0]["FullName"].ToString(), dt.Rows[0]["EmailID"].ToString());
                    objUsr.AddUserNotificationLog(int.Parse(dt.Rows[0]["ID"].ToString()), "Forget Password Email", emailcnt);
                    Email.SendMail(dt.Rows[0]["EmailID"].ToString(), emailcnt, "Password Reset - eastlaw", "Eastlaw", "");

                    msg = "Password Reset Link has been sent to the given Email ID. Please check your inbox now.";
                    return true;

                }
                else
                {
                    msg = "Account doesn't exist, kindly register your account.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        string EmailContent(string Name, string EmailID)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/ForgetPassword.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##ClickLink##", "<a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/forget-password?ac=" + EncryptDecryptHelper.Encrypt(EmailID) + "' target='_blank'>Reset your password now </a>");
                html = html.Replace("##FullLink##", ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/forget-password?ac=" + EncryptDecryptHelper.Encrypt(EmailID));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        #region Advance Search
        [WebMethod]
        public DataTable srchKwrd(string SearchText, string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call srchKwrd with parameter: SearchText," + EncryptDecryptHelper.Decrypt(SearchText) + ",St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "srchKwrd", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                int TotalRecords = 0;
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Case Advance Search", EncryptDecryptHelper.Decrypt(SearchText));
                return BindSearchResult(EncryptDecryptHelper.Decrypt(SearchText), ref dtG, ref TotalRecords, int.Parse(EncryptDecryptHelper.Decrypt(St)),
                    int.Parse(EncryptDecryptHelper.Decrypt(Et)), int.Parse(EncryptDecryptHelper.Decrypt(consume)), EncryptDecryptHelper.Decrypt(ip),
                    EncryptDecryptHelper.Decrypt(loc), ref Msg);
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "srchKwrd_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        [WebMethod]
        public DataTable srchExtPhrs(string SearchText, string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call srchExtPhrs with parameter: SearchText," + EncryptDecryptHelper.Decrypt(SearchText) + ",St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "srchExtPhrs", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                int TotalRecords = 0;

                

                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Case Advance Search","\"" +  EncryptDecryptHelper.Decrypt(SearchText) + "\"");
                return BindSearchResult("\"" +  EncryptDecryptHelper.Decrypt(SearchText) + "\"", ref dtG, ref TotalRecords, int.Parse(EncryptDecryptHelper.Decrypt(St)),
                    int.Parse(EncryptDecryptHelper.Decrypt(Et)), int.Parse(EncryptDecryptHelper.Decrypt(consume)), EncryptDecryptHelper.Decrypt(ip),
                    EncryptDecryptHelper.Decrypt(loc), ref Msg);
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "srchExtPhrs_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }

        [WebMethod]
        public DataTable srchExtMorePhrs(string SearchText1, string SearchText2, string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
               //string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call srchExtMorePhrs with parameter: SearchText 1," + EncryptDecryptHelper.Decrypt(SearchText1)+", SearchText 2," + EncryptDecryptHelper.Decrypt(SearchText2)+  ",St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "srchExtMorePhrs", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                int TotalRecords = 0;


                string keywordtxt = " \"" + EncryptDecryptHelper.Decrypt(SearchText1) + "\" and " + "\"" + EncryptDecryptHelper.Decrypt(SearchText2) + "\"";


                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Case Advance Search",keywordtxt);
                return BindSearchResult(keywordtxt, ref dtG, ref TotalRecords, int.Parse(EncryptDecryptHelper.Decrypt(St)),
                    int.Parse(EncryptDecryptHelper.Decrypt(Et)), int.Parse(EncryptDecryptHelper.Decrypt(consume)), EncryptDecryptHelper.Decrypt(ip),
                    EncryptDecryptHelper.Decrypt(loc), ref Msg);
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "srchExtMorePhrs_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        #endregion
        #region Search By Citation
        [WebMethod]
        public DataTable GJrnl(string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call GJrnl with parameter: St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "GJrnl", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Get Journals","Fetch Journals List for App");
                DataTable dt = new DataTable();
                dt = objJo.GetActiveJournals();
                return dt;

            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "GJrnl_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        [WebMethod]
        public DataTable SbCit(string yr,string jrnm,string jrid,string citnm, string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call SbCit with parameter: " + EncryptDecryptHelper.Decrypt(yr) + " - " + EncryptDecryptHelper.Decrypt(jrid) +" - "+ EncryptDecryptHelper.Decrypt(citnm) + " - " + ",location:" + EncryptDecryptHelper.Decrypt(ip) + " " + EncryptDecryptHelper.Decrypt(loc);
                WriteLogs(logtext, "SbCit", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                
                string cri = "Where Citation is not null and Publish=1";
                string forlog = "";

                if (!string.IsNullOrEmpty(EncryptDecryptHelper.Decrypt(yr)))
                {
                    forlog = forlog + " Citation Year: " + EncryptDecryptHelper.Decrypt(yr);
                    cri = cri + " AND Year='" + EncryptDecryptHelper.Decrypt(yr) + "'";
                }


                forlog = forlog + " Citation Journal: " + EncryptDecryptHelper.Decrypt(jrnm);
                cri = cri + " AND JournalID='" + EncryptDecryptHelper.Decrypt(jrid) + "'";


                if (!string.IsNullOrEmpty(EncryptDecryptHelper.Decrypt(citnm)))
                {
                    forlog = forlog + " Citation No: " + EncryptDecryptHelper.Decrypt(citnm);
                    cri = cri + " AND  (PageNo='" + EncryptDecryptHelper.Decrypt(citnm) + "')";
                    
                }

                DataTable dt = new DataTable();
                
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Search By Citation", forlog);
                dt = objcases.GetCasesSearch(cri, 0, 30);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Link");
                        dt.Columns.Add("Title");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {

                            dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                       //     dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                        }
                        dt.AcceptChanges();
                        return dt;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;

                }
                return null;
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "SbCit_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }

        #endregion
        #region Search By Section
        [WebMethod]
        public DataTable GStas(string stp,string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call GStas with parameter: St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "GStas", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Get Statutes Section", "Fetch Statutes List for App");
                DataTable dt = new DataTable();
                
                if (EncryptDecryptHelper.Decrypt(stp) == "PRIMARY")
                    dt = objstat.GetStatutesListBySection("PRIMARY");
                else if (EncryptDecryptHelper.Decrypt(stp) == "SECONDARY")
                    dt = objstat.GetStatutesListBySection("SECONDARY");

                return dt;

            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "GStas_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        [WebMethod]
        public ArrayList GSecls(string statd, string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call GSecls with parameter: St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "GSecls", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Get Statutes Section", "Fetch Statutes List for App");
                DataTable dt = new DataTable();

                string data = "";
                dt = objstat.GetTaggedSectionsByStatutes(int.Parse(EncryptDecryptHelper.Decrypt(statd.ToString())));
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    data = data + dt.Rows[a]["AttriLink"].ToString() + ",";

                }
                String[] lst = data.Split(',');
                var sList = new ArrayList();

                for (int i = 0; i < lst.Length; i++)
                {
                    if (lst[i].Contains("Order") || lst[i].Contains("Rule") || lst[i].Contains("Section") || lst[i].Contains("Article"))
                    {
                        if (!string.IsNullOrEmpty(lst[i]))
                        {
                            if (sList.Contains(lst[i].Trim()) == false)
                            {
                                sList.Add(lst[i].Trim());
                            }
                        }
                    }
                }
                sList.Sort();


                return sList;

            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "GSecls_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        
        [WebMethod]
        public DataTable GStasDT(string stpi, string stx, string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
               //string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string selectedsection = EncryptDecryptHelper.Decrypt(stx) ;
                
                if (!string.IsNullOrEmpty(selectedsection))
                {
                    selectedsection = "\"" + selectedsection + "\"";
                }

                string logtext = "Call GStasDT with parameter: St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "GStasDT", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Case Search By Section", "Statute: " + EncryptDecryptHelper.Decrypt(stpi) + " Section: " + EncryptDecryptHelper.Decrypt(stx));
                DataTable dt = new DataTable();
                dt = objstat.GetCasesSearchByStatueAndSection(int.Parse(EncryptDecryptHelper.Decrypt(stpi)), selectedsection);
              
                return dt;

            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "GStasDT_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        #endregion

        #region General Search
        [WebMethod]
        public string LoginU(string UserName, string Pwd, ref string Msg)
        {
          
            try
            {
                WriteLogs("Username: "+EncryptDecryptHelper.Decrypt(UserName) + " Pwd: "+Pwd, "Login","-");
                DataTable dt = new DataTable();
                string ret = "";
                string name="";
                int uid = UserLogin(EncryptDecryptHelper.Decrypt(UserName), Pwd, ref  name, ref Msg);
                if (uid > 1)
                {
                    ret = "<uid>"+EncryptDecryptHelper.Encrypt(uid.ToString())+"</uid><nm>"+EncryptDecryptHelper.Encrypt(name.ToString())+"</nm>";
                }
                else
                {
                    ret= "";
                }
                return ret;

            }
            catch (Exception ex)
            {

                Msg = ex.Message;
                WriteLogs(Msg, "Login_Failed", "");
                return "";
            }
        }
        [WebMethod]
        public void UsrA(string Org,string em,string pw,string nm,string mb, string cnt,string ct,string orgnm,string ad, ref string Msg)
        {

            try
            {
                if (ValidateEmail(EncryptDecryptHelper.Decrypt(em)))
                {
                    int chk = CheckEmailExist(EncryptDecryptHelper.Decrypt(em));
                    if (chk == 0)
                    {
                        chk = CheckPhoneNoExist(EncryptDecryptHelper.Decrypt(mb));
                        if (chk == 1)
                        {
                            Msg = "Mobile No. exist";
                        }
                        else
                        {
                            objUsr.OrgTypeID = int.Parse(EncryptDecryptHelper.Decrypt(Org));
                            objUsr.UserTypeID = 5;
                            objUsr.EmailID = EncryptDecryptHelper.Decrypt(em);
                            objUsr.Pwd = pw;
                            objUsr.FullName = EncryptDecryptHelper.Decrypt(nm);
                            objUsr.PhoneNo = EncryptDecryptHelper.Decrypt(mb);
                            objUsr.Country = EncryptDecryptHelper.Decrypt(cnt);
                            objUsr.CityID = int.Parse(EncryptDecryptHelper.Decrypt(ct));
                            objUsr.PlanID = 27;
                            objUsr.CompanyID = 0;
                            objUsr.CompanyName = EncryptDecryptHelper.Decrypt(orgnm);
                            objUsr.Verify = 0;
                            objUsr.Status = "Pending - Activation";
                            objUsr.Active = 0;
                            objUsr.CreatedBy = 0;
                            objUsr.NoOfPCAllowd = 0;// int.Parse(txtNoOfPCAllowed.Text.Trim());
                            objUsr.PostalAddress = EncryptDecryptHelper.Decrypt(ad);
                            objUsr.AccessExpireOn = DateTime.Now.AddDays(14).ToString("MM/dd/yyyy HH:MM:ss");
                            int chk1 = objUsr.InsertUser();
                            if (chk1 > 0)
                            {
                                string emailcnt = EmailContent(chk, EncryptDecryptHelper.Decrypt(nm));
                                objUsr.AddUserNotificationLog(chk, "New Registration Actification", emailcnt);
                                Email.SendMail(EncryptDecryptHelper.Decrypt(em), emailcnt, "Welcome to eastlaw.pk", "EastLaw", "");
                                Email.SendMail(ConfigurationManager.AppSettings["regEmailTransferToAbubakar"].ToString(), AdminEmailContent(EncryptDecryptHelper.Decrypt(nm), EncryptDecryptHelper.Decrypt(em), EncryptDecryptHelper.Decrypt(mb)), "Welcome to eastlaw.pk", "EastLaw", "");
                                Email.SendMail(ConfigurationManager.AppSettings["regEastLawTeam"].ToString(), AdminEmailContent(EncryptDecryptHelper.Decrypt(nm), EncryptDecryptHelper.Decrypt(em), EncryptDecryptHelper.Decrypt(mb)), "Welcome to eastlaw.pk", "EastLaw", "");
                                
                                Email.SendMail("staff1@eastlaw.pk", AdminEmailContent(EncryptDecryptHelper.Decrypt(nm), EncryptDecryptHelper.Decrypt(em), EncryptDecryptHelper.Decrypt(mb)), "Welcome to eastlaw.pk", "EastLaw", "");
                                SendWelcomeSMS(chk, EncryptDecryptHelper.Decrypt(nm), EncryptDecryptHelper.Decrypt(mb));
                                Msg = "Success";

                            }
                        }
                    }
                    else
                    {
                        Msg = "Email ID already exist";

                    }
                }
                else
                {
                    Msg = "Invalid Email ID";

                }



            }
            catch (Exception ex)
            {

                Msg = ex.Message;
                WriteLogs(Msg, "Registration_Failed", "");
              
            }
        }
        private bool ValidateEmail(string EmailID)
        {
           
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(EmailID);
            if (match.Success)
                return true;
            else
                return false;
        }
        //static bool IsValidEmail(this string email)
        //{
        //    string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        //    var regex = new Regex(pattern, RegexOptions.IgnoreCase);

        //    return regex.IsMatch(email);
        //}
        string EmailContent(int ID,string Name)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/NewRegistration.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }


                html = html.Replace("##FullName##", Name);
                html = html.Replace("##ClickHere##", "<a href='" + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/member-activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + "' target='_blank'>Click Here</a>");
                html = html.Replace("##FullLink##", ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/member-activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()));
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        string AdminEmailContent(string Name,string Email,string Mobile)
        {
            try
            {
                string file = "";


                string html = "";

                file = System.Web.HttpContext.Current.Server.MapPath("/EmailTemplates/NewRegistrationAdmin.html");
                StreamReader sr = new StreamReader(file);
                FileInfo fi = new FileInfo(file);

                if (System.IO.File.Exists(file))
                {
                    html += sr.ReadToEnd();
                    sr.Close();
                }



                html = html.Replace("##FullName##",Name);
                html = html.Replace("##USRNME##", Email);
                html = html.Replace("##MBLNM##", Mobile);
                // html = html.Replace("##Pwd##", Session["Pwd"].ToString());


                return html;
            }
            catch
            {
                return "";
            }



        }
        void SendWelcomeSMS(int ID,string Name,string MobNum)
        {
            try
            {
                string smstxt = "Dear " + Name +", Thank you for Registering at EastLaw.pk."
                + "Please check your email to activate your account or Click " + ConfigurationSettings.AppSettings["websiteUrl"].ToString() + "member/member-activation?uval=" + EncryptDecryptHelper.Encrypt(ID.ToString()) + " to activate it now.  Helpline#. 03-111-116-670";

                string mobilenumber = MobNum;
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
        int CheckPhoneNoExist(string PhoneNo)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckUserExistByPhone(PhoneNo);
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
        [WebMethod]
        public DataTable SRst(string SearchText, string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)

        {
         //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call SRst with parameter: SearchText," + EncryptDecryptHelper.Decrypt(SearchText) + ",St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "SRst", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG=new DataTable();
                int TotalRecords=0;
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "General Results", EncryptDecryptHelper.Decrypt(SearchText));
                return BindSearchResult(EncryptDecryptHelper.Decrypt(SearchText), ref dtG, ref TotalRecords, int.Parse(EncryptDecryptHelper.Decrypt(St)),
                    int.Parse(EncryptDecryptHelper.Decrypt(Et)), int.Parse(EncryptDecryptHelper.Decrypt(consume)), EncryptDecryptHelper.Decrypt(ip),
                    EncryptDecryptHelper.Decrypt(loc), ref Msg);
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "SRst_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        [WebMethod]
        public DataTable SRWNst(string SearchText, string SearchTextWthIn, string St, string Et, string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call SRWNst with parameter: SearchText," + EncryptDecryptHelper.Decrypt(SearchText) + " SearchWithin Text: " + EncryptDecryptHelper.Decrypt(SearchTextWthIn) + ",St:" + EncryptDecryptHelper.Decrypt(St) + ",Et:" + EncryptDecryptHelper.Decrypt(Et)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "SRWNst", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                int TotalRecords = 0;
                
                InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)), "Search", "Search Within", "Main Keyword: " + EncryptDecryptHelper.Decrypt(SearchText) + " SearchWithin Text: " + EncryptDecryptHelper.Decrypt(SearchTextWthIn));
                return BindSearchResultSearchWithin(EncryptDecryptHelper.Decrypt(SearchText), EncryptDecryptHelper.Decrypt(SearchTextWthIn), ref dtG, ref TotalRecords, int.Parse(EncryptDecryptHelper.Decrypt(St)),
                    int.Parse(EncryptDecryptHelper.Decrypt(Et)), int.Parse(EncryptDecryptHelper.Decrypt(consume)), EncryptDecryptHelper.Decrypt(ip),
                    EncryptDecryptHelper.Decrypt(loc), ref Msg);
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "SRWNst_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        [WebMethod]
        public DataTable stdt(string sti,  string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call stdt with parameter: sti," + EncryptDecryptHelper.Decrypt(sti) + ",location:" + EncryptDecryptHelper.Decrypt(ip) + " " + EncryptDecryptHelper.Decrypt(loc);
                WriteLogs(logtext, "stdt", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtLatest = new DataTable();
                if (CheckUserCaseAccessValidation(int.Parse(EncryptDecryptHelper.Decrypt(consume))) == true)
                {
                    dtLatest = objcases.GetCases(int.Parse(EncryptDecryptHelper.Decrypt(sti)));
                    //.Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", "")

                        if(dtLatest.Rows.Count>0)
                        {
                            dtLatest.Rows[0]["Judgment"] = dtLatest.Rows[0]["Judgment"].ToString().Replace("--", " ").Replace("##TS##", " ").Replace("##TE##", "").Replace("#TBS", "").Replace("#TBE", "");
                        }
                        dtLatest.AcceptChanges();
                    InsertAuditLog(int.Parse(EncryptDecryptHelper.Decrypt(consume)),"Case", EncryptDecryptHelper.Decrypt(sti), "");
                    return dtLatest;
                }
                else
                {
                    Msg = "Limit Exceeded";
                    return null;
                }
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "SRst_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        #endregion
        bool CheckUserCaseAccessValidation(int UserID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.UserElementViewReport((DateTime.Now.Date.ToString("MM/dd/yyy")), DateTime.Now.Date.ToString("MM/dd/yyy"), UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(dt.Rows[0]["noofcasesview_perday"].ToString()))
                        {
                            if (dt.Rows[0]["noofcasesview_perday"].ToString() != "0")
                            {
                                if (int.Parse(dt.Rows[0]["NoofCaseView"].ToString()) >= int.Parse(dt.Rows[0]["noofcasesview_perday"].ToString()))
                                {
                                    InsertAuditLog(UserID,"Limit Exceeded", "Case View", "User ID: " + UserID);
                                    return false;

                                }
                            }
                        }
                    }

                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "CheckUserAccessValidation", ex.Message);
            }
        }

         [WebMethod]
        public DataTable srlts(string consume, string acode, string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                if (objvaluser.UserValidate(int.Parse(EncryptDecryptHelper.Decrypt(consume)), ref Msg) == false)
                {
                    return null;
                }
                string logtext = "Call SRst "+ ",location:" + ip + " " + loc;
                WriteLogs(logtext, "srlts", EncryptDecryptHelper.Decrypt(consume));

                DataTable dtG = new DataTable();
                int TotalRecords = 0;
                DataTable dtLatest = new DataTable();
                dtLatest=  objcases.GetLatestCasesFront();
                return dtLatest;
                
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "srlts_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
        [WebMethod]
        public DataTable srltsRS(string ip, string loc, ref string Msg)
        {
            //   string Msg="";
            try
            {
                
                string logtext = "Call SRst " + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "srltsRS","Public");

                DataTable dtG = new DataTable();
                int TotalRecords = 0;
                DataTable dtLatest = new DataTable();
                dtLatest = objcases.GetLatestCasesFront_Public();
                return dtLatest;

            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "srltsRSs_Error", "Public");
                Msg = ex.Message;
                return null;
            }
        }
        DataTable BindSearchResult(string SearchText, ref DataTable dtGroup,ref int TotalRecords, int startRowIndex, int maximumRows, int UserID, string IP, string Loc, ref string errMsg)
        {
            try
            {

                string keyword = CommonClass.FormatSearchWord(SearchText);
              TotalRecords=  GetTotalRecords(keyword);
         
                DataTable dt = new DataTable();
               
                dt = objkeyword.GetSearchResultsByKeywordTest(keyword, startRowIndex, maximumRows);
                string keyw = SearchText;
           
                int chk = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Title");
                      //  dt.Columns.Add("Link");
                        dt.Columns.Add("OtherContent");
                        dt.Columns.Add("Desc");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                            {
                                dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetChracter(dt.Rows[a]["Appeallant"].ToString(), 25)) + "... VS " + EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetChracter(dt.Rows[a]["Respondent"].ToString(), 25).ToLower() + "...");
                               // dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                string Desc = "";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";
                               

                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                dt.Rows[a]["OtherContent"] = OtherCont;

                                dt.Rows[a]["Desc"] = Desc + EastlawUI_v2.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keyword).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "").Replace("##TS##", " ").Replace("##TE## ", " ");

                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                            {
                               
                                dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                               // dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                //string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() +"<br/>";
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                    OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

                                dt.Rows[a]["OtherContent"] = OtherCont;
                                //dt.Rows[a]["OtherContent"] = OtherCont;
                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                            {
                                dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
                                //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

                                dt.Rows[a]["OtherContent"] = OtherCont;

                            }

                        }
                        dt.AcceptChanges();

                        ///
                        dt = ReturnRemoveUserSearchPara(dt);

                        //Session["sesKeywordResults"] = dt;
                      
                    
                            chk = objsrc.InsertSearchText(SearchText, 1,IP,UserID, TotalRecords,"Mobile API Main Search");
                     
                     

                         dtGroup = objkeyword.GetSearchResultsByKeywordGrouping(keyword);
                         dtGroup = ReturnRemoveUserSearchPara(dtGroup);

                        #region Content Type Group
                        var queryContentType = from row in dtGroup.AsEnumerable()
                                               group row by row.Field<string>("ResultType") into ResultType
                                               orderby ResultType.Key
                                               select new
                                               {
                                                   Name = ResultType.Key.ToString() + " (" + ResultType.Count() + ")",
                                                   count = ResultType.Count(),
                                                   valfiel = ResultType.Key.ToString()
                                               };

                        #endregion

                        #region Court Group
                        var query = from row in dtGroup.AsEnumerable()
                                    where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
                                    group row by row.Field<string>("Court") into courts
                                    orderby courts.Key
                                    select new
                                    {
                                        Name = courts.Key.Trim().ToString() + " (" + courts.Count() + ")",
                                        count = courts.Count(),
                                        valfiel = courts.Key.Trim().ToString()
                                    };

                      
                        #endregion

                        #region Year Group
                        var queryYear = from row in dtGroup.AsEnumerable()
                                        // where row.Field<string>("Year") > 200
                                        group row by row.Field<string>("Year") into YEARS
                                        orderby YEARS.Key

                                        select new
                                        {
                                            Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                            count = YEARS.Count(),
                                            valfiel = YEARS.Key
                                        };

                        //chkLstYear.DataSource = queryYear;
                        //chkLstYear.DataValueField = "valfiel";
                        //chkLstYear.DataTextField = "Name";
                        //chkLstYear.DataBind();

                        //if (queryYear.Count() == 0)
                        //{
                        //    AccordionPaneYears.Visible = false;
                        //}
                        //else
                        //{
                        // AccordionPaneYears.Visible = true;


                     
                        // }
                        #endregion
                        dt.Columns.Remove("Judgment");
                        dt.AcceptChanges();
                        return dt;

                    }
                    else
                    {
                       
                       
                            chk = objsrc.InsertSearchText(SearchText, 0, IP, UserID, TotalRecords, "Mobile API Main Search");
                            return null;
                       
                    }
                }
                else
                {

                    chk = objsrc.InsertSearchText(SearchText, 0, IP, UserID, TotalRecords, "Mobile API Main Search");
                    return null;
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
        DataTable BindSearchResultSearchWithin(string SearchText,string SearchWithin, ref DataTable dtGroup, ref int TotalRecords, int startRowIndex, int maximumRows, int UserID, string IP, string Loc, ref string errMsg)
        {
            try
            {

                string keyword = CommonClass.FormatSearchWord(SearchText);
                string keywordWithIN = CommonClass.FormatSearchWordWithin(SearchWithin);
                TotalRecords = GetTotalRecordsWithIn(keyword,keywordWithIN);

                DataTable dt = new DataTable();

                dt = objkeyword.GetSearchWithinResultsByKeywordTest(keyword, keywordWithIN, startRowIndex, maximumRows);
                string keyw = SearchText;

                int chk = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Title");
                        //  dt.Columns.Add("Link");
                        dt.Columns.Add("OtherContent");
                        dt.Columns.Add("Desc");
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["ResultType"].ToString() == "Cases")
                            {
                                dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetChracter(dt.Rows[a]["Appeallant"].ToString(), 25)) + "... VS " + EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetChracter(dt.Rows[a]["Respondent"].ToString(), 25).ToLower() + "...");
                                // dt.Rows[a]["Link"] = "/cases/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "VS" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-").ToLower() + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                                string OtherCont = "<strong>Where Reported:</strong>";
                                string Desc = "";
                                if (!string.IsNullOrEmpty(dt.Rows[a]["CitationRef"].ToString()))
                                    OtherCont = OtherCont + dt.Rows[a]["CitationRef"].ToString() + "<br />";
                                else
                                    OtherCont = OtherCont + dt.Rows[a]["Citation"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Date:</strong>" + dt.Rows[a]["JDate"].ToString() + "<br />";
                                OtherCont = OtherCont + "<strong>Court:</strong>" + dt.Rows[a]["Court"].ToString() + " <br /> ";


                                if (!string.IsNullOrEmpty(dt.Rows[a]["Result"].ToString()))
                                    OtherCont = OtherCont + "<strong>Result: </strong>" + dt.Rows[a]["Result"].ToString() + "<br /><br />";
                                dt.Rows[a]["OtherContent"] = OtherCont;

                                dt.Rows[a]["Desc"] = Desc + EastlawUI_v2.CommonClass.GetShortDesc(dt.Rows[a]["Judgment"].ToString(), keyword).Replace("</p>", "").Replace("<p>", "").Replace("<br>", "").Replace("<b>", "").Replace("</b>", "").Replace("##TS##", " ").Replace("##TE## ", " ");

                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Statutes")
                            {

                                dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString());
                                // dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                //string OtherCont = "Index:" + dt.Rows[a]["Citation"].ToString() +"<br/>";
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();
                                if (!string.IsNullOrEmpty(dt.Rows[a]["Respondent"].ToString()))
                                    OtherCont = OtherCont + " | " + dt.Rows[a]["Respondent"].ToString() + "<br />";

                                dt.Rows[a]["OtherContent"] = OtherCont;
                                //dt.Rows[a]["OtherContent"] = OtherCont;
                            }
                            else if (dt.Rows[a]["ResultType"].ToString() == "Dictionary")
                            {
                                dt.Rows[a]["Title"] = EastlawUI_v2.CommonClass.MakeFirstCap(dt.Rows[a]["Year"].ToString());
                                //dt.Rows[a]["Link"] = "/Statutes/" + clsUtilities.RemoveSpecialCharacter(EastlawUI_v2.CommonClass.MakeFirstCap(EastlawUI_v2.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());
                                string OtherCont = "<br/>";
                                OtherCont = OtherCont + dt.Rows[a]["JDate"].ToString();

                                dt.Rows[a]["OtherContent"] = OtherCont;

                            }

                        }
                        dt.AcceptChanges();

                        ///
                        dt = ReturnRemoveUserSearchPara(dt);

                        //Session["sesKeywordResults"] = dt;


                        chk = objsrc.InsertSearchText(SearchText, 1, IP, UserID, TotalRecords, "Mobile API Main Search Within");



                        dtGroup = objkeyword.GetSearchResultsByKeywordGrouping(keyword);
                        dtGroup = ReturnRemoveUserSearchPara(dtGroup);

                        #region Content Type Group
                        var queryContentType = from row in dtGroup.AsEnumerable()
                                               group row by row.Field<string>("ResultType") into ResultType
                                               orderby ResultType.Key
                                               select new
                                               {
                                                   Name = ResultType.Key.ToString() + " (" + ResultType.Count() + ")",
                                                   count = ResultType.Count(),
                                                   valfiel = ResultType.Key.ToString()
                                               };

                        #endregion

                        #region Court Group
                        var query = from row in dtGroup.AsEnumerable()
                                    where !(row.Field<string>("Court") == null || row.Field<string>("Court") == "")
                                    group row by row.Field<string>("Court") into courts
                                    orderby courts.Key
                                    select new
                                    {
                                        Name = courts.Key.Trim().ToString() + " (" + courts.Count() + ")",
                                        count = courts.Count(),
                                        valfiel = courts.Key.Trim().ToString()
                                    };


                        #endregion

                        #region Year Group
                        var queryYear = from row in dtGroup.AsEnumerable()
                                        // where row.Field<string>("Year") > 200
                                        group row by row.Field<string>("Year") into YEARS
                                        orderby YEARS.Key

                                        select new
                                        {
                                            Name = YEARS.Key + " (" + YEARS.Count() + ")",
                                            count = YEARS.Count(),
                                            valfiel = YEARS.Key
                                        };

                      
                        #endregion
                        dt.Columns.Remove("Judgment");
                        dt.AcceptChanges();
                        return dt;

                    }
                    else
                    {


                        chk = objsrc.InsertSearchText(SearchWithin, 0, IP, UserID, TotalRecords, "Mobile API Main Search Within");
                        return null;

                    }
                }
                else
                {

                    chk = objsrc.InsertSearchText(SearchWithin, 0, IP, UserID, TotalRecords, "Mobile API Main Search Within");
                    return null;
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
        int GetTotalRecordsWithIn(string keyWords, string SearchWithin)
        {
            string keyword = keyWords;
            if (keyword.Contains(" "))
            {
                if (!ExactMatch(keyword, "and"))
                {
                    //if (!ExactMatch(keyword, "AND"))
                    //{
                    if (!keyword.Contains("\""))
                    {
                        keyword = "\"" + keyword + "\"";
                        // keyword.Replace(" ", "and");
                    }
                    // }
                }
            }
            DataTable dt = objkeyword.GetSearchWithInResultsByKeywordCountTest(keyword, SearchWithin);
            if (dt != null)
            {
                dt = ReturnRemoveUserSearchParaCount(dt);
                // gvLst.VirtualItemCount = rcordcount;
                object sumObject;
                sumObject = dt.Compute("Sum(TotalRows)", "");
                return int.Parse(sumObject.ToString());


               

            }
            return 0;
        }
        static bool ExactMatch(string input, string match)
        {

            return Regex.IsMatch(input, string.Format(@"\b{0}\b", Regex.Escape(match)), RegexOptions.IgnoreCase);

        }
        int GetTotalRecords(string SearchText)
        {
            DataTable dt = objkeyword.GetSearchResultsByKeywordCountTest(SearchText);

            if (dt != null)
            {
                dt = ReturnRemoveUserSearchParaCount(dt);
                // Declare an object variable.
                object sumObject;
                sumObject = dt.Compute("Sum(TotalRows)", "");
                return  int.Parse(sumObject.ToString());

            }
            return 0;
        }
        DataTable ReturnRemoveUserSearchParaCount(DataTable dt)
        {
            try
            {
              
                        //if (string.IsNullOrEmpty(membersearch[0]))
                        //{
                        //    dt.Rows[0].Delete();

                        //    dt.AcceptChanges();
                        //}
                        //if (string.IsNullOrEmpty(membersearch[1]))
                        //{
                            dt.Rows[1].Delete();
                            dt.AcceptChanges();
                        //}

                
                
            }
            catch { }
            return dt;
        }
        DataTable ReturnRemoveUserSearchPara(DataTable dt)
        {
            try
            {

                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (dt.Rows[a]["ResultType"].ToString() != "Cases")
                    {
                        dt.Rows[a].Delete();
                    }
                }
                dt.AcceptChanges();


            }
            catch { }
            return dt;
        }
       
        #region Users
        int UserLogin(string EmailID, string Pwd,ref string Name,ref string Msg)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.CheckLogin(EmailID, Pwd);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        if (dt.Rows[0]["verify"].ToString() == "0")
                        {
                         //   InsertAuditLog("Login/Logout", "Login Failed, Account is not verified." + EmailID, "");
                         
                            Msg = "Account is not Activated, Please check email for activation or Call Helpline# 042-37311670 / 71";
                            return 0;
                        }
                        else if (dt.Rows[0]["Status"].ToString() == "Expired")
                        {
                           // InsertAuditLog("Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + EmailID, "");


                            Msg = "Account is Expired, Please check email for activation or Call Helpline# 042-37311670 / 71";
                            return 0;
                        }
                        else if (dt.Rows[0]["Status"].ToString() != "Approved")
                        {
                            //InsertAuditLog("Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + EmailID, "");

                            Msg = "Account is not Activated, Please check email for activation or Call Helpline# 042-37311670 / 71";
                            return 0;
                        }
                        //else if (DateTime.Parse(dt.Rows[0]["AccessExpireOn"].ToString("dd/mm/yyyy")) < DateTime.Parse(DateTime.Now.Date.ToString("dd/mm/yyyy")))
                        else if (DateTime.Parse(dt.Rows[0]["FormatedExpire"].ToString()) < DateTime.Now.Date)
                        {
                            //InsertAuditLog("Login/Logout", "Login Failed, Account Expired on  " + dt.Rows[0]["AccessExpireOn"].ToString() + " Email ID:" + EmailID, "");

                            Msg = "Account Expired, Please email us or Call Helpline# 042-37311670 / 71";
                           
                            return 0;
                        }
                        else
                        {
                            //if (Login(txtEmailIDLogin.Text.Trim()))
                            //{
                            if (CheckUserAccessValidation(int.Parse(dt.Rows[0]["ID"].ToString()), dt.Rows[0]["EmailID"].ToString()))
                            {
                                if (CheckUserAccessTimeValidation(int.Parse(dt.Rows[0]["PlanID"].ToString()), dt.Rows[0]["EmailID"].ToString()))
                                {

                                    if (dt.Rows[0]["UserTypeID"].ToString() != "4")
                                    {
                                        //InsertAuditLog("Login/Logout", "Login Success " + EmailID, "");
                                        Name = dt.Rows[0]["FullName"].ToString();
                                        return int.Parse(dt.Rows[0]["ID"].ToString());
                                        
                                    }

                                }
                                else
                                {
                                    Msg = "Sign-in limited for complimentary package users. Please Sign-in after 7:00 am";
                                    return 0;
                                }
                            }
                            else
                            {
                                Msg = "Limit Exceed";
                                return 0;
                            }
                           
                        }


                    }
                    else
                    {
                        Msg = "Email ID or Password is incorrect.";
                        
                    }

                }
                else
                {
                   // InsertAuditLog("Login/Logout", "Login Failed" + EmailID, "");
                    
                    Msg = "Email ID or Password is incorrect.";
                    
                }
                return 0;

            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return 0;
                ExceptionHandling.SendErrorReport("Home/Register.aspx", "UserLogin", ex.Message);
            }
        }
        bool CheckUserAccessValidation(int UserID, string EmailID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.UserElementViewReport((DateTime.Now.Date.ToString("MM/dd/yyy")), DateTime.Now.Date.ToString("MM/dd/yyy"), UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(dt.Rows[0]["nooflogin_perday"].ToString()))
                        {
                            if (dt.Rows[0]["nooflogin_perday"].ToString() != "0")
                            {
                                if (int.Parse(dt.Rows[0]["NoOfLogin"].ToString()) >= int.Parse(dt.Rows[0]["nooflogin_perday"].ToString()))
                                {

                                    //InsertAuditLog("Limit Exceeded", "Login", "Email ID: " + EmailID);

                                    
                                    return false;

                                }
                            }
                        }
                    }

                }
                return true;


            }
            catch (Exception ex)
            {
                return false;
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "CheckUserAccessValidation", ex.Message);
            }
        }
        bool CheckUserAccessTimeValidation(int PlanID, string EmailID)
        {
            try
            {
                if (PlanID == 1 || PlanID == 18 || PlanID == 8 || PlanID == 25 || PlanID == 27)
                {
                    TimeSpan start = new TimeSpan(00, 30, 0); //10 o'clock
                    TimeSpan end = new TimeSpan(07, 0, 0); //12 o'clock
                    TimeSpan now = DateTime.Now.TimeOfDay;

                    if ((now > start) && (now < end))
                    {
                       // InsertAuditLog("Non Working Hours", "Login", "Email ID: " + EmailID);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
                ExceptionHandling.SendErrorReport("Home/Registration.aspx", "CheckUserAccessValidation", ex.Message);
            }
        }
        #endregion
        public void WriteLogs(string msg, string filename, string usrid)
        {
            try
            {
                string FolderName = DateTime.Now.ToString("MMyyyy");
                string day = DateTime.Now.ToString("dd");

                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + ""));


                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + usrid + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + usrid + ""));

                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + usrid + "/" + day + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + usrid + "/" + day + ""));

                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/" + FolderName + "/" + usrid + "/" + day + "/" + filename + ".txt"), true);
                //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
                sw.WriteLine("------------------------------------" + DateTime.Now + "-------------------");
                sw.WriteLine(msg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apilogs/LoggingError" + ".txt"), true);
                //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
                sw.WriteLine("------------------- Error In Loging eroor -------------------" + DateTime.Now.AddMinutes(28) + "-------------------");
                sw.WriteLine(ex.Message);
                sw.Flush();
                sw.Close();
            }
        }
        void InsertAuditLog(int UserID,string  ActType, string Action, string txt)
        {
            try
            {
                EastLawBL.Users objusr = new EastLawBL.Users();
                string Country = "";
                string Region = "";
                string City = "";
                //string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //if (String.IsNullOrEmpty(visitorIPAddress))
                //    visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                //if (string.IsNullOrEmpty(visitorIPAddress))
                //    visitorIPAddress = Request.UserHostAddress;

                int chk = 0;
               // EastlawUI_v2.CommonClass.GetIPLocation(visitorIPAddress, ref Country, ref Region, ref City);
                //Location location = new Location();
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

                string BrowserName = "";
                string SourcePlatform = "";
                //try
                //{
                //    System.Web.HttpBrowserCapabilities browser = Request.Browser;
                //    BrowserName = browser.Browser.ToString();
                //    SourcePlatform = browser.Platform.ToString();
                //}
                //catch { }


                    chk = objusr.InsertAuditLog(ActType, Action, "", "", UserID, Country, Region, City, txt, BrowserName, SourcePlatform, "Mobile App");
                
            }
            catch (Exception e)
            {
                ExceptionHandling.SendErrorReport("home/Default.aspx", "InsertAuditLog", e.Message);
            }
        }
    }
}
