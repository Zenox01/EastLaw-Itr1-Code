using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;

namespace EastlawUI_v2.inte
{
    /// <summary>
    /// Summary description for intest
    /// </summary>
    [WebService(Namespace = "eastlaw_inte")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class intest : System.Web.Services.WebService
    {
        EastLawBL.EBook objeb = new EastLawBL.EBook();
        [WebMethod]
        public DataTable GIn(string mid, string il,string ii,  string consume, string acode, string ip, string loc, ref string Msg)
      //  public DataTable GIn(string mid, string il, string ii, string consume, string acode, string ip, string loc)
        {
           // string Msg = "";
            try
            {
                
                string logtext="Call GIn with parameter: mid,"+EncryptDecryptHelper.Decrypt(mid)+",il:" +EncryptDecryptHelper.Decrypt(il)+",ii:"+EncryptDecryptHelper.Decrypt(ii)
                    +",location:"+ip+" "+loc;
                WriteLogs(logtext, "GIn", EncryptDecryptHelper.Decrypt(consume));
                DataTable dt = new DataTable();
                if (EncryptDecryptHelper.Decrypt(il) == "0")
                {
                    dt = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(mid)),int.Parse(EncryptDecryptHelper.Decrypt(il)));
                }
                else// if (EncryptDecryptHelper.Decrypt(il) == "1")
                {
                     dt = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(mid)), int.Parse(EncryptDecryptHelper.Decrypt(ii)));
                }
                //else if (EncryptDecryptHelper.Decrypt(il) == "2")
                //{
                //    dt = objeb.GetEBookParentIndex(int.Parse(EncryptDecryptHelper.Decrypt(mid)), int.Parse(EncryptDecryptHelper.Decrypt(ii)));
                //}
                dt.Columns.Remove("IndexGroup");
                dt.Columns.Remove("IndexContent");
                dt.Columns.Remove("SortOrder");
                dt.Columns.Remove("Active");
                dt.Columns.Remove("IsDeleted");
                dt.Columns.Remove("CreatedBy");
                dt.Columns.Remove("CreatedOn");
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex) {
                WriteLogs(ex.Message, "GIn_Error", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
         [WebMethod]
        public DataTable GInDT(string mid, string ii, string consume, string acode, string ip, string loc, ref string Msg)
        {
            try
            {
                string logtext = "Call GInDT with parameter: mid," + EncryptDecryptHelper.Decrypt(mid)  + ",ii:" + EncryptDecryptHelper.Decrypt(ii)
                    + ",location:" + ip + " " + loc;
                WriteLogs(logtext, "GIn", EncryptDecryptHelper.Decrypt(consume));
                DataTable dt = new DataTable();
               
                    dt = objeb.GetEBookIndex(int.Parse(EncryptDecryptHelper.Decrypt(ii)), int.Parse(EncryptDecryptHelper.Decrypt(mid)));
               
                dt.Columns.Remove("IndexGroup");
                dt.Columns.Remove("SortOrder");
                dt.Columns.Remove("Active");
                dt.Columns.Remove("IsDeleted");
                dt.Columns.Remove("CreatedBy");
                dt.Columns.Remove("CreatedOn");
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex)
            {
                WriteLogs(ex.Message, "GInDT", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                return null;
            }
        }
         [WebMethod]
         public DataTable GInSR(string mid, string swrd, string consume, string acode, string ip, string loc, ref string Msg)
         {
             try
             {
                 //string Msg = "";
                 string logtext = "Call GInSR with parameter: mid," + EncryptDecryptHelper.Decrypt(mid) + ",Search Word:" + EncryptDecryptHelper.Decrypt(swrd)
                     + ",location:" + ip + " " + loc;
                 WriteLogs(logtext, "GInSR", EncryptDecryptHelper.Decrypt(consume));
                 DataTable dt = new DataTable();

                 dt = objeb.GetEBookIndexSearch(int.Parse(EncryptDecryptHelper.Decrypt(mid)), "\"" + EncryptDecryptHelper.Decrypt(swrd) + "\"");

                 
                 return dt;
             }
             catch (Exception ex)
             {
                 WriteLogs(ex.Message, "GInSR", EncryptDecryptHelper.Decrypt(consume));
                Msg = ex.Message;
                 return null;
             }
         }

        public void WriteLogs(string msg, string filename,string usrid)
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
    }
}
