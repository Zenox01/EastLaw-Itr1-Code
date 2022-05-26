using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;


namespace EastlawUI_v2.adminpanel.newsletter
{
    public class NewsletterHTMLGenerator
    {
      public string GenerateNewsletter(int Template,int NewsletterID)
        {
            try
            {
                if (Template == 1)
                    return Template01(NewsletterID);
                else if (Template == 2)
                    return Template02(NewsletterID);

                return "";
            }
            catch {
                return "";
            }
        }
      string Template01(int NewsletterID)
      {
          try
          {
              string html = "";
              EastLawBL.Newsletter objn = new EastLawBL.Newsletter();
              string Cases = "";
              string Dept = "";
              string Statutes = "";
              string file = "";




              file = System.Web.HttpContext.Current.Server.MapPath("/adminpanel/newsletter/templates/template1/template01_processing.html");
              StreamReader sr = new StreamReader(file);
              FileInfo fi = new FileInfo(file);

              if (System.IO.File.Exists(file))
              {
                  html += sr.ReadToEnd();
                  sr.Close();
              }

              //DataTable dtCampaign = new DataTable();
              //dtCampaign = objems.GetPendingCampaign();

              //for (int b = 0; b < dtCampaign.Rows.Count; b++)
              //{
              //    if (dtCampaign.Rows.Count > 0)
              //    {
              //        if (dtCampaign.Rows[b]["TemplateID"].ToString() == "1")
              //        {

              DataTable dtNewsletter = new DataTable();
              dtNewsletter = objn.GetNewsletter(NewsletterID);


              //<!-- product table starts here -->"
              DataTable dtItemCases = new DataTable();
              dtItemCases = objn.GetNewsletterItems(NewsletterID, "Case");

              if (dtItemCases.Rows.Count > 0)
              {

                  //int chk = objems.UpdateCampaignStatus(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), "In-Progress");
                  for (int a = 0; a < dtItemCases.Rows.Count; a++)
                  {
                      if (a + 2 < dtItemCases.Rows.Count)
                      {
                          Cases = Cases + "<tr bgcolor='#FFFFFF' style='padding: 30px 0;display: table-cell;'>";

                          Cases = Cases + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemCases.Rows[a]["Title"].ToString() + "</h2>"
                          + "<p style='margin:0;min-height:100px;max-height:100px'>" + CommonClass.GetWords(dtItemCases.Rows[a]["ShortText"].ToString(), 15) + "... </p>"
                          + "<a href='https://eastlaw.pk/cases/" + dtItemCases.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemCases.Rows[a]["ItemID"].ToString()) + "' target='_blank' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Cases = Cases + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemCases.Rows[a + 1]["Title"].ToString() + "</h2>"
                          + "<p style='margin:0;min-height:100px;max-height:100px'>" + CommonClass.GetWords(dtItemCases.Rows[a + 1]["ShortText"].ToString(), 15) + "... </p>"
                          + "<a href='https://eastlaw.pk/cases/" + dtItemCases.Rows[a + 1]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemCases.Rows[a + 1]["ItemID"].ToString()) + "' target='_blank' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Cases = Cases + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemCases.Rows[a + 2]["Title"].ToString() + "</h2>"
                         + "<p style='margin:0;min-height:100px;max-height:100px'>" + CommonClass.GetWords(dtItemCases.Rows[a + 2]["ShortText"].ToString(), 15) + "... </p>"
                          + "<a href='https://eastlaw.pk/cases/" + dtItemCases.Rows[a + 2]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemCases.Rows[a + 2]["ItemID"].ToString()) + "' target='_blank' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";


                          Cases = Cases + " </tr>";
                      }
                      a = a + 2;

                  }



              }

              //Statutes
              DataTable dtItemStatutes = new DataTable();
              dtItemStatutes = objn.GetNewsletterItems(NewsletterID, "Statutes");

              if (dtItemStatutes.Rows.Count > 0)
              {

                  //int chk = objems.UpdateCampaignStatus(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), "In-Progress");
                  for (int a = 0; a < dtItemStatutes.Rows.Count; a++)
                  {
                      if (a + 2 < dtItemStatutes.Rows.Count)
                      {
                          Statutes = Statutes + "<tr bgcolor='#FFFFFF' style='padding: 30px 0;display: table-cell;'>";

                          Statutes = Statutes + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemStatutes.Rows[a]["Title"].ToString() + "</h2>"
                           + "<p style='margin:0;'>" + CommonClass.GetWords(dtItemStatutes.Rows[a]["ShortText"].ToString(), 15) + ".... </p>"
                          + "<a href='https://eastlaw.pk/statutes/" + dtItemStatutes.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemStatutes.Rows[a]["ItemID"].ToString()) + "' target='_blank' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Statutes = Statutes + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemStatutes.Rows[a + 1]["Title"].ToString() + "</h2>"
                              + "<p style='margin:0;'>" + CommonClass.GetWords(dtItemStatutes.Rows[a + 1]["ShortText"].ToString(), 15) + ".... </p>"
                          + "<a href='https://eastlaw.pk/statutes/" + dtItemStatutes.Rows[a + 1]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemStatutes.Rows[a + 1]["ItemID"].ToString()) + "' target='_blank' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Statutes = Statutes + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemStatutes.Rows[a + 2]["Title"].ToString() + "</h2>"
                              + "<p style='margin:0;'>" + CommonClass.GetWords(dtItemStatutes.Rows[a + 2]["ShortText"].ToString(), 15) + ".... </p>"
                          + "<a href='https://eastlaw.pk/statutes/" + dtItemStatutes.Rows[a + 2]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemStatutes.Rows[a + 2]["ItemID"].ToString()) + "' target='_blank' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Statutes = Statutes + " </tr>";
                      }
                      a = a + 2;

                  }

              }


              //Dept
              DataTable dtItemDept = new DataTable();
              dtItemDept = objn.GetNewsletterItems(NewsletterID, "Dept");

              if (dtItemDept.Rows.Count > 0)
              {

                  //int chk = objems.UpdateCampaignStatus(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), "In-Progress");
                  for (int a = 0; a < dtItemDept.Rows.Count; a++)
                  {
                      if (a + 2 < dtItemDept.Rows.Count)
                      {
                          Dept = Dept + "<tr bgcolor='#FFFFFF' style='padding: 30px 0;display: table-cell;'>";

                          Dept = Dept + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemDept.Rows[a]["Title"].ToString() + "/h2>"
                              + "<p style='margin:0;'>" + CommonClass.GetWords(dtItemDept.Rows[a]["ShortText"].ToString(), 15) + ".... </p>"
                          + "<a href='#' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Dept = Dept + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemDept.Rows[a]["Title"].ToString() + "</h2>"
                              + "<p style='margin:0;'>" + CommonClass.GetWords(dtItemDept.Rows[a + 1]["ShortText"].ToString(), 15) + ".... </p>"
                          + "<a href='#' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Dept = Dept + "<td width='195' style='margin-left:3px;float:left;text-align:center;'>"
                              //+"<img src='images/img_1.png' style='width:100%;'/>"
                          + "<h2 style='color:#d11515;font-size:20px;    margin: 7px 0 11px 0;'>" + dtItemDept.Rows[a]["Title"].ToString() + "</h2>"
                              + "<p style='margin:0;'>" + CommonClass.GetWords(dtItemDept.Rows[a + 2]["ShortText"].ToString(), 15) + ".... </p>"
                          + "<a href='#' style='display:inline-block;padding:5px 13px; color:#fff; text-decoration:none;background:#c11313;    font-size: 13px;margin-top: 15px;border-radius: 4px;'>Read More</a></td>";

                          Dept = Dept + " </tr>";
                      }
                      a = a + 2;

                  }

              }


              // }
              //return Content;


              if (dtNewsletter != null)
              {
                  if (dtNewsletter.Rows.Count > 0)
                  {
                      //html = html.Replace("##BNRFILE##", "<img src='/adminpanel/newsletter/banners/" + dtNewsletter.Rows[0]["NewsletterBanner"].ToString() + "' style='width:100%;' /></td>");
                      html = html.Replace("##BNRFILE##", "<img src='" + HttpContext.Current.Server.MapPath("/adminpanel/newsletter/banners/") + dtNewsletter.Rows[0]["NewsletterBanner"].ToString() + "' style='width: 100 %; ' /></td>");
                  }
              }

              html = html.Replace("##LATESTJUDGMENT##", Cases);
              html = html.Replace("##TOPSTATUTES##", Statutes);
              html = html.Replace("##DEPT##", Dept);
              html = html.Replace("##TOPPRACTICEAREA##", "");

              //  Email objEmail = new Email();
              DataTable dtEmails = new DataTable();
              //dtEmails = com.GetNewsletter();
              //if (dtEmails.Rows.Count > 0)
              //{
              //    int chk1=0;
              //    for (int a = 0; a < dtEmails.Rows.Count; a++)
              //    {
              //        if (!string.IsNullOrEmpty(dtEmails.Rows[a]["EmailID"].ToString()))
              //        {
              //            DataTable dtchkExist = objems.GetEmailCampaignSentOrNot(int.Parse(dtCampaign.Rows[b]["ID"].ToString()),dtEmails.Rows[a]["EmailID"].ToString());
              //            if(dtchkExist.Rows.Count == 0)
              //            {
              //      Content = Content + "<table width='800' border='0' cellspacing='1' cellpadding='0'>"
              // + "<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333; text-align:center; background-color:#FFF'>You can <a href='http://fabbyshop.com/unsubscribe.aspx?camp=" + dtCampaign.Rows[b]["ID"].ToString() + "&email=" + dtEmails.Rows[a]["EmailID"].ToString() + "'> unsubscribe this newsletter at any time.<br />"
              //+ "We respect your personal information and we will not share them with any other parties.<br />For further details , please review our terms and conditions.</td></tr></table>";
              //            int EChk = objEmail.SendMail(dtEmails.Rows[a]["EmailID"].ToString(), "", AddTracking(Content, dtEmails.Rows[a]["EmailID"].ToString(),dtCampaign.Rows[b]["ID"].ToString(), "fabbyshop.com"), dtCampaign.Rows[b]["Subject"].ToString(), "fabbyshop.com");

              //            if (EChk == 1)
              //               chk1 = objems.AddSentEmailCampaign(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), dtEmails.Rows[a]["EmailID"].ToString(), 1, 0);
              //            }
              //        }

              //    }

              //}
              //      Content = Content + "<table width='800' border='0' cellspacing='1' cellpadding='0'>"
              // + "<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333; text-align:center; background-color:#FFF'>You can unsubscribe this newsletter at any time <a href='#'> Click Here</a>.<br />"
              //+ "We respect your personal information and we will not share them with any other parties.<br />For further details , please review our terms and conditions.</td></tr></table>";
              //int EChk = 0;
              // EChk = objEmail.SendMail("umar.mughal83@gmail.com", "", Content + Footer("umar.mughal83@gmail.com"), "Testing ... " + dtItems.Rows[0]["Subject"].ToString(), "fabbyshop.com");
              return html;







              //    }
              //}

              //return "";
          }
          catch
          {
              return "";
          }
      }
      string Template02(int NewsletterID)
      {
          try
          {
              string html = "";
              EastLawBL.Newsletter objn = new EastLawBL.Newsletter();
              string Cases = "";
              string Dept = "";
              string Statutes = "";
              string file = "";
              string Title = "";
              string strNews = "";




              file = System.Web.HttpContext.Current.Server.MapPath("/adminpanel/newsletter/templates/template2/template02_processing.html");
              StreamReader sr = new StreamReader(file);
              FileInfo fi = new FileInfo(file);

              if (System.IO.File.Exists(file))
              {
                  html += sr.ReadToEnd();
                  sr.Close();
              }

              //DataTable dtCampaign = new DataTable();
              //dtCampaign = objems.GetPendingCampaign();

              //for (int b = 0; b < dtCampaign.Rows.Count; b++)
              //{
              //    if (dtCampaign.Rows.Count > 0)
              //    {
              //        if (dtCampaign.Rows[b]["TemplateID"].ToString() == "1")
              //        {

              DataTable dtNewsletter = new DataTable();
              dtNewsletter = objn.GetNewsletter(NewsletterID);
              if (dtNewsletter != null && dtNewsletter.Rows.Count > 0)
              {
                  Title = dtNewsletter.Rows[0]["NewsletterTitle"].ToString();
              }

              //<!-- product table starts here -->"
              DataTable dtItemCases = new DataTable();
              dtItemCases = objn.GetNewsletterItems(NewsletterID, "Case");

              if (dtItemCases.Rows.Count > 0)
              {

                  //int chk = objems.UpdateCampaignStatus(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), "In-Progress");
                  for (int a = 0; a < dtItemCases.Rows.Count; a++)
                  {

                      Cases = Cases + "<p style='color:#333;margin-top:0;font-size:15px;'><b>" + (a + 1).ToString() + ".</b> " + dtItemCases.Rows[a]["ShortText"].ToString() + "   <a href='https://eastlaw.pk/cases/" + dtItemCases.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemCases.Rows[a]["ItemID"].ToString()) + "' target='_blank'>Read More</a></p>";
                  }



              }

              //Statutes
              DataTable dtItemStatutes = new DataTable();
              dtItemStatutes = objn.GetNewsletterItems(NewsletterID, "Statutes");

              if (dtItemStatutes.Rows.Count > 0)
              {
                  Statutes = Statutes + "<p style='color:#555;font-size:17px;font-weight:600;font-size:17px;margin:0;margin-bottom:5px;'><i>Legislation Updates</i></p>";
                  //int chk = objems.UpdateCampaignStatus(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), "In-Progress");
                  Statutes = Statutes + "<ul>";
                  for (int a = 0; a < dtItemStatutes.Rows.Count; a++)
                  {

                      Statutes = Statutes + "<li style='color:#333;margin-top:0;font-size:15px;'><a href='https://eastlaw.pk/statutes/" + dtItemStatutes.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemStatutes.Rows[a]["ItemID"].ToString()) + "' target='_blank'>" + dtItemStatutes.Rows[a]["Title"].ToString() + " "  +dtItemStatutes.Rows[a]["ShortText"].ToString() +"</a></li>";
                  }
                  Statutes = Statutes + "</ul>";

              }


              //Dept
              DataTable dtItemDept = new DataTable();
              dtItemDept = objn.GetNewsletterItems(NewsletterID, "Dept");

              if (dtItemDept.Rows.Count > 0)
              {
                  Dept = Dept + "<p style='color:#555;font-size:17px;font-weight:600;font-size:17px;margin:0;margin-bottom:5px;'><i>Departments Updates</i></p>";
                  //int chk = objems.UpdateCampaignStatus(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), "In-Progress");
                  Dept = Dept + "<ul>";
                  for (int a = 0; a < dtItemDept.Rows.Count; a++)
                  {
                      Dept = Dept + "<li style='color:#333;margin-top:0;font-size:15px;'><a href='https://eastlaw.pk/departments/departments/" + dtItemDept.Rows[a]["Title"].ToString().Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dtItemDept.Rows[a]["ItemID"].ToString()) + "' target='_blank'>" + dtItemDept.Rows[a]["Title"].ToString() + " " + dtItemDept.Rows[a]["ShortText"].ToString() + "</a></li>";

                      

                  }
                  Dept = Dept + "</ul>";

              }

              //News
              DataTable dtNews = new DataTable();
              EastLawBL.News objnews = new EastLawBL.News();
              dtNews = objnews.GetActiveNews();

              for (int a = 0; a < dtNews.Rows.Count; a++)
              {
                  strNews = strNews + "<p style='color:#555;margin-top:0;font-size:15px;'>" + EastlawUI_v2.CommonClass.GetWords(EastlawUI_v2.CommonClass.RemoveHTML(dtNews.Rows[a]["FullContent"].ToString()), 50) + "<a href='" + dtNews.Rows[a]["SourceLink"].ToString() + "' target='_blank'>Read More</a></p>";
                  if (a == 5)
                      break;
              }

             
              // }
              //return Content;


              if (dtNewsletter != null)
              {
                  if (dtNewsletter.Rows.Count > 0)
                  {
                      //html = html.Replace("##BNRFILE##", "<img src='/adminpanel/newsletter/banners/" + dtNewsletter.Rows[0]["NewsletterBanner"].ToString() + "' style='width:100%;' /></td>");
                      html = html.Replace("##NLTITL##",Title);
                  }
              }

              html = html.Replace("##CITDT##", Cases);
              html = html.Replace("##LGDT##", Statutes);
              html = html.Replace("##DEPTDT##", Dept);
              html = html.Replace("##LGNWSDT##", strNews);
              //html = html.Replace("##TOPPRACTICEAREA##", "");

              //  Email objEmail = new Email();
              DataTable dtEmails = new DataTable();
              //dtEmails = com.GetNewsletter();
              //if (dtEmails.Rows.Count > 0)
              //{
              //    int chk1=0;
              //    for (int a = 0; a < dtEmails.Rows.Count; a++)
              //    {
              //        if (!string.IsNullOrEmpty(dtEmails.Rows[a]["EmailID"].ToString()))
              //        {
              //            DataTable dtchkExist = objems.GetEmailCampaignSentOrNot(int.Parse(dtCampaign.Rows[b]["ID"].ToString()),dtEmails.Rows[a]["EmailID"].ToString());
              //            if(dtchkExist.Rows.Count == 0)
              //            {
              //      Content = Content + "<table width='800' border='0' cellspacing='1' cellpadding='0'>"
              // + "<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333; text-align:center; background-color:#FFF'>You can <a href='http://fabbyshop.com/unsubscribe.aspx?camp=" + dtCampaign.Rows[b]["ID"].ToString() + "&email=" + dtEmails.Rows[a]["EmailID"].ToString() + "'> unsubscribe this newsletter at any time.<br />"
              //+ "We respect your personal information and we will not share them with any other parties.<br />For further details , please review our terms and conditions.</td></tr></table>";
              //            int EChk = objEmail.SendMail(dtEmails.Rows[a]["EmailID"].ToString(), "", AddTracking(Content, dtEmails.Rows[a]["EmailID"].ToString(),dtCampaign.Rows[b]["ID"].ToString(), "fabbyshop.com"), dtCampaign.Rows[b]["Subject"].ToString(), "fabbyshop.com");

              //            if (EChk == 1)
              //               chk1 = objems.AddSentEmailCampaign(int.Parse(dtCampaign.Rows[b]["ID"].ToString()), dtEmails.Rows[a]["EmailID"].ToString(), 1, 0);
              //            }
              //        }

              //    }

              //}
              //      Content = Content + "<table width='800' border='0' cellspacing='1' cellpadding='0'>"
              // + "<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#333; text-align:center; background-color:#FFF'>You can unsubscribe this newsletter at any time <a href='#'> Click Here</a>.<br />"
              //+ "We respect your personal information and we will not share them with any other parties.<br />For further details , please review our terms and conditions.</td></tr></table>";
              //int EChk = 0;
              // EChk = objEmail.SendMail("umar.mughal83@gmail.com", "", Content + Footer("umar.mughal83@gmail.com"), "Testing ... " + dtItems.Rows[0]["Subject"].ToString(), "fabbyshop.com");
              return html;







              //    }
              //}

              //return "";
          }
          catch
          {
              return "";
          }
      }
    }
}