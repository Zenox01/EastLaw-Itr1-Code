using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastlawUI_v2.adminpanel
{
    public static class ValidateUserGroup
    {
        public static bool ValidateGroup(int UserGroup,string CurrentPageName)
        {
            try
            {


                if (UserGroup == 1)
                {
                    string[] arrAdmin = new string[] { "adminmain.aspx", "ReviewCasesMigration.aspx", "UtilityCases.aspx", "CasesMigrationList.aspx", "DeleteCasesByFiles.aspx",
                "ManageCitationVariations.aspx","ViewCaseDetails.aspx","ManageCourts.aspx","ManageCourtsMaster.aspx","ManageCourtHierarchy.aspx","managecourtappealmastr.aspx","AddEditCourts.aspx","GenerateCaseExcel.aspx","ManageJournals.aspx","ManageJudges.aspx",
                "ManageAdvocates.aspx","CasesReport.aspx","GeneralSearch.aspx","StatutesUtility.aspx","StatutesCategories.aspx","WebScrapStatutes.aspx","AddStatute.aspx",
                "ManageWebScapStatutes.aspx","ManageKeywords.aspx","ManageDictionary.aspx","ManagePracticeCategories.aspx","ManagePracticeSubCategories.aspx","StatutesIndexDetails.aspx",
                "AddDepartment.aspx","ManageDepartments.aspx","ManageDepartmentFile.aspx","AddGlossary.aspx","ManageGlossaryList.aspx","AddUsers.aspx","ManageUsers.aspx","UpdateUserStatus.aspx",
                "GenerateUserOrder.aspx","ResendNotification.aspx","AddCompany.aspx","ManageCompanies.aspx","ManagePlans.aspx","UsersReports.aspx","CRMUserView.aspx",
                "AddGeneralAreas.aspx","ManageGeneralAreas.aspx","ManageCMSPages.aspx","AddNews.aspx","ManageNews.aspx","FrontEndActivityLogs.aspx",
                "BackendActivityLog.aspx","ChangePassword.aspx","UpdatePlanBackend.aspx","UpdateUserStatus.aspx","ResetUserPassword.aspx","StatutesIndex.aspx","ManageStatutesIndex.aspx","ManageEbookCategories.aspx","AddBook.aspx","ManageEBookIndexes.aspx","ManageEBooks.aspx"
                    ,"ManageLegalMaxim.aspx","AddNewsletter.aspx","NewsletterList.aspx","CitationDetailedReview.aspx","AddCaseUpdateHistory.aspx","StatuteSectionPanel.aspx","CreatePrintableJournal.aspx","ManagePrintableJournals.aspx","JudgeMergePanel.aspx","CRMUserExpiry.aspx"};


                    for (int i = 0; i < arrAdmin.Length; i++)
                    {
                        if (string.Equals(arrAdmin[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 6)
                {
                    string[] arrCaseLawGroup = new string[] { "AdminMain.aspx", "ReviewCasesMigration.aspx", "UtilityCases.aspx", "CasesMigrationList.aspx"
                        ,"AddCaseUpdateHistory.aspx","ViewCaseDetails.aspx", "DeleteCasesByFiles.aspx",
                "ManageCitationVariations.aspx","ManageCourts.aspx","ManageDepartmentFile.aspx","ManageCourtsMaster.aspx","ManageCourtHierarchy.aspx","managecourtappealmastr.aspx","AddEditCourts.aspx","GenerateCaseExcel.aspx","ManageJournals.aspx","ManageJudges.aspx",
                "ManageAdvocates.aspx","CasesReport.aspx","GeneralSearch.aspx","ChangePassword.aspx","CitationDetailedReview.aspx","JudgeMergePanel.aspx"};


                    for (int i = 0; i < arrCaseLawGroup.Length; i++)
                    {
                        if (string.Equals(arrCaseLawGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 7)
                {
                    string[] arrStatuteGroup = new string[] { "AdminGeneral.aspx","StatutesUtility.aspx","StatutesCategories.aspx","WebScrapStatutes.aspx","AddStatute.aspx",
                "ManageWebScapStatutes.aspx","StatutesIndex.aspx","ManageStatutesIndex.aspx","StatutesIndexDetails.aspx","ChangePassword.aspx","CRMUserView.aspx",
                "AddNews.aspx","ManageNews.aspx","ChangePassword.aspx","UpdateUserStatus.aspx","AddNewsletter.aspx","NewsletterList.aspx","CRMUserExpiry.aspx","StatuteSectionPanel.aspx"};


                


                    for (int i = 0; i < arrStatuteGroup.Length; i++)
                    {
                        if (string.Equals(arrStatuteGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else  if (UserGroup == 8)
                {
                    string[] arrMastersKeywordsGroup = new string[] { "AdminGeneral.aspx", "ManageKeywords.aspx",
                "ChangePassword.aspx",};


                    for (int i = 0; i < arrMastersKeywordsGroup.Length; i++)
                    {
                        if (string.Equals(arrMastersKeywordsGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }

                else if (UserGroup == 9)
                {
                    string[] arrMastersGroup = new string[] { "AdminGeneral.aspx","ManageDictionary.aspx","ManagePracticeCategories.aspx","ManagePracticeSubCategories.aspx",
                    "ChangePassword.aspx","ManageLegalMaxim.aspx","FrontEndActivityLogs.aspx","GenerateUserOrder.aspx"};


                    for (int i = 0; i < arrMastersGroup.Length; i++)
                    {
                        if (string.Equals(arrMastersGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else  if (UserGroup == 10)
                {
                    string[] arrGlossaryGroup = new string[] { "AdminGeneral.aspx", "AddGlossary.aspx", "ManageGlossaryList.aspx", "ChangePassword.aspx" };


                    for (int i = 0; i < arrGlossaryGroup.Length; i++)
                    {
                        if (string.Equals(arrGlossaryGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 11)
                {
                    string[] arrUserManagmentGroup = new string[] { "AdminUsers.aspx","AddUsers.aspx","ManageUsers.aspx","UpdateUserStatus.aspx",
                "GenerateUserOrder.aspx","ResendNotification.aspx","AddCompany.aspx","ManageCompanies.aspx","ManagePlans.aspx","UsersReports.aspx","CRMUserView.aspx",
                "ManageCMSPages.aspx","AddNews.aspx","ManageNews.aspx","FrontEndActivityLogs.aspx",
                "BackendActivityLog.aspx","ChangePassword.aspx","UpdatePlanBackend.aspx","UpdateUserStatus.aspx","ResetUserPassword.aspx","ManageUserPlanUpdate.aspx","CRMUserExpiry.aspx"};


                    for (int i = 0; i < arrUserManagmentGroup.Length; i++)
                    {
                        if (string.Equals(arrUserManagmentGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 12)
                {
                    string[] arrCMS = new string[] { "AdminGeneral.aspx", "ManageCMSPages.aspx", "ChangePassword.aspx" };


                    for (int i = 0; i < arrCMS.Length; i++)
                    {
                        if (string.Equals(arrCMS[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else  if (UserGroup == 13)
                {
                    string[] arrLogsGroup = new string[] { "AdminGeneral.aspx","FrontEndActivityLogs.aspx",
                "BackendActivityLog.aspx","ChangePassword.aspx"};


                    for (int i = 0; i < arrLogsGroup.Length; i++)
                    {
                        if (string.Equals(arrLogsGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 14)
                {
                    string[] arrNewsEditor = new string[] { "AdminGeneral.aspx", "AddNews.aspx", "ManageNews.aspx", "ChangePassword.aspx" };


                    for (int i = 0; i < arrNewsEditor.Length; i++)
                    {
                        if (string.Equals(arrNewsEditor[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 15)
                {
                    string[] arrDepartmentContentEditor = new string[] { "AdminGeneral.aspx", "AddDepartment.aspx", "ManageDepartments.aspx","ManageDepartmentFile.aspx", "ChangePassword.aspx" };


                    for (int i = 0; i < arrDepartmentContentEditor.Length; i++)
                    {
                        if (string.Equals(arrDepartmentContentEditor[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 16)
                {
                    string[] arrCasesUploadOnly = new string[] { "AdminGeneral.aspx", "ReviewCasesMigration.aspx", "UtilityCases.aspx", "CasesMigrationList.aspx","AddCaseUpdateHistory.aspx","ViewCaseDetails.aspx","CitationDetailedReview.aspx","CitationDetailedReview.aspx","AddCaseUpdateHistory.aspx" ,"JudgeMergePanel.aspx"};


                    for (int i = 0; i < arrCasesUploadOnly.Length; i++)
                    {
                        if (string.Equals(arrCasesUploadOnly[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 18)
                {
                    string[] arrAdmin = new string[] { "AdminUsers.aspx","AdminGeneral.aspx", "CRMUserView.aspx",
                "AddNews.aspx","ManageNews.aspx","ChangePassword.aspx","UpdateUserStatus.aspx","ResetUserPassword.aspx","GenerateUserOrder.aspx","CRMUserExpiry.aspx"};


                    for (int i = 0; i < arrAdmin.Length; i++)
                    {
                        if (string.Equals(arrAdmin[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 19)
                {
                    string[] arrCaseLawGroup = new string[] { "AdminMain.aspx", "ReviewCasesMigration.aspx", "CasesMigrationList.aspx"
                        ,"AddCaseUpdateHistory.aspx","ViewCaseDetails.aspx","ChangePassword.aspx","CitationDetailedReview.aspx"};


                    for (int i = 0; i < arrCaseLawGroup.Length; i++)
                    {
                        if (string.Equals(arrCaseLawGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }
                else if (UserGroup == 20)
                {
                    string[] arrCaseLawGroup = new string[] { "AdminGeneral.aspx", "ManageEbookCategories.aspx", "AddBook.aspx", "ManageEBookIndexes.aspx", "ManageEBooks.aspx" };


                    for (int i = 0; i < arrCaseLawGroup.Length; i++)
                    {
                        if (string.Equals(arrCaseLawGroup[i].ToString(), CurrentPageName, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        //if (arrAdmin[i].ToString() == CurrentPageName)
                    }
                    return false;
                }


                return false;
            }
            catch {
                return false;
            }
        }
        public static string getPageName(string txt)
        {
            string sPath =txt;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }
    }
}