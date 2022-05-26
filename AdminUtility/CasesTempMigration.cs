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
   public class CasesTempMigration
    {
       EastLawBL.Cases objCases = new EastLawBL.Cases();
       EastLawBL.Judges objJudge = new EastLawBL.Judges();
       string str = "";
       public void GetPendingTempCases()
       {
           try
           {
               DataTable dtCasesTemp = new DataTable();
               dtCasesTemp = objCases.GetCaseTempPendingMove();
               if (dtCasesTemp != null)
               {
                   if (dtCasesTemp.Rows.Count > 0)
                   {
                       for (int a = 0; a < dtCasesTemp.Rows.Count; a++)
                       {
                           if (objCases.isCitationExist(dtCasesTemp.Rows[a]["Citation"].ToString()) == false)
                           {



                               objCases.Status = 2;
                               objCases.JournalID = int.Parse(dtCasesTemp.Rows[a]["JournalID"].ToString());
                               objCases.Year = int.Parse(dtCasesTemp.Rows[a]["Year"].ToString());
                               objCases.Citation = dtCasesTemp.Rows[a]["Citation"].ToString();
                               objCases.JudgeStr = dtCasesTemp.Rows[a]["Judge"].ToString();
                               //objCases.Judge = int.Parse(lblJudgeID.Text);

                               objCases.Appeal = dtCasesTemp.Rows[a]["Appeal"].ToString();
                               objCases.Appeallant = dtCasesTemp.Rows[a]["Appeallant"].ToString();
                               objCases.Appeallant2 = dtCasesTemp.Rows[a]["Appeallant2"].ToString();
                               objCases.Appeallant3 = dtCasesTemp.Rows[a]["Appeallant3"].ToString();
                               objCases.AppeallantType = dtCasesTemp.Rows[a]["AppeallantType"].ToString();
                               objCases.Respondent = dtCasesTemp.Rows[a]["Respondent"].ToString();
                               objCases.Respondent2 = dtCasesTemp.Rows[a]["Respondent2"].ToString();
                               objCases.Respondent3 = dtCasesTemp.Rows[a]["Respondent3"].ToString();
                               objCases.JDate = dtCasesTemp.Rows[a]["JDate"].ToString();
                               objCases.AdvocateASTR = dtCasesTemp.Rows[a]["AdvocateA"].ToString();
                               //objCases.AdvocateA = int.Parse(lblAdvAID.Text);
                               objCases.AdvocateRSTR = dtCasesTemp.Rows[a]["AdvocateR"].ToString();
                               // objCases.AdvocateR = int.Parse(lblAdvRID.Text);
                               objCases.HearDate = dtCasesTemp.Rows[a]["HearDate"].ToString();
                               objCases.HeadNotes = dtCasesTemp.Rows[a]["HeadNotes"].ToString();
                               objCases.Judgment = dtCasesTemp.Rows[a]["Judgment"].ToString();
                               objCases.JudgmentType = dtCasesTemp.Rows[a]["JudgmentType"].ToString();
                               objCases.Result = dtCasesTemp.Rows[a]["Result"].ToString();
                               objCases.Court = dtCasesTemp.Rows[a]["Court"].ToString();
                               objCases.CreatedBy = 9999999;

                               if (string.IsNullOrEmpty(dtCasesTemp.Rows[a]["CaseSummary"].ToString()))
                                   objCases.CaseSummary = Common.GetCaseSummary(System.Text.RegularExpressions.Regex.Replace(dtCasesTemp.Rows[a]["Judgment"].ToString(), @"\r\n?|\n", "<br/>").Trim());
                               else
                                   objCases.CaseSummary = dtCasesTemp.Rows[a]["CaseSummary"].ToString();

                               objCases.CourtCityName = dtCasesTemp.Rows[a]["Court_Area"].ToString();
                               objCases.DateFormated = int.Parse(dtCasesTemp.Rows[a]["DateFormated"].ToString());
                               objCases.PageNo = dtCasesTemp.Rows[a]["Citation"].ToString().Split(' ').Last();
                               objCases.PriorityTagging = 1;
                               int chk = objCases.InsertCaseMigrate();
                               if (chk > 0)
                               {
                                   GetPendingTempCasesJuges(int.Parse(dtCasesTemp.Rows[a]["ID"].ToString()), chk);
                                    GetPendingTempCasesPracticeArea(int.Parse(dtCasesTemp.Rows[a]["ID"].ToString()), chk);
                                    GetPendingTempCasesInsideCitation(int.Parse(dtCasesTemp.Rows[a]["ID"].ToString()), chk);
                                   objCases.UpdateCaseTemp(int.Parse(dtCasesTemp.Rows[a]["ID"].ToString()), chk, 999999);

                                   WriteLogs("Temp Case ID: " + dtCasesTemp.Rows[a]["ID"].ToString() + " Main Case ID" + chk, "TempCaseID_Done");
                               }


                               else
                               {
                                   WriteLogs("Temp Case ID: " + dtCasesTemp.Rows[a]["ID"].ToString() + " Main Case ID" + chk, "TempCaseID_Failed");
                               }
                           }


                       }
                   }
               }

           }
           catch { }

       }
       public void GetPendingTempCasesJuges(int TempCaseID,int MainCaseID)
       {
           try
           {
               DataTable dtCasesTempJudges = new DataTable();
               dtCasesTempJudges = objCases.GetCaseseJuges_TempPendingMove(TempCaseID);
               if (dtCasesTempJudges != null)
               {
                   if (dtCasesTempJudges.Rows.Count > 0)
                   {
                       for (int a = 0; a < dtCasesTempJudges.Rows.Count; a++)
                       {
                           int chk = objJudge.TaggeCaseinJudgesNew(int.Parse(dtCasesTempJudges.Rows[a]["NewJudgeID"].ToString()), MainCaseID, 999999, 0, ref str);
                           if (chk > 0)
                           {
                               objCases.UpdateCaseJudgesTemp(int.Parse(dtCasesTempJudges.Rows[a]["ID"].ToString()), chk, 999999);
                           }
                       }
                   }
               }
           }
           catch { }
       }
        public void GetPendingTempCasesPracticeArea(int TempCaseID, int MainCaseID)
        {
            try
            {
                DataTable dtCasesTempPA = new DataTable();
                dtCasesTempPA = objCases.GetCasesePracticeArea_TempPendingMove(TempCaseID);
                if (dtCasesTempPA != null)
                {
                    if (dtCasesTempPA.Rows.Count > 0)
                    {
                        for (int a = 0; a < dtCasesTempPA.Rows.Count; a++)
                        {
                            int chk = objCases.TaggeCaseinPracticeArea(int.Parse(dtCasesTempPA.Rows[a]["PAID"].ToString()), MainCaseID, 999999, ref str);
                            if (chk > 0)
                            {
                                objCases.UpdateCasePracticeAreaTemp(int.Parse(dtCasesTempPA.Rows[a]["ID"].ToString()), chk, 999999);
                            }
                        }
                    }
                }
            }
            catch { }
        }
        public void GetPendingTempCasesInsideCitation(int TempCaseID, int MainCaseID)
       {
           try
           {
               DataTable dtCasesTemp = new DataTable();
               dtCasesTemp = objCases.GetCaseseInsideCitation_TempPendingMove(TempCaseID);
               if (dtCasesTemp != null)
               {
                   if (dtCasesTemp.Rows.Count > 0)
                   {
                       for (int a = 0; a < dtCasesTemp.Rows.Count; a++)
                       {
                           int chk = objCases.AddCasesInsideCitations(MainCaseID, dtCasesTemp.Rows[a]["Citation"].ToString(), "Strong", "", dtCasesTemp.Rows[a]["Journal"].ToString(), int.Parse(dtCasesTemp.Rows[a]["LinkedCaseID"].ToString()));
                           if (chk > 0)
                           {
                               objCases.UpdateCaseInsideCitationTemp(int.Parse(dtCasesTemp.Rows[a]["ID"].ToString()), chk, 999999);
                           }
                       }
                   }
               }
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
