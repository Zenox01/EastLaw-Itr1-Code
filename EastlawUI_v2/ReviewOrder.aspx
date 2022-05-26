<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewOrder.aspx.cs" Inherits="EastlawUI_v2.ReviewOrder" 
    MasterPageFile="~/Withoutlogin.Master" Async="true"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cntPlc">
       <div class="container">
<div class="row breadcrum">

<ul class="bc">
    <li><a href="/member/member-dashboard" class="first">Home</a></li>
 
    <li><a href="#" class="current">Order Review</a></li>
   
</ul>
  </div>
</div>
      <div class="container">
    <div class="row">
    
    
    
   		<div class="col-lg-12 col-md-12">
        
        	<div class="row">
                 <h5><asp:Label id="lblUserState" runat="server" ForeColor="Blue"></asp:Label></h5>

 <div class="row1" id="divSMSveri" runat="server">
                <h2>Order Review </h2>
                <center>
                     <div id="divOrderExist" runat="server" style="display:none" >
                     <h3>Please check your email</h3>
                            
                            We have sent you an email at  <asp:Label ID="lblEmailIDForExist" runat="server"></asp:Label>. regarding your order status.

                               <br /><br />
                            For any queries, please feel free to send us an email at <a href="mailto:info@eastlaw.pk" target="_top">info@eastlaw.pk</a>
                     </div>   
                    <table width="100%" id="tbFields" runat="server">
                        <tr>
                            <td style="width:200px">
                              <b>Plan Name:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblPlanName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:200px">
                              <b>Plan Days:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblPlanDays" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                <b>Plan Amount:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblPlanAmt" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td style="width:200px">
                                <b>Payment Method:</b>
                            </td>
                            <td>
                                <%--<asp:RadioButton ID="radioEasyPaisa" runat="server" GroupName="AA" /><img src="/images/EPLogo.png" width="100" height="100"  /><br />
                                <asp:RadioButton ID="radioBankTransfer" runat="server" GroupName="AA" /><img src="/images/bank.png" width="100" height="100" />--%>
                                <asp:RadioButton ID="radioBankTransfer" runat="server" GroupName="AA" Text="Online Cash/Cheque Deposit into Bank Account (United Bank Limited - UBL or Bank Alfalah)." /><br />
                                <asp:RadioButton ID="radioCourier" runat="server" GroupName="AA" Text="DD/PO/Cross Cheque by Post/Courier Service." /><br />
                                <asp:RadioButton ID="radioEasyPaisa" runat="server" GroupName="AA" Text="Telenor EasyPaisa Payment Solutions." Visible="false" /><br />
                                <asp:RadioButton ID="radioJazzCashCard" runat="server" GroupName="AA" Text="JazzCash (Master Card)." Visible="true" /><br />
                                <asp:RadioButton ID="radioJazzCashDebitCard" runat="server" GroupName="AA" Text="JazzCash (Debit Card)." Visible="true" /><br />
                                <asp:RadioButton ID="radioJazzCashShop" runat="server" GroupName="AA" Text="JazzCash (Shop)" Visible="true" /><br />
                                <asp:RadioButton ID="radioJazzCashMobileAccount" runat="server" GroupName="AA" Text="JazzCash (Mobile Account)" Visible="true" /><br />
                                
                            </td>
                        </tr>
                        
                        <tr style="text-align:center">
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnProcess" CssClass="btn btn-danger btn_style" Text="Process Order" OnClick="btnProcess_Click" />
                            </td>
                            
                        </tr>
                       
                       
                    </table>
                   
                  <br />
                    <br />
                    <br />
                    <br />
                    </center>
                </div>
            	
            
            </div>
        
        </div>
        
        
        
        
    	
    </div>  
    </div>
    <div class="clearfix"></div>
    </asp:Content>