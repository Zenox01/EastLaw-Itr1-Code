using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtility
{
    class CitationsSearchMiscWise
    {
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        public void GetSectionsSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
                string tobesearched = "";
                string aa = "";
                for (int i = 1; i < 601; i++)
                {
                    string[] arr1HeadNotes = new string[] { "Section", "section", "sections", "Sections", "Ss", "Ss.", "s.", "S.", "----Ss." };

                    for (int Lst = 0; Lst < arr1HeadNotes.Length; Lst++)
                    {
                        string[] tobesearchLst = new string[] { arr1HeadNotes[Lst].ToString() + " " + i, arr1HeadNotes[Lst].ToString() + "" + i };
                        //tobesearched = arr1HeadNotes[Lst].ToString() + " " + i;
                        for (int TOLst = 0; TOLst < tobesearchLst.Length; TOLst++)
                        {
                            //aa = Common.GetInsideSectionSearch(HeadNotes, tobesearched);
                            aa = Common.GetInsideSectionSearch(HeadNotes, tobesearchLst[TOLst].ToString());
                            if (!string.IsNullOrEmpty(aa))
                            {
                                string[] wordsHeadNotes = aa.Split('|');
                                string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                                foreach (string SearctTxt in UwordsHeadNotes)
                                {
                                    if (!string.IsNullOrEmpty(SearctTxt))
                                    {
                                        string[] wordMulti = SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "").Split(',');
                                        string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                                        if (UwordsMultiHeadNotes.Length > 0)
                                        {

                                            foreach (string SearctTxtMulti in UwordsMultiHeadNotes)
                                            {
                                                //if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1)))
                                                //{
                                                    string aaa = Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1);
                                                if(!string.IsNullOrEmpty(aaa))
                                                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Section " + Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.Trim()), 1), "Section", "Strong", "HeadNotes");
                                               // }
                                            }
                                        }
                                        else
                                        {
                                            string aaa = Common.FirstWords(UwordsMultiHeadNotes[0].ToString(), 2);
                                            objcase.AddCasesInsideSection_Rule_Article(CaseID, Common.FirstWords(aa.Trim(), 1), "Section", "Strong", "HeadNotes");
                                        }

                                        //

                                    }
                                }
                            }
                        }
                    }


                    // Judgment

                    for (int Lst = 0; Lst < arr1HeadNotes.Length; Lst++)
                    {
                        string[] tobesearchLst = new string[] { arr1HeadNotes[Lst].ToString() + " " + i, arr1HeadNotes[Lst].ToString() + "" + i };
                        //tobesearched = arr1HeadNotes[Lst].ToString() + " " + i;
                        for (int TOLst = 0; TOLst < tobesearchLst.Length; TOLst++)
                        {
                            //aa = Common.GetInsideSectionSearch(HeadNotes, tobesearched);
                            aa = Common.GetInsideSectionSearch(Judgment, tobesearchLst[TOLst].ToString());
                            if (!string.IsNullOrEmpty(aa))
                            {
                                string[] wordsHeadNotes = aa.Split('|');
                                string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                                foreach (string SearctTxt in UwordsHeadNotes)
                                {
                                    if (!string.IsNullOrEmpty(SearctTxt))
                                    {
                                        string[] wordMulti = SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "").Split(',');
                                        string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                                        if (UwordsMultiHeadNotes.Length > 0)
                                        {

                                            foreach (string SearctTxtMulti in UwordsMultiHeadNotes)
                                            {
                                                
                                                // if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1))
                                                //{
                                                    string aaa = Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1);
                                                if(!string.IsNullOrEmpty(aaa))
                                                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Section " + Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.Trim()), 1), "Section", "Strong", "Judgment");
                                               // }
                                            }
                                        }
                                        else
                                        {
                                            string aaa = Common.FirstWords(UwordsMultiHeadNotes[0].ToString(), 2);
                                            objcase.AddCasesInsideSection_Rule_Article(CaseID, Common.FirstWords(aa.Trim(), 1), "Section", "Strong", "Judgment");
                                        }

                                        //

                                    }
                                }
                            }
                        }
                    }
                  
                    //string[] arr1Judgment = new string[] {"Section","Sections", "Ss", "Ss.","s.","S.", "----Ss." };
                  
                    //for (int Lst = 0; Lst < arr1Judgment.Length; Lst++)
                    //{
                    //    tobesearched = arr1Judgment[Lst].ToString() + " " + i;
                    //    aa = Common.GetInsideSectionSearch(Judgment, tobesearched);
                    //    string[] wordsJud = aa.Split(',');
                    //    string[] UwordsJud = wordsJud.Distinct().ToArray();

                    //    foreach (string SearctTxt in UwordsJud)
                    //    {
                    //        if (!string.IsNullOrEmpty(SearctTxt))
                    //        {
                    //            string[] wordMulti = SearctTxt.Split('|');
                    //            string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                    //            if (UwordsMultiHeadNotes.Length > 0)
                    //            {
                    //                if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(UwordsMultiHeadNotes[0].ToString()),2)))
                    //                {
                    //                    string aaa = Common.FirstWords(Common.RemoveSectionExceptionWords(UwordsMultiHeadNotes[0].ToString()),2);
                    //                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Section "+Common.RemoveSectionExceptionWords(aaa.Trim()), "Section", "Strong", "Judgment");
                    //                }
                    //                //foreach (string SearctTxt in UwordsHeadNotes)
                    //                //{ }
                    //            }
                    //            //

                    //            //objcase.AddCasesInsideSection_Rule_Article(CaseID, SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "Section"), "Strong", "Section", "HeadNotes");
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
            }

        }
        public void GetRulesSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
                string tobesearched = "";
                string aa = "";
                for (int i = 1; i < 601; i++)
                {
                    string[] arr1HeadNotes = new string[] { "Rule", "-rules", "Rr.", "rr.", "----R.", "-Rr.", "---R." };

                    for (int Lst = 0; Lst < arr1HeadNotes.Length; Lst++)
                    {
                        string[] tobesearchLst = new string[] { arr1HeadNotes[Lst].ToString() + " " + i, arr1HeadNotes[Lst].ToString() + "" + i };
                        //tobesearched = arr1HeadNotes[Lst].ToString() + " " + i;
                        for (int TOLst = 0; TOLst < tobesearchLst.Length; TOLst++)
                        {
                            //aa = Common.GetInsideSectionSearch(HeadNotes, tobesearched);
                            aa = Common.GetInsideSectionSearch(HeadNotes, tobesearchLst[TOLst].ToString());
                            if (!string.IsNullOrEmpty(aa))
                            {
                                string[] wordsHeadNotes = aa.Split('|');
                                string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                                foreach (string SearctTxt in UwordsHeadNotes)
                                {
                                    if (!string.IsNullOrEmpty(SearctTxt))
                                    {
                                        string[] wordMulti = SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "").Split(',');
                                        string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                                        if (UwordsMultiHeadNotes.Length > 0)
                                        {

                                            foreach (string SearctTxtMulti in UwordsMultiHeadNotes)
                                            {
                                                //if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1)))
                                                //{
                                                string aaa = Common.FirstWords(Common.RemoveRuleExceptionWords(SearctTxtMulti.ToString().Trim()), 1);
                                                if (!string.IsNullOrEmpty(aaa))
                                                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Rule " + Common.FirstWords(Common.RemoveRuleExceptionWords(SearctTxtMulti.Trim()), 1), "Rule", "Strong", "HeadNotes");
                                                // }
                                            }
                                        }
                                        else
                                        {
                                            string aaa = Common.FirstWords(UwordsMultiHeadNotes[0].ToString(), 2);
                                            objcase.AddCasesInsideSection_Rule_Article(CaseID, Common.FirstWords(aa.Trim(), 1), "Rule", "Strong", "HeadNotes");
                                        }

                                        //

                                    }
                                }
                            }
                        }
                    }


                    // Judgment

                    for (int Lst = 0; Lst < arr1HeadNotes.Length; Lst++)
                    {
                        string[] tobesearchLst = new string[] { arr1HeadNotes[Lst].ToString() + " " + i, arr1HeadNotes[Lst].ToString() + "" + i };
                        //tobesearched = arr1HeadNotes[Lst].ToString() + " " + i;
                        for (int TOLst = 0; TOLst < tobesearchLst.Length; TOLst++)
                        {
                            //aa = Common.GetInsideSectionSearch(HeadNotes, tobesearched);
                            aa = Common.GetInsideSectionSearch(Judgment, tobesearchLst[TOLst].ToString());
                            if (!string.IsNullOrEmpty(aa))
                            {
                                string[] wordsHeadNotes = aa.Split('|');
                                string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                                foreach (string SearctTxt in UwordsHeadNotes)
                                {
                                    if (!string.IsNullOrEmpty(SearctTxt))
                                    {
                                        string[] wordMulti = SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "").Split(',');
                                        string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                                        if (UwordsMultiHeadNotes.Length > 0)
                                        {

                                            foreach (string SearctTxtMulti in UwordsMultiHeadNotes)
                                            {

                                                // if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1))
                                                //{
                                                string aaa = Common.FirstWords(Common.RemoveRuleExceptionWords(SearctTxtMulti.ToString().Trim()), 1);
                                                if (!string.IsNullOrEmpty(aaa))
                                                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Rule " + Common.FirstWords(Common.RemoveRuleExceptionWords(SearctTxtMulti.Trim()), 1), "Rule", "Strong", "Judgment");
                                                // }
                                            }
                                        }
                                        else
                                        {
                                            string aaa = Common.FirstWords(UwordsMultiHeadNotes[0].ToString(), 2);
                                            objcase.AddCasesInsideSection_Rule_Article(CaseID, Common.FirstWords(aa.Trim(), 1), "Rule", "Strong", "Judgment");
                                        }

                                        //

                                    }
                                }
                            }
                        }
                    }

                    //string[] arr1Judgment = new string[] {"Section","Sections", "Ss", "Ss.","s.","S.", "----Ss." };

                    //for (int Lst = 0; Lst < arr1Judgment.Length; Lst++)
                    //{
                    //    tobesearched = arr1Judgment[Lst].ToString() + " " + i;
                    //    aa = Common.GetInsideSectionSearch(Judgment, tobesearched);
                    //    string[] wordsJud = aa.Split(',');
                    //    string[] UwordsJud = wordsJud.Distinct().ToArray();

                    //    foreach (string SearctTxt in UwordsJud)
                    //    {
                    //        if (!string.IsNullOrEmpty(SearctTxt))
                    //        {
                    //            string[] wordMulti = SearctTxt.Split('|');
                    //            string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                    //            if (UwordsMultiHeadNotes.Length > 0)
                    //            {
                    //                if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(UwordsMultiHeadNotes[0].ToString()),2)))
                    //                {
                    //                    string aaa = Common.FirstWords(Common.RemoveSectionExceptionWords(UwordsMultiHeadNotes[0].ToString()),2);
                    //                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Section "+Common.RemoveSectionExceptionWords(aaa.Trim()), "Section", "Strong", "Judgment");
                    //                }
                    //                //foreach (string SearctTxt in UwordsHeadNotes)
                    //                //{ }
                    //            }
                    //            //

                    //            //objcase.AddCasesInsideSection_Rule_Article(CaseID, SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "Section"), "Strong", "Section", "HeadNotes");
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
            }

        }
        public void GetArticleSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
                string tobesearched = "";
                string aa = "";
                for (int i = 1; i < 601; i++)
                {
                    if (i == 199)
                    {
                        string aaaa = "umar";
                    }
                    string[] arr1HeadNotes = new string[] { "Art.", "art.", "Arts.", "----Art", "--Arts.", "-Arts.", "--Arts.", "Article", "---Arts.", "Articles" };

                    for (int Lst = 0; Lst < arr1HeadNotes.Length; Lst++)
                    {
                        string[] tobesearchLst = new string[] { arr1HeadNotes[Lst].ToString() + " " + i, arr1HeadNotes[Lst].ToString() + "" + i };
                        //tobesearched = arr1HeadNotes[Lst].ToString() + " " + i;
                        for (int TOLst = 0; TOLst < tobesearchLst.Length; TOLst++)
                        {
                            //aa = Common.GetInsideSectionSearch(HeadNotes, tobesearched);
                            aa = Common.GetInsideSectionSearch(HeadNotes, tobesearchLst[TOLst].ToString());
                            if (!string.IsNullOrEmpty(aa))
                            {
                                string[] wordsHeadNotes = aa.Split('|');
                                string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                                foreach (string SearctTxt in UwordsHeadNotes)
                                {
                                    if (!string.IsNullOrEmpty(SearctTxt))
                                    {
                                        string[] wordMulti = SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "").Split(',');
                                        string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                                        if (UwordsMultiHeadNotes.Length > 0)
                                        {

                                            foreach (string SearctTxtMulti in UwordsMultiHeadNotes)
                                            {
                                                //if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1)))
                                                //{
                                                string aaa = Common.RemoveArticleExceptionWords(Common.FirstWords(SearctTxtMulti.ToString().Trim(),1));
                                                if (!string.IsNullOrEmpty(aaa))
                                                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Article " + Common.RemoveArticleExceptionWords(Common.FirstWords(SearctTxtMulti.Trim(),1)), "Article", "Strong", "HeadNotes");
                                                // }
                                            }
                                        }
                                        else
                                        {
                                            string aaa = Common.FirstWords(UwordsMultiHeadNotes[0].ToString(), 2);
                                            objcase.AddCasesInsideSection_Rule_Article(CaseID, Common.FirstWords(aa.Trim(), 1), "Article", "Strong", "HeadNotes");
                                        }

                                        //

                                    }
                                }
                            }
                        }
                    }


                    // Judgment

                    for (int Lst = 0; Lst < arr1HeadNotes.Length; Lst++)
                    {
                        string[] tobesearchLst = new string[] { arr1HeadNotes[Lst].ToString() + " " + i, arr1HeadNotes[Lst].ToString() + "" + i };
                        //tobesearched = arr1HeadNotes[Lst].ToString() + " " + i;
                        for (int TOLst = 0; TOLst < tobesearchLst.Length; TOLst++)
                        {
                            //aa = Common.GetInsideSectionSearch(HeadNotes, tobesearched);
                            aa = Common.GetInsideSectionSearch(Judgment, tobesearchLst[TOLst].ToString());
                            if (!string.IsNullOrEmpty(aa))
                            {
                                string[] wordsHeadNotes = aa.Split('|');
                                string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                                foreach (string SearctTxt in UwordsHeadNotes)
                                {
                                    if (!string.IsNullOrEmpty(SearctTxt))
                                    {
                                        string[] wordMulti = SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "").Split(',');
                                        string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                                        if (UwordsMultiHeadNotes.Length > 0)
                                        {

                                            foreach (string SearctTxtMulti in UwordsMultiHeadNotes)
                                            {

                                                // if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(SearctTxtMulti.ToString().Trim()), 1))
                                                //{
                                                string aaa = Common.RemoveArticleExceptionWords(Common.FirstWords(SearctTxtMulti.ToString().Trim(),1));
                                                if (!string.IsNullOrEmpty(aaa))
                                                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Article " + Common.RemoveArticleExceptionWords(Common.FirstWords(SearctTxtMulti.Trim(),1)), "Article", "Strong", "Judgment");
                                                // }
                                            }
                                        }
                                        else
                                        {
                                            string aaa = Common.FirstWords(UwordsMultiHeadNotes[0].ToString(), 2);
                                            objcase.AddCasesInsideSection_Rule_Article(CaseID, Common.FirstWords(aa.Trim(), 1), "Article", "Strong", "Judgment");
                                        }

                                        //

                                    }
                                }
                            }
                        }
                    }

                    //string[] arr1Judgment = new string[] {"Section","Sections", "Ss", "Ss.","s.","S.", "----Ss." };

                    //for (int Lst = 0; Lst < arr1Judgment.Length; Lst++)
                    //{
                    //    tobesearched = arr1Judgment[Lst].ToString() + " " + i;
                    //    aa = Common.GetInsideSectionSearch(Judgment, tobesearched);
                    //    string[] wordsJud = aa.Split(',');
                    //    string[] UwordsJud = wordsJud.Distinct().ToArray();

                    //    foreach (string SearctTxt in UwordsJud)
                    //    {
                    //        if (!string.IsNullOrEmpty(SearctTxt))
                    //        {
                    //            string[] wordMulti = SearctTxt.Split('|');
                    //            string[] UwordsMultiHeadNotes = wordMulti.Distinct().ToArray();
                    //            if (UwordsMultiHeadNotes.Length > 0)
                    //            {
                    //                if (ValidNumber(Common.FirstWords(Common.RemoveSectionExceptionWords(UwordsMultiHeadNotes[0].ToString()),2)))
                    //                {
                    //                    string aaa = Common.FirstWords(Common.RemoveSectionExceptionWords(UwordsMultiHeadNotes[0].ToString()),2);
                    //                    objcase.AddCasesInsideSection_Rule_Article(CaseID, "Section "+Common.RemoveSectionExceptionWords(aaa.Trim()), "Section", "Strong", "Judgment");
                    //                }
                    //                //foreach (string SearctTxt in UwordsHeadNotes)
                    //                //{ }
                    //            }
                    //            //

                    //            //objcase.AddCasesInsideSection_Rule_Article(CaseID, SearctTxt.Replace(arr1HeadNotes[Lst].ToString(), "Section"), "Strong", "Section", "HeadNotes");
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
            }

        }
        bool ValidNumber(string Num)
        {
            try
            {
                int n;
                bool isNumeric = int.TryParse(Num, out n);
            return isNumeric;
            }
            catch{
                return false;
            }

            

        }
        
    }
}
