<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesIndex.aspx.cs" Inherits="EastlawUI_v2.adminpanel.StatutesIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%
        System.Data.DataTable dt = new System.Data.DataTable();
        EastLawBL.Statutes objstat = new EastLawBL.Statutes();
        dt = objstat.GetStatutesIndexByStatutesID(int.Parse(Request.QueryString["dis"].ToString()));
        for(int a=0;a<dt.Rows.Count;a++)
        {
            Response.Write("<a href='StatutesIndexDetails.aspx?dis=" + Request.QueryString["dis"].ToString() + "&idis=" + dt.Rows[a]["ID"].ToString() + "'>" + dt.Rows[a]["IndexTitle"].ToString() + "</a><br><br>");
        }
         %>
    </div>
    </form>
</body>
</html>
