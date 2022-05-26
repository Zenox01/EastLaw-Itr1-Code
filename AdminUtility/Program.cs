using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace AdminUtility
{
    class Program
    {
        static void Main(string[] args)
        {

            UsersReport objurp = new UsersReport();
            objurp.GenerateUserRegistrationReport();
            objurp.GenerateUserLoginReport();

            CasesTempMigration objtempm = new CasesTempMigration();
            objtempm.GetPendingTempCases();

            CaseUpdateHistory objcuh = new CaseUpdateHistory();
            objcuh.UpdateCasesFromUpdateHistoryLog();
            objcuh.DeleteCasesMarkedIsDelete_1();

            Common objcom = new Common();
            objcom.InsertLatestCaseFromMainToFront();

            CitationsInsideSearch objCISearch = new CitationsInsideSearch();
            objCISearch.GetSet_NoofJudgesFromCase();
            objCISearch.TaggCitationLinking();

        }
    }
}
