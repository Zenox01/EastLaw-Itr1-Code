using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;
using System.Configuration;

namespace EastlawUI_v2
{
    public static class CommonClass
    {
       
        private static readonly char[] DefaultDelimeters = new char[] { '—','-' };
     
        public static string LastWord(this string StringValue)
        {
            return LastWord(StringValue, DefaultDelimeters);
        }

        public static string LastWord(this string StringValue, char[] Delimeters)
        {
            int index = StringValue.LastIndexOfAny(Delimeters);

            if (index > -1)
                return StringValue.Substring(index).Remove(0,1);
            else
                return null;
        }
        public static string FirstWords(string input, int numberWords)
        {
            try
            {
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
        public static string Format_Appeallant_Field(string txt)
        {
            try
            {
                string[] arr1 = new string[] { "---Respondent", "---Appellants", "---Petitioner", "---Appellant", "--- Petitioner", "-- -Petitioners", "-- -Petitioner", "--- Petitioner", "- --Petitioner", "---Appellant", "--Petitioner", "---Appellant", "--- Petitioners", "---petitioner", "--Appellant", "-Appellant", "--- Appellant", "--- Applicant","-Petitioner"};

                string formatedtxt = txt;
                for (int i = 0; i < arr1.Length; i++)
                {
                    formatedtxt = formatedtxt.Replace(arr1[i].ToString(), "");
                }
                return formatedtxt;
            }
            catch
            {
                return "";
            }
        }
        public static string Format_Respondent_Field(string txt)
        {
            try
            {
                string[] arr1 = new string[] { "--Respondents","--- Respondent","---Respondent","---Respondent","--- Responden!s","—Respondent","—Respondents","-Respondent","---Petitione","---Petitioner"};

                string formatedtxt = txt;
                for (int i = 0; i < arr1.Length; i++)
                {
                    formatedtxt = formatedtxt.Replace(arr1[i].ToString(), "");
                }
                return formatedtxt;
            }
            catch
            {
                return "";
            }
        }
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
        public static string AutoCloseHtmlTags(string inputHtml)
        {
            try
            {
                var regexStartTag = new Regex(@"<(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                      @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|big" +
                      @"|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                      @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                      @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|" +
                      @"header|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|" +
                      @"map|mark|menu|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|" +
                      @"output|p|param|pre|progress|q|rp|rt|ruby|s|samp|script|section|select|small|" +
                      @"source|span|strike|strong|style|sub|summary|sup|table|tbody|td|textarea|" +
                      @"tfoot|th|thead|time|title|tr|track|tt|u|ul|var|video|wbr)(\s\w+.*(\u0022|'))?>");
                var startTagCollection = regexStartTag.Matches(inputHtml);
                var regexCloseTag = new Regex(@"</(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                      @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|" +
                      @"big|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                      @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                      @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|header" +
                      @"|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|menu|" +
                      @"meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|" +
                      @"progress|q|rp|rt|ruby|s|samp|script|section|select|small|source|span|strike|" +
                      @"strong|style|sub|summary|sup|table|tbody|td|textarea|tfoot|th|thead|" +
                      @"time|title|tr|track|tt|u|ul|var|video|wbr)>");
                var closeTagCollection = regexCloseTag.Matches(inputHtml);
                var startTagList = new List<string>();
                var closeTagList = new List<string>();
                var resultClose = "";
                foreach (Match startTag in startTagCollection)
                {
                    startTagList.Add(startTag.Value);
                }
                foreach (Match closeTag in closeTagCollection)
                {
                    closeTagList.Add(closeTag.Value);
                }
                startTagList.Reverse();
                for (int i = 0; i < closeTagList.Count; i++)
                {
                    if (startTagList[i] != closeTagList[i])
                    {
                        int indexOfSpace = startTagList[i].IndexOf(
                                 " ", System.StringComparison.Ordinal);
                        if (startTagList[i].Contains(" "))
                        {
                            startTagList[i].Remove(indexOfSpace);
                        }
                        startTagList[i] = startTagList[i].Replace("<", "</");
                        resultClose += startTagList[i] + ">";
                        resultClose = resultClose.Replace(">>", ">");
                    }
                }
                return inputHtml + resultClose;
            }
            catch(Exception ex)
            {
                return inputHtml;
            }
        }
        public static string GetWords(string text, int maxWordCount)
        {
            if (!string.IsNullOrEmpty(text))
            {
                int wordCounter = 0;
                int stringIndex = 0;
                char[] delimiters = new[] { '\n', ' ', ',', '.' };

                while (wordCounter < maxWordCount)
                {
                    stringIndex = text.IndexOfAny(delimiters, stringIndex + 1);
                    if (stringIndex == -1)
                        return text;

                    ++wordCounter;
                }

                return text.Substring(0, stringIndex);
            }
            else
                return text;
        }
        public static string GetChracter(string text, int maxChracter)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Length > maxChracter)
                {

                    return text.Substring(0, maxChracter);
                }
                else
                    return text;
                
            }
            else
                return text;
        }

        public static string FindSubordinatesStatutesCategory(string txt)
        {
            string[] arr1 = new string[] { "Rules", "Regulations", "Notifications", "S.R.O's", "Circulars", "Circular Letters", "Orders", "General Orders", "others", "Order", "rules", "Rule" };

            string formatedtxt = txt;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (txt.Contains(arr1[i].ToString()))
                    return arr1[i].ToString();

            }
            return "N/A";
        }
        #region GetShortDesc
        public static string GetShortDesc(string content,string searchkeyword)
        {
            string str = "";
            try
            {
               // content = CleanHtml(content);
                content = StripTagsRegex(content);
                if (searchkeyword.Contains("\""))
                {
                    //searchkeyword = searchkeyword.Replace("\" ", "");
                    //searchkeyword = searchkeyword.Replace("\"", "");
                    //searchkeyword = searchkeyword.Replace("and ", "");
                    //searchkeyword = searchkeyword.Replace("AND ", "");

                    string[] Keywords = searchkeyword.Split('\"');
                    if (Keywords.Length > 0)
                    {
                        for (int a = 0; a < Keywords.Length; a++)
                        {
                            if (!string.IsNullOrEmpty(Keywords[a].ToString()))
                            {

                                if (!Keywords[a].ToString().Contains("and"))
                                {
                                    foreach (Match match in Regex.Matches(content, Keywords[a].ToString().Trim(), RegexOptions.IgnoreCase))
                                    {
                                        //Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                                        //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");


                                        str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                                    }
                                }
                            }
                        }
                    }
                }
                else if (string.IsNullOrEmpty(str))
                {
                     
                       searchkeyword = searchkeyword.Replace(" formsof(inflectional, ", " ");
                    searchkeyword = searchkeyword.Replace(" formsof(inflectional,", " ");

                   
                   searchkeyword = searchkeyword.Replace(") ", " ");
                    searchkeyword = searchkeyword.Replace("and ", "");
                    searchkeyword = searchkeyword.Replace("AND ", "");
                    searchkeyword = searchkeyword.Replace("or ", "");
                    searchkeyword = searchkeyword.Replace("OR ", "");
                    searchkeyword = searchkeyword.Replace("of ", "");
                    searchkeyword = searchkeyword.Replace("Of ", "");
                    searchkeyword = searchkeyword.Replace("in ", "");
                    searchkeyword = searchkeyword.Replace("In ", "");
                    searchkeyword = searchkeyword.Replace("The ", "");
                    searchkeyword = searchkeyword.Replace("the ", "");
                    

                    string keywordtxt = "";
                    string[] Keywords = searchkeyword.Trim().Split(' ');
                    if (Keywords.Length > 0)
                    {
                        for (int a = 0; a < Keywords.Length; a++)
                        {
                            int count = 1;
                            foreach (Match match in Regex.Matches(content, Keywords[a].ToString(), RegexOptions.IgnoreCase))
                            {
                                // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                                //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                                str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                                count++;
                                if (count == 3)
                                    break;
                            }
                        }
                    }
                    else
                    {
                        int count = 1;
                        foreach (Match match in Regex.Matches(content, searchkeyword, RegexOptions.IgnoreCase))
                        {
                            // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                            //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                            str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                            count++;
                            if (count == 3)
                                break;
                        }
                    }
                   
                }

                ////Short text for within
                //if (!string.IsNullOrEmpty(searchwithin))
                //{
                    
                //    searchwithin = searchwithin.Replace("\" ", "");
                //    searchwithin = searchwithin.Replace("\"", "");
                //    foreach (Match match in Regex.Matches(content, searchwithin, RegexOptions.IgnoreCase))
                //    {
                //        // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                //        //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                //        str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                //    }
                //    // }
                //    if (string.IsNullOrEmpty(str))
                //    {
                //        searchwithin = searchwithin.Replace("and ", "");
                //        searchwithin = searchwithin.Replace("AND ", "");
                //        searchwithin = searchwithin.Replace("or ", "");
                //        searchwithin = searchwithin.Replace("OR ", "");
                //        searchwithin = searchwithin.Replace("of ", "");
                //        searchwithin = searchwithin.Replace("Of ", "");
                //        searchwithin = searchwithin.Replace("in ", "");
                //        searchwithin = searchwithin.Replace("In ", "");
                //        searchwithin = searchwithin.Replace("The ", "");
                //        searchwithin = searchwithin.Replace("the ", "");


                //        string keywordtxt = "";
                //        string[] Keywords = searchwithin.Split(' ');
                //        if (Keywords.Length > 0)
                //        {
                //            for (int a = 0; a < Keywords.Length; a++)
                //            {
                //                int count = 1;
                //                foreach (Match match in Regex.Matches(content, Keywords[a].ToString(), RegexOptions.IgnoreCase))
                //                {
                //                    // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                //                    //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                //                    str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                //                    count++;
                //                    if (count == 3)
                //                        break;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            int count = 1;
                //            foreach (Match match in Regex.Matches(content, searchwithin, RegexOptions.IgnoreCase))
                //            {
                //                // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                //                //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                //                str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                //                count++;
                //                if (count == 3)
                //                    break;
                //            }
                //        }

                //    }
                //}
                return str.Replace("<br>", "");
            }
            catch {

                return EastlawUI_v2.CommonClass.GetWords(str.Replace("<br>", ""),200);
            }
        }
        public static string GetShortDescWithin(string content, string searchkeyword,string searchwithin)
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
                    str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                }
                // }
                if (string.IsNullOrEmpty(str))
                {
                    searchkeyword = searchkeyword.Replace("and ", "");
                    searchkeyword = searchkeyword.Replace("AND ", "");
                    searchkeyword = searchkeyword.Replace("or ", "");
                    searchkeyword = searchkeyword.Replace("OR ", "");
                    searchkeyword = searchkeyword.Replace("of ", "");
                    searchkeyword = searchkeyword.Replace("Of ", "");
                    searchkeyword = searchkeyword.Replace("in ", "");
                    searchkeyword = searchkeyword.Replace("In ", "");
                    searchkeyword = searchkeyword.Replace("The ", "");
                    searchkeyword = searchkeyword.Replace("the ", "");


                    string keywordtxt = "";
                    string[] Keywords = searchkeyword.Split(' ');
                    if (Keywords.Length > 0)
                    {
                        for (int a = 0; a < Keywords.Length; a++)
                        {
                            int count = 1;
                            foreach (Match match in Regex.Matches(content, Keywords[a].ToString(), RegexOptions.IgnoreCase))
                            {
                                // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                                //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                                str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                                count++;
                                if (count == 3)
                                    break;
                            }
                        }
                    }
                    else
                    {
                        int count = 1;
                        foreach (Match match in Regex.Matches(content, searchkeyword, RegexOptions.IgnoreCase))
                        {
                            // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                            //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                            str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                            count++;
                            if (count == 3)
                                break;
                        }
                    }

                }

                //Short text for within
                if (!string.IsNullOrEmpty(searchwithin))
                {

                    searchwithin = searchwithin.Replace("\" ", "");
                    searchwithin = searchwithin.Replace("\"", "");
                    foreach (Match match in Regex.Matches(content, searchwithin, RegexOptions.IgnoreCase))
                    {
                        // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                        //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                        str = str + "<b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                    }
                    // }
                    if (string.IsNullOrEmpty(str))
                    {
                        searchwithin = searchwithin.Replace("and ", "");
                        searchwithin = searchwithin.Replace("AND ", "");
                        searchwithin = searchwithin.Replace("or ", "");
                        searchwithin = searchwithin.Replace("OR ", "");
                        searchwithin = searchwithin.Replace("of ", "");
                        searchwithin = searchwithin.Replace("Of ", "");
                        searchwithin = searchwithin.Replace("in ", "");
                        searchwithin = searchwithin.Replace("In ", "");
                        searchwithin = searchwithin.Replace("The ", "");
                        searchwithin = searchwithin.Replace("the ", "");


                        string keywordtxt = "";
                        string[] Keywords = searchwithin.Split(' ');
                        if (Keywords.Length > 0)
                        {
                            for (int a = 0; a < Keywords.Length; a++)
                            {
                                int count = 1;
                                foreach (Match match in Regex.Matches(content, Keywords[a].ToString(), RegexOptions.IgnoreCase))
                                {
                                    // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                                    //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                                    str = str + "<br/> <b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                                    count++;
                                    if (count == 4)
                                        break;
                                }
                            }
                        }
                        else
                        {
                            int count = 1;
                            foreach (Match match in Regex.Matches(content, searchwithin, RegexOptions.IgnoreCase))
                            {
                                // Response.Write("Found " + match.Value + " at position " + match.Index + " <br>");
                                //Response.Write(Left1(sentence, int.Parse(match.Index.ToString())) + "<br>");
                                str = str + "<br/><b>...</b>" + Left1(content, int.Parse(match.Index.ToString())) + "<b>...</b>";
                                count++;
                                if (count == 4)
                                    break;
                            }
                        }

                    }
                }
                return str.Replace("<br>", "");
            }
            catch
            {

                return EastlawUI_v2.CommonClass.GetWords(str.Replace("<br>", ""), 200);
            }
        }
        static string Left1(String input, int length)
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
                    if (input.Length >= (length - 100))
                    {
                        Startlen = length - 100;
                    }
                    else
                    {
                        Startlen = length;

                    }
                    if (input.Length <= (length + 100))
                    {
                        EndLen = 5;
                    }
                    else
                    {
                        EndLen =  100;
                    }
                    //if(input.Length <= (length + 100))
                    //txt = input.Substring(length - 100, length + 50);
                   // txt = input.Substring(length - 100, 100);

                    //txt = input.Substring(Startlen, EndLen);
                    txt = input.Substring(length, EndLen);
                    //txt = input.Substring(Startlen, EndLen - 10);
                }
                //return (input.Length < length) ? input : input.Substring(0, length);
                //return (input.Length < length) ? input : input.Substring(length - 10, length + 10);
                return txt;
            }
            catch(Exception ex)
            {
                //return EastLawUI.CommonClass.GetWords(input,200);
                return "";
            }
        }
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        #endregion
        #region GetCitationVariations
        public static string GetInsideCitationSearch(string content, string searchkeyword)
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
                    str = str + FirstWords(GetInsideCitationSearchLeft1(content, int.Parse(match.Index.ToString())),3) + ",";
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
                    if (input.Length >= (length - 20))
                    {
                        Startlen = length - 20;
                    }
                    else
                    {
                        Startlen = length;

                    }
                    if (input.Length <= (length + 20))
                    {
                        EndLen = 5;
                    }
                    else
                    {
                        EndLen = 20;
                    }
                    //if(input.Length <= (length + 100))
                    //txt = input.Substring(length - 100, length + 50);
                    // txt = input.Substring(length - 100, 100);

                    //txt = input.Substring(Startlen, EndLen);
                    txt = input.Substring(length, EndLen);
                    txt = txt.Replace(",", "").Replace("(", "").Replace(")", "");
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
       

        #endregion

        public static string CleanHtml(string html)
        {
            // start by completely removing all unwanted tags 
            html = Regex.Replace(html, @"<[/]?(font|span|xml|del|ins|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);
            // then run another pass over the html (twice), removing unwanted attributes 
            html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
            return html;
        }
        public static string RemoveHTML(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
        public static string FormatSearchWord(string word)
        {
            string keyword = word;
            keyword = keyword.Replace("(", " ");
            keyword = keyword.Replace(")", " ");
            keyword = keyword.Replace("/", " ");
            keyword = keyword.Replace("\\", " ");
            keyword = keyword.Replace(",", " ");
            if (keyword.Contains("\""))
            {
                return keyword;
            }
            else if (keyword.Contains(" "))
            {
                if (!keyword.Contains("\""))
                {
                    if (!ExactMatch(keyword, "and"))
                    {
                        if (!ExactMatch(keyword, "or"))
                        {

                            if (!keyword.Contains("\""))
                            {
                                // keyword = "\"" + keyword + "\"";
                                keyword = keyword.Replace(" ", " and ");
                            }
                            else
                            {
                                keyword = "\"" + keyword + "\"";
                            }
                            // }

                        }


                    }
                }
                //New Formating for FORMOF

                keyword = keyword.Replace("\" ", "");
                keyword = keyword.Replace("\"", "");
                keyword = keyword.Replace("and ", "");
                keyword = keyword.Replace("AND ", "");
                keyword = keyword.Replace("or ", "");
                keyword = keyword.Replace("OR ", "");
                keyword = keyword.Replace("Of ", "");
                keyword = keyword.Replace("of ", "");
                keyword = keyword.Replace("in ", "");
                keyword = keyword.Replace("In ", "");
                keyword = keyword.Replace("The ", "");
                keyword = keyword.Replace("the ", "");

                string[] KeywordsLst = keyword.Split(' ');
                string Keywordnew = "";
                if (KeywordsLst.Length > 0)
                {

                    // Keywordnew = Keywordnew + "'";
                    for (int a = 0; a < KeywordsLst.Length; a++)
                    {
                        //'formsof(inflectional,functions) AND formsof(inflectional,comprehend) AND formsof(inflectional,powers) AND formsof(inflectional,duties)'
                        if (!string.IsNullOrEmpty(KeywordsLst[a].ToString()))
                            Keywordnew = Keywordnew + " formsof(inflectional," + KeywordsLst[a].ToString().Trim() + ") AND";
                    }
                    Keywordnew = Keywordnew.Remove(Keywordnew.Length - 3);

                    //Keywordnew = Keywordnew + "'";
                }
                return Keywordnew;


            }
            else
            {

                keyword = keyword.Replace("\" ", "");
                keyword = keyword.Replace("\"", "");

                return " formsof(inflectional, " + keyword.Trim() + ") ";
            }
            

            return keyword;
        }
        public static string FormatSearchWordForShortDescription(string word)
        {
            string keyword = word;
            keyword = keyword.Replace("(", " ");
            keyword = keyword.Replace(")", " ");
            keyword = keyword.Replace("/", " ");
            keyword = keyword.Replace("\\", " ");
            if (keyword.Contains(" "))
            {
                if (!keyword.Contains("\""))
                {
                    if (!ExactMatch(keyword, "and"))
                    {
                        if (!ExactMatch(keyword, "or"))
                        {

                            if (!keyword.Contains("\""))
                            {
                                // keyword = "\"" + keyword + "\"";
                                keyword = keyword.Replace(" ", " and ");
                            }
                            else
                            {
                                keyword = "\"" + keyword + "\"";
                            }
                            // }

                        }


                    }
                }

            }

            return keyword;
        }
        public static string FormatSearchWordWithin(string word)
        {
            string keyword = word;
            keyword = keyword.Replace("(", " ");
            keyword = keyword.Replace(")", " ");
            keyword = keyword.Replace("/", " ");
            keyword = keyword.Replace("\\", " ");
            if (keyword.Contains(" "))
            {
                if (!keyword.Contains("\""))
                {
                    if (!ExactMatch(keyword, "and"))
                    {
                        if (!ExactMatch(keyword, "or"))
                        {

                            if (!keyword.Contains("\""))
                            {
                                 keyword = "\"" + keyword + "\"";
                                //keyword = keyword.Replace(" ", " and ");
                            }
                            else
                            {
                                keyword = "\"" + keyword + "\"";
                            }
                            // }

                        }


                    }
                }

            }

            return keyword;
        }
        public static string FormatSearchWordP1(string word)
        {
            string keyword = word;

            if (!keyword.Contains("\""))
            {
                keyword = "\"" + keyword + "\"";
            }

            return keyword;
        }
        public static string RemoveExtraWordsForHiglight(string word)
        {
            string keyword = word;
            if (keyword.Contains("\""))
            {
              //  keyword = keyword.Replace("and ", "");
                keyword = keyword.Replace("AND ", "and");
               // keyword = keyword.Replace("\" ", "");
                //keyword = keyword.Replace("\"", "");
            }
            else
            {
                keyword = keyword.Replace("\" ", "");
                keyword = keyword.Replace("\"", "");
                keyword = keyword.Replace("and ", "");
                keyword = keyword.Replace("AND ", "");
                keyword = keyword.Replace("or ", "");
                keyword = keyword.Replace("OR ", "");
                keyword = keyword.Replace("Of ", "");
                keyword = keyword.Replace("of ", "");
                keyword = keyword.Replace("in ", "");
                keyword = keyword.Replace("In ", "");
                keyword = keyword.Replace("The ", "");
                keyword = keyword.Replace("the ", "");
            }
            

            return keyword;
        }
        public static string RemoveExtraWordsForHiglightWithin(string word)
        {
            string keyword = word;
            //if (keyword.Contains("\""))
            //{

            //    // keyword = keyword.Replace("\" ", "");
            //    //keyword = keyword.Replace("\"", "");
            //}
            //else
            //{
                //////////keyword = keyword.Replace("\" ", " ");
                //////////keyword = keyword.Replace("\"", " ");


                keyword = keyword.Replace("and ", " ");
                keyword = keyword.Replace("AND ", " ");
                keyword = keyword.Replace("or ", " ");
                keyword = keyword.Replace("OR ", " ");
                keyword = keyword.Replace("Of ", " ");
                keyword = keyword.Replace("of ", " ");
                keyword = keyword.Replace("in ", " ");
                keyword = keyword.Replace("In ", " ");
                keyword = keyword.Replace("The ", " ");
                keyword = keyword.Replace("the ", " ");
         //   }


            return keyword;
        }
       public static bool ExactMatch(string input, string match)
        {

            return Regex.IsMatch(input, string.Format(@"\b{0}\b", Regex.Escape(match)), RegexOptions.IgnoreCase);

        }
       public static string RemoveSomeCharacters(string word)
       {
           string keyword = word;
           keyword = keyword.Replace("/", "");
           keyword = keyword.Replace("/", "");
           keyword = keyword.Replace("^", "");
           keyword = keyword.Replace("~", "");
           keyword = keyword.Replace(":", "");
           keyword = keyword.Replace(">", "");
           keyword = keyword.Replace("<", "");
           keyword = keyword.Replace("*", "");
           keyword = keyword.Replace("#", "");
           keyword = keyword.Replace("@", "");
           keyword = keyword.Replace("!", "");
           keyword = keyword.Replace("`", "");
           keyword = keyword.Replace("'", "");
           keyword = keyword.Replace("%", "");
           //keyword = keyword.Replace("(", "");
           //keyword = keyword.Replace(")", "");


           return keyword;
       }
       public static string HighlightText(string InputTxt, string SearchChar)
       {
           CommonClassParts objCls = new CommonClassParts();
           string Search_Str = SearchChar;

           Regex RegExp;//= new Regex();

           if (Search_Str.Contains("\""))
               RegExp = new Regex(Search_Str.Replace("\"", "").Trim(), RegexOptions.IgnoreCase);
           //if (Search_Str.Contains("and"))
           //    RegExp = new Regex(Search_Str.Replace("and ", "").Trim(), RegexOptions.IgnoreCase);
           else
               RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);


           return RegExp.Replace(InputTxt, new MatchEvaluator(objCls.ReplaceKeyWords));

       }
       public static void GetIPLocation(string IP, ref string Country, ref string RegionName, ref string City)
       {

           //string APIKey = "76511e33ff8498c62f458bea0a641b144b031bdb1e3eade661df53a39815cb27";
           

           try
           {
               string Val = ConfigurationSettings.AppSettings["IPToLocationEnable"].ToString();
               if (!String.IsNullOrEmpty(Val))
               {
                   if (Val == "YES")
                   {
                       string url = "http://freegeoip.net/xml/" + IP;
                       //using (System.Net.WebClient client = new System.Net.WebClient())
                       //{
                       //    string json = client.DownloadString(url);
                       //    //DataSet theDataSet = new DataSet();
                       //    //theDataSet.ReadXml(json);
                       //    //location = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Location>(json);

                       //}

                       XmlDocument doc = new XmlDocument();
                       doc.Load(url);
                       XmlNodeList nodeLstCountry = doc.GetElementsByTagName("CountryName");
                       XmlNodeList nodeLstRegion = doc.GetElementsByTagName("RegionName");
                       XmlNodeList nodeLstCity = doc.GetElementsByTagName("City");

                       //IP = "" + nodeLstCity[0].InnerText + "<br>" + IP;
                       Country = nodeLstCountry[0].InnerText;
                       RegionName = nodeLstRegion[0].InnerText;
                       City = nodeLstCity[0].InnerText;
                   }
               }
                   

               //Response.Write(IP);
           }
           catch
           { }

       }
    

    }
    public class CommonClassParts
    {
        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }
    }
}