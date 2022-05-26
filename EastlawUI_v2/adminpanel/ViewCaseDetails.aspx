<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCaseDetails.aspx.cs" Inherits="EastlawUI_v2.adminpanel.ViewCaseDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            
    <%
        System.Data.DataTable dt = new System.Data.DataTable();
        EastLawBL.Cases objcases = new EastLawBL.Cases();
        dt = objcases.GetCases(int.Parse(EncryptDecryptHelper.Decrypt(Request.QueryString["param"].ToString())));
        for (int a = 0; a < dt.Rows.Count; a++)
        {
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Appeallant VS Respondent</b></td><td>" + dt.Rows[a]["Appeallant"].ToString() + " VS " + dt.Rows[a]["Respondent"].ToString() + "<hr></td></tr>");
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Court Name:</b></td><td>" + dt.Rows[a]["Court"].ToString() + "<hr></td></tr>");
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Keywords</b></td><td>" + dt.Rows[a]["Keywords"].ToString() + "<hr></td></tr>");
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Advocate Appeallant</b></td><td>" + dt.Rows[a]["AdvA"].ToString() + "<hr></td></tr>");
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Advocate Respondent</b></td><td>" + dt.Rows[a]["AdvR"].ToString() + "<hr></td></tr>");
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Appeallant Type</b></td><td>" + dt.Rows[a]["AppeallantType"].ToString() + "<hr></td></tr>");
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Head Notes</b></td><td>" + dt.Rows[a]["HeadNotes"].ToString() + "<hr></td></tr>");
            Response.Write("<tr><td style='width:200px;vertical-align:top'><b>Judgment</b></td><td>" + dt.Rows[a]["Judgment"].ToString() + "<hr></td></tr>");

        }
         %>
            </table>
    </div>
    </form>
</body>
</html>
