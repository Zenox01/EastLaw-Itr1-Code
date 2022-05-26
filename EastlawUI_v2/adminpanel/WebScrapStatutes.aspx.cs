using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Text;
using System.Data;
using System.IO;

namespace EastlawUI_v2.adminpanel
{
    public partial class WebScrapStatutes : System.Web.UI.Page
    {
        EastLawBL.Statutes objStat = new EastLawBL.Statutes();
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
                LoadAlpha();
            }

        }
        void LoadAlpha()
        {
            try
            {
                for (int i = 0; i < 26; i++)
                {
                    ddlAlpha.Items.Add(Convert.ToChar(i + 65).ToString());
                    ddlSubordinateStatutesAlpha.Items.Add(Convert.ToChar(i + 65).ToString());
                    ddlPunjabCodeAlpha.Items.Add(Convert.ToChar(i + 65).ToString());
                    
                    //Console.WriteLine(Convert.ToChar(i + 65));
                }
            }
            catch(Exception ex)
            {

            }
        }
        #region Pakistan Code Statutes
        void ScrapPakistanCodeByAlpha()
        {
            try
            {
                int chk = 0;
                var webGet = new HtmlWeb();
                

                //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                ////document.Load(Server.MapPath("TheCode.html"));
                HtmlAgilityPack.HtmlDocument document = webGet.Load("http://pakistancode.gov.pk/LGu0xAD?alp="+ddlAlpha.SelectedValue+"&page=1");

                int count = 1;
                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@class='artlist'] | //div[@class='artdets']"))
                {

                    // var value = document.DocumentNode.SelectSingleNode("//div[@class='artdets']");
                    //string link = div.InnerHtml.ToString();
                    //if(count == 1)
                    //    Response.Write(link.ToString().Replace("href=\"", "href='http://pakistancode.gov.pk/") + "<br><br><br>");
                    if (count == 1)
                    {
                        count++;

                        string[] atrributor1 = div.InnerText.Split('|');
                        if (atrributor1.Length == 1)
                        {
                           // string link = div.InnerHtml.ToString();
                            //Response.Write(link.ToString().Replace("href=\"", "href='http://pakistancode.gov.pk/")  );
                            //Response.Write("<br><br><br>");
                            //Response.Write(div.InnerText.ToString() + "<br><br><br>");
                          //  Response.Write(div.InnerHtml.ToString() + "<br><br><br>");
                            if (!string.IsNullOrEmpty(div.InnerText.ToString()))
                            {
                                objStat.CatName = "N/A";
                                objStat.Title = div.InnerText.ToString().Trim();
                                objStat.CreatedBy = 1;
                                objStat.Active = 1;
                                objStat.WorkflowID = 1;
                                objStat.GroupID = 1;
                                objStat.SubGroupID = 1;
                                objStat.Primary_Secondary = "PRIMARY";
                                objStat.StatutesContentType = "IndexContent";
                                objStat.Act = "";
                                objStat.Date = "";
                                
                                chk = objStat.InsertStatutesUtility();
                                if (chk > 0)
                                {
                                    var doc = new HtmlDocument();
                                    string tableTag = div.InnerHtml.ToString();
                                    doc.LoadHtml(tableTag);
                                    var anchor = doc.DocumentNode.SelectSingleNode("//a");
                                    if (anchor != null)
                                    {
                                        string link1 = anchor.Attributes["href"].Value;
                                        GetIndex("http://pakistancode.gov.pk/" + link1, chk);
                                    }
                                }
                            }


                        }

                        
                    }


                    if (count == 2)
                    {
                        string[] atrributor = div.InnerText.Split('|');
                        if (atrributor.Length > 1)
                        {

                            if (atrributor.Length > 0)
                                objStat.CatName = atrributor[0].ToString().TrimStart();
                            if (atrributor.Length > 1)
                                objStat.Act = atrributor[1].ToString().TrimStart();
                            if (atrributor.Length > 2)
                                objStat.Date = atrributor[2].ToString().TrimStart();

                            objStat.CreatedBy = 1;
                            objStat.ID = chk;
                            objStat.InsertStatutesUtilityAttributes();

                            //Response.Write(div.InnerText.ToString() + "<br><br><br>");
                           // Response.Write("End Block<br>");
                            count = 1;
                        }
                        else
                            count = 1;


                    }


                }
            }
            catch { }
        }
        void GetIndex(string URL,int StateID)
        {
            try
            {
                var webGet = new HtmlWeb();
                // HtmlAgilityPack.HtmlDocument document = webGet.Load("http://pakistancode.gov.pk/UY2FqaJw1-apaUY2Fqa-bpuUY2Fp-sg-jjjjjjjjjjjjj");
                HtmlAgilityPack.HtmlDocument document = webGet.Load(URL);

                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//center"))
                {
                    if (div.InnerText.ToString().Contains("Download &amp; Print PDF"))
                    {
                        var doc = new HtmlDocument();
                        string tableTag = div.InnerHtml.ToString();
                        doc.LoadHtml(tableTag);
                        var anchor = doc.DocumentNode.SelectSingleNode("//a[2]");
                        if (anchor != null)
                        {
                            string link1 = "http://pakistancode.gov.pk/" + anchor.Attributes["href"].Value;
                            using (System.Net.WebClient webClient = new System.Net.WebClient())
                            {
                                webClient.DownloadFile(link1, Server.MapPath("../store/statutesdocs/pdf/" + StateID + ".pdf"));
                                objStat.UpdateStatutesFilesName("PDF", StateID + ".pdf", StateID);
                            }
                        }
                    }
                    if (div.InnerText.ToString().Contains("Download &amp; Print MS Word File"))
                    {
                        var doc = new HtmlDocument();
                        string tableTag = div.InnerHtml.ToString();
                        doc.LoadHtml(tableTag);
                        var anchor = doc.DocumentNode.SelectSingleNode("//a[3]");
                        if (anchor != null)
                        {
                            string link1 = "http://pakistancode.gov.pk/" + anchor.Attributes["href"].Value;
                            using (System.Net.WebClient webClient = new System.Net.WebClient())
                            {
                                webClient.DownloadFile(link1, Server.MapPath("../store/statutesdocs/word/" + StateID + ".doc"));
                                objStat.UpdateStatutesFilesName("WORD", StateID + ".doc", StateID);
                            }
                        }
                    }

                }

                int count = 0;
                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//table"))
                {
                    count++;

                    foreach (HtmlAgilityPack.HtmlNode row in div.SelectNodes("//table"))
                    {

                        HtmlAgilityPack.HtmlNodeCollection cells = row.SelectNodes("tr");

                        if (cells == null)
                        {
                            continue;
                        }

                        foreach (HtmlAgilityPack.HtmlNode cell in cells)
                        {
                            var doc = new HtmlDocument();
                            string tableTag = cell.InnerHtml.ToString();
                            doc.LoadHtml(tableTag);
                            var anchor = doc.DocumentNode.SelectSingleNode("//a[@href]");
                            if (anchor != null)
                            {

                                if (count == 5)
                                {

                                    foreach (var cell1 in div.SelectNodes(".//tr//td"))
                                    {
                                        //Regex regex = new Regex("<a>(.*)</a>");
                                        //var v = regex.Match(cell1.InnerHtml.ToString());
                                        //string s = v.Groups[1].ToString();


                                        var doc1 = new HtmlDocument();
                                        string tableTag1 = cell1.InnerHtml.ToString();
                                        doc1.LoadHtml(tableTag1);
                                        var anchor1 = doc1.DocumentNode.SelectSingleNode("//a");
                                        if (anchor1 != null)
                                        {
                                            if (cells.Count == 3)
                                            {
                                                string link1 = anchor1.InnerText.ToString();
                                                string link2 = anchor1.Attributes["href"].Value;
                                                //Response.Write("<a href='http://pakistancode.gov.pk/" + link2 + "' target='_blank'>" + link1.ToString() + "</a><br>");
                                                GetIndexDetails("http://pakistancode.gov.pk/" + link2, link1,StateID);
                                            }
                                        }

                                    }
                                }
                            }

                        }
                    }


                }
            }
            catch { }
        }
        void GetIndexDetails(string URL, string IndexName,int StateID)
        {
            try
            {
                var webGet = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = webGet.Load(URL);

                int count = 0;
                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@id='1']"))
                {
                    objStat.StatutesID = StateID;
                    objStat.IndexTitle = IndexName;
                    objStat.IndexContent = div.InnerHtml.ToString().Replace("dic.php", "eastlawdic.aspx");
                    objStat.IndexLink = URL;
                    objStat.Active = 1;
                    objStat.CreatedBy = 0;
                    int chk = objStat.InsertStatutesIndexDetailsUtility();
                    //Response.Write(IndexName + "<br>" + div.InnerHtml.ToString().Replace("dic.php", "eastlawdic.aspx"));

                }
            }
            catch { }
        }
        protected void btnPakistanCode_Click(object sender, EventArgs e)
        {
            ScrapPakistanCodeByAlpha();
        }
        #endregion
        #region Pakistan Code Subordinates Statutes
        void ScrapSubordinatesPakistanCodeByAlpha()
        {
            try
            {
                int chk = 0;
                var webGet = new HtmlWeb();


                //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                ////document.Load(Server.MapPath("TheCode.html"));
                HtmlAgilityPack.HtmlDocument document = webGet.Load("http://pakistancode.gov.pk/ruleslist.php?alp=" + ddlSubordinateStatutesAlpha.SelectedValue + "&page=1");

                int count = 1;
                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@class='artlist'] | //div[@class='artdets']"))
                {

                    // var value = document.DocumentNode.SelectSingleNode("//div[@class='artdets']");
                    //string link = div.InnerHtml.ToString();
                    //if(count == 1)
                    //    Response.Write(link.ToString().Replace("href=\"", "href='http://pakistancode.gov.pk/") + "<br><br><br>");
                    if (count == 1)
                    {
                        count++;

                        string[] atrributor1 = div.InnerText.Split('|');
                        if (atrributor1.Length == 1)
                        {
                            // string link = div.InnerHtml.ToString();
                            //Response.Write(link.ToString().Replace("href=\"", "href='http://pakistancode.gov.pk/")  );
                            //Response.Write("<br><br><br>");
                            //Response.Write(div.InnerText.ToString() + "<br><br><br>");
                           // Response.Write(div.InnerHtml.ToString() + "<br><br><br>");
                           // Response.Write(FindCategory(div.InnerText.ToString() + "<br><br><br>"));

                            if (!string.IsNullOrEmpty(div.InnerText.ToString()))
                            {
                                objStat.CatName = "N/A";
                                objStat.Title = div.InnerText.ToString().Trim();
                                objStat.CreatedBy = 1;
                                objStat.Active = 1;
                                objStat.WorkflowID = 1;
                                objStat.GroupID = 1;
                                objStat.SubGroupID = 1;
                                objStat.Primary_Secondary = "SECONDARY";
                                objStat.StatutesContentType = "IndexContent";
                                chk = objStat.InsertStatutesUtility();
                                if (chk > 0)
                                {

                                    objStat.CatName = CommonClass.FindSubordinatesStatutesCategory(div.InnerText.ToString().Trim());
                                    objStat.CreatedBy = 0;
                                    objStat.ID = chk;
                                    objStat.InsertStatutesUtilityAttributes();

                                    var doc = new HtmlDocument();
                                    string tableTag = div.InnerHtml.ToString();
                                    doc.LoadHtml(tableTag);
                                    var anchor = doc.DocumentNode.SelectSingleNode("//a");
                                    if (anchor != null)
                                    {
                                        string link1 = anchor.Attributes["href"].Value;
                                        //  GetSubordinateIndex("http://pakistancode.gov.pk/" + link1);
                                        GetSubordinateIndexDetails("http://pakistancode.gov.pk/" + link1, div.InnerText.ToString(), chk);
                                    }
                                }
                            }


                        }


                    }


                    if (count == 2)
                    {
                        string[] atrributor = div.InnerText.Split('|');
                        if (atrributor.Length > 1)
                        {

                            //if (atrributor.Length > 0)
                            //    objStat.CatName = atrributor[0].ToString().TrimStart();
                            //if (atrributor.Length > 1)
                            //    objStat.Act = atrributor[1].ToString().TrimStart();
                            //if (atrributor.Length > 2)
                            //    objStat.Date = atrributor[2].ToString().TrimStart();

                            //objStat.CreatedBy = 1;
                            //objStat.ID = chk;
                            //objStat.InsertStatutesUtilityAttributes();

                            //Response.Write(div.InnerText.ToString() + "<br><br><br>");
                            // Response.Write("End Block<br>");
                            count = 1;
                        }
                        else
                            count = 1;


                    }


                }
            }
            catch { }
        }
        void GetSubordinateIndexDetails(string URL, string IndexName, int StateID)
        {
            try
            {
                var webGet = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = webGet.Load(URL);
                // HtmlAgilityPack.HtmlDocument document = webGet.Load("http://pakistancode.gov.pk/rulesbody.php?case=Actions%20%28in%20Aid%20of%20Civil%20Power%29%20Regulation%202011&sg-jjjjjjjjjjjjj");
                // HtmlAgilityPack.HtmlDocument document = webGet.Load("http://pakistancode.gov.pk/rulesbody.php?case=Access%20to%20Justice%20Development%20Fund%20Rules%202002&sg-jjjjjjjjjjjjj");

                int count = 0;
                string txtcnt = "";
                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@class='Section1']"))
                //foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@id='1']"))
                {
                    objStat.StatutesID = StateID;
                    objStat.IndexTitle = IndexName;
                    objStat.IndexContent = div.InnerHtml.ToString().Replace("�", "");
                    objStat.IndexLink = URL;
                    objStat.Active = 1;
                    objStat.CreatedBy = 0;
                    int chk = objStat.InsertStatutesIndexDetailsUtility();
                    //Response.Write(IndexName + "<br>" + FormatContent(div.InnerHtml.ToString().Replace("�", "")));

                }
            }
            catch { }
        }

        protected void btnPakistanCodeSubordinateStatutes_Click(object sender, EventArgs e)
        {
            ScrapSubordinatesPakistanCodeByAlpha();
        }
        #endregion
        #region Punjab Code
        void ScrapPunjabCodeByAlpha()
        {
            try
            {
                int chk = 0;
                var webGet = new HtmlWeb();


                //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                ////document.Load(Server.MapPath("TheCode.html"));
                //HtmlAgilityPack.HtmlDocument document = webGet.Load("http://pakistancode.gov.pk/LGu0xAD?alp=" + ddlAlpha.SelectedValue + "&page=1");
                HtmlAgilityPack.HtmlDocument document = webGet.Load("http://punjabcode.punjab.gov.pk/index/listalph/s/a/lb/"+ddlPunjabCodeAlpha.SelectedValue);

                int count = 1;
                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@class='artlist'] | //div[@class='artdets']"))
                {

                    // var value = document.DocumentNode.SelectSingleNode("//div[@class='artdets']");
                    //string link = div.InnerHtml.ToString();
                    //if(count == 1)
                    //    Response.Write(link.ToString().Replace("href=\"", "href='http://pakistancode.gov.pk/") + "<br><br><br>");
                    if (count == 1)
                    {
                        count++;

                        string[] atrributor1 = div.InnerText.Split('|');
                        if (atrributor1.Length == 1)
                        {
                            // string link = div.InnerHtml.ToString();
                            //Response.Write(link.ToString().Replace("href=\"", "href='http://pakistancode.gov.pk/")  );
                            //Response.Write("<br><br><br>");
                            //Response.Write(div.InnerText.ToString() + "<br><br><br>");
                           // Response.Write(div.InnerHtml.ToString() + "<br><br><br>");
                            if (!string.IsNullOrEmpty(div.InnerText.ToString()))
                            {
                                    objStat.CatName = "N/A";
                                    objStat.Title = div.InnerText.ToString().Trim();
                                    objStat.CreatedBy = 1;
                                    objStat.Active = 1;
                                    objStat.WorkflowID = 1;
                                    objStat.GroupID = 2;
                                    objStat.SubGroupID = 2;
                                    objStat.Primary_Secondary = "PRIMARY";
                                    objStat.StatutesContentType = "File";
                                    objStat.Act = "";
                                    objStat.Date = "";
                                    chk = objStat.InsertStatutesUtility();
                                    if (chk > 0)
                                    {
                                        var doc = new HtmlDocument();
                                        string tableTag = div.InnerHtml.ToString();
                                        doc.LoadHtml(tableTag);
                                        var anchor = doc.DocumentNode.SelectSingleNode("//a");
                                        if (anchor != null)
                                        {
                                            string link1 = anchor.Attributes["href"].Value;
                                            GetPunjabIndex("http://punjabcode.punjab.gov.pk/" + link1, chk);
                                        }
                                    }
                            }


                        }


                    }


                    if (count == 2)
                    {
                        string[] atrributor = div.InnerText.Split('|');
                        if (atrributor.Length > 1)
                        {

                            if (atrributor.Length > 0)
                                objStat.CatName = atrributor[0].ToString().TrimStart();
                            if (atrributor.Length > 1)
                                objStat.Act = atrributor[1].ToString().TrimStart();
                            if (atrributor.Length > 2)
                                objStat.Date = atrributor[2].ToString().TrimStart();

                            objStat.CreatedBy = 1;
                            objStat.ID = chk;
                            objStat.InsertStatutesUtilityAttributes();

                            //Response.Write(div.InnerText.ToString() + "<br><br><br>");
                            // Response.Write("End Block<br>");
                            count = 1;
                        }
                        else
                            count = 1;


                    }


                }
            }
            catch { }
        }
        void GetPunjabIndex(string URL, int StateID)
        {
            try
            {
                var webGet = new HtmlWeb();
                // HtmlAgilityPack.HtmlDocument document = webGet.Load("http://pakistancode.gov.pk/UY2FqaJw1-apaUY2Fqa-bpuUY2Fp-sg-jjjjjjjjjjjjj");
                HtmlAgilityPack.HtmlDocument document = webGet.Load(URL);

                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//div[@class='pdfviewheader']"))
                {
                    if (div.InnerText.ToString().Contains("Download PDF"))
                    {
                        var doc = new HtmlDocument();
                        string tableTag = div.InnerHtml.ToString();
                        doc.LoadHtml(tableTag);
                        var anchor = doc.DocumentNode.SelectSingleNode("//a[2]");
                        if (anchor != null)
                        {
                            string link1 = "http://punjabcode.punjab.gov.pk/" + anchor.Attributes["href"].Value;
                            using (System.Net.WebClient webClient = new System.Net.WebClient())
                            {
                                webClient.DownloadFile(link1, Server.MapPath("../store/statutesdocs/pdf/" + StateID + ".pdf"));
                                objStat.UpdateStatutesFilesName("PDF", StateID + ".pdf", StateID);
                            }
                        }
                    }
                    //if (div.InnerText.ToString().Contains("Download &amp; Print MS Word File"))
                    //{
                    //    var doc = new HtmlDocument();
                    //    string tableTag = div.InnerHtml.ToString();
                    //    doc.LoadHtml(tableTag);
                    //    var anchor = doc.DocumentNode.SelectSingleNode("//a[3]");
                    //    if (anchor != null)
                    //    {
                    //        string link1 = "http://punjabcode.punjab.gov.pk/" + anchor.Attributes["href"].Value;
                    //        using (System.Net.WebClient webClient = new System.Net.WebClient())
                    //        {
                    //            webClient.DownloadFile(link1, Server.MapPath(" + StateID + ".doc"));
                    //        }
                    //    }
                    //}

                }

            }
            catch { }
        }
        protected void btnPunjabCodeAlpha_Click(object sender, EventArgs e)
        {
            ScrapPunjabCodeByAlpha();
        }
        #endregion
        #region Sindh Laws
        void GetSindhActs()
        {
           
                string urlyear = "";
                for (int i = 0; i < 110; i++)
                {
                    //urlyear = DateTime.Now.AddYears(-i).Year.ToString();
                    urlyear = DateTime.Now.AddYears(-i).Year.ToString();
                    try
                    {
                        var webGet = new HtmlWeb();
                        
                        HtmlAgilityPack.HtmlDocument document = webGet.Load("http://www.sindhlaws.gov.pk/Act.aspx?MenuID=MID-000010&Act_Year=" + urlyear + "");
                        int count = 0;
                        string txt = "";
                        string link2 = "";
                        string[] arr4 = new string[4];
                        foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//table[@id='ContentPlaceHolder1_DataList3']"))
                        {
                            // count++;
                            var doc12 = new HtmlDocument();
                            doc12.LoadHtml(div.InnerHtml);
                            int chk = 0;
                            foreach (HtmlAgilityPack.HtmlNode row22 in doc12.DocumentNode.SelectNodes("//table/tr/td[@class='normal_text'] | //a[@class='download']  | //span[@class='normal_text']"))
                            {


                                if (!string.IsNullOrEmpty(row22.InnerText))
                                {
                                    link2 = "";
                                    var doc = new HtmlDocument();
                                    string tableTag = row22.OuterHtml.ToString();
                                    doc.LoadHtml(tableTag);
                                    var anchor = doc.DocumentNode.SelectSingleNode("//a[@href]");
                                    if (anchor != null)
                                    {
                                        //string link1 = anchor.InnerText.ToString();
                                        link2 = anchor.Attributes["href"].Value;
                                    }
                                    // Response.Write(row22.OuterHtml + "<br>");
                                    if (!string.IsNullOrEmpty(link2))
                                        arr4[count] = link2;
                                    // txt = txt + link2 + ",";
                                    else
                                        arr4[count] = row22.InnerText.Replace("\r\n", string.Empty).TrimStart().TrimEnd().Trim();
                                    //txt = txt + row22.InnerText + ",";

                                }
                                count++;
                                if (count == 4)
                                {
                                    WriteLogs(" Title: " + arr4[1].ToString());
                                    //  Response.Write(arr4[0].ToString() + "<br>");
                                    objStat.CatName = "N/A";
                                    objStat.Title = arr4[1].ToString();
                                    objStat.CreatedBy = 1;
                                    objStat.Active = 1;
                                    objStat.WorkflowID = 1;
                                    objStat.GroupID = 2;
                                    objStat.SubGroupID = 3;
                                    objStat.Primary_Secondary = "PRIMARY";
                                    objStat.StatutesContentType = "File";
                                    objStat.Act = "Act " + arr4[0].ToString() + " of " + urlyear.ToString();
                                    objStat.Date = arr4[3].ToString();
                                    chk = objStat.InsertStatutesUtility();
                                    if (chk > 0)
                                    {
                                    //    objStat.CatName = "N/A";
                                    //    objStat.Act = "Act " + arr4[0].ToString() + " of " + urlyear.ToString();
                                    //    objStat.Date = arr4[3].ToString();

                                    //    objStat.CreatedBy = 1;
                                    //    objStat.ID = chk;
                                    //    objStat.InsertStatutesUtilityAttributes();

                                        string link1 = "http://www.sindhlaws.gov.pk/" + arr4[2].ToString();
                                        using (System.Net.WebClient webClient = new System.Net.WebClient())
                                        {
                                            webClient.DownloadFile(link1, Server.MapPath("../store/statutesdocs/pdf/" + chk + ".pdf"));
                                            objStat.UpdateStatutesFilesName("PDF", chk + ".pdf", chk);
                                        }
                                    }
                                    //Response.Write(txt.ToString()+"<br>");
                                    count = 0;
                                    txt = "";
                                    link2 = "";
                                    Array.Clear(arr4, 0, 4);
                                }
                            }
                        }
                    }
                    catch { }
                }
           
        }
        void GetSindhOrdinance()
        {

            string urlyear = "";
            for (int i = 0; i < 110; i++)
            {
                urlyear = DateTime.Now.AddYears(-i).Year.ToString();
                try
                {
                    var webGet = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument document = webGet.Load("http://www.sindhlaws.gov.pk/Ordinance.aspx?MenuID=MID-000011&Ord_Year=" + urlyear + "");
                    int count = 0;
                    string txt = "";
                    string link2 = "";
                    string[] arr4 = new string[4];
                    foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//table[@id='ContentPlaceHolder1_DataList3']"))
                    {
                        // count++;
                        var doc12 = new HtmlDocument();
                        doc12.LoadHtml(div.InnerHtml);
                        int chk = 0;
                        foreach (HtmlAgilityPack.HtmlNode row22 in doc12.DocumentNode.SelectNodes("//table/tr/td[@class='normal_text'] | //a[@class='download']  | //span[@class='normal_text']"))
                        {


                            if (!string.IsNullOrEmpty(row22.InnerText))
                            {
                                link2 = "";
                                var doc = new HtmlDocument();
                                string tableTag = row22.OuterHtml.ToString();
                                doc.LoadHtml(tableTag);
                                var anchor = doc.DocumentNode.SelectSingleNode("//a[@href]");
                                if (anchor != null)
                                {
                                    //string link1 = anchor.InnerText.ToString();
                                    link2 = anchor.Attributes["href"].Value;
                                }
                                // Response.Write(row22.OuterHtml + "<br>");
                                if (!string.IsNullOrEmpty(link2))
                                    arr4[count] = link2;
                                // txt = txt + link2 + ",";
                                else
                                    arr4[count] = row22.InnerText.Replace("\r\n", string.Empty).TrimStart().TrimEnd().Trim();
                                //txt = txt + row22.InnerText + ",";

                            }
                            count++;
                            if (count == 4)
                            {
                                //  Response.Write(arr4[0].ToString() + "<br>");
                                objStat.CatName = "N/A";
                                objStat.Title = arr4[1].ToString();
                                objStat.CreatedBy = 1;
                                objStat.Active = 1;
                                objStat.WorkflowID = 1;
                                objStat.GroupID = 2;
                                objStat.SubGroupID = 3;
                                objStat.Primary_Secondary = "PRIMARY";
                                objStat.StatutesContentType = "File";
                                objStat.Act = "Ordinance " + arr4[0].ToString() + " of " + urlyear.ToString();
                                objStat.Date = arr4[3].ToString();
                                chk = objStat.InsertStatutesUtility();
                                if (chk > 0)
                                {
                                    //    objStat.CatName = "N/A";
                                    //    objStat.Act = "Act " + arr4[0].ToString() + " of " + urlyear.ToString();
                                    //    objStat.Date = arr4[3].ToString();

                                    //    objStat.CreatedBy = 1;
                                    //    objStat.ID = chk;
                                    //    objStat.InsertStatutesUtilityAttributes();

                                    string link1 = "http://www.sindhlaws.gov.pk/" + arr4[2].ToString();
                                    using (System.Net.WebClient webClient = new System.Net.WebClient())
                                    {
                                        webClient.DownloadFile(link1, Server.MapPath("../store/statutesdocs/pdf/" + chk + ".pdf"));
                                        objStat.UpdateStatutesFilesName("PDF", chk + ".pdf", chk);
                                    }
                                }
                                //Response.Write(txt.ToString()+"<br>");
                                count = 0;
                                txt = "";
                                link2 = "";
                                Array.Clear(arr4, 0, 4);
                            }
                        }
                    }
                }
                catch { }
            }

        }
        protected void btnSindlawAct_Click(object sender, EventArgs e)
        {
            GetSindhActs();
        }
        protected void btnSindhLawOrdinance_Click(object sender, EventArgs e)
        {
            GetSindhOrdinance();
        }
        #endregion

        #region "KPK"
        void GetKPK(string URL)
        {
            try
            {
                var webGet = new HtmlWeb();
                int chk = 0;
                HtmlAgilityPack.HtmlDocument document = webGet.Load(URL);
                int count = 0;
                string txt = "";
                string link2 = "";
                string[] arr4 = new string[4];
                foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//tr[@class='P1']"))
                {
                    // count++;
                    var doc12 = new HtmlDocument();
                    doc12.LoadHtml(div.InnerHtml);
                    foreach (HtmlAgilityPack.HtmlNode row22 in doc12.DocumentNode.SelectNodes("//td"))
                    {


                        if (!string.IsNullOrEmpty(row22.InnerText))
                        {
                            link2 = "";
                            var doc = new HtmlDocument();
                            string tableTag = row22.OuterHtml.ToString();
                            doc.LoadHtml(tableTag);
                            var anchor = doc.DocumentNode.SelectSingleNode("//a[@href]");
                            if (anchor != null)
                            {
                                //string link1 = anchor.InnerText.ToString();
                                link2 = anchor.Attributes["href"].Value;
                            }
                            // Response.Write(row22.OuterHtml + "<br>");
                            if (!string.IsNullOrEmpty(link2))
                            {
                                arr4[3] = link2;
                                arr4[count] = row22.InnerText.Replace("\r\n", string.Empty).TrimStart().TrimEnd().Trim();
                            }
                            else
                            {
                                arr4[count] = row22.InnerText.Replace("\r\n", string.Empty).TrimStart().TrimEnd().Trim();

                            }


                        }
                        count++;
                        if (count == 3)
                        {
                            Response.Write(arr4[0].ToString() + "<br>");

                            objStat.CatName = arr4[1].ToString();
                            objStat.Title = arr4[0].ToString();
                            objStat.CreatedBy = 1;
                            objStat.Active = 1;
                            objStat.WorkflowID = 1;
                            objStat.GroupID = 2;
                            objStat.SubGroupID = 4;
                            objStat.Primary_Secondary = "PRIMARY";
                            objStat.StatutesContentType = "IndexContent";
                            objStat.Act = "";
                            objStat.Date = arr4[2].ToString();
                            chk = objStat.InsertStatutesUtility();
                            GetKPKInner("http://www.khyberpakhtunkhwa.gov.pk/Gov/" + arr4[3].ToString(), arr4[0].ToString(), chk);
                            count = 0;
                            txt = "";
                            link2 = "";
                            Array.Clear(arr4, 0, 4);
                        }

                    }
                }



            }
            catch { }
        }

        void GetKPKInner(string URL, string IndexName, int StateID)
        {
            try
            {
                var webGet = new HtmlWeb();

                HtmlAgilityPack.HtmlDocument document = webGet.Load(URL);
                int count = 0;
                string txt = "";
                string link2 = "";
                string[] arr4 = new string[4];
                //foreach (HtmlAgilityPack.HtmlNode div in document.DocumentNode.SelectNodes("//table"))
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//iframe[@src]");
                foreach (var node in nodes)
                {
                    HtmlAttribute attr = node.Attributes["src"];
                    Response.Write(attr.Value);
                    HtmlAgilityPack.HtmlDocument orgdoc = webGet.Load("http://www.khyberpakhtunkhwa.gov.pk/Gov/" + attr.Value);
                    foreach (HtmlAgilityPack.HtmlNode div in orgdoc.DocumentNode.SelectNodes("//body"))
                    {
                        string content = div.InnerHtml;
                        objStat.StatutesID = StateID;
                        objStat.IndexTitle = IndexName;
                        objStat.IndexContent = div.InnerHtml;
                        objStat.IndexLink = URL;
                        objStat.Active = 1;
                        objStat.CreatedBy = 1;
                        int chk = objStat.InsertStatutesIndexDetailsUtility();
                    }

                }
                
            }
            catch { }
        }
        protected void btnKPKUpload_Click(object sender, EventArgs e)
        {
            GetKPK("http://www.khyberpakhtunkhwa.gov.pk/Gov/Rule-Regulations-Laws-Acts.php");

        }
        #endregion
        public void WriteLogs(string msg)
        {
            try
            {
                string FolderName = DateTime.Now.ToString("MMyyyy");

                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + ""));

                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/log.txt"), true);
                //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
                sw.WriteLine("------------------------------------" + DateTime.Now + "-------------------");
                sw.WriteLine(msg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/LoggingError" + ".txt"), true);
                //StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/"+RqstCode+".txt"), true);
                sw.WriteLine("------------------- Error In Loging eroor -------------------" + DateTime.Now.AddMinutes(28) + "-------------------");
                sw.WriteLine(ex.Message);
                sw.Flush();
                sw.Close();
            }
        }

       

       





    }
}