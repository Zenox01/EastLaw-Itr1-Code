<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jazzcashpayment.aspx.cs" Inherits="EastlawUI_v2.jazzcashpayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
      <form id="onlineform" runat="server">
        <div>
            Trx Type:<asp:TextBox ID="txtTrnType" runat="server"></asp:TextBox>
            <hr />
            Amount:<asp:TextBox ID="txtAmt" runat="server"></asp:TextBox>
            <hr />
             Return URL:<asp:TextBox ID="txtRetunURL" runat="server"></asp:TextBox>
          
            <hr />
            <asp:Button Text="Checkout" OnClick="Unnamed_Click" runat="server" />
            <hr />
            <asp:Literal ID="ltResponse" runat="server" />
            <hr />
            <br />
            <label>Reseponse: </label>
            <asp:Label Text="" ID="lblResponse" runat="server" />
            <br />
            <label>Reseponse Code: </label>
            <asp:Label Text="" ID="lblCode" runat="server" />
            <br />
            <label>RetreivalReferenceNo: </label>
            <asp:Label Text="" ID="lblRrn" runat="server" />
        </div>
    </form>
</body>
</html>
