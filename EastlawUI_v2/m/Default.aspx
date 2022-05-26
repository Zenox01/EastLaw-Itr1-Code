<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EastlawUI_v2.m.Default"
    MasterPageFile="~/m/General.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="Middle">
    <div class="sign">
                    <div class="container">
                        <div class="margin">

                            <div class="logo-main">
                                <img src="images/logo_main_m.png" alt="logo" />
                            </div>
                            <div class="signinRow">
                                <div class="btn">
                                    <asp:Button ID="btnSingIn" runat="server" CssClass="whiteBtn3" Text="Sign In" OnClick="btnSingIn_Click" />
                                </div>
                                <span style="color: black">or</span>
                                <br />
                                 <div class="btn">
                                    <asp:Button ID="btnNewReg" runat="server" CssClass="whiteBtn3" Text="New User" OnClick="btnNewReg_Click" />
                                </div>
                                <%--<span><a href="/m/Member/Member-Register">New Registration</a></span>--%>
                            </div>
                        </div>
                    </div>
                </div>
</asp:Content>


