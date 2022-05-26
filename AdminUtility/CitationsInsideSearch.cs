using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
namespace AdminUtility
{
    class CitationsInsideSearch
    {
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        public void InsideCitaionsSearch()
        {
            try
            { 
                EastLawBL.Cases objcase = new EastLawBL.Cases();
                CitationsSearchJournalWise objJSearch = new CitationsSearchJournalWise();
                WriteLogs("InsideCitaionsSearch Start: " + DateTime.Now.Date.ToShortDateString(), "InsideCitaionsSearch_Job");
                DataTable dt = new DataTable();
                dt = objcase.GetCasesForInsideCitationsSearch();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    //SCMR
                    try
                    {
                        objJSearch.GetSCMRJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //CLC
                    try
                    {
                        objJSearch.GetCLCJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //PLD
                    try
                    {
                        objJSearch.GetPLDJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //PTD
                    try
                    {
                        objJSearch.GetPTDJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //PLC
                    try
                    {
                        objJSearch.GetPLCJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //YLR
                    try
                    {
                        objJSearch.GetYLRJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //MLD
                    try
                    {
                        objJSearch.GetMLDJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //CLD
                    try
                    {
                        objJSearch.GetCLDJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    //PCrLJ
                    try
                    {
                        objJSearch.GetPCrLJJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }
                    
                    
                    ////if (chk > 0)
                    ////{
                    int chk1 = objcase.UpdateInsideCitationsSearchFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    ////}
                    WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "InsideCitaionsSearch_Job");
                }

                WriteLogs("InsideCitaionsSearch End: " + DateTime.Now.Date.ToShortDateString(), "InsideCitaionsSearch_Job");


            }
            catch { }

        }
        public void InsideSectionsSearch()
        {
            try
            {
                EastLawBL.Cases objcase = new EastLawBL.Cases();
                CitationsSearchMiscWise objSearch = new CitationsSearchMiscWise();
                WriteLogs("Section Tagging Start: " + DateTime.Now.Date.ToShortDateString(), "SectionTags");
                DataTable dt = new DataTable();
                dt = objcase.GetCasesForInsideSectionsSearch();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    try
                    {
                        objSearch.GetSectionsSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                        objcase.UpdateInsideSectionsSearchFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                        WriteLogs("Section tag Complete on Caseid: " + dt.Rows[a]["ID"].ToString(), "SectionTags");
                    }
                    catch { }
                  
                    //if (chk > 0)
                    //{
                    //    int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //}
                    //  WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "Statutes_Tagging_Daily_Job");
                }

                // // WriteLogs("Statutes Tagging End: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");


            }
            catch { }

        }
        public void InsideArticleSearch()
        {
            try
            {
                EastLawBL.Cases objcase = new EastLawBL.Cases();
                CitationsSearchMiscWise objSearch = new CitationsSearchMiscWise();
                WriteLogs("Article Tagging Start: " + DateTime.Now.Date.ToShortDateString(), "ArticleTags");
                DataTable dt = new DataTable();
                dt = objcase.GetCasesForInsideArticleSearch();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    //try
                    //{
                    //    objSearch.GetRulesSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    //}
                    //catch { }

                    try
                    {
                        objSearch.GetArticleSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                        objcase.UpdateInsideArticleSearchFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                        WriteLogs("Article tag Complete on Caseid: " + dt.Rows[a]["ID"].ToString(), "ArticleTags");
                    }
                    catch { }



                    //if (chk > 0)
                    //{
                    //    int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //}
                    //  WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "Statutes_Tagging_Daily_Job");
                }

                // // WriteLogs("Statutes Tagging End: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");


            }
            catch { }

        }
        public void InsideRulesSearch()
        {
            try
            {
                EastLawBL.Cases objcase = new EastLawBL.Cases();
                CitationsSearchMiscWise objSearch = new CitationsSearchMiscWise();
                WriteLogs("Rules Tagging Start: " + DateTime.Now.Date.ToShortDateString(), "RulesTags");
                DataTable dt = new DataTable();
                dt = objcase.GetCasesForInsideRuleSearch();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    //try
                    //{
                    //    objSearch.GetRulesSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    //}
                    //catch { }

                    try
                    {
                        objSearch.GetRulesSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                        objcase.UpdateInsideRuleSearchFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                        WriteLogs("Rules tag Complete on Caseid: " + dt.Rows[a]["ID"].ToString(), "RulesTags");
                    }
                    catch { }



                    //if (chk > 0)
                    //{
                    //    int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //}
                    //  WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "Statutes_Tagging_Daily_Job");
                }

                // // WriteLogs("Statutes Tagging End: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");


            }
            catch { }

        }
        public void InsideSections_Rules_ArticleSearch()
        {
            try
            {
                EastLawBL.Cases objcase = new EastLawBL.Cases();
                CitationsSearchMiscWise objSearch = new CitationsSearchMiscWise();
                //  WriteLogs("Statutes Tagging Start: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");
                DataTable dt = new DataTable();
                dt = objcase.GetCasesForInsideCitationsSearch();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    //try
                    //{
                    //    objSearch.GetSectionsSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    //    objcase.UpdateInsideSectionsSearchFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //    WriteLogs("Section tag Complete on Caseid: " + dt.Rows[a]["ID"].ToString(),"SectionTags");
                    //}
                    //catch { }
                    //try
                    //{
                    //    objSearch.GetRulesSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    //}
                    //catch { }

                    try
                    {
                        objSearch.GetArticleSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    }
                    catch { }

                    

                    //if (chk > 0)
                    //{
                    //    int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //}
                    //  WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "Statutes_Tagging_Daily_Job");
                }

                // // WriteLogs("Statutes Tagging End: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");


            }
            catch { }

        }
        public void GetSet_NoofJudgesFromCase()
        {
            try
            {
                EastLawBL.Cases objcase = new EastLawBL.Cases();
               // CitationsSearchMiscWise objSearch = new CitationsSearchMiscWise();
                //  WriteLogs("Statutes Tagging Start: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");
                DataTable dt = new DataTable();
                dt = objcase.GetCasesForJudgesFind();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    try
                    {
                        string input = dt.Rows[a]["JudgeName"].ToString(); 
                        input = input.Replace("C.J,", "");
                        input = input.Replace("and", ", ");
                        input = input.Replace(", JJ", "");
                        input = input.Replace(", J", "");

                        input = input.Replace("c.j,", "C.J ");
                        input = input.Replace("AND", ", ");
                        input = input.Replace(", jj", "");
                        input = input.Replace(", j", "");
                        input = input.Replace("CJ", "");
                        input = input.Replace("Member", "");
                        input = input.Replace("members", "");
                        input = input.Replace("Chairman", "");
                        input = input.Replace("judicial member", "");
                        input = input.Replace("accounting member etc", "");

                        Regex regex = new Regex(@"[,]");
                        string[] substrings = regex.Split(input);
                        for (int b = 0; b < substrings.Length; b++)
                        {
                            if (!string.IsNullOrEmpty(substrings[b].ToString()))
                            {
                                int chk = objcase.AddJudgesListBycase(int.Parse(dt.Rows[a]["ID"].ToString()), Common.MakeFirstCap(substrings[b].ToString().Trim()));
                            }
                        }
                        DataTable dtCount = new DataTable();
                        dtCount = objcase.GetListofJudgesByCase(int.Parse(dt.Rows[a]["ID"].ToString()));
                        if (dtCount != null)
                        {
                            int chk2 = objcase.UpdateNoofJudgesByCaseID(int.Parse(dt.Rows[a]["ID"].ToString()), dtCount.Rows.Count);
                            WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString() + " No. of Judges: " + dtCount.Rows.Count, "NoofjudgesFind");
                        }
                            
                    }
                    catch { }

                    //try
                    //{
                    //    objJSearch.GetCLCJournalSearch(int.Parse(dt.Rows[a]["ID"].ToString()), dt.Rows[a]["HeadNotes"].ToString(), dt.Rows[a]["Judgment"].ToString());
                    //}
                    //catch { }


                    //if (chk > 0)
                    //{
                    //    int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //}
                    //  WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "Statutes_Tagging_Daily_Job");
                }

                // // WriteLogs("Statutes Tagging End: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");


            }
            catch { }

        }

        public void TaggCitationLinking()
        {
            try
            {
                EastLawBL.Cases objcase = new EastLawBL.Cases();
                CitationsSearchJournalWise objJSearch = new CitationsSearchJournalWise();
                EastLawBL.Journals objjour = new EastLawBL.Journals();
                WriteLogs("InsideCitaions Linking Start: " + DateTime.Now.Date.ToShortDateString(), "InsideCitaionsLinking_Job");
                DataTable dt = new DataTable();
                dt = objcase.GetCaseseInsideCitationsPendingLinking();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[a]["Citation"].ToString()))
                    {
                        string citationsearchelement = "";
                        string[] citaionparts = dt.Rows[a]["Citation"].ToString().Split(' ');
                        if (citaionparts.Length == 3)
                        {
                            if (IsNumeric(citaionparts[0].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[0].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[2].ToString()));
                                }
                                catch { }
                            }
                            else if (!IsNumeric(citaionparts[0].ToString()))
                            {
                                try
                                { 
                                citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[1].ToString()), 
                                    objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                    RemoveAllChractersExceptNumber(citaionparts[2].ToString()));
                                }
                                catch { }
                            }
                        }
                        if (citaionparts.Length == 4)
                        {
                            if (IsNumeric(citaionparts[0].ToString()))
                            {
                                try
                                { 
                                citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[0].ToString()), 
                                    objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                    RemoveAllChractersExceptNumber(citaionparts[2].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[1].ToString()))
                            {
                                try
                                { 
                                citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[1].ToString()),
                                    objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                    RemoveAllChractersExceptNumber(citaionparts[3].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[2].ToString()))
                            {
                                try
                                { 
                                citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[2].ToString()),
                                    objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                    RemoveAllChractersExceptNumber(citaionparts[3].ToString()));
                                }
                                catch { }
                            }
                            else if (!IsNumeric(citaionparts[3].ToString()))
                            {
                                try
                                { 
                                citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[3].ToString()),
                                    objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                    RemoveAllChractersExceptNumber(citaionparts[3].ToString()));
                                }
                                catch { }
                            }
                            
                        }
                        if (citaionparts.Length == 5)
                        {
                            if (IsNumeric(citaionparts[0].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[0].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[4].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[1].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[1].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[4].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[2].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[2].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[4].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[2].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[2].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[4].ToString()));
                                }
                                catch { }
                            }
                            else if (!IsNumeric(citaionparts[3].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[3].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[4].ToString()));
                                }
                                catch { }
                            }

                        }
                        if (citaionparts.Length == 7)
                        {
                            if (IsNumeric(citaionparts[0].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[0].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[6].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[1].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[1].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[6].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[2].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[2].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[6].ToString()));
                                }
                                catch { }
                            }
                            else if (IsNumeric(citaionparts[3].ToString()))
                            {
                                try
                                {
                                    citationsearchelement = CitationsSearchQuery(int.Parse(citaionparts[3].ToString()),
                                        objjour.GetJournalIDByName(dt.Rows[a]["JournalName"].ToString()),
                                        RemoveAllChractersExceptNumber(citaionparts[6].ToString()));
                                }
                                catch { }
                            }
                        }

                        DataTable dt1 = new DataTable();
                        int chk = 0;
                        if (!string.IsNullOrEmpty(citationsearchelement))
                        {
                            dt1 = objcase.GetCasesSearch(citationsearchelement,0,20);
                            if (dt1 != null)
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    chk = objcase.TaggedCitaionLinking(int.Parse(dt.Rows[a]["ID"].ToString()), int.Parse(dt.Rows[a]["CaseID"].ToString()), int.Parse(dt1.Rows[0]["ID"].ToString()));
                                }
                                else
                                {
                                    chk = objcase.TaggedCitaionLinking(int.Parse(dt.Rows[a]["ID"].ToString()), int.Parse(dt.Rows[a]["CaseID"].ToString()), -1);
                                }

                            }
                        }
                        else
                        {
                            chk = objcase.TaggedCitaionLinking(int.Parse(dt.Rows[a]["ID"].ToString()), int.Parse(dt.Rows[a]["CaseID"].ToString()), -2);
                        }
                         WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "InsideCitaionsLinking_Job");
                    }
                }
                  WriteLogs("InsideCitaions Linking End: " + DateTime.Now.Date.ToShortDateString(), "InsideCitaionsLinking_Job");
            }
            catch(Exception ex) {
                WriteLogs(ex.Message, "InsideCitaionsLinking_Job-Error");
            }

        }
        string CitationsSearchQuery(int Year,int JournalID,string Qry)
        {
            try
            {
                string cri = "Where Citation is not null ";
                string forlog = "";

                cri = cri + " AND Year='" + Year + "'";
                cri = cri + " AND JournalID='" + JournalID + "'";
                cri = cri + " AND  (PageNo='" + Qry + "')";


                return cri;
                //DataTable dt = new DataTable();
                //InsertAuditLog("Search", "Search By Citation", forlog);
                //dt = objcases.GetCasesSearch(cri);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        dt.Columns.Add("Link");
                //        dt.Columns.Add("Title");
                //        for (int a = 0; a < dt.Rows.Count; a++)
                //        {

                //            dt.Rows[a]["Title"] = EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Appeallant"].ToString()) + " VS " + EastLawUI.CommonClass.MakeFirstCap(dt.Rows[a]["Respondent"].ToString());
                //            dt.Rows[a]["Link"] = "/Cases/" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Appeallant"].ToString(), 3).ToString())).Replace(" ", "-") + "VS" + clsUtilities.RemoveSpecialCharacter(EastLawUI.CommonClass.MakeFirstCap(EastLawUI.CommonClass.GetWords(dt.Rows[a]["Respondent"].ToString(), 3).ToString())).Replace(" ", "-") + "." + EncryptDecryptHelper.Encrypt(dt.Rows[a]["ID"].ToString());

                //        }
                //        dt.AcceptChanges();
                //        Session["CasesSearch"] = dt;
                //        Session["SearchMain"] = null;
                //        Session["SearchWithIn"] = null;
                //        lblCitationMsg.Text = "";
                //        lblCitationMsg.Visible = false;
                //        Response.Redirect("/Cases/Search-Result");
                //    }
                //    else
                //    {
                //        lblCitationMsg.Text = "Record not found";
                //        lblCitationMsg.Visible = true;
                //    }
                //}
                //else
                //{
                //    lblCitationMsg.Text = "Record not found";
                //    lblCitationMsg.Visible = true;

                //}

            }
            catch (Exception ex)
            {
                return "";

            }
        }
          static readonly Regex _isNumericRegex =
     new Regex("^(" +
            /*Hex*/ @"0x[0-9a-f]+" + "|" +
            /*Bin*/ @"0b[01]+" + "|" +
            /*Oct*/ @"0[0-7]*" + "|" +
            /*Dec*/ @"((?!0)|[-+]|(?=0+\.))(\d*\.)?\d+(e\d+)?" +
                 ")$");
        static bool IsNumeric(string value)
        {
            return _isNumericRegex.IsMatch(value);
        }
        string RemoveAllChractersExceptNumber(string str)
        {
            try
            {
                string s = str;
                string result = string.Empty;
                foreach (var c in s)
                {
                    int ascii = (int)c;
                    if ((ascii >= 48 && ascii <= 57))
                        result += c;
                }
                return result;
            }
            catch {
                return str;
            }
        }
        public void WriteLogs(string msg, string filename)
        {
            try
            {
                string FolderName = DateTime.Now.ToString("MMyyyy");

                if (!Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "")))
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + ""));

                StreamWriter sw = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs/" + FolderName + "/" + filename + ".txt"), true);
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
