using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;
using System.Configuration;
namespace AdminUtility
{
    class Common
    {
        EastLawBL.Cases objc = new EastLawBL.Cases();
        public static string GetCaseSummary(string txt)
        {
            try
            {
                List<string> mat = ExtractFromString(txt, "##TS##", "##TE##");
                string summr = "";
                foreach (string prime in mat)
                {
                    //summr = summr + "<p>" + prime.Replace("<p>", "").Replace("</p>", "") + "</p>";
                    summr = summr + "<p>" + prime + "</p>";

                }
                return summr;
            }
            catch
            {
                return "";
            }
        }
        private static List<string> ExtractFromString(string text, string startString, string endString)
        {
            List<string> matched = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = text.IndexOf(startString);
                indexEnd = text.IndexOf(endString);
                if (indexStart != -1 && indexEnd != -1)
                {
                    matched.Add(text.Substring(indexStart + startString.Length,
                        indexEnd - indexStart - startString.Length));
                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                    exit = true;
            }
            return matched;
        }
        #region GetCitationVariations
        public static string GetInsideCitationSearch(string content, string searchkeyword,int NoOfWords)
        {
            string str = "";
            try
            {
                // content = CleanHtml(content);
                content = StripTagsRegex(content);
                //if (searchkeyword.Contains("\""))
                //{
                searchkeyword = searchkeyword.Replace("\" ", "");
                searchkeyword = searchkeyword.Replace("\"", "");
                foreach (Match match in Regex.Matches(content, searchkeyword, RegexOptions.IgnoreCase))
                {
                    // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                    //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                    str = str + FirstWords(GetInsideCitationSearchLeft1(content, int.Parse(match.Index.ToString())), NoOfWords) + ",";
                }
                // }
                //if (string.IsNullOrEmpty(str))
                //{
                //    searchkeyword = searchkeyword.Replace("and ", "");
                //    searchkeyword = searchkeyword.Replace("AND ", "");
                //    searchkeyword = searchkeyword.Replace("or ", "");
                //    searchkeyword = searchkeyword.Replace("OR ", "");
                //    searchkeyword = searchkeyword.Replace("of ", "");
                //    searchkeyword = searchkeyword.Replace("Of ", "");
                //    searchkeyword = searchkeyword.Replace("in ", "");
                //    searchkeyword = searchkeyword.Replace("In ", "");
                //    searchkeyword = searchkeyword.Replace("The ", "");
                //    searchkeyword = searchkeyword.Replace("the ", "");

                str = str.Replace("]\n,", "");
                str = str.Replace(";", "");
                


                //    string keywordtxt = "";
                //    string[] Keywords = searchkeyword.Split(' ');
                //    if (Keywords.Length > 0)
                //    {
                //        for (int a = 0; a < Keywords.Length - 1; a++)
                //        {

                //            foreach (Match match in Regex.Matches(content, Keywords[a].ToString(), RegexOptions.IgnoreCase))
                //            {
                //                // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                //                //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                //                str = str +  GetInsideCitationSearchLeft1(content, int.Parse(match.Index.ToString()))+",";
                //            }
                //        }
                //    }
                //    else
                //    {
                //        foreach (Match match in Regex.Matches(content, searchkeyword, RegexOptions.IgnoreCase))
                //        {
                //            // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                //            //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                //            str = str + GetInsideCitationSearchLeft1(content, int.Parse(match.Index.ToString())) + ",";
                //        }
                //    }

                //}
                return str;
            }
            catch
            {

                return "";// EastLawUI.CommonClass.GetWords(str.Replace("<br>", ""), 200);
            }
        }
        public static string GetInsideSectionSearch(string content, string searchkeyword)
        {
            string str = "";
            try
            {
                // content = CleanHtml(content);
                content = StripTagsRegex(content);
                //if (searchkeyword.Contains("\""))
                //{
                searchkeyword = searchkeyword.Replace("\" ", "");
                searchkeyword = searchkeyword.Replace("\"", "");

                if (searchkeyword.Contains("-") || searchkeyword.Contains("-"))
                {
                    //searchkeyword = "@" + searchkeyword;
                    foreach (Match match in Regex.Matches(content, @"\b" + searchkeyword + @"\b", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                    //   foreach (Match match in Regex.Matches(content, searchkeyword))
                    {
                        // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                        //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                        //str = str + FirstWords(GetInsideSectionSearchLeft1(content, int.Parse(match.Index.ToString())),4) + "|";
                        str = str + GetInsideSectionSearchLeft1(content, int.Parse(match.Index.ToString())) + "|";
                    }
                }
                else
                {
                    foreach (Match match in Regex.Matches(content,searchkeyword, RegexOptions.Singleline | RegexOptions.IgnoreCase))
                    //   foreach (Match match in Regex.Matches(content, searchkeyword))
                    {
                        // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                        //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                        //str = str + FirstWords(GetInsideSectionSearchLeft1(content, int.Parse(match.Index.ToString())),4) + "|";
                        str = str + GetInsideSectionSearchLeft1(content, int.Parse(match.Index.ToString())) + "|";
                    }
                }
                // }
                //if (string.IsNullOrEmpty(str))
                //{
                //    searchkeyword = searchkeyword.Replace("and ", "");
                //    searchkeyword = searchkeyword.Replace("AND ", "");
                //    searchkeyword = searchkeyword.Replace("or ", "");
                //    searchkeyword = searchkeyword.Replace("OR ", "");
                //    searchkeyword = searchkeyword.Replace("of ", "");
                //    searchkeyword = searchkeyword.Replace("Of ", "");
                //    searchkeyword = searchkeyword.Replace("in ", "");
                //    searchkeyword = searchkeyword.Replace("In ", "");
                //    searchkeyword = searchkeyword.Replace("The ", "");
                //    searchkeyword = searchkeyword.Replace("the ", "");


                //    string keywordtxt = "";
                //    string[] Keywords = searchkeyword.Split(' ');
                //    if (Keywords.Length > 0)
                //    {
                //        for (int a = 0; a < Keywords.Length - 1; a++)
                //        {

                //            foreach (Match match in Regex.Matches(content, Keywords[a].ToString(), RegexOptions.IgnoreCase))
                //            {
                //                // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                //                //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                //                str = str +  GetInsideCitationSearchLeft1(content, int.Parse(match.Index.ToString()))+",";
                //            }
                //        }
                //    }
                //    else
                //    {
                //        foreach (Match match in Regex.Matches(content, searchkeyword, RegexOptions.IgnoreCase))
                //        {
                //            // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                //            //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                //            str = str + GetInsideCitationSearchLeft1(content, int.Parse(match.Index.ToString())) + ",";
                //        }
                //    }

                //}
                return str;
            }
            catch
            {

                return "";// EastLawUI.CommonClass.GetWords(str.Replace("<br>", ""), 200);
            }
        }

            static string GetInsideCitationSearchLeft1(String input, int length)
            {
                try
                {
                    string txt = "";
                    int Startlen = 0;
                    int EndLen = 0;
                    input = StripTagsRegex(input);
                    if (input.Length < length)
                    {
                        txt = input;
                    }
                    else
                    {
                        if (input.Length >= (length - 40))
                        {
                            Startlen = length - 40;
                        }
                        else
                        {
                            Startlen = length;

                        }
                        if (input.Length <= (length + 40))
                        {
                            EndLen = 10;
                        }
                        else
                        {
                            EndLen = 40;
                        }
                        //if(input.Length <= (length + 100))
                        //txt = input.Substring(length - 100, length + 50);
                        // txt = input.Substring(length - 100, 100);

                        //txt = input.Substring(Startlen, EndLen);
                        txt = input.Substring(length, EndLen);
                        txt = txt.Replace(", ","").Replace(",", "").Replace("(", "").Replace(")", "");
                        //txt = input.Substring(Startlen, EndLen - 10);
                    }
                    //return (input.Length < length) ? input : input.Substring(0, length);
                    //return (input.Length < length) ? input : input.Substring(length - 10, length + 10);
                    return txt;
                }
                catch (Exception ex)
                {
                    //return EastLawUI.CommonClass.GetWords(input,200);
                    return "";
                }
            }
        static string GetInsideSectionSearchLeft1(String input, int length)
        {
            try
            {
                string txt = "";
                int Startlen = 0;
                int EndLen = 0;
                input = StripTagsRegex(input);
                if (input.Length < length)
                {
                    txt = input;
                }
                else
                {
                    if (input.Length >= (length - 35))
                    {
                        Startlen = length - 35;
                    }
                    else
                    {
                        Startlen = length;

                    }
                    if (input.Length <= (length + 35))
                    {
                        EndLen = 5;
                    }
                    else
                    {
                        EndLen = 35;
                    }
                    //if(input.Length <= (length + 100))
                    //txt = input.Substring(length - 100, length + 50);
                    // txt = input.Substring(length - 100, 100);

                    //txt = input.Substring(Startlen, EndLen);
                    txt = input.Substring(length, EndLen);
                    //txt = txt.Replace("&", ",").Replace("-", "|").Replace("and", ",").Replace("to", ",").Replace("(", "|").Replace(")", "|").Replace("/", "|").Replace("\\", "|").Replace(",", "|");
                    txt = txt.Replace("&", ",").Replace("-", "|").Replace("and", ",").Replace("to", ",").Replace("/", "|").Replace("\\", "|").Replace(",", "|");
                    //txt = input.Substring(Startlen, EndLen - 10);
                }
                //return (input.Length < length) ? input : input.Substring(0, length);
                //return (input.Length < length) ? input : input.Substring(length - 10, length + 10);
                return txt;
            }
            catch (Exception ex)
            {
                //return EastLawUI.CommonClass.GetWords(input,200);
                return "";
            }
        }

        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
        public static string FirstWords(string input, int numberWords)
        {
            try
            {
                input = input.Replace(":", "");
                // Number of words we still want to display.
                int words = numberWords;
                // Loop through entire summary.
                for (int i = 0; i < input.Length; i++)
                {
                    // Increment words on a space.
                    if (input[i] == ' ')
                    {
                        words--;
                    }
                    // If we have no more words to display, return the substring.
                    if (words == 0)
                    {
                        return input.Substring(0, i);
                    }
                }
                return input;
            }
            catch (Exception)
            {
                // Log the error.
            }
            return string.Empty;
        }
        public static string MakeFirstCap(string InputTxt)
        {

            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(InputTxt.ToLower());

            //   return ("<span class=highlight>" + Search_Str + "</span>");
        }

        public static string RemoveSectionExceptionWords(string txt)
        {
            try
            {
                string[] arr1 = new string[] { "Section","section","sections", "Sections", "Ss", "Ss.", "s.", "S.", "----Ss." };

                string formatedtxt = txt;
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (ValidNumber(txt))
                    {
                        return txt;
                    }
                    else
                    {
                        if (CheckWordHaveException(txt, arr1))
                        {
                            formatedtxt = formatedtxt.Replace(arr1[i].ToString(), "").Trim();
                        }
                        else
                            return "";
                    }
                    
                }
                return formatedtxt;
            }
            catch
            {
                return "";
            }
        }
        public static string RemoveRuleExceptionWords(string txt)
        {
            try
            {
                string[] arr1 = new string[] { "Rule", "-rules", "Rr.", "rr.", "----R.", "-Rr.", "---R." };

                string formatedtxt = txt;
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (ValidNumber(txt))
                    {
                        return txt;
                    }
                    else
                    {
                        if (CheckWordHaveException(txt, arr1))
                        {
                            formatedtxt = formatedtxt.Replace(arr1[i].ToString(), "").Trim();
                        }
                        else
                            return "";
                    }

                }
                return formatedtxt;
            }
            catch
            {
                return "";
            }
        }
        public static string RemoveArticleExceptionWords(string txt)
        {
            try
            {
                string[] arr1 = new string[] { "Art.", "art.", "Arts.", "----Art", "--Arts.", "-Arts.", "--Arts.", "Article", "---Arts.", "Articles" };

                string formatedtxt = txt;
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (ValidNumber(txt))
                    {
                        return txt;
                    }
                    else
                    {
                        //txt = arr1[i].ToString() +" " +txt;
                        if (CheckWordHaveException(txt, arr1))
                        {
                            formatedtxt = formatedtxt.Replace(arr1[i].ToString(), "").Trim();
                        }
                        else
                            return "";
                    }

                }
                return formatedtxt;
            }
            catch
            {
                return "";
            }
        }
        public static bool CheckWordHaveException(string Word, string[] Arry)
        {
            string stringToCheck = Word;
            string[] stringArray = Arry;
            foreach (string x in stringArray)
            {
                if (stringToCheck.Contains(x))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ValidNumber(string Num)
        {
            try
            {
                int n;
                bool isNumeric = int.TryParse(Num, out n);
                if (isNumeric == false)
                {
                    if (Num.Contains("(") || Num.Contains(")"))
                        return true;
                }
                return isNumeric;
            }
            catch
            {
                return false;
            }

        }
        public static string FormatStatutesTaggingWord(string word)
        {
            string keyword = word;
            keyword = keyword.Replace(",", " ");
            keyword = keyword.Replace("(", " ");
            keyword = keyword.Replace(")", " ");

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            keyword = regex.Replace(keyword, " ");

            keyword = keyword.Replace(" ", ", ");

            return keyword;
        }
        #endregion
        #region GetLatestCaseFromMainTo Front
        public void InsertLatestCaseFromMainToFront()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objc.GetLatestCasesFromMain();
                if (dt != null)
                {
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        objc.InsertLatestCasesIntoFrontView(int.Parse(dt.Rows[a]["ID"].ToString()),
                            dt.Rows[a]["Citation"].ToString(), dt.Rows[a]["Appeallant"].ToString(), dt.Rows[a]["Respondent"].ToString(),
                            dt.Rows[a]["JDate"].ToString(), dt.Rows[a]["Court"].ToString(), dt.Rows[a]["ReturnCasePracticeAreas"].ToString(),
                            dt.Rows[a]["ReturnCaseTaggedStatute"].ToString(), dt.Rows[a]["casesummary"].ToString(), 9999999);
                    }
                }
            }
            catch { }
        }
        #endregion
    }
}
