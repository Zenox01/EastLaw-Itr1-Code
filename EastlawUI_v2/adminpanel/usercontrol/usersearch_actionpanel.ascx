<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="usersearch_actionpanel.ascx.cs" Inherits="EastLawUI.adminpanel.usercontrol.usersearch_actionpanel" %>
    <table style="width:100%">
                    <tr>
                        <td>Email ID: </td>
                        <td> <asp:TextBox ID="txtSearchEmailID" runat="server" Width="220" class="form-control"> </asp:TextBox></td>
                         <td>Mobile Number: </td>
                        <td> <asp:TextBox ID="txSearchtMobileNo" runat="server" Width="220" class="form-control"> </asp:TextBox></td>
                        <td><asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-block btn-primary"  Width="100" OnClick="btnSearch_Click" CausesValidation="false" />
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>

                        </td>
                    </tr>
                    <tr >
                        <td colspan="5">   
                            </td>
                    </tr>
                  
                </table>
<div id="trActions" runat="server" style="display:none;padding-bottom:30px;padding-top:10px">
    <br />
    User Name: <asp:Label ID="lblUserName" runat="server"></asp:Label>
    <br />
    Email ID: <asp:Label ID="lblEmailID" runat="server"></asp:Label>
    <br />
    Mobile No: <asp:Label ID="lblMobNo" runat="server"></asp:Label><br /><br />
     <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" CssClass="btn btn-primary" Width="100" OnClick="btnUpdateStatus_Click" CausesValidation="false"/> 
                             &nbsp; 
                             <asp:Button ID="btnUpdatePlan" runat="server" Text="Update Plan" CssClass="btn btn-primary" Width="100" OnClick="btnUpdatePlan_Click" CausesValidation="false"/>
                             &nbsp;
                             <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="btn btn-primary" OnClick="btnResetPassword_Click" Width="200" CausesValidation="false"/>
                             &nbsp;
                             <asp:Button ID="btnHistory" runat="server" Text="History" CssClass="btn btn-primary" Width="125" OnClick="btnHistory_Click" CausesValidation="false"/>
</div>