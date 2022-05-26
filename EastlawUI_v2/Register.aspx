<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EastlawUI_v2.Register"
    MasterPageFile="~/Withoutlogin.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cntPlc">
    <asp:UpdatePanel ID="upPnlTop" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">

                    <!------------ Left Side ------------->


                    <!------------ Left Side End ------------->

                    <!------------ Right Side ------------->

                    <div class="col-lg-9 col-md-9 center bg_cover">

                        <div class="col-lg-6 col-md-6 center">

                            <div class="row link text-center">

                                <h4>“Member Login or <a href="/member/member-register">Click Here</a> for New Registration”</h4>

                            </div>

                            <div class="row">

                                <div class="login">

                                    <div class="style_3">
                                        <i class="fa fa-user icon_color"></i><span class="span_style">Login</span>
                                    </div>

                                    <div>

                                        <%--<input type="text" class="form-control" placeholder="Email Address" />--%>
                                        <asp:TextBox ID="txtEmailIDLogin" runat="server" class="form-control" placeholder="Email Address" ValidationGroup="BB"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="BB" runat="server"
                                            ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmailIDLogin" Display="Dynamic"></asp:RequiredFieldValidator>



                                        <asp:TextBox ID="txtPasswordLogin" runat="server" class="form-control" placeholder="Password"
                                            TextMode="Password" AutoPostBack="False" ValidationGroup="BB" OnTextChanged="txtPasswordLogin_TextChanged"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="BB" runat="server" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtPasswordLogin"></asp:RequiredFieldValidator>



                                    </div>

                                    <div class="row">

                                        <div class="pull-left">
                                            <asp:CheckBox ID="chkRem" runat="server" Text="Remember Me" />

                                        </div>

                                        <div class="pull-right">
                                            <a href="/member/forget-password" class="forgot_pass">Forgot Password ?</a>
                                        </div>


                                    </div>

                                    <div>
                                        <span style="font-size: 10pt; line-height: 14pt; text-align: left">
                                            <br />
                                            <%--<input name="" type="checkbox" value="">--%> &nbsp;
                            <asp:CheckBox ID="chkTNC" runat="server" Checked="true" />

                                            Use of this service is subject to <a href="/en/Terms-of-Use" target="_blank">Terms & Conditions</a> and <a href="/en/Privacy-Policy" target="_blank">Privacy Policy</a>. Please review this information before proceeding. You must accept the terms and conditions to use the service
                                        </span>
                                        <asp:Button ID="btnLogin" runat="server" Text="Login" Width="150" OnClick="btnLogin_Click" class="btn btn-danger btn_style" ValidationGroup="BB" /><br />
                                        <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                        <%--<input type="button" class="btn btn-danger btn_style" value="Login" />--%>
                                    </div>

                                    <div class="line" style="display: none">

                                        <div class="login_with">
                                            <span>or login using</span>
                                        </div>

                                    </div>

                                    <div class="row" style="display: none">

                                        <div class="text-center">

                                            <a href="http://facebook.com" class="btn btn-social-icon btn-facebook">
                                                <i class="fa fa-facebook"></i></a>

                                            <a class="btn btn-social-icon btn-google-plus"><i class="fa fa-google-plus"></i></a>

                                            <a class="btn btn-social-icon btn-linkedin"><i class="fa fa-linkedin"></i></a>

                                            <a class="btn btn-social-icon btn-twitter"><i class="fa fa-twitter"></i></a>

                                        </div>


                                    </div>


                                    <div class="row subscription">

                                        <ul>
                                            <li class="padding-left"><a href="/subscription">Subscription</a></li>
                                            <li><a href="#">Renew Subcription</a></li>
                                            <li><a href="/member/member-register" class="a_style2">Free Trial</a></li>
                                        </ul>

                                    </div>


                                </div>






                            </div>

                        </div>

                    </div>

                    <!------------ Right Side End ------------->


                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlTop">
                <ProgressTemplate>

                    <div class="modal1">
                        <div class="center1">
                            <img alt="" src="/style/img/ajax_loader_big.gif" />
                        </div>
                    </div>



                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


