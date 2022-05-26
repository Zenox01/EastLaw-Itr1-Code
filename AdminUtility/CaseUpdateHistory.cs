using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;

namespace AdminUtility
{
     class CaseUpdateHistory
    {
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        public void UpdateCasesFromUpdateHistoryLog()
        {
            try
            {
                DataTable dtUpdateHistory = new DataTable();
                dtUpdateHistory = objcase.GetCaseHistoryForUpdate(0);
                if (dtUpdateHistory != null)
                {
                    if (dtUpdateHistory.Rows.Count > 0)
                    {
                        for (int a = 0; a < dtUpdateHistory.Rows.Count; a++)
                        {
                            objcase.Citation = dtUpdateHistory.Rows[a]["Citation"].ToString();
                            objcase.CitationRef = dtUpdateHistory.Rows[a]["CitationRef"].ToString();
                            objcase.Appeal = dtUpdateHistory.Rows[a]["Appeal"].ToString();
                            objcase.Appeallant = dtUpdateHistory.Rows[a]["Appeallant"].ToString();
                            objcase.Respondent = dtUpdateHistory.Rows[a]["Respondent"].ToString();
                            objcase.JDate = dtUpdateHistory.Rows[a]["JDate"].ToString();
                            objcase.HearDate = dtUpdateHistory.Rows[a]["HearDate"].ToString();
                            objcase.HeadNotes = dtUpdateHistory.Rows[a]["HeadNotes"].ToString();
                            objcase.Judgment = dtUpdateHistory.Rows[a]["Judgment"].ToString();
                            objcase.JudgmentType = dtUpdateHistory.Rows[a]["JudgmentType"].ToString();
                            objcase.Result = dtUpdateHistory.Rows[a]["Result"].ToString();
                            objcase.Court = dtUpdateHistory.Rows[a]["Court"].ToString();
                            objcase.ModifiedBy = 999999;
                            objcase.AppeallantType = dtUpdateHistory.Rows[a]["AppeallantType"].ToString();
                            objcase.Year = int.Parse(dtUpdateHistory.Rows[a]["Year"].ToString());
                            objcase.Status = int.Parse(dtUpdateHistory.Rows[a]["Status"].ToString());
                            objcase.CourtCityName = dtUpdateHistory.Rows[a]["Court_Area"].ToString();
                            objcase.PageNo = dtUpdateHistory.Rows[a]["PageNo"].ToString();
                            int chk = objcase.EditCase_FromUpdateHistory(int.Parse(dtUpdateHistory.Rows[a]["CaseID"].ToString()));
                            WriteLogs("Case ID: " + dtUpdateHistory.Rows[a]["CaseID"].ToString() + ", Updated", "CaseUpdate");
                            if(chk > 0)
                            {
                                int chkup = objcase.UpdateCaseUpdateHistoryStatus(int.Parse(dtUpdateHistory.Rows[a]["ID"].ToString()), 999999);
                                WriteLogs("Case History ID: " + dtUpdateHistory.Rows[a]["ID"].ToString() + ", Updated", "CaseUpdateHistory");
                            }
                        }
                    }
                }

            }
            catch { }
            
        }
        public void DeleteCasesMarkedIsDelete_1()
        {
            try
            {
                WriteLogs("Case Delete Start: " + DateTime.Now.Date.ToShortDateString(), "Case_Delete_Logs");
                DataTable dt = new DataTable();
                dt = objcase.GetCasesMarkedIsDeleted1();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    int chk = objcase.DeleteCasesMarkIsDeleted1(int.Parse(dt.Rows[a]["ID"].ToString()));
                    string LogStr="Case ID: " +dt.Rows[a]["ID"].ToString()+" JournalID: "+dt.Rows[a]["JournalID"].ToString() +" Citation: "+dt.Rows[a]["Citation"].ToString()+" Court: " + dt.Rows[a]["Court"].ToString();
                    WriteLogs(LogStr, "Case_Delete_Logs");

                }
                 WriteLogs("Case Delete End: " + DateTime.Now.Date.ToShortDateString(), "Case_Delete_Logs");
                    


            }
            catch { }

        }
        public void StatutesTaggingDailyJob()
        {
            try
            {
                WriteLogs("Statutes Tagging Start: " + DateTime.Now.Date.ToShortDateString() , "Statutes_Tagging_Daily_Job");
                 DataTable dt = new DataTable();
                 dt = objcase.GetCasesForStatutesTagging();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    int chk = objcase.StatutesTaggingDailyJob(int.Parse(dt.Rows[a]["ID"].ToString()));
                   // if (chk > 0)
                    //{
                        int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //}
                    WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "Statutes_Tagging_Daily_Job");
                }
                
                WriteLogs("Statutes Tagging End: " + DateTime.Now.Date.ToShortDateString() , "Statutes_Tagging_Daily_Job");
                

            }
            catch { }

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
