using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Compilation;
using System.Web.UI;
using System.Data;

/// <summary>
/// Summary description for PropertyRewrite
/// </summary>
public class PropertyRewrite : IRouteHandler
{
    public PropertyRewrite()
    {
        //
        // TODO: Add constructor logic here
        //
        //GetHttpHandler(RequestContext requestContext);
    }
    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        try
        {
            string News = requestContext.RouteData.Values["News"] as string;
            string General = requestContext.RouteData.Values["General"] as string;
            string PracticeArea = requestContext.RouteData.Values["PA"] as string;
            string BecomeMember = requestContext.RouteData.Values["BecomeMember"] as string;
            string Citations = requestContext.RouteData.Values["Citations"] as string;
            string PCitations = requestContext.RouteData.Values["PCitations"] as string;
            string Pstatutes = requestContext.RouteData.Values["Pstatutes"] as string;
            string Statutes = requestContext.RouteData.Values["Statutes"] as string;
            string StatutesIndex = requestContext.RouteData.Values["StatutesIndex"] as string;
            string Page = requestContext.RouteData.Values["Page"] as string;
            string Search = requestContext.RouteData.Values["SearchResult"] as string;
            string SearchCitation = requestContext.RouteData.Values["SearchCitation"] as string;
            string Dictionary = requestContext.RouteData.Values["word"] as string;
            string Dept = requestContext.RouteData.Values["Dept"] as string;
            string DeptGroup = requestContext.RouteData.Values["DeptGroup"] as string;
            string PASearch = requestContext.RouteData.Values["PASearchResult"] as string;
            string RestrictedRegion = requestContext.RouteData.Values["RestrictedRegion"] as string;
            string Corporate = requestContext.RouteData.Values["CorporateEntity"] as string;
            string ebookcat = requestContext.RouteData.Values["ebookcat"] as string;
            string ebookdetails = requestContext.RouteData.Values["ebookdetails"] as string;

            string MMembers = requestContext.RouteData.Values["MemberElements"] as string;
            string MCitations = requestContext.RouteData.Values["MCitations"] as string;
            string MSearch = requestContext.RouteData.Values["MSearchResult"] as string;
            string MGeneral = requestContext.RouteData.Values["MGeneral"] as string;
            string MPracticeArea = requestContext.RouteData.Values["MPA"] as string;
            string MDept = requestContext.RouteData.Values["MDept"] as string;
            string MDeptGroup = requestContext.RouteData.Values["MDeptGroup"] as string;
            string MPASearch = requestContext.RouteData.Values["MPASearchResult"] as string;
            string MStatutes = requestContext.RouteData.Values["MStatutes"] as string;
            string MStatutesIndex = requestContext.RouteData.Values["MStatutesIndex"] as string;
            string MPage = requestContext.RouteData.Values["MPage"] as string;



            DataTable dt = new DataTable();

            #region Desktop Website

            if (!string.IsNullOrEmpty(General))
            {
                if (General == "current-awareness")
                {
                    HttpContext.Current.Items["Title"] = "EastLaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CurrentAwareness.aspx", typeof(Page)) as Page;
                }
                if (General == "legislation")
                {
                    HttpContext.Current.Items["Title"] = "EastLaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/Legislation.aspx", typeof(Page)) as Page;
                }
                if (General == "practice-Area")
                {
                    HttpContext.Current.Items["Title"] = "Eastlaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/PracticeArea.aspx", typeof(Page)) as Page;
                }
                if (General == "e-newslive")
                {
                    HttpContext.Current.Items["Title"] = "Eastlaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/NewsLive.aspx", typeof(Page)) as Page;
                    //return BuildManager.CreateInstanceFromVirtualPath("~/News.aspx", typeof(Page)) as Page;
                }
                ///News Live New
                if (General == "newslivenew")
                {
                    HttpContext.Current.Items["Title"] = "Eastlaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/NewsLive.aspx", typeof(Page)) as Page;
                }
                if (General == "subscription")
                {
                    HttpContext.Current.Items["Title"] = "Eastlaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/Subscription.aspx", typeof(Page)) as Page;
                }
                if (General == "ebook")
                {
                    HttpContext.Current.Items["Title"] = "Eastlaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/ebook.aspx", typeof(Page)) as Page;
                }
            }
            if (!string.IsNullOrEmpty(ebookdetails))
            {
                string Val = ebookdetails;
                string[] ebookdetailsid = Val.Split('.');

                EastLawBL.EBook objeb = new EastLawBL.EBook();
                DataTable dtEbook = new DataTable();
                //dtEbook = objeb.GetEBook(int.Parse(EncryptDecryptHelper.Decrypt(ebookdetails[ebookdetails.Length - 1].ToString())));
                dtEbook = objeb.GetEBook(int.Parse(EncryptDecryptHelper.Decrypt(ebookdetailsid[ebookdetailsid.Length - 1].ToString())));
                //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                if (dtEbook.Rows.Count > 0)
                {

                    HttpContext.Current.Items["ebookid"] = dtEbook.Rows[0]["ID"].ToString();
                    HttpContext.Current.Items["Title"] = dtEbook.Rows[0]["Title"].ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/ebookdetails.aspx", typeof(Page)) as Page;
                }

            }

            if (!string.IsNullOrEmpty(News))
            {
                string Val = News;
                string[] Newsid = Val.Split('.');

                EastLawBL.News objn = new EastLawBL.News();
                DataTable dtNews = new DataTable();
                dtNews = objn.GetNews(int.Parse(EncryptDecryptHelper.Decrypt(Newsid[Newsid.Length - 1].ToString())));
                //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                if (dtNews.Rows.Count > 0)
                {

                    HttpContext.Current.Items["newsid"] = dtNews.Rows[0]["ID"].ToString();
                    HttpContext.Current.Items["Title"] = dtNews.Rows[0]["Title"].ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/NewsDetails.aspx", typeof(Page)) as Page;
                }

            }
            if (!string.IsNullOrEmpty(PracticeArea))
            {
                //string Val = PracticeArea;
                //string[] citationsid = Val.Split('.');

                //EastLawBL.Cases objcases = new EastLawBL.Cases();
                //DataTable dtCase = new DataTable();
                //dtCase = objcases.GetCases(int.Parse(EncryptDecryptHelper.Decrypt(citationsid[citationsid.Length - 1].ToString())));

                HttpContext.Current.Items["Title"] = PracticeArea.ToString().Replace("-", " ");

                return BuildManager.CreateInstanceFromVirtualPath("~/PracticeAreaSearchGeneral.aspx", typeof(Page)) as Page;
                //if (General == "Current-Awareness")
                //{
                //    HttpContext.Current.Items["Title"] = General.ToString().Replace("-", " ");
                //    return BuildManager.CreateInstanceFromVirtualPath("~/CurrentAwareness.aspx", typeof(Page)) as Page;
                //}
                //if (General == "Legislation")
                //{
                //    HttpContext.Current.Items["Title"] = General.ToString().Replace("-", " ");
                //    return BuildManager.CreateInstanceFromVirtualPath("~/Legislation.aspx", typeof(Page)) as Page;
                //}
                //if (General == "Practice-Area")
                //{
                //    HttpContext.Current.Items["Title"] = General.ToString().Replace("-", " ");
                //    return BuildManager.CreateInstanceFromVirtualPath("~/PracticeArea.aspx", typeof(Page)) as Page;
                //}
            }
            if (!string.IsNullOrEmpty(Corporate))
            {
                HttpContext.Current.Items["Title"] = Corporate.Replace("-", " ");
                if (Corporate == "dashboard")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/companyadminpanel/Dashboard.aspx", typeof(Page)) as Page;
                }
                if (Corporate == "add-user")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/companyadminpanel/AddUsers.aspx", typeof(Page)) as Page;
                }
                if (Corporate == "manage-users")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/companyadminpanel/ManageUsers.aspx", typeof(Page)) as Page;
                }
                if (Corporate == "password-reset")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/companyadminpanel/resetpassword.aspx", typeof(Page)) as Page;
                }
                if (Corporate == "change-password")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/companyadminpanel/changepassword.aspx", typeof(Page)) as Page;
                }
            }

            if (!string.IsNullOrEmpty(BecomeMember))
            {

                HttpContext.Current.Items["Title"] = BecomeMember.Replace("-", " ");
                if (BecomeMember == "member-activation")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/ActivateUser.aspx", typeof(Page)) as Page;
                }

                if (BecomeMember == "member-login")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/Register.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "member-register")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/RegistrationPage.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "forget-password")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/ForgetPassword.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "member-dashboard")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/MemberHome.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "my-documents")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/MyFolders.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "my-search")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/MySearch.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "my-settings")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/MyAccount.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "my-subscription")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/MySubscription.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "subscription")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/Subscription.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "complementary-subscription")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/Subscription.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "review-order")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/ReviewOrder.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "order-confirmation")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/OrderConfirmation.aspx", typeof(Page)) as Page;
                }
                if (BecomeMember == "order-confirmation.com")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/OrderConfirmation.aspx", typeof(Page)) as Page;
                }
            }
            if (!string.IsNullOrEmpty(General))
            {
                if (General == "current-awareness")
                {
                    HttpContext.Current.Items["Title"] = General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CurrentAwareness.aspx", typeof(Page)) as Page;
                }
            }
            if (!string.IsNullOrEmpty(Citations))
            {


                if (Citations == "search-result")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CitationSearchResult.aspx", typeof(Page)) as Page;
                }
                if (Citations == "search-by-section-result")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CaseSearchResultBySection.aspx", typeof(Page)) as Page;
                }
                if (Citations == "advance-search")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CaseAdvanceSearch.aspx", typeof(Page)) as Page;
                }
                if (Citations == "search-bysection")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CaseSearchBySection.aspx", typeof(Page)) as Page;
                }
                if (Citations == "latest-judgments")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    HttpContext.Current.Items["CaseFilter"] = Citations.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestCases.aspx", typeof(Page)) as Page;
                }
                if (Citations == "latest-judgments-public")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    HttpContext.Current.Items["CaseFilter"] = Citations.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LastestCasesPublic.aspx", typeof(Page)) as Page;
                }
                if (Citations == "supreme-court-of-pakistan")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    HttpContext.Current.Items["CaseFilter"] = Citations.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestCases.aspx", typeof(Page)) as Page;
                }
                if (Citations == "high-court")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    HttpContext.Current.Items["CaseFilter"] = Citations.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestCases.aspx", typeof(Page)) as Page;
                }
                if (Citations == "practice-area-latest-judgments")
                {
                    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                    HttpContext.Current.Items["CaseFilter"] = Citations.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestCases.aspx", typeof(Page)) as Page;
                }

                else
                {
                    string Val = Citations;
                    string[] citationsid = Val.Split('.');

                    EastLawBL.Cases objcases = new EastLawBL.Cases();
                    DataTable dtCase = new DataTable();
                    dtCase = objcases.GetCases(int.Parse(EncryptDecryptHelper.Decrypt(citationsid[citationsid.Length - 1].ToString())));
                    //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                    if (dtCase.Rows.Count > 0)
                    {

                        HttpContext.Current.Items["caseid"] = dtCase.Rows[0]["ID"].ToString();
                        //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                        //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                        HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                        return BuildManager.CreateInstanceFromVirtualPath("~/CitationDetails.aspx", typeof(Page)) as Page;
                    }
                }

            }
            if (!string.IsNullOrEmpty(PCitations))
            {


                //if (Citations == "supreme-court-of-pakistan")
                //{
                //    HttpContext.Current.Items["Title"] = Citations.ToString().Replace("-", " ");
                //    HttpContext.Current.Items["CaseFilter"] = Citations.ToString();
                //    return BuildManager.CreateInstanceFromVirtualPath("~/LatestCases.aspx", typeof(Page)) as Page;
                //}



                string Val = PCitations;
                string[] citationsid = Val.Split('.');

                EastLawBL.Cases objcases = new EastLawBL.Cases();
                DataTable dtCase = new DataTable();
                dtCase = objcases.GetCases(int.Parse(EncryptDecryptHelper.Decrypt(citationsid[citationsid.Length - 1].ToString())));
                //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                if (dtCase.Rows.Count > 0)
                {

                    HttpContext.Current.Items["caseid"] = dtCase.Rows[0]["ID"].ToString();
                    //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                    //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                    HttpContext.Current.Items["Title"] = PCitations.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CaseDetailsPublic.aspx", typeof(Page)) as Page;
                }


            }
            if (!string.IsNullOrEmpty(Pstatutes))
            {
                string Val = Pstatutes;
                
                string[] statutesid = Val.Split('.');

                EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                DataTable dtStatutes = new DataTable();
                dtStatutes = objstate.GetStatutes(int.Parse(EncryptDecryptHelper.Decrypt(statutesid[statutesid.Length - 1].ToString())));
                //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                if (dtStatutes.Rows.Count > 0)
                {

                    HttpContext.Current.Items["statutesid"] = dtStatutes.Rows[0]["ID"].ToString();
                    HttpContext.Current.Items["statutespdffilename"] = dtStatutes.Rows[0]["PDFFileName"].ToString();
                    HttpContext.Current.Items["statuteswordfilename"] = dtStatutes.Rows[0]["WordFileName"].ToString();

                    //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                    //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                    HttpContext.Current.Items["Title"] = dtStatutes.Rows[0]["Title"].ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesIndexDetails_Public.aspx", typeof(Page)) as Page;
                }


            }
            if (!string.IsNullOrEmpty(StatutesIndex))
            {
                string Val = StatutesIndex;
                string[] statutesindexid = Val.Split('.');

                EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                DataTable dtStatutesIndex = new DataTable();
                dtStatutesIndex = objstate.GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(statutesindexid[statutesindexid.Length - 1].ToString())));
                //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                if (dtStatutesIndex.Rows.Count > 0)
                {

                    HttpContext.Current.Items["statutesindexid"] = dtStatutesIndex.Rows[0]["ID"].ToString();
                    HttpContext.Current.Items["statutesid"] = dtStatutesIndex.Rows[0]["StatutesID"].ToString();
                    HttpContext.Current.Items["statutespdffilename"] = dtStatutesIndex.Rows[0]["PDFFileName"].ToString();
                    HttpContext.Current.Items["statuteswordfilename"] = dtStatutesIndex.Rows[0]["WordFileName"].ToString();
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesIndexDetails.aspx", typeof(Page)) as Page;
                }


            }
            if (!string.IsNullOrEmpty(Statutes))
            {
                if (Statutes == "legislations")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/Legislation.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "federal-legislation")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "provincial-legislation")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "federal-amendment-acts")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "provincial-amendment-acts")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "federal-rules-and-regulations")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "provincial-rules-and-regulations")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "bill-by-national-assembly")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "bill-by-provincial-assembly")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "latest-legislations")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "practice-area-legislations")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = Statutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (Statutes == "search-result")
                {
                    HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/StatutesList.aspx", typeof(Page)) as Page;
                }
                else
                {
                    string Val = Statutes;
                    string[] statutesid = Val.Split('.');

                    EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                    DataTable dtStatutes = new DataTable();
                    dtStatutes = objstate.GetStatutes(int.Parse(EncryptDecryptHelper.Decrypt(statutesid[statutesid.Length - 1].ToString())));
                    //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                    if (dtStatutes.Rows.Count > 0)
                    {

                        HttpContext.Current.Items["statutesid"] = dtStatutes.Rows[0]["ID"].ToString();
                        HttpContext.Current.Items["statutespdffilename"] = dtStatutes.Rows[0]["PDFFileName"].ToString();
                        HttpContext.Current.Items["statuteswordfilename"] = dtStatutes.Rows[0]["WordFileName"].ToString();

                        //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                        //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                        HttpContext.Current.Items["Title"] = Statutes.ToString().Replace("-", " ");
                        return BuildManager.CreateInstanceFromVirtualPath("~/StatutesIndex.aspx", typeof(Page)) as Page;
                    }
                }

            }

            if (!string.IsNullOrEmpty(Search))
            {
                HttpContext.Current.Items["keywordtxt"] = Search.ToString().Replace("-", " ");
                HttpContext.Current.Items["Title"] = Search.ToString().Replace("-", " ");
                //return BuildManager.CreateInstanceFromVirtualPath("~/KeywordSearchResults.aspx", typeof(Page)) as Page;
                return BuildManager.CreateInstanceFromVirtualPath("~/SearchResults.aspx", typeof(Page)) as Page;
            }
            if (!string.IsNullOrEmpty(PASearch))
            {
                HttpContext.Current.Items["PAkeywordtxt"] = PASearch.ToString().Replace("-", " ");
                HttpContext.Current.Items["Title"] = PASearch.ToString().Replace("-", " ");
                return BuildManager.CreateInstanceFromVirtualPath("~/PracticeAreaSearchResult.aspx", typeof(Page)) as Page;
            }
            if (!string.IsNullOrEmpty(SearchCitation))
            {
                HttpContext.Current.Items["keywordtxtcitation"] = SearchCitation.ToString().Replace("-", " ");
                HttpContext.Current.Items["Title"] = SearchCitation.ToString().Replace("-", " ");
                return BuildManager.CreateInstanceFromVirtualPath("~/CitationSearchResult.aspx", typeof(Page)) as Page;
            }



            if (!string.IsNullOrEmpty(Page))
            {
                if (Page == "site-feedback")
                {
                    HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");

                    return BuildManager.CreateInstanceFromVirtualPath("~/sitefeedback.aspx", typeof(Page)) as Page;
                }
                if (Page == "contact-us")
                {
                    HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");

                    return BuildManager.CreateInstanceFromVirtualPath("~/contactus.aspx", typeof(Page)) as Page;
                }
                if (Page == "data-coverage")
                {
                    HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");

                    return BuildManager.CreateInstanceFromVirtualPath("~/datacoverage.aspx", typeof(Page)) as Page;
                }
                if (Page == "features")
                {
                    HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");

                    return BuildManager.CreateInstanceFromVirtualPath("~/feature.aspx", typeof(Page)) as Page;
                }
                if (Page == "Subscription")
                {
                    HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");

                    return BuildManager.CreateInstanceFromVirtualPath("~/Subscription.aspx", typeof(Page)) as Page;
                }
                if (Page == "practice-area")
                {
                    HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");

                    return BuildManager.CreateInstanceFromVirtualPath("~/PracticeArea.aspx", typeof(Page)) as Page;
                }
                if (Page == "Departments")
                {
                    HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");

                    return BuildManager.CreateInstanceFromVirtualPath("~/Departments.aspx", typeof(Page)) as Page;
                }
                else
                {
                    EastLawBL.Pages objpages = new EastLawBL.Pages();
                    dt = objpages.GetPageByName(Page.ToString().Replace("-", " "));
                    if (dt.Rows.Count > 0)
                    {
                        HttpContext.Current.Items["Title"] = Page.ToString().Replace("-", " ");
                        HttpContext.Current.Items["pageid"] = dt.Rows[0]["ID"].ToString();
                        return BuildManager.CreateInstanceFromVirtualPath("~/Pages.aspx", typeof(Page)) as Page;
                        //}
                    }
                }
            }
            if (!string.IsNullOrEmpty(Dictionary))
            {
                if (Dictionary == "dictionary-all")
                {

                    return BuildManager.CreateInstanceFromVirtualPath("~/DictionaryBlock.aspx", typeof(Page)) as Page;
                }
                else if (Dictionary == "legal-maxims")
                {

                    return BuildManager.CreateInstanceFromVirtualPath("~/LegalMaximBlock.aspx", typeof(Page)) as Page;
                }
                else
                {
                    HttpContext.Current.Items["word"] = Dictionary.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/eastlawdic.aspx", typeof(Page)) as Page;
                }
                //}

            }
            if (!string.IsNullOrEmpty(Dept))
            {
                if (Dept == "departments-home")
                {

                    HttpContext.Current.Items["Title"] = "Departments";
                    //return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentsList.aspx", typeof(Page)) as Page;
                    return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentsHome.aspx", typeof(Page)) as Page;
                }
                else if (Dept == "search-result")
                {

                    HttpContext.Current.Items["Title"] = "Departments Search Result";
                    //return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentsList.aspx", typeof(Page)) as Page;
                    return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentsList.aspx", typeof(Page)) as Page;
                }
                else if (Dept == "practice-area-departments-updates")
                {
                    HttpContext.Current.Items["Title"] = Dept.ToString().Replace("-", " ");
                    HttpContext.Current.Items["DeptFilter"] = Dept.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestDepartmentsList.aspx", typeof(Page)) as Page;
                }
                else if (Dept == "latest-notifications")
                {
                    HttpContext.Current.Items["Title"] = Dept.ToString().Replace("-", " ");
                    HttpContext.Current.Items["DeptFilter"] = Dept.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestDepartmentsList.aspx", typeof(Page)) as Page;
                }
                else if (Dept == "latest-other")
                {
                    HttpContext.Current.Items["Title"] = Dept.ToString().Replace("-", " ");
                    HttpContext.Current.Items["DeptFilter"] = Dept.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestDepartmentsList.aspx", typeof(Page)) as Page;
                }
                else if (Dept == "latest-circular")
                {
                    HttpContext.Current.Items["Title"] = Dept.ToString().Replace("-", " ");
                    HttpContext.Current.Items["DeptFilter"] = Dept.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/LatestDepartmentsList.aspx", typeof(Page)) as Page;
                }
                else
                {

                    string Val = Dept;
                    string[] Deptid = Val.Split('.');
                    HttpContext.Current.Items["DeptGroupID"] = Deptid[Deptid.Length - 1].ToString();
                    HttpContext.Current.Items["Title"] = Deptid[0].ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentsList.aspx", typeof(Page)) as Page;

                    //EastLawBL.Departments objdept = new EastLawBL.Departments();
                    //DataTable dtDept = new DataTable();
                    //dtDept = objdept.GetDepartmentFileDetailsByID(int.Parse(EncryptDecryptHelper.Decrypt(Deptid[Deptid.Length - 1].ToString())));
                    ////dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                    //if (dtDept.Rows.Count > 0)
                    //{

                    //    HttpContext.Current.Items["dptdocid"] = dtDept.Rows[0]["ID"].ToString();
                    //    HttpContext.Current.Items["dptHTMLFile"] = dtDept.Rows[0]["HTMLFile"].ToString();
                    //    HttpContext.Current.Items["dptWordFile"] = dtDept.Rows[0]["WordFile"].ToString();
                    //    HttpContext.Current.Items["dptFileContent"] = dtDept.Rows[0]["FileContent"].ToString();
                    //    //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                    //    //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                    //    HttpContext.Current.Items["Title"] = dtDept.Rows[0]["WordFile"].ToString();
                    //    return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentDocumentView.aspx", typeof(Page)) as Page;
                    //}


                }
                //}

            }
            if (!string.IsNullOrEmpty(DeptGroup))
            {
                string Val = DeptGroup;
                string[] Deptid = Val.Split('.');

                EastLawBL.Departments objdept = new EastLawBL.Departments();
                DataTable dtDept = new DataTable();
                dtDept = objdept.GetDepartmentFileDetailsByID(int.Parse(EncryptDecryptHelper.Decrypt(Deptid[Deptid.Length - 1].ToString())));
                //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                if (dtDept.Rows.Count > 0)
                {

                    HttpContext.Current.Items["dptdocid"] = dtDept.Rows[0]["ID"].ToString();
                    HttpContext.Current.Items["dptHTMLFile"] = dtDept.Rows[0]["HTMLFile"].ToString();
                    HttpContext.Current.Items["dptWordFile"] = dtDept.Rows[0]["WordFile"].ToString();
                    HttpContext.Current.Items["dptFileContent"] = dtDept.Rows[0]["FileContent"].ToString();
                    //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                    //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                    HttpContext.Current.Items["Title"] = dtDept.Rows[0]["Title"].ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentDocumentView.aspx", typeof(Page)) as Page;
                }



                //}

            }
            if (!string.IsNullOrEmpty(RestrictedRegion))
            {
                if (RestrictedRegion == "limit-exceeded")
                {
                    HttpContext.Current.Items["Title"] = "Limit Exceeded";

                    return BuildManager.CreateInstanceFromVirtualPath("~/OverLimit.aspx", typeof(Page)) as Page;
                }
                else
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/RestrictedRegion.aspx", typeof(Page)) as Page;
                }
            }
            #endregion

            #region Mobile Website

            if (!string.IsNullOrEmpty(MGeneral))
            {
                if (MGeneral == "Current-Awareness")
                {
                    HttpContext.Current.Items["Title"] = "EastLaw - " + MGeneral.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/CurrentAwareness.aspx", typeof(Page)) as Page;
                }
                if (MGeneral == "Legislation")
                {
                    HttpContext.Current.Items["Title"] = "EastLaw - " + MGeneral.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/Legislation.aspx", typeof(Page)) as Page;
                }
                if (MGeneral == "Practice-Area")
                {
                    HttpContext.Current.Items["Title"] = "Eastlaw - " + MGeneral.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/PracticeArea.aspx", typeof(Page)) as Page;
                }
                if (MGeneral == "News")
                {
                    HttpContext.Current.Items["Title"] = "Eastlaw - " + General.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/News.aspx", typeof(Page)) as Page;
                }
            }

            if (!string.IsNullOrEmpty(MSearch))
            {
                HttpContext.Current.Items["keywordtxt"] = MSearch.ToString().Replace("-", " ");
                HttpContext.Current.Items["Title"] = MSearch.ToString().Replace("-", " ");
                return BuildManager.CreateInstanceFromVirtualPath("~/m/SearchResult.aspx", typeof(Page)) as Page;
            }
            if (!string.IsNullOrEmpty(MCitations))
            {


                if (MCitations == "Search-Result")
                {
                    HttpContext.Current.Items["Title"] = MCitations.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/CitationsList.aspx", typeof(Page)) as Page;
                }
                if (MCitations == "Advance-Search")
                {
                    HttpContext.Current.Items["Title"] = MCitations.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/CasesAdvanceSearch.aspx", typeof(Page)) as Page;
                }

                else
                {
                    string Val = MCitations;
                    string[] citationsid = Val.Split('.');

                    EastLawBL.Cases objcases = new EastLawBL.Cases();
                    DataTable dtCase = new DataTable();
                    dtCase = objcases.GetCases(int.Parse(EncryptDecryptHelper.Decrypt(citationsid[citationsid.Length - 1].ToString())));
                    //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                    if (dtCase.Rows.Count > 0)
                    {

                        HttpContext.Current.Items["caseid"] = dtCase.Rows[0]["ID"].ToString();
                        //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                        //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                        HttpContext.Current.Items["Title"] = MCitations.ToString().Replace("-", " ");
                        return BuildManager.CreateInstanceFromVirtualPath("~/m/CitationDetails.aspx", typeof(Page)) as Page;
                    }
                }

            }
            if (!string.IsNullOrEmpty(MMembers))
            {

                HttpContext.Current.Items["Title"] = MMembers.Replace("-", " ");
                //if (BecomeMember == "Member-Activation")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/ActivateUser.aspx", typeof(Page)) as Page;
                //}
                if (MMembers == "Member-Login")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/Login.aspx", typeof(Page)) as Page;
                }
                if (MMembers == "Member-Register")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/Register.aspx", typeof(Page)) as Page;
                }
                if (MMembers == "Forget-Password")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/ForgotPassword.aspx", typeof(Page)) as Page;
                }
                if (MMembers == "Member-Dashboard")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/MemberHome.aspx", typeof(Page)) as Page;
                }
                if (MMembers == "My-Folders")
                {
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/MyFolder.aspx", typeof(Page)) as Page;
                }
                //if (BecomeMember == "My-Search")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/MySearch.aspx", typeof(Page)) as Page;
                //}
                //if (BecomeMember == "My-Settings")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/AccountSettings.aspx", typeof(Page)) as Page;
                //}
                //if (BecomeMember == "My-Subscription")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/MySubscription.aspx", typeof(Page)) as Page;
                //}
                //if (BecomeMember == "Subscription")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/Subscription.aspx", typeof(Page)) as Page;
                //}
                //if (BecomeMember == "Complementary-Subscription")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/Subscription.aspx", typeof(Page)) as Page;
                //}
                //if (BecomeMember == "Review-Order")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/ReviewOrder.aspx", typeof(Page)) as Page;
                //}
                //if (BecomeMember == "Order-Confirmation")
                //{
                //    return BuildManager.CreateInstanceFromVirtualPath("~/OrderConfirmation.aspx", typeof(Page)) as Page;
                //}
            }
            if (!string.IsNullOrEmpty(MDept))
            {
                if (MDept == "All-Departments")
                {

                    HttpContext.Current.Items["Title"] = "Departments";
                    //return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentsList.aspx", typeof(Page)) as Page;
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/DepartmentsHome.aspx", typeof(Page)) as Page;
                }
                else if (MDept == "Search-Result")
                {

                    HttpContext.Current.Items["Title"] = "Departments Search Result";
                    //return BuildManager.CreateInstanceFromVirtualPath("~/DepartmentsList.aspx", typeof(Page)) as Page;
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/DepartmentsList.aspx", typeof(Page)) as Page;
                }
                else
                {

                    string Val = MDept;
                    string[] Deptid = Val.Split('.');
                    HttpContext.Current.Items["DeptGroupID"] = Deptid[Deptid.Length - 1].ToString();
                    HttpContext.Current.Items["Title"] = MDept;
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/DepartmentsList.aspx", typeof(Page)) as Page;
                }


            }
            if (!string.IsNullOrEmpty(MDeptGroup))
            {
                string Val = MDeptGroup;
                string[] Deptid = Val.Split('.');

                EastLawBL.Departments objdept = new EastLawBL.Departments();
                DataTable dtDept = new DataTable();
                dtDept = objdept.GetDepartmentFileDetailsByID(int.Parse(EncryptDecryptHelper.Decrypt(Deptid[Deptid.Length - 1].ToString())));

                if (dtDept.Rows.Count > 0)
                {

                    HttpContext.Current.Items["dptdocid"] = dtDept.Rows[0]["ID"].ToString();
                    HttpContext.Current.Items["dptHTMLFile"] = dtDept.Rows[0]["HTMLFile"].ToString();
                    HttpContext.Current.Items["dptWordFile"] = dtDept.Rows[0]["WordFile"].ToString();
                    HttpContext.Current.Items["dptFileContent"] = dtDept.Rows[0]["FileContent"].ToString();

                    HttpContext.Current.Items["Title"] = dtDept.Rows[0]["Title"].ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/DepartmentDocumentView.aspx", typeof(Page)) as Page;
                }



                //}

            }
            if (!string.IsNullOrEmpty(MPracticeArea))
            {
                HttpContext.Current.Items["Title"] = MPracticeArea.ToString().Replace("-", " ");
                return BuildManager.CreateInstanceFromVirtualPath("~/m/PracticeAreaSearchGeneral.aspx", typeof(Page)) as Page;
            }
            if (!string.IsNullOrEmpty(MPASearch))
            {
                HttpContext.Current.Items["PAkeywordtxt"] = MPASearch.ToString().Replace("-", " ");
                HttpContext.Current.Items["Title"] = MPASearch.ToString().Replace("-", " ");
                return BuildManager.CreateInstanceFromVirtualPath("~/m/PracticeAreaSearchResult.aspx", typeof(Page)) as Page;
            }
            if (!string.IsNullOrEmpty(MStatutesIndex))
            {
                string Val = MStatutesIndex;
                string[] statutesindexid = Val.Split('.');

                EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                DataTable dtStatutesIndex = new DataTable();
                dtStatutesIndex = objstate.GetStatutesIndex(int.Parse(EncryptDecryptHelper.Decrypt(statutesindexid[statutesindexid.Length - 1].ToString())));
                //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                if (dtStatutesIndex.Rows.Count > 0)
                {

                    HttpContext.Current.Items["statutesindexid"] = dtStatutesIndex.Rows[0]["ID"].ToString();
                    HttpContext.Current.Items["statutesid"] = dtStatutesIndex.Rows[0]["StatutesID"].ToString();
                    HttpContext.Current.Items["statutespdffilename"] = dtStatutesIndex.Rows[0]["PDFFileName"].ToString();
                    HttpContext.Current.Items["statuteswordfilename"] = dtStatutesIndex.Rows[0]["WordFileName"].ToString();
                    HttpContext.Current.Items["Title"] = MStatutesIndex.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesIndexDetails.aspx", typeof(Page)) as Page;
                }


            }
            if (!string.IsNullOrEmpty(MStatutes))
            {

                if (MStatutes == "federal-legislation")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "provincial-legislation")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "federal-amendment-acts")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "provincial-amendment-acts")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "federal-rules-and-regulations")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "provincial-rules-and-regulations")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "bill-by-national-assembly")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "bill-by-provincial-assembly")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "latest-legislations")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "practice-area-legislations")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    HttpContext.Current.Items["StatutesFilter"] = MStatutes.ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesListAutoFilter.aspx", typeof(Page)) as Page;
                }
                if (MStatutes == "Search-Result")
                {
                    HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesList.aspx", typeof(Page)) as Page;
                }
                else
                {
                    string Val = MStatutes;
                    string[] statutesid = Val.Split('.');

                    EastLawBL.Statutes objstate = new EastLawBL.Statutes();
                    DataTable dtStatutes = new DataTable();
                    dtStatutes = objstate.GetStatutes(int.Parse(EncryptDecryptHelper.Decrypt(statutesid[statutesid.Length - 1].ToString())));
                    //dtPromoter = objPro.GetPromoterByName(Promoter.ToString().Replace("-", " "));
                    if (dtStatutes.Rows.Count > 0)
                    {

                        HttpContext.Current.Items["statutesid"] = dtStatutes.Rows[0]["ID"].ToString();
                        HttpContext.Current.Items["statutespdffilename"] = dtStatutes.Rows[0]["PDFFileName"].ToString();
                        HttpContext.Current.Items["statuteswordfilename"] = dtStatutes.Rows[0]["WordFileName"].ToString();

                        //HttpContext.Current.Items["promoter"] = Promoter.ToString().Replace("-", " ");
                        //HttpContext.Current.Items["PromoterLogo"] = dtPromoter.Rows[0]["Logo"].ToString();
                        HttpContext.Current.Items["Title"] = MStatutes.ToString().Replace("-", " ");
                        return BuildManager.CreateInstanceFromVirtualPath("~/m/StatutesIndex.aspx", typeof(Page)) as Page;
                    }
                }

            }
            if (!string.IsNullOrEmpty(MPage))
            {
                EastLawBL.Pages objpages = new EastLawBL.Pages();
                dt = objpages.GetPageByName(MPage.ToString().Replace("-", " "));
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Current.Items["Title"] = MPage.ToString().Replace("-", " ");
                    HttpContext.Current.Items["pageid"] = dt.Rows[0]["ID"].ToString();
                    return BuildManager.CreateInstanceFromVirtualPath("~/m/Pages.aspx", typeof(Page)) as Page;
                    //}
                }
            }

            #endregion 




            return null;
        }
        catch (Exception ex)
        {
            //Email.SendMail("umar.mughal83@gmail.com", ex.Message, "eastlaw error", "eastlaw.pk", "");
            return null;
        }
    }
}