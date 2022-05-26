using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtility
{
    class CitationsSearchJournalWise
    {
        EastLawBL.Cases objcase = new EastLawBL.Cases();
        public void GetSCMRJournalSearch(int CaseID,string HeadNotes,string Judgment)
        {
            try
            {
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " SCMR";

                        aa = Common.GetInsideCitationSearch(Judgment, tobesearched, 3);
                        string[] wordsJud = aa.Split(',');
                        string[] UwordsJud = wordsJud.Distinct().ToArray();

                        foreach (string Citations in UwordsJud)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                //
                                objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "SCMR",0);
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " SCMR";
                        aa = Common.GetInsideCitationSearch(HeadNotes, tobesearched, 3);
                        string[] wordsHeadNotes = aa.Split(',');
                        string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();

                        foreach (string Citations in UwordsHeadNotes)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                //
                                objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "SCMR",0);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
            }
 
        }
        public void GetCLCJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " CLC";
                        aa = Common.GetInsideCitationSearch(Judgment, tobesearched, 3);
                        string[] wordsJud = aa.Split(',');
                        string[] UwordsJud = wordsJud.Distinct().ToArray();

                        foreach (string Citations in UwordsJud)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                //
                                objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "CLC",0);
                            }

                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " CLC";
                        aa = Common.GetInsideCitationSearch(HeadNotes, tobesearched, 3);
                        string[] wordsHeadNotes = aa.Split(',');
                        string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                        foreach (string Citations in UwordsHeadNotes)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                //
                                objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "CLC",0);
                            }

                        }
                    }

                }
            }
            catch(Exception ex) { }

        }
        public void GetPLDJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
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
                            aa = Common.GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "PLD",0);
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "PLD",0);
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex) { }

        }
        public void GetPTDJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                   // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {DateTime.Now.AddYears(-i).Year.ToString()+" PTD " + "(Trib.)","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" PTD " + "Trib.","4"},
                        {DateTime.Now.AddYears(-i).Year.ToString()+" PTD " + "(trib.)","4"}
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "PTD",0);
                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "PTD",0);
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex) { }

        }
        public void GetPLCJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
                string tobesearched = "";
                string aa = "";
                for (int i = 0; i < 69; i++)
                {
                    // tobesearched =  "PLD" +DateTime.Now.AddYears(-i).Year.ToString() +" Lahore ";

                    string[,] arrToBesearch = new string[,] { 
                        {"PLC " +DateTime.Now.AddYears(-i).Year.ToString()+" (C.S)","4"},
                        {"PLC " +DateTime.Now.AddYears(-i).Year.ToString()+" C.S","4"},
                        {"PLC " +DateTime.Now.AddYears(-i).Year.ToString()+" C S","4"},
                        
                };

                    int cout = 0;
                    if (!string.IsNullOrEmpty(Judgment))
                    {
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "PLC",0);
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "PLC",0);
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex) { }

        }
        public void GetYLRJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
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
                            aa = Common.GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "YLR",0);
                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "YLR",0);
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex) { }

        }
        public void GetMLDJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
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
                            aa = Common.GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "MLD",0);
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "MLD",0);
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex) { }

        }
        public void GetCLDJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
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
                        aa = Common.GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                        string[] wordsJud = aa.Split(',');
                        string[] UwordsJud = wordsJud.Distinct().ToArray();

                        foreach (string Citations in UwordsJud)
                        {
                            if (!string.IsNullOrEmpty(Citations))
                            {
                                //
                                objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "MLD",0);
                            }

                        }
                    }
                     }
                     if (!string.IsNullOrEmpty(HeadNotes))
                     {
                         //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                         for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                         {
                             aa = Common.GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                             //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                             string[] wordsHeadNotes = aa.Split(',');
                             string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                             foreach (string Citations in UwordsHeadNotes)
                             {
                                 if (!string.IsNullOrEmpty(Citations))
                                 {
                                     //
                                     objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "MLD",0);
                                 }

                             }
                         }
                     }

                }
            }
            catch (Exception ex) { }

        }
        public void GetPCrLJJournalSearch(int CaseID, string HeadNotes, string Judgment)
        {
            try
            {
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
                            aa = Common.GetInsideCitationSearch(Judgment, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            string[] wordsJud = aa.Split(',');
                            string[] UwordsJud = wordsJud.Distinct().ToArray();

                            foreach (string Citations in UwordsJud)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "Judgment", "PCrLJ",0);
                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(HeadNotes))
                    {
                        //tobesearched = DateTime.Now.AddYears(-i).Year.ToString() + " PLD";
                        for (int Lst = 0; Lst < arrToBesearch.GetLength(0); Lst++)
                        {
                            aa = Common.GetInsideCitationSearch(HeadNotes, arrToBesearch[Lst, 0].ToString(), int.Parse(arrToBesearch[Lst, 1].ToString()));
                            //aa = Common.GetInsideCitationSearch(Judgment, tobesearched,5);

                            string[] wordsHeadNotes = aa.Split(',');
                            string[] UwordsHeadNotes = wordsHeadNotes.Distinct().ToArray();


                            foreach (string Citations in UwordsHeadNotes)
                            {
                                if (!string.IsNullOrEmpty(Citations))
                                {
                                    //
                                    objcase.AddCasesInsideCitations(CaseID, Citations, "Strong", "HeadNotes", "PCrLJ",0);
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex) { }

        }
    }
}
