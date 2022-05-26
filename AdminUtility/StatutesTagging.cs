using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace AdminUtility
{
    public class StatutesTagging
    {
        EastLawBL.Statutes objStat = new EastLawBL.Statutes();
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        public void StatutesTaggingDailyJob()
        {
            try
            {
                WriteLogs("Statutes Tagging Start: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");

                DataTable dtStatutes=new DataTable();
                dtStatutes = objStat.GetActiveStatutesLessInfo();

                DataTable dt = new DataTable();
                dt = objcase.GetCasesForStatutesTagging();
                for (int a = 0; a < dt.Rows.Count; a++)
                {


                    for (int b = 0; b < dtStatutes.Rows.Count; b++)
                    {
                        string title = Common.FormatStatutesTaggingWord(dtStatutes.Rows[b]["Title"].ToString());//.Replace(",", " ").Replace(" ", ", ").Replace("(", "").Replace(")", "");
                        DataTable dtChkCondition = objcase.GetCheckStatutesTaggingCondition(int.Parse(dt.Rows[a]["ID"].ToString()), title);
                        if (dtChkCondition != null)
                        {
                            if (dtChkCondition.Rows.Count > 0)
                            {
                                WriteLogs("Citation: " + dt.Rows[a]["Citation"].ToString() + " Statutes: " + dtStatutes.Rows[b]["Title"].ToString(), "Statutes_Tagging_Daily_Job");

                                int chk = objStat.InsertStatutesLinking("Case", int.Parse(dtStatutes.Rows[b]["ID"].ToString()), int.Parse(dt.Rows[a]["ID"].ToString()), 99999999);
                            }
                        }

                        // int aa = LevenshteinDistance(dtStatutes.Rows[b]["Title"].ToString(), dt.Rows[a]["Judgment"].ToString());
                        //  WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString() +"Statutes:"+dtStatutes.Rows[b]["Title"].ToString() +" Percentage " + aa.ToString(), "Statutes_Tagging_Daily_Job");
                    }
                    int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));

                    //int chk = objcase.StatutesTaggingDailyJob(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //if (chk > 0)
                    //{
                    //    int chk1 = objcase.UpdateStatutesTaggingFlagOnCases(int.Parse(dt.Rows[a]["ID"].ToString()));
                    //}
                    // WriteLogs("Case ID: " + dt.Rows[a]["ID"].ToString(), "Statutes_Tagging_Daily_Job");
                }

              //  WriteLogs("Statutes Tagging End: " + DateTime.Now.Date.ToShortDateString(), "Statutes_Tagging_Daily_Job");


            }
            catch { }

        }
        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }
            for (int i = 0; i <= n; d[i, 0] = i++)
                ;
            for (int j = 0; j <= m; d[0, j] = j++)
                ;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
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
