<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivateUser.aspx.cs" Inherits="EastlawUI_v2.ActivateUser" 
    MasterPageFile="~/Withoutlogin.Master"%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
     <div  class="container">
         <div class="row">
        <div class="grayContainer" style="background-color:white;padding-left:25px;width:100%;float:left">
            <div class="srchLft">
                <div class="row1" id="divSMSveri" runat="server" style="display:none">
                <h2>Account Activation</h2>
                <center>
                       We have just sent you verification code on your provided mobile number, Please enter your code for one time verification
                <table>
                    <tr>
                        <td>SMS Code:</td>
                        <td><asp:TextBox ID="txtSMSCode" runat="server" class="input1" Width="100"></asp:TextBox></td>
                        
                    </tr>
                    <tr style="text-align:right">
                        <td style="padding-right:50px"></td>
                        <td><br /><asp:Button ID="btnResend" runat="server" Text="Resend" CssClass="btn btn-danger btn_style" OnClick="btnResend_Click"  />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnVerifyCode" runat="server" Text="OK" CssClass="btn btn-danger btn_style" OnClick="btnVerifyCode_Click"  /></td>
                    </tr>
                </table>
                    </center>
                </div>
                 <p id="divConfirm" runat="server" style="display:none;font-size:20px;">
                     <br />
                     <br />
                     Congratulations! It is our pleasure to inform you that your account at www.eastlaw.pk has been successfully activated. Please <a href="http://eastlaw.pk/member/member-login"> sign-in</a> and start searching Pakistan’s Largest Online Law Library.
                     <br />
                     <br />
                     </p>
                <p id="divAlreadyVerified" runat="server" style="display:none">Account is already verified, kindly login with your email id and password <a href="/member/member-login">Login</a></p>
                <p id="divInvalid" runat="server" style="display:none">Invalid Verification, kindly conact us</p>
             
                </div>
            </div>
    
   </div>
        </div>
</asp:Content>


