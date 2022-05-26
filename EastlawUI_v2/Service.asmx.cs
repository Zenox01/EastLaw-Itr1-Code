using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
namespace EastlawUI_v2
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetKeywords(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                List<string> terms = prefix.Split(',').ToList();
                terms = terms.Select(s => s.Trim()).ToList();

                //Extract the term to be searched from the list
                string searchTerm = terms.LastOrDefault().ToString().Trim();

                //Return if Search Term is empty
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return new string[0];
                }

                //Populate the terms that need to be filtered out
                List<string> excludeTerms = new List<string>();
                if (terms.Count > 1)
                {
                    terms.RemoveAt(terms.Count - 1);
                    excludeTerms = terms;
                }

                conn.ConnectionString = DBHelper.GetConnectionString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "select keywords from tbl_keywords where  " +
                    "keywords like @SearchText + '%'";

                    //Filter out the existing searched items
                    if (excludeTerms.Count > 0)
                    {
                        query += string.Format(" and keywords not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                    }
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //customers.Add(string.Format("{0}-{1}", CommonClass.HighlightText(sdr["keywords"].ToString(),searchTerm), CommonClass.HighlightText(sdr["keywords"].ToString(),searchTerm)));
                            customers.Add(string.Format("{0}-{1}", sdr["keywords"], sdr["keywords"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetStatutesTitle(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                List<string> terms = prefix.Split(',').ToList();
                terms = terms.Select(s => s.Trim()).ToList();

                //Extract the term to be searched from the list
                string searchTerm = terms.LastOrDefault().ToString().Trim();

                //Return if Search Term is empty
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return new string[0];
                }

                //Populate the terms that need to be filtered out
                List<string> excludeTerms = new List<string>();
                if (terms.Count > 1)
                {
                    terms.RemoveAt(terms.Count - 1);
                    excludeTerms = terms;
                }

                conn.ConnectionString = DBHelper.GetConnectionString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "select title from tbl_Statutes where active=1 and Pri_Sec!='Version' and isdeleted=0 and active=1 and " +
                    "title like '%'+ @SearchText + '%' order by title";

                    //Filter out the existing searched items
                    //if (excludeTerms.Count > 0)
                    //{
                    //    query += string.Format(" and title not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                    //}
                    cmd.CommandText = query;// "[sp_get_statutes_livesearch]";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["title"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetStatutesTitleBySectionSearch(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {


                using (SqlDataReader sdr = objstat.GetStatutesListBySectionReader(prefix))
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["title"]));
                    }
                }

                return customers.ToArray();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetJudgesTitle(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                List<string> terms = prefix.Split(',').ToList();
                terms = terms.Select(s => s.Trim()).ToList();

                //Extract the term to be searched from the list
                string searchTerm = terms.LastOrDefault().ToString().Trim();

                //Return if Search Term is empty
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return new string[0];
                }

                //Populate the terms that need to be filtered out
                List<string> excludeTerms = new List<string>();
                if (terms.Count > 1)
                {
                    terms.RemoveAt(terms.Count - 1);
                    excludeTerms = terms;
                }

                conn.ConnectionString = DBHelper.GetConnectionString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "select JudgeName from tbl_Judges where " +
                    "JudgeName like '%'+ @SearchText + '%'";

                    //Filter out the existing searched items
                    if (excludeTerms.Count > 0)
                    {
                        query += string.Format(" and JudgeName not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                    }
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["JudgeName"], sdr["JudgeName"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAdvocateTitle(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                List<string> terms = prefix.Split(',').ToList();
                terms = terms.Select(s => s.Trim()).ToList();

                //Extract the term to be searched from the list
                string searchTerm = terms.LastOrDefault().ToString().Trim();

                //Return if Search Term is empty
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return new string[0];
                }

                //Populate the terms that need to be filtered out
                List<string> excludeTerms = new List<string>();
                if (terms.Count > 1)
                {
                    terms.RemoveAt(terms.Count - 1);
                    excludeTerms = terms;
                }

                conn.ConnectionString = DBHelper.GetConnectionString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "select AdvocateName from tbl_Advocates where " +
                    "AdvocateName like '%'+ @SearchText + '%'";

                    //Filter out the existing searched items
                    if (excludeTerms.Count > 0)
                    {
                        query += string.Format(" and AdvocateName not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                    }
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["AdvocateName"], sdr["AdvocateName"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetPracticeAreaKeywords(string prefix,string PracticeAreaID)
        {
            //PracticeAreaID = EncryptDecryptHelper.Decrypt(PracticeAreaID.ToString());
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                List<string> terms = prefix.Split(',').ToList();
                terms = terms.Select(s => s.Trim()).ToList();

                //Extract the term to be searched from the list
                string searchTerm = terms.LastOrDefault().ToString().Trim();

                //Return if Search Term is empty
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return new string[0];
                }

                //Populate the terms that need to be filtered out
                List<string> excludeTerms = new List<string>();
                if (terms.Count > 1)
                {
                    terms.RemoveAt(terms.Count - 1);
                    excludeTerms = terms;
                }

                conn.ConnectionString = DBHelper.GetConnectionString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "select Keyword from tbl_PracticeArea_Keywords where isdeleted=0 and PracticeAreaID=" + PracticeAreaID + " and Keyword like '%'+ @SearchText + '%'";

                    //Filter out the existing searched items
                    if (excludeTerms.Count > 0)
                    {
                        query += string.Format(" and Keyword not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                    }
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}",sdr["Keyword"], sdr["Keyword"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetDictionary(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                List<string> terms = prefix.Split(',').ToList();
                terms = terms.Select(s => s.Trim()).ToList();

                //Extract the term to be searched from the list
                string searchTerm = terms.LastOrDefault().ToString().Trim();

                //Return if Search Term is empty
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return new string[0];
                }

                //Populate the terms that need to be filtered out
                List<string> excludeTerms = new List<string>();
                if (terms.Count > 1)
                {
                    terms.RemoveAt(terms.Count - 1);
                    excludeTerms = terms;
                }

                conn.ConnectionString = DBHelper.GetConnectionString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "select Word from tbl_Dictionary where " +
                    "Word like '%'+ @SearchText + '%'";

                    //Filter out the existing searched items
                    if (excludeTerms.Count > 0)
                    {
                        query += string.Format(" and Word not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                    }
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["Word"], sdr["Word"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }
    }
}
