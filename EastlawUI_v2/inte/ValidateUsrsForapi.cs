using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace EastlawUI_v2.inte
{
    public class ValidateUsrsForapi
    {
        EastLawBL.Users objUsr = new EastLawBL.Users();
       public bool UserValidate(int UserID,ref string strMessage)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objUsr.GetUsers(UserID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        if (dt.Rows[0]["verify"].ToString() == "0")
                        {
                            InsertAuditLog(int.Parse(dt.Rows[0]["ID"].ToString()), "Login/Logout", "Login Failed, Account is not verified." + dt.Rows[0]["EmailID"].ToString(), "");
                            strMessage = "Account is not Activated, Please check email for activation or Call Helpline# 03-111-116-670";
                            return false;
                        }
                        else if (dt.Rows[0]["Status"].ToString() == "Expired")
                        {
                            InsertAuditLog(int.Parse(dt.Rows[0]["ID"].ToString()), "Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + dt.Rows[0]["EmailID"].ToString(), "");

                            strMessage = "Account is Expired, Please check email for activation or Call Helpline# 042-37311670 / 71";
                            return false;
                        }
                        else if (dt.Rows[0]["Status"].ToString() != "Approved")
                        {
                            InsertAuditLog(int.Parse(dt.Rows[0]["ID"].ToString()), "Login/Logout", "Login Failed, Account Locked with status of " + dt.Rows[0]["Status"].ToString() + " Email ID:" + dt.Rows[0]["EmailID"].ToString(), "");
                            strMessage = "Account is not Activated, Please check email for activation or Call Helpline# 042-37311670 / 71";
                            return false ;
                        }
                        //else if (DateTime.Parse(dt.Rows[0]["AccessExpireOn"].ToString("dd/mm/yyyy")) < DateTime.Parse(DateTime.Now.Date.ToString("dd/mm/yyyy")))
                        else if (DateTime.Parse(dt.Rows[0]["FormatedExpire"].ToString()) < DateTime.Now.Date)
                        {
                            InsertAuditLog(int.Parse(dt.Rows[0]["ID"].ToString()), "Login/Logout", "Login Failed, Account Expired on  " + dt.Rows[0]["AccessExpireOn"].ToString() + " Email ID:" + dt.Rows[0]["EmailID"].ToString(), "");
                            strMessage = "Account Expired, Please email us or Call Helpline# 042-37311670 / 71";
                            return false;
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

                                        InsertAuditLog(int.Parse(dt.Rows[0]["ID"].ToString()), "Login/Logout", "Login Success " + dt.Rows[0]["EmailID"].ToString(), "");

                                        return true;
                                    }
                                }

                                else
                                {
                                    strMessage = "Sign-in limited for complimentary package users. Please Sign-in after 7:00 am";
                                    return false;
                                }
                            }
                            //if (Request.UrlReferrer != null)
                            //{

                            //    if (ViewState["LastPage"] != null)
                            //    {
                            //        Uri uri = new Uri(ViewState["LastPage"].ToString());
                            //        string filename = System.IO.Path.GetFileName(uri.AbsolutePath);
                            //        if (filename == "Member-Activation")
                            //            Response.Redirect("/Member/Member-Dashboard");
                            //        else
                            //            Response.Redirect(ViewState["LastPage"].ToString());
                            //    }
                            //    else
                            //       Response.Redirect("/Member/Member-Dashboard");

                            //}
                            //else
                            //{
                            //    Response.Redirect("/Member/Member-Dashboard");
                            //}
                            //}
                            //else
                            //{
                            //    InsertAuditLog("Login/Logout", "Multi Access Try " + txtEmailIDLogin.Text, "This User is already login, kindly logout first");
                            //    lblMsg.Text = "This User is already login, kindly logout first";
                            //    lblMsg.Visible = true;
                            //    return;
                            //}
                        }


                    }
                    else
                    {
                        InsertAuditLog(0, "Login/Logout", "Login Failed ", "");
                      
                        strMessage = "Email ID or Password is incorrect.";
                        return false;
                    }

                }
                else
                {
                    InsertAuditLog(0, "Login/Logout", "Login Failed", "");
                   
                    strMessage = "Email ID or Password is incorrect.";
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message;
                return false;
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

                                    InsertAuditLog(0,"Limit Exceeded", "Login", "Email ID: " + EmailID);

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
                        InsertAuditLog(0,"Non Working Hours", "Login", "Email ID: " + EmailID);
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
        void InsertAuditLog(int UserID, string ActType, string Action, string txt)
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