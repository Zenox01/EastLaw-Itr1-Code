using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace EastlawUI_v2.adminpanel
{
    public class CitationsSearchJournalWise
    {
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        
        public string GetSCMRJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " SCMR";

                        aa = GetInsideCitationSearch(Judgment, tobesearched, 3);
                        string[] wordsJud = aa.Split(',');
                        string[] UwordsJud = wordsJud.Distinct().ToArray();

                        foreach (string Citations in UwordsJud)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                findcitation = findcitation + "!" + Citations;
                                
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " SCMR";
                        aa = GetInsideCitationSearch(HeadNotes, tobesearched, 3);
                        string[] wordsHeadNotes = aa.Split(',');
                        string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                        foreach (string Citations in UwordsHeadNotes)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                findcitation = findcitation + "!" + Citations;
                                
                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public string GetCLCJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " CLC";
                        aa = GetInsideCitationSearch(Judgment, tobesearched, 3);
                        string[] wordsJud = aa.Split(',');
                        string[] UwordsJud = wordsJud.Distinct().ToArray();

                        foreach (string Citations in UwordsJud)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                findcitation = findcitation + "!" + Citations;
                                
                            }

                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " CLC";
                        aa = GetInsideCitationSearch(HeadNotes, tobesearched, 3);
                        string[] wordsHeadNotes = aa.Split(',');
                        string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                        foreach (string Citations in UwordsHeadNotes)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                findcitation = findcitation + "!" + Citations;
                                
                            }

                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public string GetPLDJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " S C","7"},
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString()+" SC","4"},
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " S.C.","4"},
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","4"}, 
                        {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","6"},
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","4"},
                        {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","6"},
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Karachi","4"}, 
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","4"},
                        {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","6"},
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Supreme Court","5"}, 
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(W. P.) Lahore","6"}, 
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(WP) Lah.","5"}, 
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Pesh.","4"}, 
                        {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " S C (Pak.)","8"}, 
                        {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Dacca","4"}, 
                    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " F C","5"}, 
                    {DateTime.Now.AddYears(-i).Year.ToString()+" PLD " + " West Pakistan","5"}
                        
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public string GetPTDJournalSearch( string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] {
                        {DateTime.Now.AddYears(-i).Year.ToString()+" PTD ","3"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" P T D ","3"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" PTD " + "(Trib.)","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" PTD " + "Trib.","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" PTD " + "(trib.)","4"}
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public string GetPLCJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {"PLC " +DateTime.Now.AddYears(-i).Year.ToString()+" (C.S)","4"},
                        {"PLC " +DateTime.Now.AddYears(-i).Year.ToString()+" C.S","4"},
                        {"PLC " +DateTime.Now.AddYears(-i).Year.ToString()+" (CS)","4"},
                        {"PLC " +DateTime.Now.AddYears(-i).Year.ToString()+" C S","4"},

                        {DateTime.Now.AddYears(-i).Year.ToString()+"PLC (C.S)","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+"PLC (C.S.)","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+"PLC C.S","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+"PLC (CS)","4"},
                        
                         {DateTime.Now.AddYears(-i).Year.ToString()+" PLC ","3"},
                         {DateTime.Now.AddYears(-i).Year.ToString()+" P L C ","3"},
                          {DateTime.Now.AddYears(-i).Year.ToString()+" P L C ","3"},

                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public string GetYLRJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {DateTime.Now.AddYears(-i).Year.ToString() + " YLR","3"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" Y L R","5"}
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " S.C.","4"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","4"}, 
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","4"},
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Karachi","4"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","4"},
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Supreme Court","5"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(W. P.) Lahore","6"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(WP) Lah.","5"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Pesh.","4"}, 
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " S C (Pak.)","8"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Dacca","4"}, 
                    //{"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " F C","5"}, 
                    //{DateTime.Now.AddYears(-i).Year.ToString()+" PLD " + " West Pakistan","5"}
                        
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public string GetMLDJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {DateTime.Now.AddYears(-i).Year.ToString() + " MLD","3"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" M L D","5"}
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " S.C.","4"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","4"}, 
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","4"},
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Karachi","4"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","4"},
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Supreme Court","5"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(W. P.) Lahore","6"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(WP) Lah.","5"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Pesh.","4"}, 
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " S C (Pak.)","8"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Dacca","4"}, 
                    //{"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " F C","5"}, 
                    //{DateTime.Now.AddYears(-i).Year.ToString()+" PLD " + " West Pakistan","5"}
                        
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public string GetCLDJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {DateTime.Now.AddYears(-i).Year.ToString() + " CLD","3"},
                        {DateTime.Now.AddYears(-i).Year.ToString() + " CLD","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" C L D","5"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" C L D","4"},
                        
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " S.C.","4"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","4"}, 
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lahore","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","4"},
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Lah.","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Karachi","4"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","4"},
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " Kar.","6"},
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Supreme Court","5"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(W. P.) Lahore","6"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + "(WP) Lah.","5"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Pesh.","4"}, 
                    //    {"P L D " + DateTime.Now.AddYears(-i).Year.ToString() + " S C (Pak.)","8"}, 
                    //    {"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " Dacca","4"}, 
                    //{"PLD " + DateTime.Now.AddYears(-i).Year.ToString() + " F C","5"}, 
                    //{DateTime.Now.AddYears(-i).Year.ToString()+" PLD " + " West Pakistan","5"}
                        
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public string GetPCrLJJournalSearch(string HeadNotes, string Judgment)
        {
            try
            {
                string findcitation = "";
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {DateTime.Now.AddYears(-i).Year.ToString() + " PCr.LJ","3"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" P Cr. L J","7"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" P Cr. L J","5"},
                    {DateTime.Now.AddYears(-i).Year.ToString()+" PCr.LJ (AJ&K Sh.C)","6"}
                
                        
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    findcitation = findcitation + "!" + Citations;
                                
                                }

                            }
                        }
                    }

                }
                return findcitation;
            }
            catch (Exception ex) { return ""; }

        }
        public static string GetInsideCitationSearch(string content, string searchkeyword, int NoOfWords)
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
                    txt = txt.Replace(", ", "").Replace(",", "").Replace("(", "").Replace(")", "");
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
    }
}