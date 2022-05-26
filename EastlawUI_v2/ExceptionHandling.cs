using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EastlawUI_v2
{
    public class ExceptionHandling
    {
        public static void  SendErrorReport(string PageName,string BlockName ,string msg)
        {
            Email.SendMail(ConfigurationSettings.AppSettings["smtp"].ToString(),"<br>"+PageName+"<br>"+BlockName+"<br>"+msg,"Error From East Law","Innovative","");
            
        }
    }
}