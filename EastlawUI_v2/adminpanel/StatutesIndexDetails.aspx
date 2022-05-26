<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatutesIndexDetails.aspx.cs" Inherits="EastlawUI_v2.adminpanel.StatutesIndexDetails" %>

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
        dt = objstat.GetStatutesIndex(int.Parse(Request.QueryString["idis"].ToString()));
        for(int a=0;a<dt.Rows.Count;a++)
        {
            Response.Write("<a href='StatutesIndex.aspx?dis="+Request.QueryString["dis"].ToString()+"'>Go to Index Page</a>");
            
            Response.Write(FormatContent(dt.Rows[a]["IndexContent"].ToString()));
            
        }
         %>
    </div>
    </form>
</body>
</html>
