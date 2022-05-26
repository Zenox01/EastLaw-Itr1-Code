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
   public class SimilarCitation
    {
       EastLawBL.Cases objcase = new EastLawBL.Cases();
       public void GetSimilarCases()
       {
           string err = "";
           try
           {
               DataTable dtFrom = new DataTable();
               
               int TotalCasesSource = 33009;// objcase.GetCasesAllCount();
               for (int p = 0; p < TotalCasesSource; p += 20)
               {
                   dtFrom = objcase.GetCasesAll_ForSimillarCitation(p, p + 20);
                   //dtFrom = objcase.GetCasesAll_ForSimillarCitation(p,20);

                   if (dtFrom != null)
                   {
                       WriteLogs("Citation Process: " + dtFrom.Rows.Count.ToString(), "similar judgment process" + DateTime.Now.Date.ToShortDateString());


                       for (int a = 0; a < dtFrom.Rows.Count; a++)
                       {
                           int TotalCasesSearch = 33009;// objcase.GetCasesAllCount();
                           for (int q = 0; q < TotalCasesSearch; q += 20)
                           {
                               
                               DataTable dtfromsearch = new DataTable();
                               dtfromsearch = objcase.GetCasesAll_ForSimillarCitation(q, q + 20);
                               //dtfromsearch = objcase.GetCasesAll_ForSimillarCitation(q, 20);
                              // WriteLogs("Citation Process search from: " + dtfromsearch.Rows.Count.ToString() +" q start = "+ q.ToString() + " q end ="+(q+20).ToString(), "similar judgment process" + DateTime.Now.Date.ToShortDateString());
                               for (int b = 0; b < dtfromsearch.Rows.Count; b++)
                               {
                                   if (dtFrom.Rows[a]["ID"].ToString() != dtfromsearch.Rows[b]["ID"].ToString())
                                   {
                                       //if(!string.IsNullOrEmpty(dtFrom.Rows[a]["Judgment"].ToString()) && (!string.IsNullOrEmpty(dtfromsearch.Rows[b]["Judgment"].ToString())))
                                       //{

                                       //double per = StringCompare(dtFrom.Rows[a]["Judgment"].ToString(), dtfromsearch.Rows[b]["Judgment"].ToString(),ref err);
                                           double per = objcase.GetSimilarCiationWithLevenshtein(int.Parse(dtFrom.Rows[a]["ID"].ToString()),int.Parse(dtfromsearch.Rows[b]["ID"].ToString()));

                                           //if (per > 50)
                                           //{
                                               //WriteLogs("Citation From: " + dtFrom.Rows[a]["Citation"].ToString() + "ID: " + dtFrom.Rows[a]["ID"].ToString() + " Similar Citation with Percentage: " + dtfromsearch.Rows[b]["Citation"].ToString() + " " + per.ToString() + "%" + " ID: " + dtfromsearch.Rows[b]["ID"].ToString(), "similar judgment");
                                               int chk = objcase.AddSimilarCitation(int.Parse(dtFrom.Rows[a]["ID"].ToString()), int.Parse(dtfromsearch.Rows[b]["ID"].ToString()), double.Parse(per.ToString(("#.000"))));
                                          // }
                                       //}

                                   }
                               }
                               
                           }
                           WriteLogs("Citation Process: " + dtFrom.Rows[a]["ID"].ToString() + " Citation ID: " + dtFrom.Rows[a]["ID"].ToString(), "similar judgment process" + DateTime.Now.Date.ToShortDateString());
                       }
                   }
                   else
                   {
                       WriteLogs("Null DS", "similar judgment error");
                   }
               }



           }
           catch (Exception ex)
           {
               WriteLogs(ex.Message + "-"+err, "similar judgment error");
           }

       }
       static double StringCompare(string a, string b,ref string err)
       {
           try
           {
               if (a == b) //Same string, no iteration needed.
                   return 100;
               if ((a.Length == 0) || (b.Length == 0)) //One is empty, second is not
               {
                   return 0;
               }
               double maxLen = a.Length > b.Length ? a.Length : b.Length;
               int minLen = a.Length < b.Length ? a.Length : b.Length;
               int sameCharAtIndex = 0;
               for (int i = 0; i < minLen; i++) //Compare char by char
               {
                   if (a[i] == b[i])
                   {
                       sameCharAtIndex++;
                   }
               }
               return sameCharAtIndex / maxLen * 100;
           }
           catch (Exception ex)
           {
               err = ex.Message;
               return 0;
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
