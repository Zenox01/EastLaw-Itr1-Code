using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;


namespace EastlawUI_v2.adminpanel
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserID"] == null)
            {
                Response.Redirect("default.aspx");
            }
            if (!Page.IsPostBack)
            {
                lblUserName.Text = Session["UserName"].ToString();
                Validate();
            }
            // Session["UserID"] = "1";
        }
        void Validate()
        {
            try
            {
                if (Session["UserTypeID"] != null)
                {
                    if(Session["UserTypeID"].ToString() == "1")
                    {
                        MainliAdminDashboard.Style["Display"] = "";

                        MainliManageCases.Style["Display"] = "";
                        liSingleCase.Style["Display"] = "";
                        liMigrateCaseUtility.Style["Display"] = "";
                        liReviewCaseUtility.Style["Display"] = "";
                        liRemoveCaseByFile.Style["Display"] = "";
                        liManageCitationVariation.Style["Display"] = "";

                        MainliManageCourt.Style["Display"] = "";
                        liCoutAppealMaster.Style["Display"] = "";
                        liAddCourtsMaster.Style["Display"] = "";
                        liManageCourtsNames.Style["Display"] = "";
                        liManageCourtsMaster.Style["Display"] = "";
                        liCourtHierarchyMaster.Style["Display"] = "";

                        liGenerateCaseExcel.Style["Display"] = "";
                        liManageJournals.Style["Display"] = "";
                        
                        liManageAdvocates.Style["Display"] = "";
                        liCaseReport.Style["Display"] = "";
                        liGeneralSearch.Style["Display"] = "";
                        


                        MainliManageJudges.Style["Display"] = "";
                        liManageJudges.Style["Display"] = "";
                        liJudgesCasesPanel.Style["Display"] = "";

                        MainliStatutesUtility.Style["Display"] = "";
                        liMigrateStatutesUtility.Style["Display"] = "";
                        liReviewStatutesUtility.Style["Display"] = "";


                        MainliWebStatutes.Style["Display"] = "";
                        liWebStatutesCategory.Style["Display"] = "";
                        liWebStatutesScrapping.Style["Display"] = "";
                        liWebStatutesManage.Style["Display"] = "";
                        liAddStatutes.Style["Display"] = "";
                        liStatuteSOA.Style["Display"] = "";


                        MainliMasters.Style["Display"] = "";
                        liManageKeywords.Style["Display"] = "";
                        liManageDictionary.Style["Display"] = "";
                        liManageLegalMaxim.Style["Display"] = "";
                        liManagePracticeAreaCat.Style["Display"] = "";
                        liManagePracticeAreaSubCat.Style["Display"] = "";


                        MainliDept.Style["Display"] = "";
                        liCreateDept.Style["Display"] = "";
                        liManageDept.Style["Display"] = "";

                        MainliGlossory.Style["Display"] = "";
                        liCreateGlossory.Style["Display"] = "";
                        liManageGlossory.Style["Display"] = "";

                        MainliUsers.Style["Display"] = "";
                        liAddUser.Style["Display"] = "";
                        liManageUsers.Style["Display"] = "";
                        liUpdateUserStatus.Style["Display"] = "";
                        liGenerateUserOrder.Style["Display"] = "";
                        liResendNotification.Style["Display"] = "";
                        liAddCompany.Style["Display"] = "";
                        liManageCompany.Style["Display"] = "";
                        liManagePlans.Style["Display"] = "";
                        liUserReports.Style["Display"] = "";
                        liUserCRM.Style["Display"] = "";
                        liUserExpiryLit.Style["Display"] = "";

                        MainliGeneralAreas.Style["Display"] = "";
                        liAddGeneralAreas.Style["Display"] = "";
                        liManageGeneralAreas.Style["Display"] = "";


                        MainliCMS.Style["Display"] = "";
                        liManageCMS.Style["Display"] = "";

                        MainliNews.Style["Display"] = "";
                        liAddNews.Style["Display"] = "";
                        liManageNews.Style["Display"] = "";

                        MainliLogs.Style["Display"] = "";
                        liFrontEndActivity.Style["Display"] = "";
                        liBackEndActivity.Style["Display"] = "";

                        MainliEBook.Style["Display"] = "";
                        liEBookManageCat.Style["Display"] = "";
                        liEBookAdd.Style["Display"] = "";
                        liEBookManage.Style["Display"] = "";

                        MainLiPrintJournal.Style["Display"] = "";
                        liPrintJournalAdd.Style["Display"] = "";
                        liPrintJournalManage.Style["Display"] = "";
                        

                        MainliNewsletter.Style["Display"] = "";
                        liNewsletterAdd.Style["Display"] = "";
                        liNewsletterList.Style["Display"] = "";
                        
                        
                        

                    }
                    else if (Session["UserTypeID"].ToString() == "6")
                    {
                        MainliAdminDashboard.Style["Display"] = "";

                        MainliManageCases.Style["Display"] = "";
                        liSingleCase.Style["Display"] = "";
                        liMigrateCaseUtility.Style["Display"] = "";
                        liReviewCaseUtility.Style["Display"] = "";
                        liRemoveCaseByFile.Style["Display"] = "";
                        liManageCitationVariation.Style["Display"] = "";
                        //liManageCourtsNames.Style["Display"] = "";
                        //liManageCourtsMaster.Style["Display"] = "";
                        liGenerateCaseExcel.Style["Display"] = "";
                        liManageJournals.Style["Display"] = "";
                        
                        liManageAdvocates.Style["Display"] = "";
                        liCaseReport.Style["Display"] = "";
                        liGeneralSearch.Style["Display"] = "";
                        


                        MainliManageCourt.Style["Display"] = "";
                        liCoutAppealMaster.Style["Display"] = "";
                        liAddCourtsMaster.Style["Display"] = "";
                        liManageCourtsNames.Style["Display"] = "";
                        liManageCourtsMaster.Style["Display"] = "";
                        liCourtHierarchyMaster.Style["Display"] = "";

                        MainliManageJudges.Style["Display"] = "";
                        liManageJudges.Style["Display"] = "";
                        liJudgesCasesPanel.Style["Display"] = "";
                    }

                    else if (Session["UserTypeID"].ToString() == "7")
                    {
                        MainliStatutesUtility.Style["Display"] = "";
                        liMigrateStatutesUtility.Style["Display"] = "";
                        liReviewStatutesUtility.Style["Display"] = "";


                        MainliWebStatutes.Style["Display"] = "";
                        liWebStatutesCategory.Style["Display"] = "";
                        liWebStatutesScrapping.Style["Display"] = "";
                        liWebStatutesManage.Style["Display"] = "";
                        liAddStatutes.Style["Display"] = "";
                        liStatuteSOA.Style["Display"] = "";


                        MainliNews.Style["Display"] = "";
                        liAddNews.Style["Display"] = "";
                        liManageNews.Style["Display"] = "";


                        MainliUsers.Style["Display"] = "";
                        liUserCRM.Style["Display"] = "";
                        liUserExpiryLit.Style["Display"] = "";

                        MainliNewsletter.Style["Display"] = "";
                        liNewsletterAdd.Style["Display"] = "";
                        liNewsletterList.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "8")
                    {
                        MainliMasters.Style["Display"] = "";
                        liManageKeywords.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "9")
                    {
                        MainliMasters.Style["Display"] = "";
                        liManageDictionary.Style["Display"] = "";
                        liManageLegalMaxim.Style["Display"] = "";
                        liManagePracticeAreaCat.Style["Display"] = "";
                        liManagePracticeAreaSubCat.Style["Display"] = "";

                        MainliUsers.Style["Display"] = "";
                       liGenerateUserOrder.Style["Display"] = "";
                       
                    }
                    else if (Session["UserTypeID"].ToString() == "10")
                    {
                        MainliGlossory.Style["Display"] = "";
                        liCreateGlossory.Style["Display"] = "";
                        liManageGlossory.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "11")
                    {
                        MainUserDashboard.Style["Display"] = "";
                        MainliUsers.Style["Display"] = "";
                        liAddUser.Style["Display"] = "";
                        liManageUsers.Style["Display"] = "";
                        liUpdateUserStatus.Style["Display"] = "";
                        liGenerateUserOrder.Style["Display"] = "";
                        liResendNotification.Style["Display"] = "";
                        liAddCompany.Style["Display"] = "";
                        liManageCompany.Style["Display"] = "";
                        liManagePlans.Style["Display"] = "";
                        liUserReports.Style["Display"] = "";
                        liUserCRM.Style["Display"] = "";
                        liUserExpiryLit.Style["Display"] = "";

                        MainliLogs.Style["Display"] = "";
                        liFrontEndActivity.Style["Display"] = "";
                        liBackEndActivity.Style["Display"] = "";

                        MainliNews.Style["Display"] = "";
                        liAddNews.Style["Display"] = "";
                        liManageNews.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "12")
                    {
                        MainliCMS.Style["Display"] = "";
                        liManageCMS.Style["Display"] = "";

                      
                    }
                    else if (Session["UserTypeID"].ToString() == "13")
                    {
                        MainliLogs.Style["Display"] = "";
                        liFrontEndActivity.Style["Display"] = "";
                        liBackEndActivity.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "14")
                    {
                        MainliNews.Style["Display"] = "";
                        liAddNews.Style["Display"] = "";
                        liManageNews.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "15")
                    {
                        MainliDept.Style["Display"] = "";
                        liCreateDept.Style["Display"] = "";
                        liManageDept.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "16")
                    {
                        MainliManageCases.Style["Display"] = "";
                        liSingleCase.Style["Display"] = "";
                        liMigrateCaseUtility.Style["Display"] = "";
                        liReviewCaseUtility.Style["Display"] = "";
                        liJudgesCasesPanel.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "18")
                    {
                        MainliNews.Style["Display"] = "";
                        liAddNews.Style["Display"] = "";
                        liManageNews.Style["Display"] = "";

                        MainUserDashboard.Style["Display"] = "";
                        MainliUsers.Style["Display"] = "";
                        liUserCRM.Style["Display"] = "";
                        liGenerateUserOrder.Style["Display"] = "";
                        liUserExpiryLit.Style["Display"] = "";
                    }
                    else if (Session["UserTypeID"].ToString() == "19")
                    {
                        MainliAdminDashboard.Style["Display"] = "";

                        MainliManageCases.Style["Display"] = "";
                        liSingleCase.Style["Display"] = "";
                        liReviewCaseUtility.Style["Display"] = "";
                        
                    }
                    else if (Session["UserTypeID"].ToString() == "20")
                    {
                      
                        MainliEBook.Style["Display"] = "";
                        liEBookManageCat.Style["Display"] = "";
                        liEBookAdd.Style["Display"] = "";
                        liEBookManage.Style["Display"] = "";


                    }

                    if (Session["UserID"].ToString() == "2878")
                    {
                        MainliLogs.Style["Display"] = "";
                        liFrontEndActivity.Style["Display"] = "";
                    }
                    
                }
            }
            catch { }
        }
    }
}